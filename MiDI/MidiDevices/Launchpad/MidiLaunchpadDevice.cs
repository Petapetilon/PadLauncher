using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MiDI.Events;
using MiDI.Events.Launchpad;
using MiDI.MidiWinAPI;
using MiDI.MidiDevices.Launchpad.Animation;


namespace MiDI.MidiDevices.Launchpad
{
    /// <summary>
    /// Midi Device that represents a Launchpad Device
    /// </summary>
    public sealed class MidiLaunchpadDevice : MidiIODevice
    {
        #region Values

        /// <summary>
        /// Event that will be called when the a Button on the Launchpad is pressed
        /// </summary>
        public event EventHandler<LaunchpadInputEventArgs> OnPadDown;

        /// <summary>
        /// Event that will be called when the a Button on the Launchpad is released
        /// </summary>
        public event EventHandler<LaunchpadInputEventArgs> OnPadUp;

        /// <summary>
        /// Button Layout of the Launchpad
        /// </summary>
        public LaunchpadButtonLayout CurrentButtonLayout { get; set; }

        /// <summary>
        /// Model of the Launchpad
        /// </summary>
        public LaunchpadVersion DeviceLaunchpadVersion {get; set;}

        /// <summary>
        /// Assigned Animation of the Launchpad
        /// </summary>
        public LaunchpadAnimation Animation { get { return animation; } set { animation = value; if(animation != null) animation.launchpad = this; } }



        private LaunchpadColorFrame currentLaunchpadColors;
        private List<LaunchpadColorValue> launchpadColorValues;
        private LaunchpadColorValue padColorOnPress;
        private bool[] isPadPressed;
        private LaunchpadAnimation animation;

        #endregion



        #region Methods

        internal MidiLaunchpadDevice()
        {
            CurrentButtonLayout = LaunchpadButtonLayout.ProgrammerMode;
            launchpadColorValues = new List<LaunchpadColorValue>();
            isPadPressed = new bool[100];
            Animation = new LaunchpadAnimation();
            currentLaunchpadColors = new LaunchpadColorFrame();
            padColorOnPress = new LaunchpadColorValue(0, LaunchpadColor.Black);
        }

        /// <summary>
        /// Returns a Device with he given IDs and Launchpad Version
        /// </summary>
        /// <param name="IDIn">ID of the InDevice (ID == Index of the Name of the InDevice in the InDevice List)</param>
        /// <param name="IDOut">ID of the OutDevice (ID == Index of the Name of the OutDevice in the OutDevice List)</param>
        /// <param name="version">Launchpad Model</param>
        /// <returns>MidiOutDevice with the given ID</returns>
        public static MidiLaunchpadDevice GetDeviceByIDs(uint IDIn, uint IDOut, LaunchpadVersion version)
        {
            MidiLaunchpadDevice launchpadDevice = new MidiLaunchpadDevice();
            launchpadDevice.InDevice = MidiInDevice.GetDeviceByID(IDIn);
            launchpadDevice.OutDevice = MidiOutDevice.GetDeviceByID(IDOut);
            launchpadDevice.DeviceLaunchpadVersion = version;
            if (launchpadDevice.InDevice != null & launchpadDevice.OutDevice != null)
                return launchpadDevice;

            return null;
        }


        /// <summary>
        /// Clears all Colors from the Buttons
        /// </summary>
        public void ClearPads()
        {
            currentLaunchpadColors.ClearPads();


            if(CurrentButtonLayout == LaunchpadButtonLayout.ProgrammerMode)
            {
                for(byte i = 11; i < 100; i++)
                {
                    if (i % 10 == 0) continue;
                    MidiOutWinAPI.midiOutShortMsg(OutDevice.GetWinHandle(), MidiWinAPI.MidiWinAPI.PackShortEventBytes(0x80, i, 0));
                }
            }
        }


