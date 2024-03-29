﻿/*
*  CoinTicker by Wendelstein7
*  I am terribly sorry for this terrible code and terrible application. But it works, it was made in a day and I had fun :)
*/

// ArduinoJson - Version: Latest
#include <ArduinoJson.h>
#include <ArduinoJson.hpp>

// MD_Parola - Version: Latest
#include <MD_Parola.h>
#include <MD_MAX72xx.h>
#include <SPI.h>

// Font
MD_MAX72XX::fontType_t CoinTicker[] PROGMEM =
    {
        0,                               // 0
        0,                               // 1
        0,                               // 2
        0,                               // 3
        0,                               // 4
        0,                               // 5
        0,                               // 6
        0,                               // 7
        0,                               // 8
        0,                               // 9
        0,                               // 10
        0,                               // 11
        0,                               // 12
        0,                               // 13
        0,                               // 14
        0,                               // 15
        0,                               // 16
        0,                               // 17
        0,                               // 18
        0,                               // 19
        0,                               // 20
        0,                               // 21
        0,                               // 22
        0,                               // 23
        0,                               // 24
        0,                               // 25
        0,                               // 26
        0,                               // 27
        0,                               // 28
        0,                               // 29
        0,                               // 30
        0,                               // 31
        2, 0, 0,                         // 32 - 'Space'
        0,                               // 33
        0,                               // 34
        0,                               // 35
        5, 20, 62, 85, 65, 34,           // 36 - '$'
        0,                               // 37
        0,                               // 38
        0,                               // 39
        0,                               // 40
        0,                               // 41
        0,                               // 42
        0,                               // 43
        2, 0x60, 0x60,                   // 44 - ','
        0,                               // 45
        2, 0x60, 0x60,                   // 46 - '.'
        0,                               // 47
        5, 0x3e, 0x51, 0x49, 0x45, 0x3e, // 48 - '0'
        5, 0, 4, 2, 127, 0,              // 49 - '1'
        5, 0x72, 0x49, 0x49, 0x49, 0x46, // 50 - '2'
        5, 0x21, 0x41, 0x49, 0x4d, 0x33, // 51 - '3'
        5, 0x18, 0x14, 0x12, 0x7f, 0x10, // 52 - '4'
        5, 0x27, 0x45, 0x45, 0x45, 0x39, // 53 - '5'
        5, 0x3c, 0x4a, 0x49, 0x49, 0x31, // 54 - '6'
        5, 0x41, 0x21, 0x11, 0x09, 0x07, // 55 - '7'
        5, 0x36, 0x49, 0x49, 0x49, 0x36, // 56 - '8'
        5, 0x46, 0x49, 0x49, 0x29, 0x1e, // 57 - '9'
        0,                               // 58
        0,                               // 59
        0,                               // 60
        0,                               // 61
        0,                               // 62
        0,                               // 63
        0,                               // 64
        0,                               // 65
        0,                               // 66
        0,                               // 67
        0,                               // 68
        0,                               // 69
        0,                               // 70
        0,                               // 71
        0,                               // 72
        0,                               // 73
        0,                               // 74
        0,                               // 75
        0,                               // 76
        0,                               // 77
        0,                               // 78
        0,                               // 79
        0,                               // 80
        0,                               // 81
        0,                               // 82
        0,                               // 83
        0,                               // 84
        0,                               // 85
        0,                               // 86
        0,                               // 87
        0,                               // 88
        0,                               // 89
        0,                               // 90
        0,                               // 91
        0,                               // 92
        0,                               // 93
        0,                               // 94
        0,                               // 95
        0,                               // 96
        0,                               // 97
        0,                               // 98
        0,                               // 99
        0,                               // 100
        0,                               // 101
        0,                               // 102
        0,                               // 103
        0,                               // 104
        0,                               // 105
        0,                               // 106
        0,                               // 107
        0,                               // 108
        0,                               // 109
        0,                               // 110
        0,                               // 111
        0,                               // 112
        0,                               // 113
        0,                               // 114
        0,                               // 115
        0,                               // 116
        0,                               // 117
        0,                               // 118
        0,                               // 119
        0,                               // 120
        0,                               // 121
        0,                               // 122
        0,                               // 123
        0,                               // 124
        0,                               // 125
        0,                               // 126
        0,                               // 127
        0,                               // 128
        0,                               // 129
        0,                               // 130
        0,                               // 131
        0,                               // 132
        0,                               // 133
        0,                               // 134
        0,                               // 135
        0,                               // 136
        0,                               // 137
        0,                               // 138
        0,                               // 139
        0,                               // 140
        0,                               // 141
        0,                               // 142
        0,                               // 143
        0,                               // 144
        0,                               // 145
        0,                               // 146
        0,                               // 147
        0,                               // 148
        0,                               // 149
        0,                               // 150
        0,                               // 151
        0,                               // 152
        0,                               // 153
        0,                               // 154
        0,                               // 155
        0,                               // 156
        0,                               // 157
        0,                               // 158
        0,                               // 159
        0,                               // 160
        0,                               // 161
        0,                               // 162
        0,                               // 163
        0,                               // 164
        0,                               // 165
        0,                               // 166
        0,                               // 167
        0,                               // 168
        0,                               // 169
        0,                               // 170
        0,                               // 171
        0,                               // 172
        0,                               // 173
        0,                               // 174
        0,                               // 175
        0,                               // 176
        0,                               // 177
        0,                               // 178
        0,                               // 179
        0,                               // 180
        0,                               // 181
        0,                               // 182
        0,                               // 183
        0,                               // 184
        0,                               // 185
        0,                               // 186
        0,                               // 187
        0,                               // 188
        0,                               // 189
        0,                               // 190
        0,                               // 191
        0,                               // 192
        0,                               // 193
        0,                               // 194
        0,                               // 195
        0,                               // 196
        0,                               // 197
        0,                               // 198
        0,                               // 199
        0,                               // 200
        0,                               // 201
        0,                               // 202
        0,                               // 203
        0,                               // 204
        0,                               // 205
        0,                               // 206
        0,                               // 207
        0,                               // 208
        0,                               // 209
        0,                               // 210
        0,                               // 211
        0,                               // 212
        0,                               // 213
        0,                               // 214
        0,                               // 215
        0,                               // 216
        0,                               // 217
        0,                               // 218
        0,                               // 219
        0,                               // 220
        0,                               // 221
        0,                               // 222
        0,                               // 223
        0,                               // 224
        0,                               // 225
        0,                               // 226
        0,                               // 227
        0,                               // 228
        0,                               // 229
        0,                               // 230
        0,                               // 231
        0,                               // 232
        0,                               // 233
        0,                               // 234
        0,                               // 235
        0,                               // 236
        0,                               // 237
        0,                               // 238
        0,                               // 239
        0,                               // 240
        0,                               // 241
        0,                               // 242
        0,                               // 243
        0,                               // 244
        0,                               // 245
        0,                               // 246
        0,                               // 247
        0,                               // 248
        0,                               // 249
        0,                               // 250
        0,                               // 251
        0,                               // 252
        0,                               // 253
        0,                               // 254
        0,                               // 255
};

