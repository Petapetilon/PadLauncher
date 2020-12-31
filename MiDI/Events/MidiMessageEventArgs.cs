using System;
using System.Collections.Generic;
using System.Text;


namespace MiDI.Events
{
    /// <summary>
    /// Standard Midi Message Event Args
    /// </summary>
    public sealed class MidiMessageEventArgs : EventArgs
    {
        public MidiEventType midiMessageStatus;
        public byte data1;
        public byte data2;

        public MidiMessageEventArgs(MidiEventType _midiMessageStatus, byte _data1, byte _data2)
        {
            midiMessageStatus = _midiMessageStatus;
            data1 = _data1;
            data2 = _data2;
        }

        public MidiMessageEventArgs () { }
    }
}
