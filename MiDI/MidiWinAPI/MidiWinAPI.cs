using System;
using System.Runtime.InteropServices;


namespace MiDI.MidiWinAPI
{
    internal static class MidiWinAPI
    {
        #region Types

        [StructLayout(LayoutKind.Sequential)]
        internal struct MIDIHDR
        {
            public IntPtr lpData;
            public int dwBufferLength;
            public int dwBytesRecorded;
            public IntPtr dwUser;
            public int dwFlags;
            public IntPtr lpNext;
            public IntPtr reserved;
            public int dwOffset;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public int[] dwReserved;
        }

        public delegate void MidiMessageCallback(IntPtr hMidi, MidiMessage wMsg, IntPtr dwInstance, IntPtr dwParam1, IntPtr dwParam2);

        #endregion

        #region Constants

        public const uint MaxErrorLength = 256;
        public const uint CallbackFunction = 196608;

        public static readonly int MidiHeaderSize = Marshal.SizeOf(typeof(MIDIHDR));

        public const uint MMSYSERR_NOERROR = 0;
        public const uint MMSYSERR_ERROR = 1;
        public const uint MMSYSERR_INVALHANDLE = 5;

        public const uint MIDIERR_NOTREADY = 67;

        public const uint TIMERR_NOCANDO = 97;

        #endregion

        #region Methods


        [DllImport("winmm.dll", ExactSpelling = true)]
        public static extern uint midiConnect(IntPtr midiInHandle, IntPtr midiOutHandle, IntPtr pRseverd); 
        
        
        [DllImport("winmm.dll", ExactSpelling = true)]
        public static extern uint midiDisconnect(IntPtr midiInHandle, IntPtr midiOutHandle, IntPtr pRseverd);


        [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern IntPtr memcpy(IntPtr dest, IntPtr src, UIntPtr count);


        public static byte[] UnpackSysExBytes(IntPtr headerPointer)
        {
            var header = (MIDIHDR)Marshal.PtrToStructure(headerPointer, typeof(MIDIHDR));
            var data = new byte[header.dwBytesRecorded - 1];
            Marshal.Copy(IntPtr.Add(header.lpData, 1), data, 0, data.Length);

            return data;
        }


        public static void UnpackShortMessageEventBytes(int message, out byte midiMessageStatus, out byte data1, out byte data2)
        {
            //statusByte = message.GetFourthByte();
            //firstDataByte = message.GetThirdByte();
            //secondDataByte = message.GetSecondByte();
            midiMessageStatus = 0;
            data1 = 0;
            data2 = 0;


            if (message == 0) return;
            midiMessageStatus = (byte)(message);
            data1 = (byte)((message >> 8) & 0xFF);
            data2 = (byte)((message >> 16) & 0xFF);
        }


        public static uint PackShortEventBytes(byte midiMessageStatus, byte data1, byte data2)
        {
            return ((uint)midiMessageStatus) | (((uint)data1) << 8) | (((uint)data2) << 16);
        }

        #endregion
    }
}

