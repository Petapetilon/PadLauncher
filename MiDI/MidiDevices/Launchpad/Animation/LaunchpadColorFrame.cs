using System;
using System.Collections.Generic;
using System.Text;

namespace MiDI.MidiDevices.Launchpad.Animation
{
    /// <summary>
    /// Represents a Frame of Colors with their Display Types on the Launchpad
    /// </summary>
    public sealed class LaunchpadColorFrame
    {
        #region Values

        private LaunchpadColorValue[,] padColors = new LaunchpadColorValue[9, 9];
        private LaunchpadColor[,] flashingColors = new LaunchpadColor[9, 9];
        private PadColorDisplayType[,] padDisplayType = new PadColorDisplayType[9, 9];
        internal int displayDuration;

        #endregion



        #region Methods

        public LaunchpadColorFrame() 
        { 
            displayDuration = 100;
            padColors.Initialize();
        }
        public LaunchpadColorFrame(int displayDurationOfFrame) { displayDuration = displayDurationOfFrame; }
        public LaunchpadColorFrame(LaunchpadColorFrame original)
        {
            padColors = original.padColors.Clone() as LaunchpadColorValue[,];
            padDisplayType = original.padDisplayType.Clone() as PadColorDisplayType[,];
            displayDuration = original.displayDuration;
        }        
        public LaunchpadColorFrame(LaunchpadColorFrame original, int displayDurationOfFrame)
        {
            padColors = original.padColors.Clone() as LaunchpadColorValue[,];
            padDisplayType = original.padDisplayType.Clone() as PadColorDisplayType[,];
            displayDuration = displayDurationOfFrame;
        }


        /// <summary>
        /// Applys the data of the frame to the given Launchpad using the velocity of a Midi message for color representation
        /// </summary>
        public void ApplyToLaunchpadUsingMidi(MidiLaunchpadDevice launchpadDevice)
        {
            for(int x = 0; x < 9; x++)
                for(int y = 0; y < 9; y++)
                    launchpadDevice.SetPadColor(x, y, padColors[x, y].padColor, padDisplayType[x, y]);
        }


        /// <summary>
        /// Applys the data of the frame to the given Launchpad using the SysEx communcation functions of the Launchpad
        /// </summary>
        public void ApplyToLaunchpadUsingSysEx(MidiLaunchpadDevice launchpadDevice)
        {
            List<byte> frameData = new List<byte>();

            launchpadDevice.FillSysExMessageHeader(ref frameData);
            frameData.Add(3); //Instruction for Setting Colors

            switch (launchpadDevice.DeviceLaunchpadVersion)
            {
                case LaunchpadVersion.MiniMK3:
                    for(int x = 0; x < 9; x++)
                    {
                        for(int y = 0; y < 9; y++)
                        {
                            frameData.Add((byte)padDisplayType[x, y]);
                            frameData.Add(launchpadDevice.ConvertPadCoordinateToByte(x, y));

                            switch (padDisplayType[x, y])
                            {
                                case PadColorDisplayType.SolidColor:
                                case PadColorDisplayType.PulsingColor:
                                    frameData.Add((byte)padColors[x, y].padColor);
                                    break;

                                case PadColorDisplayType.FlashingColor:
                                    frameData.Add((byte)padColors[x, y].padColor);
                                    frameData.Add((byte)flashingColors[x, y]);
                                    break;

                                case PadColorDisplayType.RGBColor:
                                    frameData.Add((byte)padColors[x, y].red);
                                    frameData.Add((byte)padColors[x, y].green);
                                    frameData.Add((byte)padColors[x, y].blue);
                                    break;
                            }
                        }
                    }

                    break;
            }

            launchpadDevice.EndSysExMessage(ref frameData);
            launchpadDevice.SendSysExMessage(frameData.ToArray());
        }


        /// <summary>
        /// Clears all Colors from the Buttons
        /// </summary>
        public void ClearPads()
        {
            padColors = new LaunchpadColorValue[9, 9];
            padDisplayType = new PadColorDisplayType[9, 9];
        }


        /// <summary>
        /// Sets the given Color to the Pad(x, y) with the corresponding Display Type
        /// </summary>
        /// <param name="x">X value of the Pad</param>
        /// <param name="y">Y value of the Pad</param>
        /// <param name="color">Color of the Pad</param>
        /// <param name="displayType">Type of color display</param>
        /// <param name="flashingColor">Alternating Flashing Color of the Pad</param>

