using System;
using System.Collections.Generic;
using System.Text;

namespace MiDI.MidiDevices.Launchpad
{
    /// <summary>
    /// Default Colros of the Launchpad
    /// </summary>
    public enum LaunchpadColor
    {
        Black = 0,
        DarkGray = 1,
        LightGray = 2,
        White = 3,
        LightRed = 4,
        BrightRed = 5,
        DarkerRed = 6,
        DarkRed = 7,
        CremeWhite = 8,
        BrightOrange = 9, 
        DarkOrange = 10,
        Brown = 11,
        LightOrange = 12,
        BrightYellow = 13,
        DarkerYellow = 14,
        DarkYellow = 15,
        LightLime = 16,
        BrightLime = 17,
        DarkerLime = 18,
        DarkLime = 19,
        LightForestGreen = 20,
        BrightForestGreen = 21,
        DarkerForestGreen = 22,
        DarkForestGreen = 23,
        LightGreen = 24,
        BrightGreen = 25,
        DarkerGreen = 26,
        DarkGreen = 27,
        LightPetrolium = 28,
        BrightPetrolium = 29,
        DarkerPetrolium = 30,
        DarkPetrolium = 31,
        LightCyan = 32,
        BrightCyan = 33,
        DarkerCyan = 34,
        DarkCyan = 35,
        LightSkyBlue = 36,
        BrightSkyBlue = 37,
        DarkerSkyBlue = 38,
        DarkSkyBlue = 39,
        LightMarineBlue = 40,
        BrightMarineBlue = 41,
        DarkerMarineBlue = 42,
        DarkMarineBlue = 43,
        LightBlue = 44,
        BrightBlue = 45,
        DarkerBlue = 46,
        DarkBlue = 47,
        LightPurple = 48,
        BrightPurple = 49,
        DarkerPurple = 50,
        DarkPurple = 51,
        LightPink = 52,
        BrightPink = 53,
        DarkerPink = 54,
        DarkPink = 55,
        LightCandyRed = 56,
        BrightCandyRed = 57,
        DarkerCandyRed = 58,
        DarkCandyRed = 59,
        BrightOrangeRed = 60,
        BrightOrangeYellow = 61,
        BrightGold = 62,
        DarkYellowGreen = 63
    }



    /// <summary>
    /// Class for representing RGB Colors on the Launchpad
    /// </summary>
    public struct LaunchpadColorValue
    {
        #region Values

        public byte red;
        public byte green;
        public byte blue;

        public int hexColorValue;
        public LaunchpadColor padColor;

        #endregion



        #region Methods

        public LaunchpadColorValue(int hexValue, LaunchpadColor padColor)
        {
            hexColorValue = hexValue;
            blue = (byte)(hexValue & 0x7F);
            green = (byte)(((hexValue >> 8) & 0x7F));
            red = (byte)(((hexValue >> 16) & 0x7F));
            this.padColor = padColor;
        }



        public void SetColorRGB(byte red, byte green, byte blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }


        public void SetColorRGB(float red, float green, float blue)
        {
            if (red < 0) red = 0;
            if (red > 1) red = 1;
            
            if (green < 0) green = 0;
            if (green > 1) green = 1;
            
            if (blue < 0) blue = 0;
            if (blue > 1) blue = 1;

            this.red = (byte)(red * 127.0f);
            this.green = (byte)(green * 127.0f);
            this.blue = (byte)(blue * 127.0f);
        }


        public void SetColorHSV(float hue, float saturation, float value)
        {
            if (hue < 0) hue = 0;
            if (hue >= 360) hue = 0;

            if (saturation < 0) saturation = 0;
            if (saturation > 1) saturation = 1;

            if (value < 0) value = 0;
            if (value > 1) value = 1;

            float c = saturation * value;
            float x = c * (1 - MathF.Abs(((int)(hue / 60)) % 2 - 1));
            float m = value - c;
            float _red, _green, _blue;

            if(hue >= 0 && hue < 60)
            {
                _red = (byte)(c * 255);
                _green = (byte)(x * 255);
                _blue = 0;
            }
            else if(hue >= 60 && hue < 120)
            {
                _red = x;
                _green = c;
                _blue = 0;
            }
            else if(hue >= 120 && hue < 180)
            {
                _red = 0;
                _green = c;
                _blue = x;
            }
            else if(hue >= 180 && hue < 240)
            {
                _red = 0;
                _green = x;
                _blue = c;
            }
            else if(hue >= 240 && hue < 300)
            {
                _red = x;
                _green = 0;
                _blue = c;
            }
            else
            {
                _red = c;
                _green = 0;
                _blue = x;
            }

            red = (byte)((_red + m) * 255);
            green = (byte)((_green + m) * 255);
            blue = (byte)((_blue + m) * 255);
        }

