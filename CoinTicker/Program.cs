using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.IO.Ports;
using MarqueeDisplay;

namespace CoinTicker
{
    class Program
    {
        /*
         *  CoinTicker by Wendelstein7
         *  I am terribly sorry for this terrible code and terrible application. But it works, it was made in a day and I had fun :)
         */

        // Remote REST APIs
        private const string PoolUrl = @"https://api.ethermine.org/miner/ <WALLET> /currentStats";
        private const string ExchangeUrl = @"https://api.blockchain.com/v3/exchange/tickers/ETH-EUR";

        // Update Intervals
        private static TimeSpan RemoteUpdateInterval = new TimeSpan(0, 0, 10, 0, 0);
        private static TimeSpan DisplayUpdateInterval = new TimeSpan(0, 0, 0, 0, 50);

        // Hardware
        private static readonly int BaudRate = 38400;
        private static readonly string ComPort = "COM3";

        // Application variables
        private static DateTime dataTime = DateTime.MinValue;
        private static DateTime lastRemoteUpdate = DateTime.MinValue;
        private static DateTime lastDisplayUpdate = DateTime.MinValue;
        private static long balanceTarget;
        private static long balanceCurrent;
        private static double rate;

        private static decimal displayValue;

        private static HttpClient Client;
        private static SerialPort Port;

        static void Main(string[] args)
        {
            log("Main", "Hello World! Starting application...");

            Client = PrepareHttpClient();
            Port = PrepareSerialPort();

            log("Main", "Press any key to stop the application.");
            while (!Console.KeyAvailable)
            {
                if (lastRemoteUpdate + RemoteUpdateInterval < DateTime.Now)
                {
                    _ = UpdateRemote();
                    lastRemoteUpdate = DateTime.Now;
                }

                if (lastDisplayUpdate + DisplayUpdateInterval < DateTime.Now)
                {
                    UpdateDisplay();
                    lastDisplayUpdate = DateTime.Now;
                }

                Thread.Sleep(10);
            }

            if (Port != null && Port.IsOpen)
            {
                Port.Close();

                log("Main", String.Format("Port connection to '{0}' closed.", Port.PortName));
            }

            log("Main", "Application exit.");
        }

        private static async Task UpdateRemote()
        {
            log("RemoteUpdate", "Fetching remote data from APIs...");

            string poolResponseString = await Client.GetStringAsync(PoolUrl);

            string exchangeResponseString = await Client.GetStringAsync(ExchangeUrl);


            Pool.Root pool = JsonConvert.DeserializeObject<Pool.Root>(poolResponseString);

            Exchange.Root exchange = JsonConvert.DeserializeObject<Exchange.Root>(exchangeResponseString);


            if (pool != null && pool.status == "OK")
            {
                balanceCurrent = (balanceCurrent == 0) ? pool.data.unpaid : balanceTarget;
                balanceTarget = pool.data.unpaid;
                dataTime = DateTime.Now;

                log("RemoteUpdate", String.Format("Fetched remote data from APIs: Balance: {0}", balanceTarget));
            }
            else
                log("RemoteUpdate", "Fetching remote data from APIs failed for pool!");

            if (exchange != null && exchange.symbol == "ETH-EUR")
            {
                rate = exchange.last_trade_price;

                log("RemoteUpdate", String.Format("Fetched remote data from APIs: Exchange rate: {0}", rate));
            }
            else
                log("RemoteUpdate", "Fetching remote data from APIs failed for exchange!");
        }

        private static void UpdateDisplay()
        {
            if (Port == null || !Port.IsOpen)
            {
                log("UpdateDisplay", "Could not update display, no serial connection present!");
                Thread.Sleep( 1000 );
                Port = PrepareSerialPort();
                //Environment.Exit(1);
                //return;
            }

            long difference = balanceTarget - balanceCurrent;

            double factor = DateTime.Now.Subtract(dataTime).Divide(RemoteUpdateInterval);

            long inbetween = balanceCurrent + (long) (difference * factor);

            displayValue = (decimal) ((inbetween * rate) / Math.Pow(10, 18));

            MarqueeText marqueeText = new MarqueeText()
            {
                Alignment = TextPosition.PA_CENTER,
                Brightness = 3,
                EffectIn = TextEffect.PA_NO_EFFECT,
                EffectOut = TextEffect.PA_NO_EFFECT,
                Inverted = false,
                Loop = true,
                Pause = 1000,
                Speed = 0,
                Text = "$ " + displayValue.ToString("00.000000"),
            };

            string json = JsonConvert.SerializeObject(new MarqueeText_o(marqueeText), Formatting.None);

            Port.WriteLine(json);

            //log( "UpdateDisplay", String.Format( "current: {0}, inbetween: {1}, target: {2}", balanceCurrent, inbetween, balanceTarget ) );
        }

        private static HttpClient PrepareHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "CoinTicker");

            return client;
        }

        private static SerialPort PrepareSerialPort()
        {
            log("PrepareSerialPort", String.Format("Connecting to '{0}' at rate {1}.", ComPort, BaudRate));

            SerialPort port = new SerialPort(ComPort, BaudRate);
            port.DtrEnable = true;

            try
            {
                port.Open();
                log("PrepareSerialPort", "Waiting for Arduino to boot...");
                Thread.Sleep(1000); // wait for Arduino to boot

                if (port.IsOpen)
                    log("PrepareSerialPort", String.Format("Port connection to '{0}' opened, success.", port.PortName));
            }
            catch
            {
                port = null;

                log("PrepareSerialPort", String.Format("Port connection to '{0}' failed.", ComPort));
            }

            return port;
        }

        private static void log(string source, string message)
        {
            DateTime now = DateTime.Now;

            //Console.WriteLine("{0} {1} [{2}] {3}", now.ToShortDateString(), now.ToShortTimeString(), source, message);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(now.ToShortDateString() + " " + now.ToShortTimeString() + " ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(source + " ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }
    }
}

namespace Exchange
{
    public class Root
    {
        public string symbol { get; set; }
        public double price_24h { get; set; }
        public double volume_24h { get; set; }
        public double last_trade_price { get; set; }
    }
}

namespace Pool
{
    public class Data
    {
        public int time { get; set; }
        public int lastSeen { get; set; }
        public int reportedHashrate { get; set; }
        public double currentHashrate { get; set; }
        public int validShares { get; set; }
        public int invalidShares { get; set; }
        public int staleShares { get; set; }
        public double averageHashrate { get; set; }
        public int activeWorkers { get; set; }
        public long unpaid { get; set; }
        public object unconfirmed { get; set; }
        public double coinsPerMin { get; set; }
        public double usdPerMin { get; set; }
        public double btcPerMin { get; set; }
    }

    public class Root
    {
        public string status { get; set; }
        public Data data { get; set; }
    }
}