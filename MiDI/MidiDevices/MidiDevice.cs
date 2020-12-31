using System;
using System.Text;
using System.Runtime.InteropServices;
using MiDI.MidiWinAPI;


namespace MiDI.MidiDevices
{
    public abstract class MidiDevice
    {
        #region Values

        public string Name { get; protected set; }
        public ushort ProductIdentifier { get; protected set; }
        public string DriverVersion {get; protected set;}


        internal IntPtr winHandle;
        internal int uWinDeviceID;
        internal MidiWinAPI.MidiWinAPI.MIDIHDR midiHeader;
        internal IntPtr sysExHeaderPointer;
        internal MidiWinAPI.MidiWinAPI.MidiMessageCallback midiMessageCallback;

        #endregion



        #region Methods

        protected void SetBasicInfos(string name, ushort productIdentifier, uint driverVersion)
        {
            Name = name;
            ProductIdentifier = productIdentifier;
            DriverVersion = (driverVersion >> 8) + "." + (driverVersion & 0xFF); 
        }


        internal void ProcessWinApiCallResult(uint result)
        {
            if (result == MidiWinAPI.MidiWinAPI.MMSYSERR_NOERROR)
                return;

            var stringBuilder = new StringBuilder((int)MidiWinAPI.MidiWinAPI.MaxErrorLength);
            var getErrorTextResult = GetErrorText(result, stringBuilder, MidiWinAPI.MidiWinAPI.MaxErrorLength + 1);
            if (getErrorTextResult != MidiWinAPI.MidiWinAPI.MMSYSERR_NOERROR)
                throw new Exception("Error during Device operation");

            var errorText = stringBuilder.ToString();
            throw new Exception(errorText);
        }


        internal IntPtr GetWinHandle() { return winHandle; }


        public abstract bool StartCommunication();
        public abstract void EndCommunication();
        internal abstract uint GetErrorText(uint winAPIError, StringBuilder pszText, uint cchText);
        internal abstract void midiMessageCallbackFunction(IntPtr hMidi, MidiMessage wMsg, IntPtr dwInstance, IntPtr dwParam1, IntPtr dwParam2);



        public virtual string GetDebugInformation()
        {
            return "Name: " + Name + ", Identifier: " + ProductIdentifier + ", Driver Version: " + DriverVersion;
        }


        ~MidiDevice()
        {
            Marshal.FreeHGlobal(midiHeader.lpData);
        }

        #endregion
    }
}