        public void SetPadColor(int x, int y, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor, LaunchpadColor flashingColor = LaunchpadColor.Black)
        {
            if (x < 0 || x > 8 || y < 0 || y > 8) return;
            var colorValue = LaunchpadColorValueLib.Instance.launchpadColorValueLib.Find(x => x.padColor == color);
            padColors[x, y] = new LaunchpadColorValue(colorValue.hexColorValue, colorValue.padColor);
            flashingColors[x, y] = flashingColor;
            padDisplayType[x, y] = displayType;
        }


        /// <summary>
        /// Sets the given Color the PadCoordinate with the corresponding Display Type
        /// </summary>
        /// <param name="x">X value of the Pad</param>
        /// <param name="y">Y value of the Pad</param>        
        /// <param name="color">Color of the Pad</param>
        /// <param name="displayType">Type of color display</param>
        public void SetPadColor(int x, int y, LaunchpadColorValue color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
            if (x < 0 || x > 8 || y < 0 || y > 8) return;
            padColors[x, y] = color;
            if (color.padColor == 0 && (color.red != 0 || color.green != 0 || color.blue != 0))
                displayType = PadColorDisplayType.RGBColor;
            padDisplayType[x, y] = displayType;
        }


        /// <summary>
        /// Sets the given Color to the Pad(x, y) with the corresponding Display Type
        /// </summary>
        /// <param name="point">Coordinate of the Pad</param>
        /// <param name="color">Color of the Pad</param>
        /// <param name="displayType">Type of color display</param>
        /// <param name="flashingColor">Alternating Flashing Color of the Pad</param>
        public void SetPadColor(PadCoordinate point, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor, LaunchpadColor flashingColor = LaunchpadColor.Black)
        {
            if (point.x < 0 || point.x > 8 || point.y < 0 || point.y > 8) return;
            var colorValue = LaunchpadColorValueLib.Instance.launchpadColorValueLib.Find(x => x.padColor == color);
            padColors[point.x, point.y] = new LaunchpadColorValue(colorValue.hexColorValue, colorValue.padColor);
            flashingColors[point.x, point.y] = flashingColor;
            padDisplayType[point.x, point.y] = displayType;
        }


        /// <summary>
        /// Sets the given Color to the Pad(x, y) with the corresponding Display Type
        /// </summary>
        /// <param name="point">Coordinate of the Pad</param>
        /// <param name="color">Color of the Pad</param>
        /// <param name="displayType">Type of color display</param>
        public void SetPadColor(PadCoordinate point, LaunchpadColorValue color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
            if (point.x < 0 || point.x > 8 || point.y < 0 || point.y > 8) return;
            padColors[point.x, point.y] = color;
            if (color.padColor == 0 && (color.red != 0 || color.green != 0 || color.blue != 0))
                displayType = PadColorDisplayType.RGBColor;
            padDisplayType[point.x, point.y] = displayType;
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
        /// <param name="flashingColor">Alternating Flashing Color of the Pad</param>
        public void DrawLine(int xA, int yA, int xB, int yB, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor, LaunchpadColor flashingColor = LaunchpadColor.Black)
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
                SetPadColor((byte)MathF.Round(x), (byte)MathF.Round(y), color, displayType, flashingColor);
                x += dx;
                y += dy;
            }

            SetPadColor(xB, yB, color, displayType, flashingColor);
        }