        #endregion
    }



    /// <summary>
    /// Internal class for assigning default Colors a HEX Value
    /// </summary>
    internal class LaunchpadColorValueLib
    {
        #region Values

        public List<LaunchpadColorValue> launchpadColorValueLib = new List<LaunchpadColorValue>();
        public static LaunchpadColorValueLib Instance { get { if (instance == null) return new LaunchpadColorValueLib(); return instance; } private set { } }
        private static LaunchpadColorValueLib instance;

        #endregion



        #region Methods

        private LaunchpadColorValueLib()
        {
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x000000, LaunchpadColor.Black));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xB3B3B3, LaunchpadColor.DarkGray));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xDCDCDC, LaunchpadColor.LightGray));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xFFFFFF, LaunchpadColor.White));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xFEB2B2, LaunchpadColor.LightRed));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xFE6060, LaunchpadColor.BrightRed));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xDD6161, LaunchpadColor.DarkerRed));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xB36161, LaunchpadColor.DarkRed));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xFFF3D5, LaunchpadColor.CremeWhite));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xFFB361, LaunchpadColor.BrightOrange));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xDD8C61, LaunchpadColor.DarkOrange));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xB27560, LaunchpadColor.Brown));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xFFEEA1, LaunchpadColor.LightOrange));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xFFFF61, LaunchpadColor.BrightYellow));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xDDDD61, LaunchpadColor.DarkerYellow));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xB3B361, LaunchpadColor.DarkYellow));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xDDFFA1, LaunchpadColor.LightLime));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xC2FF61, LaunchpadColor.BrightLime));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xA1DD61, LaunchpadColor.DarkerLime));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x81B361, LaunchpadColor.DarkLime));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xC2FFB3, LaunchpadColor.LightForestGreen));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x61FF61, LaunchpadColor.BrightForestGreen));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x61DD61, LaunchpadColor.DarkerForestGreen));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x61B361, LaunchpadColor.DarkForestGreen));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xC2FFC2, LaunchpadColor.LightGreen));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x61FF8C, LaunchpadColor.BrightGreen));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x61DD76, LaunchpadColor.DarkerGreen));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x61B36B, LaunchpadColor.DarkGreen));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xC2FFCC, LaunchpadColor.LightPetrolium));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x61FFCC, LaunchpadColor.BrightPetrolium));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x61DDA1, LaunchpadColor.DarkerPetrolium));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x61B381, LaunchpadColor.DarkPetrolium));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xC1FEF2, LaunchpadColor.LightCyan));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x60FEE8, LaunchpadColor.BrightCyan));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x61DDC2, LaunchpadColor.DarkerCyan));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x61B396, LaunchpadColor.DarkCyan));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xC2F3FF, LaunchpadColor.LightSkyBlue));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x61EEFF, LaunchpadColor.BrightSkyBlue));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x61C7DD, LaunchpadColor.DarkerSkyBlue));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x61A1B3, LaunchpadColor.DarkSkyBlue));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xC2DDFF, LaunchpadColor.LightMarineBlue));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xC1C7FF, LaunchpadColor.BrightMarineBlue));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x61A1DD, LaunchpadColor.DarkerMarineBlue));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x6181B3, LaunchpadColor.DarkMarineBlue));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xA18CFF, LaunchpadColor.LightBlue));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x6161FF, LaunchpadColor.BrightBlue));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x6161DD, LaunchpadColor.DarkerBlue));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x6161B3, LaunchpadColor.DarkBlue));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xCBB2FE, LaunchpadColor.LightPurple));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xA161FF, LaunchpadColor.BrightPurple));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x8161DD, LaunchpadColor.DarkerPurple));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0x7661B3, LaunchpadColor.DarkPurple));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xFFB3FF, LaunchpadColor.LightPink));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xFF61FF, LaunchpadColor.BrightPink));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xDD61DD, LaunchpadColor.DarkerPink));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xB361B3, LaunchpadColor.DarkPink));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xFFB3D5, LaunchpadColor.LightCandyRed));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xFE60C1, LaunchpadColor.BrightCandyRed));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xDD61A1, LaunchpadColor.DarkerCandyRed));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xB3618C, LaunchpadColor.DarkCandyRed));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xFF7661, LaunchpadColor.BrightOrangeRed));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xE9B361, LaunchpadColor.BrightOrangeYellow));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xDDC261, LaunchpadColor.BrightGold));
            launchpadColorValueLib.Add(new LaunchpadColorValue(0xA1A161, LaunchpadColor.DarkYellowGreen));
        }

        #endregion
    }
}
