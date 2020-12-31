using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using MiDI.MidiWinAPI;
using MiDI.Events;


namespace MiDI.MidiDevices
{
    /// <summary>
    /// Midi Device Object that handles the communcation with a Midi Out Device
    /// </summary>
    public sealed class MidiOutDevice : MidiDevice
    {
        #region Values

        ///// <summary>
        ///// Midi Message Event Args most recently broadcasted by the Event
        ///// </summary>
        //public MidiMessageEventArgs lastMidiMessageEventValues { get; private set; }
        //
        ///// <summary>
        ///// SysEx Message Event Args most recently broadcasted by the Event
        ///// </summary>
        //public SysExMessageEventArgs lastSysExMessageEventValues { get; private set; }
        //
        ///// <summary>
        ///// Event that will be invoked when a Midi Out Message is received (
        ///// </summary>
        //public event EventHandler<MidiMessageEventArgs> OnMidiOutMessageReceived;
        //public event EventHandler<SysExMessageEventArgs> OnSysExOutMessageReceived;

        private ushort midiDeviceTechnology;
        private ushort midiDeviceVoices;
        private ushort midiDeviceNotes;
        private ushort midiDeviceChannelMask;

        #endregion

        #region Functions

        private MidiOutDevice(int deviceID, MidiOutWinAPI.MIDIOUTCAPS deviceCapabilities)
        {
            uWinDeviceID = deviceID;
            winHandle = IntPtr.Zero;
            SetBasicInfos(deviceCapabilities.szPname, deviceCapabilities.wPid, deviceCapabilities.vDriverVersion);
            //lastMidiMessageEventValues = new MidiMessageEventArgs();
            //lastSysExMessageEventValues = new SysExMessageEventArgs();
            midiDeviceTechnology = deviceCapabilities.wTechnology;
            midiDeviceVoices = deviceCapabilities.wVoices;
            midiDeviceNotes = deviceCapabilities.wNotes;
            midiDeviceChannelMask = deviceCapabilities.wChannelMask;
        }
        ~MidiOutDevice()
        {
            EndCommunication();
        }


        /// <summary>
        /// Returns a Device with the given name
        /// </summary>
        /// <param name="name">Name of the Device</param>
        /// <returns>MidiOutDevice with given name</returns>
        public static MidiOutDevice GetDeviceByName(string name)
        {
            var capabilities = default(MidiOutWinAPI.MIDIOUTCAPS);
            for (int i = 0; i < MidiInWinAPI.midiInGetNumDevs(); i++)
                if (MidiOutWinAPI.midiOutGetDevCaps(new IntPtr(i), ref capabilities, (uint)Marshal.SizeOf(capabilities)) == MidiWinAPI.MidiWinAPI.MMSYSERR_NOERROR)
                    if (capabilities.szPname == name)
                        return new MidiOutDevice(i, capabilities);

            return null;
        }


        /// <summary>
        /// Returns a Device with he given ID
        /// </summary>
        /// <param name="ID">ID of the Device (ID == Index of the Name of the Device in the Device List)</param>
        /// <returns>MidiOutDevice with the given ID</returns>
        public static MidiOutDevice GetDeviceByID(uint ID)
        {
            var capabilities = default(MidiOutWinAPI.MIDIOUTCAPS);
            if (MidiOutWinAPI.midiOutGetDevCaps(new IntPtr(ID), ref capabilities, (uint)Marshal.SizeOf(capabilities)) == MidiWinAPI.MidiWinAPI.MMSYSERR_NOERROR)
                return new MidiOutDevice((int)ID, capabilities);

            return null;
        }


        /// <summary>
        /// Gets all available Devices and returns their names
        /// </summary>
        /// <returns>Names of the Devices</returns>
        public static string[] GetAllOutDeviceNames()
        {
            List<string> outDevices = new List<string>();
            var capabilities = default(MidiOutWinAPI.MIDIOUTCAPS);
            for (uint id = 0; id < MidiOutWinAPI.midiOutGetNumDevs(); id++)
            {
                if (MidiOutWinAPI.midiOutGetDevCaps(new IntPtr(id), ref capabilities, (uint)Marshal.SizeOf(capabilities)) == MidiWinAPI.MidiWinAPI.MMSYSERR_NOERROR)
                {
                    outDevices.Add(capabilities.szPname);
                }
            }

            return outDevices.ToArray();
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
                ProcessWinApiCallResult(MidiOutWinAPI.midiOutOpen(out winHandle, uWinDeviceID, midiMessageCallback, IntPtr.Zero, MidiWinAPI.MidiWinAPI.CallbackFunction));
                ProcessWinApiCallResult(MidiOutWinAPI.midiOutPrepareHeader(winHandle, sysExHeaderPointer, MidiWinAPI.MidiWinAPI.MidiHeaderSize));
                return true;
            }
            catch (Exception e)
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
                ProcessWinApiCallResult(MidiOutWinAPI.midiOutClose(winHandle));
                Marshal.FreeHGlobal(midiHeader.lpData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        /// <summary>
        /// Sends a Midi Message to the Device
        /// </summary>
        /// <param name="midiEventType">Midi Message Status Byte</param>
        /// <param name="data1">Data 1 Byte</param>
        /// <param name="data2">Data 2 Byte</param>
        public void SendMidiMessage(byte midiEventType, byte data1, byte data2)
        {
            ProcessWinApiCallResult(MidiOutWinAPI.midiOutShortMsg(winHandle, MidiWinAPI.MidiWinAPI.PackShortEventBytes(midiEventType, data1, data2)));
        }


        /// <summary>
        /// Sends a SysExMessage to the Device
        /// </summary>
        /// <param name="data">Data of the Message</param>
        public void SendSysExMessage(byte[] data)
        {
            byte[] tempData = data;
            if(data.Length != 2048)
            {
                tempData = new byte[2048];
                data.CopyTo(tempData, 0);
            }

            GCHandle pinnedArray = GCHandle.Alloc(tempData, GCHandleType.Pinned);
            IntPtr dataAddress = pinnedArray.AddrOfPinnedObject();
            MidiWinAPI.MidiWinAPI.memcpy(midiHeader.lpData, dataAddress, new UIntPtr(2048));
            pinnedArray.Free();
            MidiOutWinAPI.midiOutLongMsg(winHandle, sysExHeaderPointer, Marshal.SizeOf(midiHeader));
        }


        internal override uint GetErrorText(uint winAPIError, StringBuilder pszText, uint cchText)
        {
            return MidiOutWinAPI.midiOutGetErrorText(winAPIError, pszText, cchText);
        }


        internal override void midiMessageCallbackFunction(IntPtr hMidi, MidiMessage wMsg, IntPtr dwInstance, IntPtr dwParam1, IntPtr dwParam2)
        {
           
        }


        /// <summary>
        /// Returns a string with Device Information
        /// </summary>
        /// <returns>string with Device Information</returns>
        public override string GetDebugInformation()
        {
            return base.GetDebugInformation() + ", Technology: " + midiDeviceTechnology + ", Voices" + midiDeviceVoices + ", Notes: " + midiDeviceNotes + ", Channel Mask" + midiDeviceChannelMask;
        }

        #endregion
    }
}
