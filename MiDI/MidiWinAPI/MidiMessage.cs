namespace MiDI.MidiWinAPI
{
    internal enum MidiMessage : int
    {
        MIN_CLOSE = 962,
        MIN_DATA = 963,
        MIN_ERROR = 965,
        MIN_LONGDATA = 964,
        MIN_LONGERROR = 966,
        MIN_MOREDATA = 972,
        MIN_OPEN = 961,
        MOUT_CLOSE = 968,
        MOUT_DONE = 969,
        MOUT_OPEN = 967,
        MOUT_POSITIONCB = 970
    }
}
