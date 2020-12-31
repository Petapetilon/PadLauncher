using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using MiDI.MidiDevices;
using MiDI.MidiDevices.Launchpad;
using MiDI.MidiDevices.Launchpad.Animation;
using System.Diagnostics;
using NAudio.Wave;
using NAudio.Dsp;


namespace Launchpad_Animator
{
    public partial class LaunchpadSelectionLabel : Form
    {
        MidiLaunchpadDevice launchpad;
        LaunchpadVersion selectedLaunchpadVersion;
        string selectedMidiInDeviceName = "";
        uint selectedMidiInDeviceID;
        string selectedMidiOutDeviceName = "";
        uint selectedMidiOutDeviceID;
        float[] oldBands = new float[9];
        float[] result = new float[9];
        bool shouldRun;
        int timesAdded = 0;


        SampleAggregator sampleAggregator;


        public LaunchpadSelectionLabel()
        {
            InitializeComponent();

            selectLaunchpadDevcieInputComboBox.Items.AddRange(MidiInDevice.GetAllInDeviceNames());
            selectLaunchpadDeviceOutputComboBox.Items.AddRange(MidiOutDevice.GetAllOutDeviceNames());
            selectLaunchpadModelComboBox.Items.AddRange(Enum.GetNames(typeof(LaunchpadVersion)));

            ValidateInput();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SelectLaunchpadModelLabel_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void selectLaunchpadDeviceInputComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedMidiInDeviceName = selectLaunchpadDevcieInputComboBox.SelectedItem.ToString();
            selectedMidiInDeviceID = (uint)selectLaunchpadDevcieInputComboBox.SelectedIndex;
            ValidateInput();
        }

        private void selectLaunchpadDeviceOutputComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedMidiOutDeviceName = selectLaunchpadDeviceOutputComboBox.SelectedItem.ToString();
            selectedMidiOutDeviceID = (uint)selectLaunchpadDeviceOutputComboBox.SelectedIndex;
            ValidateInput();
        }

