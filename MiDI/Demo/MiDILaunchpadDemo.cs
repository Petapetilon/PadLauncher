using System;
using System.Collections.Generic;
using System.Threading;
using MiDI.MidiDevices.Launchpad.Animation;
using MiDI.MidiDevices.Launchpad;
using MiDI.Events.Launchpad;



namespace MiDI.Demo
{
    public class MiDILaunchpadDemo
    {
        public static void RainbowAnimationDemo(MidiLaunchpadDevice launchpadDevice)
        {
            launchpadDevice.Animation = new LaunchpadAnimation();
            
            var animationFrame1 = new LaunchpadColorFrame();
            animationFrame1.SetPadColor(0, 0, LaunchpadColor.BrightRed);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame1);
            
            var animationFrame2 = new LaunchpadColorFrame();
            animationFrame2.DrawLine(1, 0, 0, 1, LaunchpadColor.BrightCandyRed);
            animationFrame2.SetPadColor(0, 0, LaunchpadColor.BrightPink);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame2);
            
            var animationFrame3 = new LaunchpadColorFrame();
            animationFrame3.DrawLine(2, 0, 0, 2, LaunchpadColor.BrightCandyRed);
            animationFrame3.DrawLine(1, 0, 0, 1, LaunchpadColor.BrightPink);
            animationFrame3.SetPadColor(0, 0, LaunchpadColor.BrightPurple);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame3);
            
            var animationFrame4 = new LaunchpadColorFrame();
            animationFrame4.DrawLine(3, 0, 0, 3, LaunchpadColor.BrightCandyRed);
            animationFrame4.DrawLine(2, 0, 0, 2, LaunchpadColor.BrightPink);
            animationFrame4.DrawLine(1, 0, 0, 1, LaunchpadColor.BrightPurple);
            animationFrame4.SetPadColor(0, 0, LaunchpadColor.BrightBlue);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame4);
            
            var animationFrame5 = new LaunchpadColorFrame();
            animationFrame5.DrawLine(4, 0, 0, 4, LaunchpadColor.BrightCandyRed);
            animationFrame5.DrawLine(3, 0, 0, 3, LaunchpadColor.BrightPink);
            animationFrame5.DrawLine(2, 0, 0, 2, LaunchpadColor.BrightPurple);
            animationFrame5.DrawLine(1, 0, 0, 1, LaunchpadColor.BrightBlue);
            animationFrame5.SetPadColor(0, 0, LaunchpadColor.BrightMarineBlue);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame5);
            
            var animationFrame6 = new LaunchpadColorFrame();
            animationFrame6.DrawLine(5, 0, 0, 5, LaunchpadColor.BrightCandyRed);
            animationFrame6.DrawLine(4, 0, 0, 4, LaunchpadColor.BrightPink);
            animationFrame6.DrawLine(3, 0, 0, 3, LaunchpadColor.BrightPurple);
            animationFrame6.DrawLine(2, 0, 0, 2, LaunchpadColor.BrightBlue);
            animationFrame6.DrawLine(1, 0, 0, 1, LaunchpadColor.BrightMarineBlue);
            animationFrame6.SetPadColor(0, 0, LaunchpadColor.BrightSkyBlue);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame6);
            
            var animationFrame7 = new LaunchpadColorFrame();
            animationFrame7.DrawLine(6, 0, 0, 6, LaunchpadColor.BrightCandyRed);
            animationFrame7.DrawLine(5, 0, 0, 5, LaunchpadColor.BrightPink);
            animationFrame7.DrawLine(4, 0, 0, 4, LaunchpadColor.BrightPurple);
            animationFrame7.DrawLine(3, 0, 0, 3, LaunchpadColor.BrightBlue);
            animationFrame7.DrawLine(2, 0, 0, 2, LaunchpadColor.BrightMarineBlue);
            animationFrame7.DrawLine(1, 0, 0, 1, LaunchpadColor.BrightSkyBlue);
            animationFrame7.SetPadColor(0, 0, LaunchpadColor.BrightCyan);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame7);
            
            var animationFrame8 = new LaunchpadColorFrame();
            animationFrame8.DrawLine(7, 0, 0, 7, LaunchpadColor.BrightCandyRed);
            animationFrame8.DrawLine(6, 0, 0, 6, LaunchpadColor.BrightPink);
            animationFrame8.DrawLine(5, 0, 0, 5, LaunchpadColor.BrightPurple);
            animationFrame8.DrawLine(4, 0, 0, 4, LaunchpadColor.BrightBlue);
            animationFrame8.DrawLine(3, 0, 0, 3, LaunchpadColor.BrightMarineBlue);
            animationFrame8.DrawLine(2, 0, 0, 2, LaunchpadColor.BrightSkyBlue);
            animationFrame8.DrawLine(1, 0, 0, 1, LaunchpadColor.BrightCyan);
            animationFrame8.SetPadColor(0, 0, LaunchpadColor.BrightPetrolium);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame8);
            
            var animationFrame9 = new LaunchpadColorFrame();
            animationFrame9.DrawLine(8, 0, 0, 8, LaunchpadColor.BrightCandyRed);
            animationFrame9.DrawLine(7, 0, 0, 7, LaunchpadColor.BrightPink);
            animationFrame9.DrawLine(6, 0, 0, 6, LaunchpadColor.BrightPurple);
            animationFrame9.DrawLine(5, 0, 0, 5, LaunchpadColor.BrightBlue);
            animationFrame9.DrawLine(4, 0, 0, 4, LaunchpadColor.BrightMarineBlue);
            animationFrame9.DrawLine(3, 0, 0, 3, LaunchpadColor.BrightSkyBlue);
            animationFrame9.DrawLine(2, 0, 0, 2, LaunchpadColor.BrightCyan);
            animationFrame9.DrawLine(1, 0, 0, 1, LaunchpadColor.BrightPetrolium);
            animationFrame9.SetPadColor(0, 0, LaunchpadColor.BrightGreen);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame9);
            
            var animationFrame10 = new LaunchpadColorFrame();
            animationFrame10.DrawLine(8, 1, 1, 8, LaunchpadColor.BrightCandyRed);
            animationFrame10.DrawLine(8, 0, 0, 8, LaunchpadColor.BrightPink);
            animationFrame10.DrawLine(7, 0, 0, 7, LaunchpadColor.BrightPurple);
            animationFrame10.DrawLine(6, 0, 0, 6, LaunchpadColor.BrightBlue);
            animationFrame10.DrawLine(5, 0, 0, 5, LaunchpadColor.BrightMarineBlue);
            animationFrame10.DrawLine(4, 0, 0, 4, LaunchpadColor.BrightSkyBlue);
            animationFrame10.DrawLine(3, 0, 0, 3, LaunchpadColor.BrightCyan);
            animationFrame10.DrawLine(2, 0, 0, 2, LaunchpadColor.BrightPetrolium);
            animationFrame10.DrawLine(1, 0, 0, 1, LaunchpadColor.BrightGreen);
            animationFrame10.SetPadColor(0, 0, LaunchpadColor.BrightForestGreen);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame10);
            
            var animationFrame11 = new LaunchpadColorFrame();
            animationFrame11.DrawLine(8, 2, 2, 8, LaunchpadColor.BrightCandyRed);
            animationFrame11.DrawLine(8, 1, 1, 8, LaunchpadColor.BrightPink);
            animationFrame11.DrawLine(8, 0, 0, 8, LaunchpadColor.BrightPurple);
            animationFrame11.DrawLine(7, 0, 0, 7, LaunchpadColor.BrightBlue);
            animationFrame11.DrawLine(6, 0, 0, 6, LaunchpadColor.BrightMarineBlue);
            animationFrame11.DrawLine(5, 0, 0, 5, LaunchpadColor.BrightSkyBlue);
            animationFrame11.DrawLine(4, 0, 0, 4, LaunchpadColor.BrightCyan);
            animationFrame11.DrawLine(3, 0, 0, 3, LaunchpadColor.BrightPetrolium);
            animationFrame11.DrawLine(2, 0, 0, 2, LaunchpadColor.BrightGreen);
            animationFrame11.DrawLine(1, 0, 0, 1, LaunchpadColor.BrightForestGreen);
            animationFrame11.SetPadColor(0, 0, LaunchpadColor.BrightLime);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame11);
            
            var animationFrame12 = new LaunchpadColorFrame();
            animationFrame12.DrawLine(8, 3, 3, 8, LaunchpadColor.BrightCandyRed);
            animationFrame12.DrawLine(8, 2, 2, 8, LaunchpadColor.BrightPink);
            animationFrame12.DrawLine(8, 1, 1, 8, LaunchpadColor.BrightPurple);
            animationFrame12.DrawLine(8, 0, 0, 8, LaunchpadColor.BrightBlue);
            animationFrame12.DrawLine(7, 0, 0, 7, LaunchpadColor.BrightMarineBlue);
            animationFrame12.DrawLine(6, 0, 0, 6, LaunchpadColor.BrightSkyBlue);
            animationFrame12.DrawLine(5, 0, 0, 5, LaunchpadColor.BrightCyan);
            animationFrame12.DrawLine(4, 0, 0, 4, LaunchpadColor.BrightPetrolium);
            animationFrame12.DrawLine(3, 0, 0, 3, LaunchpadColor.BrightGreen);
            animationFrame12.DrawLine(2, 0, 0, 2, LaunchpadColor.BrightForestGreen);
            animationFrame12.DrawLine(1, 0, 0, 1, LaunchpadColor.BrightLime);
            animationFrame12.SetPadColor(0, 0, LaunchpadColor.BrightYellow);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame12);
            
            var animationFrame13 = new LaunchpadColorFrame();
            animationFrame13.DrawLine(8, 4, 4, 8, LaunchpadColor.BrightCandyRed);
            animationFrame13.DrawLine(8, 3, 3, 8, LaunchpadColor.BrightPink);
            animationFrame13.DrawLine(8, 2, 2, 8, LaunchpadColor.BrightPurple);
            animationFrame13.DrawLine(8, 1, 1, 8, LaunchpadColor.BrightBlue);
            animationFrame13.DrawLine(8, 0, 0, 8, LaunchpadColor.BrightMarineBlue);
            animationFrame13.DrawLine(7, 0, 0, 7, LaunchpadColor.BrightSkyBlue);
            animationFrame13.DrawLine(6, 0, 0, 6, LaunchpadColor.BrightCyan);
            animationFrame13.DrawLine(5, 0, 0, 5, LaunchpadColor.BrightPetrolium);
            animationFrame13.DrawLine(4, 0, 0, 4, LaunchpadColor.BrightGreen);
            animationFrame13.DrawLine(3, 0, 0, 3, LaunchpadColor.BrightForestGreen);
            animationFrame13.DrawLine(2, 0, 0, 2, LaunchpadColor.BrightLime);
            animationFrame13.DrawLine(1, 0, 0, 1, LaunchpadColor.BrightYellow);
            animationFrame13.SetPadColor(0, 0, LaunchpadColor.BrightOrangeYellow);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame13);
            
            var animationFrame14 = new LaunchpadColorFrame();
            animationFrame14.DrawLine(8, 5, 5, 8, LaunchpadColor.BrightCandyRed);
            animationFrame14.DrawLine(8, 4, 4, 8, LaunchpadColor.BrightPink);
            animationFrame14.DrawLine(8, 3, 3, 8, LaunchpadColor.BrightPurple);
            animationFrame14.DrawLine(8, 2, 2, 8, LaunchpadColor.BrightBlue);
            animationFrame14.DrawLine(8, 1, 1, 8, LaunchpadColor.BrightMarineBlue);
            animationFrame14.DrawLine(8, 0, 0, 8, LaunchpadColor.BrightSkyBlue);
            animationFrame14.DrawLine(7, 0, 0, 7, LaunchpadColor.BrightCyan);
            animationFrame14.DrawLine(6, 0, 0, 6, LaunchpadColor.BrightPetrolium);
            animationFrame14.DrawLine(5, 0, 0, 5, LaunchpadColor.BrightGreen);
            animationFrame14.DrawLine(4, 0, 0, 4, LaunchpadColor.BrightForestGreen);
            animationFrame14.DrawLine(3, 0, 0, 3, LaunchpadColor.BrightLime);
            animationFrame14.DrawLine(2, 0, 0, 2, LaunchpadColor.BrightYellow);
            animationFrame14.DrawLine(1, 0, 0, 1, LaunchpadColor.BrightOrangeYellow);
            animationFrame14.SetPadColor(0, 0, LaunchpadColor.BrightOrange);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame14);
            
            var animationFrame15 = new LaunchpadColorFrame();
            animationFrame15.DrawLine(8, 6, 6, 8, LaunchpadColor.BrightCandyRed);
            animationFrame15.DrawLine(8, 5, 5, 8, LaunchpadColor.BrightPink);
            animationFrame15.DrawLine(8, 4, 4, 8, LaunchpadColor.BrightPurple);
            animationFrame15.DrawLine(8, 3, 3, 8, LaunchpadColor.BrightBlue);
            animationFrame15.DrawLine(8, 2, 2, 8, LaunchpadColor.BrightMarineBlue);
            animationFrame15.DrawLine(8, 1, 1, 8, LaunchpadColor.BrightSkyBlue);
            animationFrame15.DrawLine(8, 0, 0, 8, LaunchpadColor.BrightCyan);
            animationFrame15.DrawLine(7, 0, 0, 7, LaunchpadColor.BrightPetrolium);
            animationFrame15.DrawLine(6, 0, 0, 6, LaunchpadColor.BrightGreen);
            animationFrame15.DrawLine(5, 0, 0, 5, LaunchpadColor.BrightForestGreen);
            animationFrame15.DrawLine(4, 0, 0, 4, LaunchpadColor.BrightLime);
            animationFrame15.DrawLine(3, 0, 0, 3, LaunchpadColor.BrightYellow);
            animationFrame15.DrawLine(2, 0, 0, 2, LaunchpadColor.BrightOrangeYellow);
            animationFrame15.DrawLine(1, 0, 0, 1, LaunchpadColor.BrightOrange);
            animationFrame15.SetPadColor(0, 0, LaunchpadColor.BrightRed);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame15);
            
            var animationFrame16 = new LaunchpadColorFrame();
            animationFrame16.DrawLine(8, 7, 7, 8, LaunchpadColor.BrightCandyRed);
            animationFrame16.DrawLine(8, 6, 6, 8, LaunchpadColor.BrightPink);
            animationFrame16.DrawLine(8, 5, 5, 8, LaunchpadColor.BrightPurple);
            animationFrame16.DrawLine(8, 4, 4, 8, LaunchpadColor.BrightBlue);
            animationFrame16.DrawLine(8, 3, 3, 8, LaunchpadColor.BrightMarineBlue);
            animationFrame16.DrawLine(8, 2, 2, 8, LaunchpadColor.BrightSkyBlue);
            animationFrame16.DrawLine(8, 1, 1, 8, LaunchpadColor.BrightCyan);
            animationFrame16.DrawLine(8, 0, 0, 8, LaunchpadColor.BrightPetrolium);
            animationFrame16.DrawLine(7, 0, 0, 7, LaunchpadColor.BrightGreen);
            animationFrame16.DrawLine(6, 0, 0, 6, LaunchpadColor.BrightForestGreen);
            animationFrame16.DrawLine(5, 0, 0, 5, LaunchpadColor.BrightLime);
            animationFrame16.DrawLine(4, 0, 0, 4, LaunchpadColor.BrightYellow);
            animationFrame16.DrawLine(3, 0, 0, 3, LaunchpadColor.BrightOrangeYellow);
            animationFrame16.DrawLine(2, 0, 0, 2, LaunchpadColor.BrightOrange);
            animationFrame16.DrawLine(1, 0, 0, 1, LaunchpadColor.BrightOrangeRed);
            animationFrame16.SetPadColor(0, 0, LaunchpadColor.BrightRed);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame16);
            
            var animationFrame17 = new LaunchpadColorFrame();
            animationFrame17.SetPadColor(8, 8, LaunchpadColor.BrightCandyRed);
            animationFrame17.DrawLine(8, 7, 7, 8, LaunchpadColor.BrightPink);
            animationFrame17.DrawLine(8, 6, 6, 8, LaunchpadColor.BrightPurple);
            animationFrame17.DrawLine(8, 5, 5, 8, LaunchpadColor.BrightBlue);
            animationFrame17.DrawLine(8, 4, 4, 8, LaunchpadColor.BrightMarineBlue);
            animationFrame17.DrawLine(8, 3, 3, 8, LaunchpadColor.BrightSkyBlue);
            animationFrame17.DrawLine(8, 2, 2, 8, LaunchpadColor.BrightCyan);
            animationFrame17.DrawLine(8, 1, 1, 8, LaunchpadColor.BrightPetrolium);
            animationFrame17.DrawLine(8, 0, 0, 8, LaunchpadColor.BrightGreen);
            animationFrame17.DrawLine(7, 0, 0, 7, LaunchpadColor.BrightForestGreen);
            animationFrame17.DrawLine(6, 0, 0, 6, LaunchpadColor.BrightLime);
            animationFrame17.DrawLine(5, 0, 0, 5, LaunchpadColor.BrightYellow);
            animationFrame17.DrawLine(4, 0, 0, 4, LaunchpadColor.BrightOrangeYellow);
            animationFrame17.DrawLine(3, 0, 0, 3, LaunchpadColor.BrightOrange);
            animationFrame17.DrawLine(2, 0, 0, 2, LaunchpadColor.BrightOrangeRed);
            animationFrame17.DrawLine(1, 0, 0, 1, LaunchpadColor.BrightRed);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame17);
            
            var animationFrame18 = new LaunchpadColorFrame();
            animationFrame18.SetPadColor(8, 8, LaunchpadColor.BrightPink);
            animationFrame18.DrawLine(8, 7, 7, 8, LaunchpadColor.BrightPurple);
            animationFrame18.DrawLine(8, 6, 6, 8, LaunchpadColor.BrightBlue);
            animationFrame18.DrawLine(8, 5, 5, 8, LaunchpadColor.BrightMarineBlue);
            animationFrame18.DrawLine(8, 4, 4, 8, LaunchpadColor.BrightSkyBlue);
            animationFrame18.DrawLine(8, 3, 3, 8, LaunchpadColor.BrightCyan);
            animationFrame18.DrawLine(8, 2, 2, 8, LaunchpadColor.BrightPetrolium);
            animationFrame18.DrawLine(8, 1, 1, 8, LaunchpadColor.BrightGreen);
            animationFrame18.DrawLine(8, 0, 0, 8, LaunchpadColor.BrightForestGreen);
            animationFrame18.DrawLine(7, 0, 0, 7, LaunchpadColor.BrightLime);
            animationFrame18.DrawLine(6, 0, 0, 6, LaunchpadColor.BrightYellow);
            animationFrame18.DrawLine(5, 0, 0, 5, LaunchpadColor.BrightOrangeYellow);
            animationFrame18.DrawLine(4, 0, 0, 4, LaunchpadColor.BrightOrange);
            animationFrame18.DrawLine(3, 0, 0, 3, LaunchpadColor.BrightOrangeRed);
            animationFrame18.DrawLine(2, 0, 0, 2, LaunchpadColor.BrightRed);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame18);
            
            var animationFrame19 = new LaunchpadColorFrame();
            animationFrame19.SetPadColor(8, 8, LaunchpadColor.BrightPurple);
            animationFrame19.DrawLine(8, 7, 7, 8, LaunchpadColor.BrightBlue);
            animationFrame19.DrawLine(8, 6, 6, 8, LaunchpadColor.BrightMarineBlue);
            animationFrame19.DrawLine(8, 5, 5, 8, LaunchpadColor.BrightSkyBlue);
            animationFrame19.DrawLine(8, 4, 4, 8, LaunchpadColor.BrightCyan);
            animationFrame19.DrawLine(8, 3, 3, 8, LaunchpadColor.BrightPetrolium);
            animationFrame19.DrawLine(8, 2, 2, 8, LaunchpadColor.BrightGreen);
            animationFrame19.DrawLine(8, 1, 1, 8, LaunchpadColor.BrightForestGreen);
            animationFrame19.DrawLine(8, 0, 0, 8, LaunchpadColor.BrightLime);
            animationFrame19.DrawLine(7, 0, 0, 7, LaunchpadColor.BrightYellow);
            animationFrame19.DrawLine(6, 0, 0, 6, LaunchpadColor.BrightOrangeYellow);
            animationFrame19.DrawLine(5, 0, 0, 5, LaunchpadColor.BrightOrange);
            animationFrame19.DrawLine(4, 0, 0, 4, LaunchpadColor.BrightOrangeRed);
            animationFrame19.DrawLine(3, 0, 0, 3, LaunchpadColor.BrightRed);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame19);
            
            var animationFrame20 = new LaunchpadColorFrame();
            animationFrame20.SetPadColor(8, 8, LaunchpadColor.BrightBlue);
            animationFrame20.DrawLine(8, 7, 7, 8, LaunchpadColor.BrightMarineBlue);
            animationFrame20.DrawLine(8, 6, 6, 8, LaunchpadColor.BrightSkyBlue);
            animationFrame20.DrawLine(8, 5, 5, 8, LaunchpadColor.BrightCyan);
            animationFrame20.DrawLine(8, 4, 4, 8, LaunchpadColor.BrightPetrolium);
            animationFrame20.DrawLine(8, 3, 3, 8, LaunchpadColor.BrightGreen);
            animationFrame20.DrawLine(8, 2, 2, 8, LaunchpadColor.BrightForestGreen);
            animationFrame20.DrawLine(8, 1, 1, 8, LaunchpadColor.BrightLime);
            animationFrame20.DrawLine(8, 0, 0, 8, LaunchpadColor.BrightYellow);
            animationFrame20.DrawLine(7, 0, 0, 7, LaunchpadColor.BrightOrangeYellow);
            animationFrame20.DrawLine(6, 0, 0, 6, LaunchpadColor.BrightOrange);
            animationFrame20.DrawLine(5, 0, 0, 5, LaunchpadColor.BrightOrangeRed);
            animationFrame20.DrawLine(4, 0, 0, 4, LaunchpadColor.BrightRed);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame20);
            
            var animationFrame21 = new LaunchpadColorFrame();
            animationFrame21.SetPadColor(8, 8, LaunchpadColor.BrightMarineBlue);
            animationFrame21.DrawLine(8, 7, 7, 8, LaunchpadColor.BrightSkyBlue);
            animationFrame21.DrawLine(8, 6, 6, 8, LaunchpadColor.BrightCyan);
            animationFrame21.DrawLine(8, 5, 5, 8, LaunchpadColor.BrightPetrolium);
            animationFrame21.DrawLine(8, 4, 4, 8, LaunchpadColor.BrightGreen);
            animationFrame21.DrawLine(8, 3, 3, 8, LaunchpadColor.BrightForestGreen);
            animationFrame21.DrawLine(8, 2, 2, 8, LaunchpadColor.BrightLime);
            animationFrame21.DrawLine(8, 1, 1, 8, LaunchpadColor.BrightYellow);
            animationFrame21.DrawLine(8, 0, 0, 8, LaunchpadColor.BrightOrangeYellow);
            animationFrame21.DrawLine(7, 0, 0, 7, LaunchpadColor.BrightOrange);
            animationFrame21.DrawLine(6, 0, 0, 6, LaunchpadColor.BrightOrangeRed);
            animationFrame21.DrawLine(5, 0, 0, 5, LaunchpadColor.BrightRed);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame21);
            
            var animationFrame22 = new LaunchpadColorFrame();
            animationFrame22.SetPadColor(8, 8, LaunchpadColor.BrightSkyBlue);
            animationFrame22.DrawLine(8, 7, 7, 8, LaunchpadColor.BrightCyan);
            animationFrame22.DrawLine(8, 6, 6, 8, LaunchpadColor.BrightPetrolium);
            animationFrame22.DrawLine(8, 5, 5, 8, LaunchpadColor.BrightGreen);
            animationFrame22.DrawLine(8, 4, 4, 8, LaunchpadColor.BrightForestGreen);
            animationFrame22.DrawLine(8, 3, 3, 8, LaunchpadColor.BrightLime);
            animationFrame22.DrawLine(8, 2, 2, 8, LaunchpadColor.BrightYellow);
            animationFrame22.DrawLine(8, 1, 1, 8, LaunchpadColor.BrightOrangeYellow);
            animationFrame22.DrawLine(8, 0, 0, 8, LaunchpadColor.BrightOrange);
            animationFrame22.DrawLine(7, 0, 0, 7, LaunchpadColor.BrightOrangeRed);
            animationFrame22.DrawLine(6, 0, 0, 6, LaunchpadColor.BrightRed);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame22);
            
            var animationFrame23 = new LaunchpadColorFrame();
            animationFrame23.SetPadColor(8, 8, LaunchpadColor.BrightCyan);
            animationFrame23.DrawLine(8, 7, 7, 8, LaunchpadColor.BrightPetrolium);
            animationFrame23.DrawLine(8, 6, 6, 8, LaunchpadColor.BrightGreen);
            animationFrame23.DrawLine(8, 5, 5, 8, LaunchpadColor.BrightForestGreen);
            animationFrame23.DrawLine(8, 4, 4, 8, LaunchpadColor.BrightLime);
            animationFrame23.DrawLine(8, 3, 3, 8, LaunchpadColor.BrightYellow);
            animationFrame23.DrawLine(8, 2, 2, 8, LaunchpadColor.BrightOrangeYellow);
            animationFrame23.DrawLine(8, 1, 1, 8, LaunchpadColor.BrightOrange);
            animationFrame23.DrawLine(8, 0, 0, 8, LaunchpadColor.BrightOrangeRed);
            animationFrame23.DrawLine(7, 0, 0, 7, LaunchpadColor.BrightRed);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame23);
            
            var animationFrame24 = new LaunchpadColorFrame();
            animationFrame24.SetPadColor(8, 8, LaunchpadColor.BrightPetrolium);
            animationFrame24.DrawLine(8, 7, 7, 8, LaunchpadColor.BrightGreen);
            animationFrame24.DrawLine(8, 6, 6, 8, LaunchpadColor.BrightForestGreen);
            animationFrame24.DrawLine(8, 5, 5, 8, LaunchpadColor.BrightLime);
            animationFrame24.DrawLine(8, 4, 4, 8, LaunchpadColor.BrightYellow);
            animationFrame24.DrawLine(8, 3, 3, 8, LaunchpadColor.BrightOrangeYellow);
            animationFrame24.DrawLine(8, 2, 2, 8, LaunchpadColor.BrightOrange);
            animationFrame24.DrawLine(8, 1, 1, 8, LaunchpadColor.BrightOrangeRed);
            animationFrame24.DrawLine(8, 0, 0, 8, LaunchpadColor.BrightRed);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame24);
            
            var animationFrame25 = new LaunchpadColorFrame();
            animationFrame25.SetPadColor(8, 8, LaunchpadColor.BrightGreen);
            animationFrame25.DrawLine(8, 7, 7, 8, LaunchpadColor.BrightForestGreen);
            animationFrame25.DrawLine(8, 6, 6, 8, LaunchpadColor.BrightLime);
            animationFrame25.DrawLine(8, 5, 5, 8, LaunchpadColor.BrightYellow);
            animationFrame25.DrawLine(8, 4, 4, 8, LaunchpadColor.BrightOrangeYellow);
            animationFrame25.DrawLine(8, 3, 3, 8, LaunchpadColor.BrightOrange);
            animationFrame25.DrawLine(8, 2, 2, 8, LaunchpadColor.BrightOrangeRed);
            animationFrame25.DrawLine(8, 1, 1, 8, LaunchpadColor.BrightRed);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame25);
            
            var animationFrame26 = new LaunchpadColorFrame();
            animationFrame26.SetPadColor(8, 8, LaunchpadColor.BrightForestGreen);
            animationFrame26.DrawLine(8, 7, 7, 8, LaunchpadColor.BrightLime);
            animationFrame26.DrawLine(8, 6, 6, 8, LaunchpadColor.BrightYellow);
            animationFrame26.DrawLine(8, 5, 5, 8, LaunchpadColor.BrightOrangeYellow);
            animationFrame26.DrawLine(8, 4, 4, 8, LaunchpadColor.BrightOrange);
            animationFrame26.DrawLine(8, 3, 3, 8, LaunchpadColor.BrightOrangeRed);
            animationFrame26.DrawLine(8, 2, 2, 8, LaunchpadColor.BrightRed);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame26);
            
            var animationFrame27 = new LaunchpadColorFrame();
            animationFrame27.SetPadColor(8, 8, LaunchpadColor.BrightLime);
            animationFrame27.DrawLine(8, 7, 7, 8, LaunchpadColor.BrightYellow);
            animationFrame27.DrawLine(8, 6, 6, 8, LaunchpadColor.BrightOrangeYellow);
            animationFrame27.DrawLine(8, 5, 5, 8, LaunchpadColor.BrightOrange);
            animationFrame27.DrawLine(8, 4, 4, 8, LaunchpadColor.BrightOrangeRed);
            animationFrame27.DrawLine(8, 3, 3, 8, LaunchpadColor.BrightRed);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame27);
            
            var animationFrame28 = new LaunchpadColorFrame();
            animationFrame28.SetPadColor(8, 8, LaunchpadColor.BrightYellow);
            animationFrame28.DrawLine(8, 7, 7, 8, LaunchpadColor.BrightOrangeYellow);
            animationFrame28.DrawLine(8, 6, 6, 8, LaunchpadColor.BrightOrange);
            animationFrame28.DrawLine(8, 5, 5, 8, LaunchpadColor.BrightOrangeRed);
            animationFrame28.DrawLine(8, 4, 4, 8, LaunchpadColor.BrightRed);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame28);
            
            var animationFrame29 = new LaunchpadColorFrame();
            animationFrame29.SetPadColor(8, 8, LaunchpadColor.BrightOrangeYellow);
            animationFrame29.DrawLine(8, 7, 7, 8, LaunchpadColor.BrightOrange);
            animationFrame29.DrawLine(8, 6, 6, 8, LaunchpadColor.BrightOrangeRed);
            animationFrame29.DrawLine(8, 5, 5, 8, LaunchpadColor.BrightRed);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame29);
            
            var animationFrame30 = new LaunchpadColorFrame();
            animationFrame30.SetPadColor(8, 8, LaunchpadColor.BrightOrange);
            animationFrame30.DrawLine(8, 7, 7, 8, LaunchpadColor.BrightOrangeRed);
            animationFrame30.DrawLine(8, 6, 6, 8, LaunchpadColor.BrightRed);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame30);
            
            var animationFrame31 = new LaunchpadColorFrame();
            animationFrame31.SetPadColor(8, 8, LaunchpadColor.BrightOrangeRed);
            animationFrame31.DrawLine(8, 7, 7, 8, LaunchpadColor.BrightRed);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame31);
            
            var animationFrame32 = new LaunchpadColorFrame();
            animationFrame32.SetPadColor(8, 8, LaunchpadColor.BrightRed);
            launchpadDevice.Animation.AppendAnimationFrame(animationFrame32);



            launchpadDevice.Animation.PlayAnimation(24, 0, AnimationPlayMode.LoopPingPong);
        }
        public static void BeatSaberAnimationDemo(MidiLaunchpadDevice launchpadDevice) 
        {
            launchpadDevice.Animation = new LaunchpadAnimation();

            var frame1 = new LaunchpadColorFrame();
            frame1.DrawRectangle(1, 1, 7, 7, true, LaunchpadColor.BrightBlue);
            frame1.SetPadColor(1, 1, LaunchpadColor.Black);
            frame1.SetPadColor(7, 1, LaunchpadColor.Black);
            frame1.SetPadColor(1, 7, LaunchpadColor.Black);
            frame1.SetPadColor(7, 7, LaunchpadColor.Black);
            frame1.DrawTriangle(2, 6, 4, 4, 6, 6, true, LaunchpadColor.LightGray);
            frame1.displayDuration = 500;
            launchpadDevice.Animation.AppendAnimationFrame(new LaunchpadColorFrame(frame1));

            frame1.DrawLine(3, 8, 3, 6, LaunchpadColor.White);
            frame1.displayDuration = 33;
            launchpadDevice.Animation.AppendAnimationFrame(new LaunchpadColorFrame(frame1));

            frame1.DrawLine(4, 5, 4, 3, LaunchpadColor.White);
            launchpadDevice.Animation.AppendAnimationFrame(new LaunchpadColorFrame(frame1));

            frame1.DrawLine(5, 2, 5, 0, LaunchpadColor.White);
            launchpadDevice.Animation.AppendAnimationFrame(new LaunchpadColorFrame(frame1));


            var frame3 = new LaunchpadColorFrame();
            frame3.DrawRectangle(0, 0, 1, 6, true, LaunchpadColor.BrightBlue);
            frame3.DrawLine(2, 0, 2, 4, LaunchpadColor.BrightBlue);
            frame3.DrawLine(3, 0, 3, 1, LaunchpadColor.BrightBlue);
            frame3.SetPadColor(1, 5, LaunchpadColor.LightGray);
            frame3.SetPadColor(2, 4, LaunchpadColor.LightGray);

            frame3.DrawRectangle(7, 0, 8, 6, true, LaunchpadColor.BrightBlue);
            frame3.DrawLine(5, 6, 5, 5, LaunchpadColor.BrightBlue);
            frame3.DrawLine(6, 6, 6, 2, LaunchpadColor.BrightBlue);
            frame3.DrawLine(5, 5, 7, 5, LaunchpadColor.LightGray);
            frame3.SetPadColor(6, 4, LaunchpadColor.LightGray);

            frame3.SetPadColor(0, 0, LaunchpadColor.Black);
            frame3.SetPadColor(0, 8, LaunchpadColor.Black);
            frame3.SetPadColor(0, 6, LaunchpadColor.Black);
            frame3.SetPadColor(8, 6, LaunchpadColor.Black);

            frame3.DrawLine(3, 8, 3, 6, LaunchpadColor.White);
            frame3.DrawLine(4, 5, 4, 3, LaunchpadColor.White);
            frame3.DrawLine(5, 2, 5, 0, LaunchpadColor.White);
            frame3.displayDuration = 500;
            launchpadDevice.Animation.AppendAnimationFrame(new LaunchpadColorFrame(frame3));

            var blankFrame = new LaunchpadColorFrame();
            blankFrame.displayDuration = 250;
            launchpadDevice.Animation.AppendAnimationFrame(blankFrame);

            frame1 = new LaunchpadColorFrame();
            frame1.DrawRectangle(1, 1, 7, 7, true, LaunchpadColor.BrightCandyRed);
            frame1.SetPadColor(1, 1, LaunchpadColor.Black);
            frame1.SetPadColor(7, 1, LaunchpadColor.Black);
            frame1.SetPadColor(1, 7, LaunchpadColor.Black);
            frame1.SetPadColor(7, 7, LaunchpadColor.Black);
            frame1.DrawTriangle(2, 6, 4, 4, 6, 6, true, LaunchpadColor.LightGray);
            frame1.displayDuration = 500; launchpadDevice.Animation.AppendAnimationFrame(new LaunchpadColorFrame(frame1));

            frame1.DrawLine(3, 8, 3, 6, LaunchpadColor.White);
            frame1.displayDuration = 33;
            launchpadDevice.Animation.AppendAnimationFrame(new LaunchpadColorFrame(frame1));

            frame1.DrawLine(4, 5, 4, 3, LaunchpadColor.White);
            launchpadDevice.Animation.AppendAnimationFrame(new LaunchpadColorFrame(frame1));

            frame1.DrawLine(5, 2, 5, 0, LaunchpadColor.White);
            launchpadDevice.Animation.AppendAnimationFrame(new LaunchpadColorFrame(frame1));

            frame3.ReplaceColor(LaunchpadColor.BrightBlue, LaunchpadColor.BrightCandyRed);
            launchpadDevice.Animation.AppendAnimationFrame(new LaunchpadColorFrame(frame3));

            launchpadDevice.Animation.AppendAnimationFrame(blankFrame);



            launchpadDevice.Animation.PlayAnimation(0, AnimationPlayMode.LoopForwards);
        }
        public static void AudioVisualizerDemo(MidiLaunchpadDevice launchpadDevice)
        {
        }
        public static void SnakeDemo(MidiLaunchpadDevice launchpadDevice)
        {
            launchpadDevice.OnPadDown += EventReceived;
            launchpadDevice.Animation.OnAnimationComplete += delegate { StartSnakeDemo(launchpadDevice); };
            StartSnakeDemo(launchpadDevice);
        }




        private static PadCoordinate headingDirection = new PadCoordinate(0, 0);
        private static List<PadCoordinate> snakeBody = new List<PadCoordinate>();
        private static bool awaitingInput = true;
        private static void SnakeDemoCode(MidiLaunchpadDevice launchpadDevice)
        {
            launchpadDevice.Animation.ClearAnimationFrames();

            var frame1 = new LaunchpadColorFrame();
            frame1.DrawLine(2, 2, 6, 6, LaunchpadColor.BrightCandyRed);
            frame1.DrawLine(2, 6, 6, 2, LaunchpadColor.BrightCandyRed);
            var frame2 = new LaunchpadColorFrame();
            launchpadDevice.Animation.AppendAnimationFrame(frame1);
            launchpadDevice.Animation.AppendAnimationFrame(frame2);
            launchpadDevice.Animation.AppendAnimationFrame(frame1);
            launchpadDevice.Animation.AppendAnimationFrame(frame2);
            launchpadDevice.Animation.AppendAnimationFrame(frame1);


            snakeBody = new List<PadCoordinate>();
            headingDirection = new PadCoordinate(0, 0);
            snakeBody.Add(new PadCoordinate(4, 4));
            snakeBody.Add(new PadCoordinate(4, 3));

            Random rand = new Random();
            PadCoordinate food = new PadCoordinate(rand.Next(0, 8), rand.Next(0, 8));

            bool isPlaying = true;
            launchpadDevice.ClearPads();
            awaitingInput = true;


            launchpadDevice.SetPadColor(snakeBody[0], LaunchpadColor.BrightPink);
            for (int i = 1; i < snakeBody.Count; i++)
            {
                launchpadDevice.SetPadColor(snakeBody[i], LaunchpadColor.BrightPurple);
            }

            launchpadDevice.SetPadColor(food, LaunchpadColor.BrightLime, PadColorDisplayType.PulsingColor);
            while (awaitingInput) { }


            while (isPlaying)
            {
                Thread.Sleep(200);

                launchpadDevice.SetPadColor(snakeBody[snakeBody.Count - 1], LaunchpadColor.Black);
                for (int i = snakeBody.Count - 1; i > 0; i--)
                {
                    snakeBody[i] = snakeBody[i - 1];
                }

                snakeBody[0] = snakeBody[0] + headingDirection;



                if (snakeBody[0] == food)
                {
                    launchpadDevice.SetPadColor(food, LaunchpadColor.Black);
                    food = new PadCoordinate(rand.Next(0, 8), rand.Next(0, 8));
                    snakeBody.Add(new PadCoordinate());
                }


                //lose condition
                for (int i = 1; i < snakeBody.Count; i++)
                {
                    if (snakeBody[i] == snakeBody[0])
                    {
                        launchpadDevice.Animation.PlayAnimation(10, 0, AnimationPlayMode.ForwardsOnce);
                        isPlaying = false;
                        return;
                    }
                }

                if (snakeBody[0].x < 0 || snakeBody[0].x > 8 || snakeBody[0].y < 0 || snakeBody[0].y > 8)
                {
                    launchpadDevice.Animation.PlayAnimation(10, 0, AnimationPlayMode.ForwardsOnce);
                    isPlaying = false;
                    return;
                }




                launchpadDevice.SetPadColor(snakeBody[0], LaunchpadColor.BrightPink);
                for (int i = 1; i < snakeBody.Count; i++)
                {
                    launchpadDevice.SetPadColor(snakeBody[i], LaunchpadColor.BrightPurple);
                }

                launchpadDevice.SetPadColor(food, LaunchpadColor.BrightLime);
            }
        }
        private static void EventReceived(object sender, LaunchpadInputEventArgs eventArgs)
        {
            awaitingInput = false;
            if (headingDirection.x == 0)
            {
                if (eventArgs.x == 8)
                {
                    headingDirection.x = 1;
                    headingDirection.y = 0;
                }

                if (0 == eventArgs.x)
                {
                    headingDirection.x = -1;
                    headingDirection.y = 0;
                }
            }

            if (headingDirection.y == 0)
            {
                if (8 == eventArgs.y)
                {
                    headingDirection.x = 0;
                    headingDirection.y = 1;
                }

                if (0 == eventArgs.y)
                {
                    headingDirection.x = 0;
                    headingDirection.y = -1;
                }
            }
        }
        private static void StartSnakeDemo(MidiLaunchpadDevice launchpadDevice)
        {
            var snake = new Thread(new ParameterizedThreadStart(delegate { SnakeDemoCode(launchpadDevice); }));
            snake.Start();
        }
    }
}
