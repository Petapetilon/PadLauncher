using System;
using System.Collections.Generic;
using System.Text;
using MiDI.MidiWinAPI;
using MiDI.Events;


namespace MiDI.MidiDevices
{
    /// <summary>
    /// Combined Midi Device for ease of use with Devices that support In and Out Communication
    /// </summary>
    public class MidiIODevice
    {
        #region Values

        /// <summary>
        /// Event that will be invoked when a Midi In Message is received
        /// </summary>
        public event EventHandler<MidiMessageEventArgs> OnMidiInMessageReceived;
        
        /// <summary>
        /// Event that will be invoked when a SysEx Message is received
        /// </summary>
        public event EventHandler<MidiMessageEventArgs> OnMidiOutMessageReceived;

        /// <summary>
        /// Midi In Device of the IO Device
        /// </summary>
        public MidiInDevice InDevice { get; protected set; }

        /// <summary>
        /// Midi Out Device of the IO Device
        /// </summary>
        public MidiOutDevice OutDevice { get; protected set; }

        protected bool devicesConnected;
        

        public MidiIODevice() { devicesConnected = false; }

        #endregion



        #region Methods

        /// <summary>
        /// Returns a Device with the given IDs
        /// </summary>
        /// <param name="IDIn">ID of the In Device</param>
        /// <param name="IDOut">ID of the Out Device</param>
        /// <returns></returns>
        public static MidiIODevice GetDeviceByIDs(uint IDIn, uint IDOut)
        {
            MidiIODevice ioDevice = new MidiIODevice();
            ioDevice.InDevice = MidiInDevice.GetDeviceByID(IDIn);
            ioDevice.OutDevice = MidiOutDevice.GetDeviceByID(IDOut);
            if (ioDevice.InDevice != null & ioDevice.OutDevice != null)
                return ioDevice;

            return null;
        }


        /// <summary>
        /// Starts Communcation with this Device
        /// </summary>
        /// <returns>success</returns>
        public bool StartCommunication()
        {
            if (InDevice == null || OutDevice == null)
                return false;

            if( InDevice.StartCommunication() &&
                OutDevice.StartCommunication())
            {
                InDevice.OnMidiInMessageReceived += OnMidiInMessageFunction;
                //OutDevice.OnMidiOutMessageReceived += OnMidiOutMessageFunction;
                return true;
            }

            return false;
        }


        /// <summary>
        /// Ends Communication with this Device
        /// </summary>
        public void EndCommunication()
        {
            if (InDevice == null || OutDevice == null)
                return;

            InDevice.EndCommunication();
            OutDevice.EndCommunication();
        }


        /// <summary>
        /// Sends a Midi Message to the Device
        /// </summary>
        /// <param name="midiEventType">Midi Message Status Byte</param>
        /// <param name="data1">Data 1 Byte</param>
        /// <param name="data2">Data 2 Byte</param>
        public void SendMidiMessage(byte midiEventType, byte data1, byte data2)
        {
            OutDevice.SendMidiMessage(midiEventType, data1, data2);
        }


        /// <summary>
        /// Sends a SysExMessage to the Device
        /// </summary>
        /// <param name="data">Data of the Message</param>
        public void SendSysExMessage(byte[] data)
        {
            OutDevice.SendSysExMessage(data);
        }


        /// <summary>
        /// Connects the In Device directly to the Out Device
        /// </summary>
        public void ConnectInToOut() { MidiWinAPI.MidiWinAPI.midiConnect(InDevice.GetWinHandle(), OutDevice.GetWinHandle(), IntPtr.Zero); devicesConnected = true; }


        /// <summary>
        /// Disconnects the In Device from the Out Device
        /// </summary>
        public void DisconnectInFromOut() { if(devicesConnected) MidiWinAPI.MidiWinAPI.midiDisconnect(InDevice.GetWinHandle(), OutDevice.GetWinHandle(), IntPtr.Zero); devicesConnected = false; } 


        protected virtual void OnMidiInMessageFunction(object sender, MidiMessageEventArgs eventArgs) { OnMidiInMessageReceived?.Invoke(this, eventArgs); }
        protected void OnMidiOutMessageFunction(object sender, MidiMessageEventArgs eventArgs) { OnMidiOutMessageReceived?.Invoke(this, eventArgs); }

        #endregion
    }
}
