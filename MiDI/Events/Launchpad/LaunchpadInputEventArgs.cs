using System;
using System.Collections.Generic;
using System.Text;
using MiDI.MidiDevices.Launchpad;


namespace MiDI.Events.Launchpad
{
    public class LaunchpadInputEventArgs : EventArgs
    {
        public int x;
        public int y;
        public byte padID;
        public byte velocity;

        public LaunchpadInputEventArgs(int _x, int _y, byte _padID, byte _velocity)
        {
            x = _x;
            y = _y;
            padID = _padID;
            velocity = _velocity;
        }

        public LaunchpadInputEventArgs(PadCoordinate point, byte _padID, byte _velocity)
        {
            x = point.x;
            y = point.y;
            padID = _padID;
            velocity = _velocity;
        }
    }
}
