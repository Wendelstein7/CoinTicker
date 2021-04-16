/*
 *  CoinTicker by Wendelstein7
 *  I am terribly sorry for this terrible code and terrible application. But it works, it was made in a day and I had fun :)
 */

namespace MarqueeDisplay
{
    enum TextPosition
    {
        PA_LEFT,
        PA_CENTER,
        PA_RIGHT,
    }

    enum TextEffect
    {
        PA_NO_EFFECT,
        PA_PRINT,
        PA_SCROLL_UP,
        PA_SCROLL_DOWN,
        PA_SCROLL_LEFT,
        PA_SCROLL_RIGHT,
        PA_SPRITE,
        PA_SLICE,
        PA_MESH,
        PA_FADE,
        PA_DISSOLVE,
        PA_BLINDS,
        PA_RANDOM,
        PA_WIPE,
        PA_WIPE_CURSOR,
        PA_SCAN_HORIZ,
        PA_SCAN_HORIZX,
        PA_SCAN_VERT,
        PA_SCAN_VERTX,
        PA_OPENING,
        PA_OPENING_CURSOR,
        PA_CLOSING,
        PA_CLOSING_CURSOR,
        PA_SCROLL_UP_LEFT,
        PA_SCROLL_UP_RIGHT,
        PA_SCROLL_DOWN_LEFT,
        PA_SCROLL_DOWN_RIGHT,
        PA_GROW_UP,
        PA_GROW_DOWN,
    }

    class MarqueeText
    {
        // Display settings
        public int Brightness = 0;
        public bool Inverted = false;

        // Effect tuning
        public int Speed = 100;
        public int Pause = 100;

        // Animation
        public TextEffect EffectIn = TextEffect.PA_NO_EFFECT;
        public TextEffect EffectOut = TextEffect.PA_NO_EFFECT;

        // Text
        public TextPosition Alignment = TextPosition.PA_CENTER;

        // Content
        public string Text = "";

        // Time
        public bool Loop = false;
    }

    class MarqueeText_o
    {
        public int b;
        public bool i;
        public int s;
        public int p;
        public TextEffect eI;
        public TextEffect eO;
        public TextPosition a;
        public string t;
        public bool l;

        public MarqueeText_o(MarqueeText marqueeText)
        {
            b = marqueeText.Brightness;
            i = marqueeText.Inverted;
            s = marqueeText.Speed;
            p = marqueeText.Pause;
            eI = marqueeText.EffectIn;
            eO = marqueeText.EffectOut;
            a = marqueeText.Alignment;
            t = marqueeText.Text;
            l = marqueeText.Loop;
        }
    }
}