        /// <summary>
        /// Sets the given Color to the Pad(x, y) with the corresponding Display Type
        /// </summary>
        /// <param name="x">X value of the Pad</param>
        /// <param name="y">Y value of the Pad</param>
        /// <param name="color">Color of the Pad</param>
        /// <param name="displayType">Type of color display</param>
        public void SetPadColor(int x, int y, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor) 
        {
            currentLaunchpadColors.SetPadColor(x, y, color, displayType);
            MidiOutWinAPI.midiOutShortMsg(OutDevice.GetWinHandle(), MidiWinAPI.MidiWinAPI.PackShortEventBytes((byte)((byte)MidiEventType.NoteOn | (byte)(displayType)), ConvertPadCoordinateToByte(x, y), (byte)color));
        }


        /// <summary>
        /// Sets the given Color the PadCoordinate with the corresponding Display Type
        /// </summary>
        /// <param name="point">Coordinate of the Pad</param>
        /// <param name="color">Color of the Pad</param>
        /// <param name="displayType">Type of color display</param>
        public void SetPadColor(PadCoordinate point, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor) 
        {
            currentLaunchpadColors.SetPadColor(point, color, displayType);


            byte padNumber = 0;
            if(CurrentButtonLayout == LaunchpadButtonLayout.ProgrammerMode)
            {
                padNumber = (byte)(point.x + point.y * 10 + 11);
            }

            MidiOutWinAPI.midiOutShortMsg(OutDevice.GetWinHandle(), MidiWinAPI.MidiWinAPI.PackShortEventBytes((byte)(0x90 | (byte)displayType), padNumber, (byte)color));
        }


        /// <summary>
        /// Draws a line from the Pad(xA, yA) to Pad(xB, yB) with the corresponding Display Type
        /// </summary>
        /// <param name="xA">X value of the Start Pad</param>
        /// <param name="yA">Y value of the Start Pad</param>
        /// <param name="xB">X value of the End Pad</param>
        /// <param name="yB">Y value of the End Pad</param>
        /// <param name="color">Color of the Pad</param>
        /// <param name="displayType">Type of color display</param>
        public void DrawLine(int xA, int yA, int xB, int yB, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
            currentLaunchpadColors.DrawLine(xA, yA, xB, yB, color, displayType);


            float x = xA;
            float y = yA;
            float dx = Math.Abs(xB - xA);
            float dy = Math.Abs(yB - yA);
            float step = dx >= dy ? dx : dy;

            dx = (xB - xA) / step;
            dy = (yB - yA) / step;
        
            for(int i = 1; i <= step; ++i)
            {
                SetPadColor((byte)MathF.Round(x), (byte)MathF.Round(y), color, displayType);
                x += dx;
                y += dy;
            }

            SetPadColor(xB, yB, color, displayType);
        }


        /// <summary>
        /// Draws a line from the PadCoordinate A to PadCoordinate B with the corresponding Display Type
        /// </summary>
        /// <param name="pointA">Coordinate of the Start Pad</param>
        /// <param name="pointB">Coordinate of the End Pad</param>
        /// <param name="color">Color of the Pad</param>
        /// <param name="displayType">Type of the color display</param>
        public void DrawLine(PadCoordinate pointA, PadCoordinate pointB, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
            currentLaunchpadColors.DrawLine(pointA, pointB, color, displayType);


            float x = pointA.x;
            float y = pointA.y;
            float dx = Math.Abs(pointB.x - pointA.x);
            float dy = Math.Abs(pointB.y - pointA.y);
            float step = dx >= dy ? dx : dy;

            dx = (pointB.x - pointA.x) / step;
            dy = (pointB.y - pointA.y) / step;
        
            for(int i = 1; i <= step; ++i)
            {
                SetPadColor((byte)MathF.Round(x), (byte)MathF.Round(y), color, displayType);
                x += dx;
                y += dy;
            }

            SetPadColor(pointB.x, pointB.y, color, displayType);
        }