        /// <summary>
        /// Draws a line from the PadCoordinate A to PadCoordinate B with the corresponding Display Type
        /// </summary>
        /// <param name="xA">X value of the Start Pad</param>
        /// <param name="yA">Y value of the Start Pad</param>
        /// <param name="xB">X value of the End Pad</param>
        /// <param name="yB">Y value of the End Pad</param>
        /// <param name="color">Color of the Pad</param>
        /// <param name="displayType">Type of the color display</param>
        public void DrawLine(int xA, int yA, int xB, int yB, LaunchpadColorValue color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
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
        /// <param name="flashingColor">Alternating Flashing Color of the Pad</param>
        public void DrawLine(PadCoordinate pointA, PadCoordinate pointB, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor, LaunchpadColor flashingColor = LaunchpadColor.Black)
        {
            float x = pointA.x;
            float y = pointA.y;
            float dx = Math.Abs(pointB.x - pointA.x);
            float dy = Math.Abs(pointB.y - pointA.y);
            float step = dx >= dy ? dx : dy;

            dx = (pointB.x - pointA.x) / step;
            dy = (pointB.y - pointA.y) / step;

            for (int i = 1; i <= step; ++i)
            {
                SetPadColor((byte)MathF.Round(x), (byte)MathF.Round(y), color, displayType, flashingColor);
                x += dx;
                y += dy;
            }

            SetPadColor(pointB.x, pointB.y, color, displayType, flashingColor);
        }


        /// <summary>
        /// Draws a line from the PadCoordinate A to PadCoordinate B with the corresponding Display Type
        /// </summary>
        /// <param name="pointA">Coordinate of the Start Pad</param>
        /// <param name="pointB">Coordinate of the End Pad</param>
        /// <param name="color">Color of the Pad</param>
        /// <param name="displayType">Type of the color display</param>
        public void DrawLine(PadCoordinate pointA, PadCoordinate pointB, LaunchpadColorValue color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
            float x = pointA.x;
            float y = pointA.y;
            float dx = Math.Abs(pointB.x - pointA.x);
            float dy = Math.Abs(pointB.y - pointA.y);
            float step = dx >= dy ? dx : dy;

            dx = (pointB.x - pointA.x) / step;
            dy = (pointB.y - pointA.y) / step;

            for (int i = 1; i <= step; ++i)
            {
                SetPadColor((byte)MathF.Round(x), (byte)MathF.Round(y), color, displayType);
                x += dx;
                y += dy;
            }

            SetPadColor(pointB.x, pointB.y, color, displayType);
        }


        /// <summary>
        /// Draws a line from the PadCoordinate A to PadCoordinate B and interpolates the color from A to B
        /// </summary>
        /// <param name="xA">X value of the Start Pad</param>
        /// <param name="yA">Y value of the Start Pad</param>
        /// <param name="xB">X value of the End Pad</param>
        /// <param name="yB">Y value of the End Pad</param>
        /// <param name="colorPointA">Color of the Pad</param>
        /// <param name="colorPointB">Color of the Pad</param>
        /// <param name="numberOfPixels">Override number of pixels for color interpolation</param>
        public void DrawLineInterpolated(int xA, int yA, int xB, int yB, LaunchpadColorValue colorPointA, LaunchpadColorValue colorPointB, int numberOfPixels = -1)
        {
            List<PadCoordinate> linePoints = new List<PadCoordinate>();

            float x = xA;
            float y = yA;
            float dx = Math.Abs(xB - xA);
            float dy = Math.Abs(yB - yA);
            float step = dx >= dy ? dx : dy;

            dx = (xB - xA) / step;
            dy = (yB - yA) / step;

            for (int i = 1; i <= step; ++i)
            {
                linePoints.Add(new PadCoordinate((int)MathF.Round(x), (int)MathF.Round(y)));
                x += dx;
                y += dy;
            }

            linePoints.Add(new PadCoordinate(xB, yB));

            if (numberOfPixels == -1) numberOfPixels = linePoints.Count - 1;

            float k = 0;
            Console.WriteLine(numberOfPixels);
            Console.WriteLine(colorPointA.red);
            Console.WriteLine(colorPointA.green);
            Console.WriteLine(colorPointA.blue);
            LaunchpadColorValue interpolColor = new LaunchpadColorValue(0, LaunchpadColor.Black);
            foreach(var coord in linePoints)
            {
                Console.WriteLine(((int)(colorPointA.red * (k / numberOfPixels))).ToString("X") + ", " + ((int)(colorPointA.green * (k / numberOfPixels))).ToString("X") + ", " + ((int)(colorPointA.blue * (k / numberOfPixels))).ToString("X"));
                interpolColor.red = (byte)((int)(colorPointB.red * (k / numberOfPixels)) | (int)(colorPointA.red * (1 - k / numberOfPixels)));
                interpolColor.green = (byte)((int)(colorPointB.green * (k / numberOfPixels)) | (int)(colorPointA.green * (1 - k / numberOfPixels)));
                interpolColor.blue = (byte)((int)(colorPointB.blue * (k / numberOfPixels)) | (int)(colorPointA.blue * (1 - k / numberOfPixels)));
                if(k < numberOfPixels) k++;

                SetPadColor(coord, interpolColor, PadColorDisplayType.RGBColor);
            }
        }


        /// <summary>
        /// Draws a line from the PadCoordinate A to PadCoordinate B and interpolates the color from A to B
        /// </summary>
        /// <param name="pointA">Coordinate of the Start Pad</param>
        /// <param name="pointB">Coordinate of the End Pad</param>
        /// <param name="colorPointA">Color of the Pad</param>
        /// <param name="colorPointB">Color of the Pad</param>
        /// <param name="numberOfPixels">Override number of pixels for color interpolation</param>
        public void DrawLineInterpolated(PadCoordinate pointA, PadCoordinate pointB, LaunchpadColorValue colorPointA, LaunchpadColorValue colorPointB, int numberOfPixels = -1)
        {
            List<PadCoordinate> linePoints = new List<PadCoordinate>();

            float x = pointA.x;
            float y = pointA.y;
            float dx = Math.Abs(pointB.x - pointA.x);
            float dy = Math.Abs(pointB.y - pointA.y);
            float step = dx >= dy ? dx : dy;

            dx = (pointB.x - pointA.x) / step;
            dy = (pointB.y - pointA.y) / step;

            for (int i = 1; i <= step; ++i)
            {
                linePoints.Add(new PadCoordinate((int)MathF.Round(x), (int)MathF.Round(y)));
                x += dx;
                y += dy;
            }

            linePoints.Add(pointB);

            if (numberOfPixels == -1) numberOfPixels = linePoints.Count - 1;

            float k = 0;
            Console.WriteLine(numberOfPixels);
            Console.WriteLine(colorPointA.red);
            Console.WriteLine(colorPointA.green);
            Console.WriteLine(colorPointA.blue);
            LaunchpadColorValue interpolColor = new LaunchpadColorValue(0, LaunchpadColor.Black);
            foreach(var coord in linePoints)
            {
                Console.WriteLine(((int)(colorPointA.red * (k / numberOfPixels))).ToString("X") + ", " + ((int)(colorPointA.green * (k / numberOfPixels))).ToString("X") + ", " + ((int)(colorPointA.blue * (k / numberOfPixels))).ToString("X"));
                interpolColor.red = (byte)((int)(colorPointB.red * (k / numberOfPixels)) | (int)(colorPointA.red * (1 - k / numberOfPixels)));
                interpolColor.green = (byte)((int)(colorPointB.green * (k / numberOfPixels)) | (int)(colorPointA.green * (1 - k / numberOfPixels)));
                interpolColor.blue = (byte)((int)(colorPointB.blue * (k / numberOfPixels)) | (int)(colorPointA.blue * (1 - k / numberOfPixels)));
                if(k < numberOfPixels) k++;

                SetPadColor(coord, interpolColor, PadColorDisplayType.RGBColor);
            }
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
        /// <param name="flashingColor">Alternating Flashing Color of the Pad</param>
        public void DrawTriangle(int xA, int yA, int xB, int yB, int xC, int yC, bool fillTriangle, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor, LaunchpadColor flashingColor = LaunchpadColor.Black)
        {
            if (fillTriangle)
                DrawLineFilled(xA, yA, xB, yB, xC, yC, color, displayType, flashingColor);
            else
                DrawLine(xA, yA, xB, yB, color, displayType, flashingColor);

            DrawLine(xB, yB, xC, yC, color, displayType, flashingColor);
            DrawLine(xC, yC, xA, yA, color, displayType, flashingColor);
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
        public void DrawTriangle(int xA, int yA, int xB, int yB, int xC, int yC, bool fillTriangle, LaunchpadColorValue color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
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
        /// <param name="flashingColor">Alternating Flashing Color of the Pad</param>
        public void DrawTriangle(PadCoordinate pointA, PadCoordinate pointB, PadCoordinate pointC, bool fillTriangle, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor, LaunchpadColor flashingColor = LaunchpadColor.Black)
        {
            if (fillTriangle)
                DrawLineFilled(pointA.x, pointA.y, pointB.x, pointB.y, pointC.x, pointC.y, color, displayType, flashingColor);
            else
                DrawLine(pointA, pointB, color, displayType, flashingColor);

            DrawLine(pointB, pointC, color, displayType, flashingColor);
            DrawLine(pointC, pointA, color, displayType, flashingColor);
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
        public void DrawTriangle(PadCoordinate pointA, PadCoordinate pointB, PadCoordinate pointC, bool fillTriangle, LaunchpadColorValue color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
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
        /// <param name="flashingColor">Alternating Flashing Color of the Pad</param>
        public void DrawRectangle(int xA, int yA, int xB, int yB, bool fillRectangle, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor, LaunchpadColor flashingColor = LaunchpadColor.Black)
        {
            if (fillRectangle)
            {
                DrawTriangle(xA, yA, xB, yA, xA, yB, true, color, displayType, flashingColor);
                DrawTriangle(xB, yB, xB, yA, xA, yB, true, color, displayType, flashingColor);
            }
            else
            {
                DrawLine(xA, yA, xA, yB, color, displayType, flashingColor);
                DrawLine(xA, yA, xB, yA, color, displayType, flashingColor);
                DrawLine(xA, yB, xB, yB, color, displayType, flashingColor);
                DrawLine(xB, yA, xB, yB, color, displayType, flashingColor);
            }
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
        public void DrawRectangle(int xA, int yA, int xB, int yB, bool fillRectangle, LaunchpadColorValue color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
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
        /// <param name="flashingColor">Alternating Flashing Color of the Pad</param>
        public void DrawRectangle(PadCoordinate pointA, PadCoordinate pointB, bool fillRectangle, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor, LaunchpadColor flashingColor = LaunchpadColor.Black)
        {
            if (fillRectangle)
            {
                DrawTriangle(pointA.x, pointA.y, pointB.x, pointA.y, pointA.x, pointB.y, true, color, displayType, flashingColor);
                DrawTriangle(pointB.x, pointB.y, pointB.x, pointA.y, pointA.x, pointB.y, true, color, displayType, flashingColor);
            }
            else
            {
                DrawLine(pointA.x, pointA.y, pointA.x, pointB.y, color, displayType, flashingColor);
                DrawLine(pointA.x, pointA.y, pointB.x, pointA.y, color, displayType, flashingColor);
                DrawLine(pointA.x, pointB.y, pointB.x, pointB.y, color, displayType, flashingColor);
                DrawLine(pointB.x, pointA.y, pointB.x, pointB.y, color, displayType, flashingColor);
            }
        }


        /// <summary>
        /// Draws a rectangle from PadCoordinate A == bottom left corner to Pad Coordinate B == top right corner with the corresponding Display Type
        /// </summary>
        /// <param name="pointA">Bottom left PadCoordinate</param>
        /// <param name="pointB">Top right PadCoordinate</param>
        /// <param name="fillRectangle">wether the Rectangle should be filled or wireframe</param>
        /// <param name="color">Color of the Pad</param>
        /// <param name="displayType">Type of the color display</param
        public void DrawRectangle(PadCoordinate pointA, PadCoordinate pointB, bool fillRectangle, LaunchpadColorValue color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
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
        /// <param name="flashingColor">Alternating Flashing Color of the Pad</param>
        public void DrawCircle(int x, int y, int radius, bool fillCircle, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor, LaunchpadColor flashingColor = LaunchpadColor.Black)
        {
            int u = 0;
            int v = radius;
            int d = 3 - 2 * radius;

            SetPadColor(x + u, y + v, color, displayType, flashingColor);
            SetPadColor(x - u, y + v, color, displayType, flashingColor);
            SetPadColor(x + u, y - v, color, displayType, flashingColor);
            SetPadColor(x - u, y - v, color, displayType, flashingColor);
            SetPadColor(x + v, y + u, color, displayType, flashingColor);
            SetPadColor(x - v, y + u, color, displayType, flashingColor);
            SetPadColor(x + v, y - u, color, displayType, flashingColor);
            SetPadColor(x - v, y - u, color, displayType, flashingColor);

            while (v >= u)
            {
                u++;
                if (d > 0)
                {
                    v--;
                    d += 4 * (u - v) + 10;
                }
                else
                {
                    d += 4 * u + 6;
                }

                SetPadColor(x + u, y + v, color, displayType, flashingColor);
                SetPadColor(x - u, y + v, color, displayType, flashingColor);
                SetPadColor(x + u, y - v, color, displayType, flashingColor);
                SetPadColor(x - u, y - v, color, displayType, flashingColor);
                SetPadColor(x + v, y + u, color, displayType, flashingColor);
                SetPadColor(x - v, y + u, color, displayType, flashingColor);
                SetPadColor(x + v, y - u, color, displayType, flashingColor);
                SetPadColor(x - v, y - u, color, displayType, flashingColor);
            }

            if (fillCircle)
                for (int i = radius - 1; i >= 0; i--)
                    DrawCircle(x, y, i, false, color, displayType, flashingColor);
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
        public void DrawCircle(int x, int y, int radius, bool fillCircle, LaunchpadColorValue color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
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
                if (d > 0)
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

            if (fillCircle)
                for (int i = radius - 1; i >= 0; i--)
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
        /// <param name="displayType">Type of the color display</param>
        /// <param name="flashingColor">Alternating Flashing Color of the Pad</param>
        public void DrawCircle(PadCoordinate centre, int radius, bool fillCircle, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor, LaunchpadColor flashingColor = LaunchpadColor.Black)
        {
            int u = 0;
            int v = radius;
            int d = 3 - 2 * radius;

            SetPadColor(centre.x + u, centre.y + v, color, displayType, flashingColor);
            SetPadColor(centre.x - u, centre.y + v, color, displayType, flashingColor);
            SetPadColor(centre.x + u, centre.y - v, color, displayType, flashingColor);
            SetPadColor(centre.x - u, centre.y - v, color, displayType, flashingColor);
            SetPadColor(centre.x + v, centre.y + u, color, displayType, flashingColor);
            SetPadColor(centre.x - v, centre.y + u, color, displayType, flashingColor);
            SetPadColor(centre.x + v, centre.y - u, color, displayType, flashingColor);
            SetPadColor(centre.x - v, centre.y - u, color, displayType, flashingColor);

            while (v >= u)
            {
                u++;
                if (d > 0)
                {
                    v--;
                    d += 4 * (u - v) + 10;
                }
                else
                {
                    d += 4 * u + 6;
                }

                SetPadColor(centre.x + u, centre.y + v, color, displayType, flashingColor);
                SetPadColor(centre.x - u, centre.y + v, color, displayType, flashingColor);
                SetPadColor(centre.x + u, centre.y - v, color, displayType, flashingColor);
                SetPadColor(centre.x - u, centre.y - v, color, displayType, flashingColor);
                SetPadColor(centre.x + v, centre.y + u, color, displayType, flashingColor);
                SetPadColor(centre.x - v, centre.y + u, color, displayType, flashingColor);
                SetPadColor(centre.x + v, centre.y - u, color, displayType, flashingColor);
                SetPadColor(centre.x - v, centre.y - u, color, displayType, flashingColor);
            }

            if (fillCircle)
                for (int i = radius - 1; i >= 0; i--)
                    DrawCircle(centre.x, centre.y, i, false, color, displayType, flashingColor);
        }


        /// <summary>
        /// Draws a circle with Pad(x, y) at the centre with the given radius and with the corresponding Display Type
        /// </summary>
        /// <param name="centre">Padcoordinate of the centre</param>
        /// <param name="radius"> radius of the circle</param>
        /// <param name="fillCircle">wether the Circle should be filled or wireframe</param>
        /// <param name="color">Color of the Pad</param>
        /// <param name="displayType">Type of the color display</param>
        public void DrawCircle(PadCoordinate centre, int radius, bool fillCircle, LaunchpadColorValue color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
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
                if (d > 0)
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

            if (fillCircle)
                for (int i = radius - 1; i >= 0; i--)
                    DrawCircle(centre.x, centre.y, i, false, color, displayType);
        }


        /// <summary>
        /// Draws a Polygon from the given points with the given color and corresponding Display Type
        /// </summary>
        /// <param name="points">Corners of the Polygon</param>
        /// <param name="color">Color of the Pad</param>
        /// <param name="displayType">Type of the color display</param>
        /// <param name="flashingColor">Alternating Flashing Color of the Pad</param>
        public void DrawPolygon(PadCoordinate[] points, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor, LaunchpadColor flashingColor = LaunchpadColor.Black)
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                DrawLine(points[i], points[i + 1], color, displayType, flashingColor);
            }

            DrawLine(points[0], points[points.Length - 1], color, displayType, flashingColor);
        }


        /// <summary>
        /// Draws a Polygon from the given points with the given color and corresponding Display Type
        /// </summary>
        /// <param name="points">Corners of the Polygon</param>
        /// <param name="color">Color of the Pad</param>
        /// <param name="displayType">Type of the color display</param>
        public void DrawPolygon(PadCoordinate[] points, LaunchpadColorValue color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
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
        /// <param name="flashingColor">Alternating Flashing Color of the Pad</param>
        public void DrawHeart(LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor, LaunchpadColor flashingColor = LaunchpadColor.Black)
        {
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
                color, displayType, flashingColor);
        }


        /// <summary>
        /// Draws a Heart on the Centre of the Launchpad with the given color and corresponding Display Type
        /// </summary>
        /// <param name="color"></param>
        /// <param name="displayType"></param>
        public void DrawHeart(LaunchpadColorValue color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
        {
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
        /// Translates all currently set pixels of the frame by the given offset
        /// </summary>
        /// <param name="offset">Offset of the frame</param>
        /// <param name="keepPrevious">Copies, translates, overrides pixels</param>
        /// <param name="wrapHorizontal">Pixels moved out the sides are moved in the opposite side </param>
        /// <param name="wrapVertical">Pixels moved out the top or bottom are movedn in the opposite side</param>
        public void TranslateFrame(PadCoordinate offset, bool keepPrevious = false, bool wrapHorizontal = false, bool wrapVertical = false)
        {
            LaunchpadColorValue[, ] colorCopy = padColors.Clone() as LaunchpadColorValue[, ];
            PadColorDisplayType[, ] displayTypeCopy = padDisplayType.Clone() as PadColorDisplayType[, ];

            int newX, newY;

            if (!keepPrevious)
            {
                padColors = new LaunchpadColorValue[9, 9];
                padDisplayType = new PadColorDisplayType[9, 9];
            }

            for(int x = 0; x < 9; x++)
            {
                for(int y = 0; y < 9; y++)
                {
                    newX = x + offset.x;
                    newY = y + offset.y;

                    if (wrapHorizontal) newX = (newX + 9) % 9;
                    if (wrapVertical)   newY = (newY + 9) % 9;

                    if (newX < 0 || newX > 8 || newY < 0 || newY > 8) continue;

                    padColors[newX, newY] = colorCopy[x, y];
                    padDisplayType[newX, newY] = displayTypeCopy[x, y];
                }
            }
        }
        
        
        /// <summary>
        /// Replaces every apperance of the color with the new one
        /// </summary>
        /// <param name="original">reference color</param>
        /// <param name="replacement">override color</param>
        public void ReplaceColor(LaunchpadColor original, LaunchpadColor replacement)
        {
            for(int x = 0; x < 9; x++)
            {
                for(int y = 0; y < 9; y++)
                {
                    if(padColors[x, y].padColor == original)
                    {
                        padColors[x, y] = LaunchpadColorValueLib.Instance.launchpadColorValueLib.Find(x => x.padColor == replacement);
                    }
                }
            }
        }


        /// <summary>
        /// Replaces every apperance of the color with the new one
        /// </summary>
        /// <param name="original">reference color</param>
        /// <param name="replacement">override color</param>
        public void ReplaceColor(LaunchpadColorValue original, LaunchpadColorValue replacement)
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (padColors[x, y].hexColorValue == original.hexColorValue)
                    {
                        padColors[x, y] = replacement;
                    }
                }
            }
        }



        private void DrawLineFilled(int xA, int yA, int xB, int yB, int xC, int yC, LaunchpadColor color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor, LaunchpadColor flashingColor = LaunchpadColor.Black)
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
                SetPadColor((byte)MathF.Round(x), (byte)MathF.Round(y), color, displayType, flashingColor);
                DrawLine((byte)MathF.Round(x), (byte)MathF.Round(y), xC, yC, color, displayType, flashingColor);
                x += dx;
                y += dy;
            }

            SetPadColor(xB, yB, color, displayType, flashingColor);
            DrawLine((byte)MathF.Round(x), (byte)MathF.Round(y), xC, yC, color, displayType, flashingColor);
        }
        private void DrawLineFilled(int xA, int yA, int xB, int yB, int xC, int yC, LaunchpadColorValue color, PadColorDisplayType displayType = PadColorDisplayType.SolidColor)
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

        #endregion
    }
}
