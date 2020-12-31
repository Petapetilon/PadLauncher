using System;
using System.Collections.Generic;
using System.Text;

namespace MiDI.Events
{
    /// <summary>
    /// Enum for easier usage of the Midi Message Status
    /// </summary>
    public enum MidiEventType
    {
        NoteOff = 0x80,
        NoteOn = 0x90,
        PolyphonicAftertouch = 0xA0,
        ControlChange = 0xB0,
        ProgramChange = 0xC0,
        ChannelAftertouch = 0xD0,
        PitchWheel = 0xE0
    }
}