        /// <summary>
        /// Draws a triangle with the corresponding Display Type
        /// </summary>
        /// <param name="xA">X value of A Pad</param>
        /// <param name="yA">Y value of A Pad</param>
        /// <param name="xB">X value of B Pad</param>
        /// <param name="yB">Y value of B Pad</param>
        /// <param name="xC">X value of C Pad</param>
        /// <param name="yC">Y value of C Pad</param>
        /// <param name="fillTriangle">wether the Triangle should be filled or wireframe</param>
        /// <param name="color">Color of the Pad</param>
        /// <param name="displayType">Type of the color display</param>
        public void DrawTriangle(int xA, int yA, int xB, int yB, int xC, int yC, bool fillTriangle, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
            currentLaunchpadColors.DrawTriangle(xA, yA, xB, yB, xC, yC, fillTriangle, color, displayType);


            if (fillTriangle)
                DrawLineFilled(xA, yA, xB, yB, xC, yC, color, displayType);
            else
                DrawLine(xA, yA, xB, yB, color, displayType);

            DrawLine(xB, yB, xC, yC, color, displayType);
            DrawLine(xC, yC, xA, yA, color, displayType);
        }


        /// <summary>
        /// Draws a triangle with the corresponding Display Type
        /// </summary>
        /// <param name="pointA">Coordinate of A Pad</param>
        /// <param name="pointB">Coordinate of B Pad</param>
        /// <param name="pointC">Coordinate of C Pad</param>
        /// <param name="fillTriangle">wether the Triangle should be filled or wireframe</param>
        /// <param name="color">Color of the Pad</param>
        /// <param name="displayType">Type of the color display</param>
        public void DrawTriangle(PadCoordinate pointA, PadCoordinate pointB, PadCoordinate pointC, bool fillTriangle, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
            currentLaunchpadColors.DrawTriangle(pointA, pointB, pointC, fillTriangle, color, displayType);


            if (fillTriangle)
                DrawLineFilled(pointA.x, pointA.y, pointB.x, pointB.y, pointC.x, pointC.y, color, displayType);
            else
                DrawLine(pointA, pointB, color, displayType);

            DrawLine(pointB, pointC, color, displayType);
            DrawLine(pointC, pointA, color, displayType);
        }


        /// <summary>
        /// Draws a rectangle from Pad(xA, yA) == bottom left corner to Pad(xB, yB) == top right corner with the corresponding Display Type
        /// </summary>
        /// <param name="xA">X value of the bottom left Pad</param>
        /// <param name="yA">Y value of the bottom left Pad</param>
        /// <param name="xB">X value of the top right Pad</param>
        /// <param name="yB">Y value of the top right Pad</param>
        /// <param name="fillRectangle">wether the Rectangle should be filled or wireframe</param>
        /// <param name="color">Color of the Pad</param>
        /// <param name="displayType">Type of the color display</param>
        public void DrawRectangle(int xA, int yA, int xB, int yB, bool fillRectangle, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
            currentLaunchpadColors.DrawRectangle(xA, yA, xB, yB, fillRectangle, color, displayType);


            if (fillRectangle)
            {
                DrawTriangle(xA, yA, xB, yA, xA, yB, true, color, displayType);
                DrawTriangle(xB, yB, xB, yA, xA, yB, true, color, displayType);
            }
            else
            {
                DrawLine(xA, yA, xA, yB, color, displayType);
                DrawLine(xA, yA, xB, yA, color, displayType);
                DrawLine(xA, yB, xB, yB, color, displayType);
                DrawLine(xB, yA, xB, yB, color, displayType);
            }
        }


        /// <summary>
        /// Draws a rectangle from PadCoordinate A == bottom left corner to Pad Coordinate B == top right corner with the corresponding Display Type
        /// </summary>
        /// <param name="pointA">Bottom left PadCoordinate</param>
        /// <param name="pointB">Top right PadCoordinate</param>
        /// <param name="fillRectangle">wether the Rectangle should be filled or wireframe</param>
        /// <param name="color">Color of the Pad</param>
        /// <param name="displayType">Type of the color display</param>
        public void DrawRectangle(PadCoordinate pointA, PadCoordinate pointB, bool fillRectangle, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
            currentLaunchpadColors.DrawRectangle(pointA, pointB, fillRectangle, color, displayType);


            if (fillRectangle)
            {
                DrawTriangle(pointA.x, pointA.y, pointB.x, pointA.y, pointA.x, pointB.y, true, color, displayType);
                DrawTriangle(pointB.x, pointB.y, pointB.x, pointA.y, pointA.x, pointB.y, true, color, displayType);
            }
            else
            {
                DrawLine(pointA.x, pointA.y, pointA.x, pointB.y, color, displayType);
                DrawLine(pointA.x, pointA.y, pointB.x, pointA.y, color, displayType);
                DrawLine(pointA.x, pointB.y, pointB.x, pointB.y, color, displayType);
                DrawLine(pointB.x, pointA.y, pointB.x, pointB.y, color, displayType);
            }
        }


