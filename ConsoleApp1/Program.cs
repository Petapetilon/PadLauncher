using System;
using MiDI;
using MiDI.MidiDevices;
using MiDI.MidiDevices.Launchpad;
using MiDI.MidiDevices.Launchpad.Animation;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var launchpad = MidiLaunchpadDevice.GetDeviceByIDs(2, 3, LaunchpadVersion.MiniMK3);
            launchpad.StartCommunication();
            launchpad.ClearPads();

            //MiDI.Demo.MiDILaunchpadDemo.BeatSaberAnimationDemo(launchpad);
            var frame = new LaunchpadColorFrame();
            frame.DrawLineInterpolated(0, 0, 8, 8, new LaunchpadColorValue(0x7F0000, LaunchpadColor.Black), new LaunchpadColorValue(0x7F, LaunchpadColor.Black));
            frame.ApplyToLaunchpadUsingSysEx(launchpad);

            Console.ReadKey();
        }
    }
}