        private void selectLaunchpadModelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedLaunchpadVersion = (LaunchpadVersion)selectLaunchpadModelComboBox.SelectedIndex;
        }

        private void refreshMidiDevicesButton_Click(object sender, EventArgs e)
        {
            selectLaunchpadDevcieInputComboBox.Items.Clear();
            selectLaunchpadDeviceOutputComboBox.Items.Clear();

            selectLaunchpadDevcieInputComboBox.Items.AddRange(MidiInDevice.GetAllInDeviceNames());
            selectLaunchpadDeviceOutputComboBox.Items.AddRange(MidiOutDevice.GetAllOutDeviceNames());
        }


        private void ValidateInput()
        {
            if (selectedMidiOutDeviceName != "" && selectedMidiInDeviceName != "")
            {
                startButton.Enabled = true;
            }
            else
                startButton.Enabled = false;
        }


        private void startButton_Click(object sender, EventArgs e)
        {
            try
            {
            launchpad = MidiLaunchpadDevice.GetDeviceByIDs(selectedMidiInDeviceID, selectedMidiOutDeviceID, selectedLaunchpadVersion);
            launchpad.StartCommunication();
            launchpad.ClearPads();

            }
            catch(Exception ex) { Debug.Write(ex.Message); }
            //launchpad.BeProud();
            Debug.WriteLine("starting");



            sampleAggregator = new SampleAggregator(8192);
            sampleAggregator.FftCalculated += OnFFTCalculated;
            sampleAggregator.PerformFFT = true;

            var capture = new WasapiLoopbackCapture();
            capture.DataAvailable += OnLoopbackDataAvailable;
            capture.StartRecording();

            var animationThread = new Thread(new ParameterizedThreadStart(delegate { DrawLaunchpadFrameCoroutine(); }));
            shouldRun = true;
            animationThread.Start();
        }


        private void OnFFTCalculated(object e, FftEventArgs eventArgs)
        {
            result = new float[9];

            for(int i = 0; i < 10; i++)
                result[0] += MathF.Sqrt(eventArgs.Result[i].X * eventArgs.Result[i].X + eventArgs.Result[i].Y * eventArgs.Result[i].Y);
            result[0] *= 15;
            
            
            for(int i = 10; i < 20; i++)
                result[1] += MathF.Sqrt(eventArgs.Result[i].X * eventArgs.Result[i].X + eventArgs.Result[i].Y * eventArgs.Result[i].Y);
            result[1] *= 15;
            
            
            for(int i = 20; i < 40; i++)
                result[2] += MathF.Sqrt(eventArgs.Result[i].X * eventArgs.Result[i].X + eventArgs.Result[i].Y * eventArgs.Result[i].Y);
            result[2] *= 15;
            
            
            for (int i = 40; i < 80; i++)
                result[3] += MathF.Sqrt(eventArgs.Result[i].X * eventArgs.Result[i].X + eventArgs.Result[i].Y * eventArgs.Result[i].Y);
            result[3] *= 10;
            
            
            for (int i = 80; i < 160; i++)
                result[4] += MathF.Sqrt(eventArgs.Result[i].X * eventArgs.Result[i].X + eventArgs.Result[i].Y * eventArgs.Result[i].Y);
            result[4] *= 50 / 8;
            
            
            for (int i = 160; i < 320; i++)
                result[5] += MathF.Sqrt(eventArgs.Result[i].X * eventArgs.Result[i].X + eventArgs.Result[i].Y * eventArgs.Result[i].Y);
            result[5] *= 80 / 16;
            
            
            for(int i = 320; i < 640; i++)
                result[6] += MathF.Sqrt(eventArgs.Result[i].X * eventArgs.Result[i].X + eventArgs.Result[i].Y * eventArgs.Result[i].Y);
            result[6] *= 120 / 32;
            
            
            for (int i = 640; i < 1280; i++)
                result[7] += MathF.Sqrt(eventArgs.Result[i].X * eventArgs.Result[i].X + eventArgs.Result[i].Y * eventArgs.Result[i].Y);
            result[7] *= 210 / 64;
            
            
            for(int i = 1280; i < 4096; i++)
                result[8] += MathF.Sqrt(eventArgs.Result[i].X * eventArgs.Result[i].X + eventArgs.Result[i].Y * eventArgs.Result[i].Y);
            result[8] *= 1260 / 128;
        }


        private void OnLoopbackDataAvailable(object e, WaveInEventArgs eventArgs)
        {
            byte[] buffer = eventArgs.Buffer;
            int bytesRecorded = eventArgs.BytesRecorded;
            int bufferIncrement = ((WasapiLoopbackCapture)e).WaveFormat.BlockAlign;

            for (int index = 0; index < bytesRecorded; index += bufferIncrement)
            {
                float sample32 = BitConverter.ToSingle(buffer, index);
                sampleAggregator.Add(sample32);
            }
        }

        private void DrawLaunchpadFrameCoroutine()
        {
            while (shouldRun)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                var frame = new LaunchpadColorFrame();

                //frame.DrawLine(0, 0, 0, (int)result[0], new LaunchpadColorValue(0x7F007F, LaunchpadColor.Black), new LaunchpadColorValue(0x7F0000, LaunchpadColor.Black), 7);
                //frame.DrawLine(1, 0, 1, (int)result[1], new LaunchpadColorValue(0x6F007F, LaunchpadColor.Black), new LaunchpadColorValue(0x7F0000, LaunchpadColor.Black), 7);
                //frame.DrawLine(2, 0, 2, (int)result[2], new LaunchpadColorValue(0x5F007F, LaunchpadColor.Black), new LaunchpadColorValue(0x7F0000, LaunchpadColor.Black), 7);
                //frame.DrawLine(3, 0, 3, (int)result[3], new LaunchpadColorValue(0x4F007F, LaunchpadColor.Black), new LaunchpadColorValue(0x7F0000, LaunchpadColor.Black), 7);
                //frame.DrawLine(4, 0, 4, (int)result[4], new LaunchpadColorValue(0x3F007F, LaunchpadColor.Black), new LaunchpadColorValue(0x7F0000, LaunchpadColor.Black), 7);
                //frame.DrawLine(5, 0, 5, (int)result[5], new LaunchpadColorValue(0x2F007F, LaunchpadColor.Black), new LaunchpadColorValue(0x7F0000, LaunchpadColor.Black), 7);
                //frame.DrawLine(6, 0, 6, (int)result[6], new LaunchpadColorValue(0x1F007F, LaunchpadColor.Black), new LaunchpadColorValue(0x7F0000, LaunchpadColor.Black), 7);
                //frame.DrawLine(7, 0, 7, (int)result[7], new LaunchpadColorValue(0x0F007F, LaunchpadColor.Black), new LaunchpadColorValue(0x7F0000, LaunchpadColor.Black), 7);
                //frame.DrawLine(8, 0, 8, (int)result[8], new LaunchpadColorValue(0x00007F, LaunchpadColor.Black), new LaunchpadColorValue(0x7F0000, LaunchpadColor.Black), 7);

                frame.DrawLine(0, 0, 0, (int)result[0], LaunchpadColor.BrightCandyRed);
                frame.DrawLine(1, 0, 1, (int)result[1], LaunchpadColor.BrightPink);
                frame.DrawLine(2, 0, 2, (int)result[2], LaunchpadColor.BrightPurple);
                frame.DrawLine(3, 0, 3, (int)result[3], LaunchpadColor.BrightBlue);
                frame.DrawLine(4, 0, 4, (int)result[4], LaunchpadColor.BrightMarineBlue);
                frame.DrawLine(5, 0, 5, (int)result[5], LaunchpadColor.BrightSkyBlue);
                frame.DrawLine(6, 0, 6, (int)result[6], LaunchpadColor.BrightCyan);
                frame.DrawLine(7, 0, 7, (int)result[7], LaunchpadColor.BrightPetrolium);
                frame.DrawLine(8, 0, 8, (int)result[8], LaunchpadColor.BrightGreen);
                
                
                if (result[0] > 5) frame.DrawLine(0, 6, 0, 7, LaunchpadColor.BrightOrange);
                if (result[0] > 7) frame.SetPadColor(0, 8, LaunchpadColor.BrightRed);
                
                if (result[1] > 5) frame.DrawLine(1, 6, 1, 7, LaunchpadColor.BrightOrange);
                if (result[1] > 7) frame.SetPadColor(1, 8, LaunchpadColor.BrightRed);
                
                if (result[2] > 5) frame.DrawLine(2, 6, 2, 7, LaunchpadColor.BrightOrange);
                if (result[2] > 7) frame.SetPadColor(2, 8, LaunchpadColor.BrightRed);
                
                if (result[3] > 5) frame.DrawLine(3, 6, 3, 7, LaunchpadColor.BrightOrange);
                if (result[3] > 7) frame.SetPadColor(3, 8, LaunchpadColor.BrightRed);
                
                if (result[4] > 5) frame.DrawLine(4, 6, 4, 7, LaunchpadColor.BrightOrange);
                if (result[4] > 7) frame.SetPadColor(4, 8, LaunchpadColor.BrightRed);
                
                if (result[5] > 5) frame.DrawLine(5, 6, 5, 7, LaunchpadColor.BrightOrange);
                if (result[5] > 7) frame.SetPadColor(5, 8, LaunchpadColor.BrightRed);
                
                if (result[6] > 5) frame.DrawLine(6, 6, 6, 7, LaunchpadColor.BrightOrange);
                if (result[6] > 7) frame.SetPadColor(6, 8, LaunchpadColor.BrightRed);
                
                if (result[7] > 5) frame.DrawLine(7, 6, 7, 7, LaunchpadColor.BrightOrange);
                if (result[7] > 7) frame.SetPadColor(7, 8, LaunchpadColor.BrightRed);
                
                if (result[8] > 5) frame.DrawLine(8, 6, 8, 7, LaunchpadColor.BrightOrange);
                if (result[8] > 7) frame.SetPadColor(8, 8, LaunchpadColor.BrightRed);


                frame.ApplyToLaunchpadUsingSysEx(launchpad);

                watch.Stop();
                Debug.WriteLine(watch.ElapsedMilliseconds);
                //Thread.Sleep((int)watch.ElapsedMilliseconds);
            }
        }
    }



    class SampleAggregator
    {
        public event EventHandler<FftEventArgs> FftCalculated;
        public bool PerformFFT { get; set; }

        private Complex[] fftBuffer;
        private FftEventArgs fftArgs;
        private int fftPos;
        private int fftLength;
        private int m;

        public SampleAggregator(int fftLength)
        {
            if (!IsPowerOfTwo(fftLength))
            {
                throw new ArgumentException("FFT Length must be a power of two");
            }
            this.m = (int)Math.Log(fftLength, 2.0);
            this.fftLength = fftLength;
            this.fftBuffer = new Complex[fftLength];
            this.fftArgs = new FftEventArgs(fftBuffer);
        }

        bool IsPowerOfTwo(int x)
        {
            return (x & (x - 1)) == 0;
        }

        public void Add(float value)
        {
            if (PerformFFT && FftCalculated != null)
            {
                // Remember the window function! There are many others as well.
                fftBuffer[fftPos].X = (float)(value * FastFourierTransform.HammingWindow(fftPos, fftLength));
                fftBuffer[fftPos].Y = 0; // This is always zero with audio.
                fftPos++;
                if (fftPos >= fftLength)
                {
                    fftPos = 0;
                    FastFourierTransform.FFT(true, m, fftBuffer);
                    FftCalculated(this, fftArgs);
                }
            }
        }
    }

    public class FftEventArgs : EventArgs
    {
        [DebuggerStepThrough]
        public FftEventArgs(Complex[] result)
        {
            this.Result = result;
        }
        public Complex[] Result { get; private set; }
    }
}