        /// <summary>
        /// Draws a circle with Pad(x, y) at the centre with the given radius and with the corresponding Display Type
        /// </summary>
        /// <param name="x">X value of the centre Pad</param>
        /// <param name="y">Y value of the centre Pad</param>
        /// <param name="radius">radius of the circle</param>
        /// <param name="fillCircle">wether the Circle should be filled or wireframe</param>
        /// <param name="color">Color of the Pad</param>
        /// <param name="displayType">Type of the color display</param>
        public void DrawCircle(int x, int y, int radius, bool fillCircle, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
            currentLaunchpadColors.DrawCircle(x, y, radius, fillCircle, color, displayType);


            int u = 0;
            int v = radius;
            int d = 3 - 2 * radius;

            SetPadColor(x + u, y + v, color, displayType);
            SetPadColor(x - u, y + v, color, displayType);
            SetPadColor(x + u, y - v, color, displayType);
            SetPadColor(x - u, y - v, color, displayType);
            SetPadColor(x + v, y + u, color, displayType);
            SetPadColor(x - v, y + u, color, displayType);
            SetPadColor(x + v, y - u, color, displayType);
            SetPadColor(x - v, y - u, color, displayType);

            while (v >= u)
            {
                u++;
                if(d > 0)
                {
                    v--;
                    d += 4 * (u - v) + 10;
                }
                else
                {
                    d += 4 * u + 6;
                }

                SetPadColor(x + u, y + v, color, displayType);
                SetPadColor(x - u, y + v, color, displayType);
                SetPadColor(x + u, y - v, color, displayType);
                SetPadColor(x - u, y - v, color, displayType);
                SetPadColor(x + v, y + u, color, displayType);
                SetPadColor(x - v, y + u, color, displayType);
                SetPadColor(x + v, y - u, color, displayType);
                SetPadColor(x - v, y - u, color, displayType);
            }

            if(fillCircle)
                for(int i = radius - 1; i >= 0; i--)
                    DrawCircle(x, y, i, false, color, displayType);
        }


        /// <summary>
        /// Draws a circle with Pad(x, y) at the centre with the given radius and with the corresponding Display Type
        /// </summary>
        /// <param name="centre">Padcoordinate of the centre</param>
        /// <param name="radius"> radius of the circle</param>
        /// <param name="fillCircle">wether the Circle should be filled or wireframe</param>
        /// <param name="color">Color of the Pad</param>
        /// <param name="displayType">Type of the color display</param>
        public void DrawCircle(PadCoordinate centre, int radius, bool fillCircle, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
            currentLaunchpadColors.DrawCircle(centre, radius, fillCircle, color, displayType);


            int u = 0;
            int v = radius;
            int d = 3 - 2 * radius;

            SetPadColor(centre.x + u, centre.y + v, color, displayType);
            SetPadColor(centre.x - u, centre.y + v, color, displayType);
            SetPadColor(centre.x + u, centre.y - v, color, displayType);
            SetPadColor(centre.x - u, centre.y - v, color, displayType);
            SetPadColor(centre.x + v, centre.y + u, color, displayType);
            SetPadColor(centre.x - v, centre.y + u, color, displayType);
            SetPadColor(centre.x + v, centre.y - u, color, displayType);
            SetPadColor(centre.x - v, centre.y - u, color, displayType);

            while (v >= u)
            {
                u++;
                if(d > 0)
                {
                    v--;
                    d += 4 * (u - v) + 10;
                }
                else
                {
                    d += 4 * u + 6;
                }

                SetPadColor(centre.x + u, centre.y + v, color, displayType);
                SetPadColor(centre.x - u, centre.y + v, color, displayType);
                SetPadColor(centre.x + u, centre.y - v, color, displayType);
                SetPadColor(centre.x - u, centre.y - v, color, displayType);
                SetPadColor(centre.x + v, centre.y + u, color, displayType);
                SetPadColor(centre.x - v, centre.y + u, color, displayType);
                SetPadColor(centre.x + v, centre.y - u, color, displayType);
                SetPadColor(centre.x - v, centre.y - u, color, displayType);
            }

            if(fillCircle)
                for(int i = radius - 1; i >= 0; i--)
                    DrawCircle(centre.x, centre.y, i, false, color, displayType);
        }


