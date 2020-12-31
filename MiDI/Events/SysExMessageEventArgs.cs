using System;
using System.Collections.Generic;
using System.Text;
using MiDI.MidiWinAPI;

namespace MiDI.Events
{
    /// <summary>
    /// System Exclusive Midi Message Event Args
    /// </summary>
    public sealed class SysExMessageEventArgs : EventArgs
    {
        public byte[] data = new byte[2048];
    }
}
