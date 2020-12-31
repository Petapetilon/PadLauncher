using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using MiDI.MidiWinAPI;
using MiDI.Events;


namespace MiDI.MidiDevices
{
    /// <summary>
    /// Midi Device Object that handles the communcation with a Midi In Device
    /// </summary>
    public sealed class MidiInDevice : MidiDevice
    {
        #region Values

        public MidiMessageEventArgs lastMidiMessageEventValues { get; private set; }
        public SysExMessageEventArgs lastSysExMessageEventValues { get; private set; }
        public event EventHandler<MidiMessageEventArgs> OnMidiInMessageReceived;
        public event EventHandler<SysExMessageEventArgs> OnSysExInMessageReceived;

        #endregion



        #region Methods

        private MidiInDevice(int deviceID, MidiInWinAPI.MIDIINCAPS deviceCapabilities)
        {
            uWinDeviceID = deviceID;
            winHandle = IntPtr.Zero;
            SetBasicInfos(deviceCapabilities.szPname, deviceCapabilities.wPid, deviceCapabilities.vDriverVersion);
            lastMidiMessageEventValues = new MidiMessageEventArgs();
            lastSysExMessageEventValues = new SysExMessageEventArgs();
        }
        ~MidiInDevice()
        {
            EndCommunication();
        }


        /// <summary>
        /// Returns a Device with the given name
        /// </summary>
        /// <param name="name">Name of the Device</param>
        /// <returns>MidiInDevice with given name</returns>
        public static MidiInDevice GetDeviceByName(string name)
        {
            var capabilities = default(MidiInWinAPI.MIDIINCAPS);
            for(int i = 0; i < MidiInWinAPI.midiInGetNumDevs(); i++)
                if(MidiInWinAPI.midiInGetDevCaps(new IntPtr(i), ref capabilities, (uint)Marshal.SizeOf(capabilities)) == MidiWinAPI.MidiWinAPI.MMSYSERR_NOERROR)
                    if(capabilities.szPname == name)
                        return new MidiInDevice(i, capabilities);

            return null;
        }


        /// <summary>
        /// Returns a Device with he given ID
        /// </summary>
        /// <param name="ID">ID of the Device (ID == Index of the Name of the Device in the Device List)</param>
        /// <returns>MidiInDevice with the given ID</returns>
        public static MidiInDevice GetDeviceByID(uint ID)
        {
            var capabilities = default(MidiInWinAPI.MIDIINCAPS);
            if(MidiInWinAPI.midiInGetDevCaps(new IntPtr(ID), ref capabilities, (uint)Marshal.SizeOf(capabilities)) == MidiWinAPI.MidiWinAPI.MMSYSERR_NOERROR)
                return new MidiInDevice((int)ID, capabilities);

            return null;
        }


        /// <summary>
        /// Gets all available Devices and returns their names
        /// </summary>
        /// <returns>Names of the Devices</returns>
        public static string[] GetAllInDeviceNames()
        {
            List<string> inDevices = new List<string>();
            var capabilities = default(MidiInWinAPI.MIDIINCAPS);
            for (uint id = 0; id < MidiInWinAPI.midiInGetNumDevs(); id++)
            {
                if (MidiInWinAPI.midiInGetDevCaps(new IntPtr(id), ref capabilities, (uint)Marshal.SizeOf(capabilities)) == MidiWinAPI.MidiWinAPI.MMSYSERR_NOERROR)
                {
                    inDevices.Add(capabilities.szPname);
                }
            }

            return inDevices.ToArray();
        }


        /// <summary>
        /// Starts Communcation with this Device
        /// </summary>
        /// <returns>success</returns>
        public override bool StartCommunication()
        {
            try
            {
                midiMessageCallback = midiMessageCallbackFunction;
                midiHeader = new MidiWinAPI.MidiWinAPI.MIDIHDR { lpData = Marshal.AllocHGlobal(2048), dwBufferLength = 2048, dwBytesRecorded = 2048 };
                sysExHeaderPointer = Marshal.AllocHGlobal(MidiWinAPI.MidiWinAPI.MidiHeaderSize);
                Marshal.StructureToPtr(midiHeader, sysExHeaderPointer, false);
                ProcessWinApiCallResult(MidiInWinAPI.midiInOpen(out winHandle, uWinDeviceID, midiMessageCallback, IntPtr.Zero, MidiWinAPI.MidiWinAPI.CallbackFunction));
                ProcessWinApiCallResult(MidiInWinAPI.midiInPrepareHeader(winHandle, sysExHeaderPointer, MidiWinAPI.MidiWinAPI.MidiHeaderSize));
                ProcessWinApiCallResult(MidiInWinAPI.midiInAddBuffer(winHandle, sysExHeaderPointer, MidiWinAPI.MidiWinAPI.MidiHeaderSize));
                ProcessWinApiCallResult(MidiInWinAPI.midiInStart(winHandle));
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }


        /// <summary>
        /// Ends Communication with this Device
        /// </summary>
        public override void EndCommunication()
        {
            try
            {
                ProcessWinApiCallResult(MidiInWinAPI.midiInStop(winHandle));
                Marshal.FreeHGlobal(midiHeader.lpData);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }


        internal override uint GetErrorText(uint winAPIError, StringBuilder pszText, uint cchText)
        {
            return MidiInWinAPI.midiInGetErrorText(winAPIError, pszText, cchText);
        }


        /// <summary>
        /// Function that invokes the corresponding Event when a Message is received
        /// </summary>
        internal override void midiMessageCallbackFunction(IntPtr hMidi, MidiMessage wMsg, IntPtr dwInstance, IntPtr dwParam1, IntPtr dwParam2)
        {
            if(wMsg == MidiMessage.MIN_DATA)
            {
                MidiWinAPI.MidiWinAPI.UnpackShortMessageEventBytes(dwParam1.ToInt32(), out var midiMessageStatus, out var data1, out var data2);
                //Console.WriteLine("message received from " + Name + ":    Status:" + ((short)midiMessageStatus).ToString("X2") + ", Data 1: " + ((short)data1).ToString("X2") + ", Data 2: " + ((short)data2).ToString("X2"));

                lastMidiMessageEventValues.midiMessageStatus = (MidiEventType)midiMessageStatus;
                lastMidiMessageEventValues.data1 = data1;
                lastMidiMessageEventValues.data2 = data2;
                OnMidiInMessageReceived?.Invoke(this, lastMidiMessageEventValues);
            }
            else if(wMsg == MidiMessage.MIN_LONGDATA)
            {
                GCHandle pinnedArray = GCHandle.Alloc(lastSysExMessageEventValues.data, GCHandleType.Pinned);
                IntPtr dataAddress = pinnedArray.AddrOfPinnedObject();
                MidiWinAPI.MidiWinAPI.memcpy(dataAddress, midiHeader.lpData, new UIntPtr(2048));
                pinnedArray.Free();
                OnSysExInMessageReceived?.Invoke(this, lastSysExMessageEventValues);
            }
        }

        #endregion
    }
}