        /// <summary>
        /// Draws a Polygon from the given points with the given color and corresponding Display Type
        /// </summary>
        /// <param name="points">Corners of the Polygon</param>
        /// <param name="color">Color of the Pad</param>
        /// <param name="displayType">Type of the color display</param>
        public void DrawPolygon(PadCoordinate[] points, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
            currentLaunchpadColors.DrawPolygon(points, color, displayType);


            for (int i = 0; i < points.Length - 1; i++)
            {
                DrawLine(points[i], points[i + 1], color, displayType);
            }

            DrawLine(points[0], points[points.Length - 1], color, displayType);
        }


        /// <summary>
        /// Draws a Heart on the Centre of the Launchpad with the given color and corresponding Display Type
        /// </summary>
        /// <param name="color"></param>
        /// <param name="displayType"></param>
        public void DrawHeart(LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
            currentLaunchpadColors.DrawHeart(color, displayType);


            DrawPolygon(new PadCoordinate[] { 
                new PadCoordinate(4, 0), 
                new PadCoordinate(0, 4), 
                new PadCoordinate(0, 6), 
                new PadCoordinate(2, 8), 
                new PadCoordinate(4, 7), 
                new PadCoordinate(6, 8), 
                new PadCoordinate(8, 6), 
                new PadCoordinate(8, 4), 
                new PadCoordinate(4, 0) }, 
                color, displayType);
        }

        /// <summary>
        /// Find out what it does ^^
        /// </summary>
        public void BeProud()
        {
            currentLaunchpadColors.BeProud();


            SetPadColor(4, 0, LaunchpadColor.BrightPurple, PadColorDisplayType.PulsingColor);
            DrawLine(3, 1, 5, 1, LaunchpadColor.BrightPurple, PadColorDisplayType.PulsingColor);
            DrawLine(2, 2, 6, 2, LaunchpadColor.BrightPurple, PadColorDisplayType.PulsingColor);
            DrawLine(1, 3, 7, 3, LaunchpadColor.BrightMarineBlue, PadColorDisplayType.PulsingColor);
            DrawLine(0, 4, 8, 4, LaunchpadColor.BrightForestGreen, PadColorDisplayType.PulsingColor);
            DrawLine(0, 5, 8, 5, LaunchpadColor.BrightYellow, PadColorDisplayType.PulsingColor);
            DrawLine(0, 6, 8, 6, LaunchpadColor.BrightOrange, PadColorDisplayType.PulsingColor);
            DrawLine(1, 7, 7, 7, LaunchpadColor.BrightRed, PadColorDisplayType.PulsingColor);
            DrawLine(2, 8, 3, 8, LaunchpadColor.BrightRed, PadColorDisplayType.PulsingColor);
            DrawLine(5, 8, 6, 8, LaunchpadColor.BrightRed, PadColorDisplayType.PulsingColor);
        }


        /// <summary>
        /// Creates a Launchpad Color Frame from the current displayed colors on the Launchpad
        /// </summary>
        public LaunchpadColorFrame CreateColorFrameFromCurrent() {  return new LaunchpadColorFrame(currentLaunchpadColors); }


        /// <summary>
        /// Finds the Launchpad Color Value for the given Launchpad Color
        /// </summary>
        public LaunchpadColorValue GetColorValueFromLaunchpadColor(LaunchpadColor color) { return launchpadColorValues.Find(x => x.padColor == color); }