#define HARDWARE_TYPE MD_MAX72XX::FC16_HW
#define MAX_DEVICES 8
#define CS_PIN 3
MD_Parola myDisplay = MD_Parola(HARDWARE_TYPE, CS_PIN, MAX_DEVICES);

//#define DATA_PIN 2
//#define CLK_PIN 4
//MD_Parola myDisplay = MD_Parola(HARDWARE_TYPE, DATA_PIN, CLK_PIN, CS_PIN, MAX_DEVICES);

char inputString[128]; // a String to hold incoming data
byte inputStringIndex = 0;
bool stringComplete = false; // whether the string is complete

void setup()
{
  Serial.begin(38400);
  Serial.println("MarqueeArduino");

  myDisplay.begin();
  myDisplay.displayClear();
  myDisplay.setFont(CoinTicker);
}

static bool looping = false;

void loop()
{
  if (stringComplete)
  {
    StaticJsonDocument<96> doc;
    DeserializationError error = deserializeJson(doc, inputString, inputStringIndex);

    if (error)
    {
      Serial.print(F("deserializeJson() failed: "));
      Serial.println(error.f_str());
      return;
    }

    looping = doc["l"];

    myDisplay.setIntensity(doc["b"]);
    myDisplay.setInvert(doc["i"]);
    myDisplay.displayText(doc["t"], doc["a"], doc["s"], doc["p"], doc["eI"], doc["eO"]);

    inputString[0] = '\0';
    stringComplete = false;
    inputStringIndex = 0;
  }

  if (myDisplay.displayAnimate())
  {
    if (looping)
      myDisplay.displayReset();
  }
}

void serialEvent()
{
  while (Serial.available())
  {
    char inChar = (char)Serial.read();
    if (inChar == '\n' || inputStringIndex >= 128)
    {
      stringComplete = true;
    }
    else
    {
      inputString[inputStringIndex] = inChar;
      inputString[inputStringIndex + 1] = '\0';
      inputStringIndex++;
    }
  }
}