        /// <summary>
        /// Fills in the Header for the apporiatly Set Launchpadversion
        /// </summary>
        /// <param name="data">data Array</param>
        /// <returns>Length of Header in bytes</returns>
        public int FillSysExMessageHeader(ref byte[] data)
        {
            byte[] headerData = null;
            switch (DeviceLaunchpadVersion)
            {
                case LaunchpadVersion.MiniMK3:
                    headerData = new byte[] { 240, 0, 32, 41, 2, 13 };
                    break;

                case LaunchpadVersion.ProMK3:
                    headerData = new byte[] { 240, 0, 32, 41, 2, 14 };
                    break;

                case LaunchpadVersion.Launchpad:
                    headerData = new byte[0];
                    break;

                default:
                    headerData = new byte[0];
                    break;
            }

            data.SetValue(headerData, 0);
            return headerData.Length;
        }


        /// <summary>
        /// Fills in the Header for the apporiatly Set Launchpadversion
        /// </summary>
        /// <param name="data">data List</param>
        /// <returns>Length of Header in bytes</returns>
        public int FillSysExMessageHeader(ref List<byte> data)
        {
            byte[] headerData = null;
            switch (DeviceLaunchpadVersion)
            {
                case LaunchpadVersion.MiniMK3:
                    headerData = new byte[] { 240, 0, 32, 41, 2, 13 };
                    break;

                case LaunchpadVersion.ProMK3:
                    headerData = new byte[] { 240, 0, 32, 41, 2, 14 };
                    break;

                default:
                    headerData = new byte[0];
                    break;
            }

            data.AddRange(headerData);
            return headerData.Length;
        }


        /// <summary>
        /// Ends the message
        /// </summary>
        public void EndSysExMessage(ref List<byte> data)
        {
            data.Add(247);
        }


        /// <summary>
        /// Be sure to use the "FillSysExMessageHeader" and "EndSysExMessage" functions on the data array
        /// </summary>
        new public void SendSysExMessage(byte[] data)
        {
            base.SendSysExMessage(data);
        }


        /// <summary>
        /// Returns wether the Pad(x, y) is currently held down
        /// </summary>
        /// <param name="x">X value of the Pad</param>
        /// <param name="y">Y value of the Pad</param>
        /// <returns>State of the Pad</returns>
        public bool IsButtonPressed(int x, int y)
        {
            return isPadPressed[ConvertPadCoordinateToByte(x, y)];
        }        
        

        /// <summary>
        /// Returns wether the Pad the given PadCoordinate is currently held down
        /// </summary>
        /// <param name="pad">PadCoordinate of the Pad</param>
        /// <returns>State of the Pad</returns>
        public bool IsButtonPressed(PadCoordinate pad)
        {
            return isPadPressed[ConvertPadCoordinateToByte(pad.x, pad.y)];
        }


        /// <summary>
        /// Converts a Byte to a PadCoordinate
        /// </summary>
        /// <param name="data">Data Byte</param>
        public PadCoordinate ConvertByteToPadCoordinate(byte data)
        {
            switch (DeviceLaunchpadVersion) 
            {
                case LaunchpadVersion.MiniMK3:
                    return new PadCoordinate((data - 11) % 10, (data - 11) / 10);

                case LaunchpadVersion.ProMK3:
                    return new PadCoordinate(0, 0);

                case LaunchpadVersion.Launchpad:
                    return new PadCoordinate(0, 0);

                default:
                    return new PadCoordinate(-1, -1);
            }
        }


        /// <summary>
        /// Converts a X value and a Y value to a byte
        /// </summary>
        /// <param name="x">X value of the Point</param>
        /// <param name="y">Y value of the Point</param>
        public byte ConvertPadCoordinateToByte(int x, int y)
        {
            switch (DeviceLaunchpadVersion)
            {
                case LaunchpadVersion.MiniMK3:
                    return (byte)(x + y * 10 + 11);

                case LaunchpadVersion.ProMK3:
                    return 0;

                case LaunchpadVersion.Launchpad:
                    return (byte)(x + (80 - y * 10));

                default:
                    return 0;
            }
        }        
        
        
        /// <summary>
        /// Converts a PadCoordinate to a byte
        /// </summary>
        /// <param name="point">PadCoordinate of the Point</param>
        public byte ConvertPadCoordinateToByte(PadCoordinate point)
        {
            switch (DeviceLaunchpadVersion)
            {
                case LaunchpadVersion.MiniMK3:
                    return (byte)(point.x + point.y * 10 + 11);

                case LaunchpadVersion.ProMK3:
                    return 0;

                case LaunchpadVersion.Launchpad:
                    return (byte)(point.x + (80 - point.y * 10));

                default:
                    return 0;
            }
        }


        private void DrawLineFilled(int xA, int yA, int xB, int yB, int xC, int yC, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
            float x = xA;
            float y = yA;
            float dx = Math.Abs(xB - xA);
            float dy = Math.Abs(yB - yA);
            float step = dx >= dy ? dx : dy;

            dx = (xB - xA) / step;
            dy = (yB - yA) / step;

            for (int i = 1; i <= step; ++i)
            {
                SetPadColor((byte)MathF.Round(x), (byte)MathF.Round(y), color, displayType);
                DrawLine((byte)MathF.Round(x), (byte)MathF.Round(y), xC, yC, color, displayType);
                x += dx;
                y += dy;
            }

            SetPadColor(xB, yB, color, displayType);
            DrawLine((byte)MathF.Round(x), (byte)MathF.Round(y), xC, yC, color, displayType);
        }
        protected override void OnMidiInMessageFunction(object sender, MidiMessageEventArgs eventArgs)
        {
            base.OnMidiInMessageFunction(sender, eventArgs);

            if(eventArgs.midiMessageStatus == MidiEventType.NoteOn)
            {                
                if(eventArgs.data2 != 0)
                {
                    OnPadDown?.Invoke(this, new LaunchpadInputEventArgs(ConvertByteToPadCoordinate(eventArgs.data1), eventArgs.data1, eventArgs.data2));
                    isPadPressed[eventArgs.data1] = true;
                }
                else
                {
                    OnPadUp?.Invoke(this, new LaunchpadInputEventArgs(ConvertByteToPadCoordinate(eventArgs.data1), eventArgs.data1, eventArgs.data2));
                    isPadPressed[eventArgs.data1] = false;
                }
            }


            if(eventArgs.midiMessageStatus == MidiEventType.ControlChange)
            {
                if (eventArgs.data2 > 0)
                {
                    OnPadDown?.Invoke(this, new LaunchpadInputEventArgs(ConvertByteToPadCoordinate(eventArgs.data1), eventArgs.data1, eventArgs.data2));
                    isPadPressed[eventArgs.data1] = true;
                }
                else
                {
                    OnPadUp?.Invoke(this, new LaunchpadInputEventArgs(ConvertByteToPadCoordinate(eventArgs.data1), eventArgs.data1, eventArgs.data2));
                    isPadPressed[eventArgs.data1] = false;
                }
            }



            if(padColorOnPress.hexColorValue != 0)
            {
                if (isPadPressed[eventArgs.data1])
                {
                    List<byte> messageData = new List<byte>();
                    FillSysExMessageHeader(ref messageData);
                    messageData.Add(3);
                    messageData.Add(3);
                    messageData.Add(eventArgs.data1);
                    messageData.Add(padColorOnPress.red);
                    messageData.Add(padColorOnPress.green);
                    messageData.Add(padColorOnPress.blue);
                    EndSysExMessage(ref messageData);

                    OutDevice.SendSysExMessage(messageData.ToArray());
                }
                else
                {
                    List<byte> messageData = new List<byte>();
                    FillSysExMessageHeader(ref messageData);
                    messageData.Add(3);
                    messageData.Add(0);
                    messageData.Add(eventArgs.data1);
                    messageData.Add(0);
                    EndSysExMessage(ref messageData);

                    OutDevice.SendSysExMessage(messageData.ToArray());
                }
            }
        }

        #endregion
    }
}
