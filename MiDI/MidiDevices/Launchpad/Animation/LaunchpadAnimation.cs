using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace MiDI.MidiDevices.Launchpad.Animation
{
    public delegate void Notify();


    /// <summary>
    /// Class for handling an Animation
    /// </summary>
    public sealed class LaunchpadAnimation
    {
        #region Values

        /// <summary>
        /// Event that gets invoked when the Animation has completed
        /// </summary>
        public event Notify OnAnimationComplete;

        private List<LaunchpadColorFrame> animationFrames = new List<LaunchpadColorFrame>();
        private Thread animationThread;
        internal MidiLaunchpadDevice launchpad;

        #endregion



        #region Methods

        public LaunchpadAnimation() { }
        internal LaunchpadAnimation(MidiLaunchpadDevice launchpadDevice) { launchpad = launchpadDevice; }


        /// <summary>
        /// Adds a Launchpad Color Frame to the end of the Animation
        /// </summary>
        /// <param name="animationFrame">Appended Launchpad Color Frame</param>
        public void AppendAnimationFrame(LaunchpadColorFrame animationFrame) { animationFrames.Add(animationFrame); }


        /// <summary>
        /// Adds a Launchpad Color Frame at the given Index
        /// </summary>
        /// <param name="animationFrame">Inserted Launchpad Color Frame</param>
        /// <param name="index">Index of Insertion</param>
        public void InsertAnimationFrame(LaunchpadColorFrame animationFrame, int index) { animationFrames.Insert(index, animationFrame); }


        /// <summary>
        /// Returns the Launchpad Color Frame at the position of the index
        /// </summary>
        /// <param name="index">index of Launchpad Color Frame</param>
        /// <returns>Launchpad Color Frame at the index</returns>
        public LaunchpadColorFrame GetAnimationFrame(int index) { return index < animationFrames.Count ? animationFrames[index] : null; }


        /// <summary>
        /// Returns the Number of Launchpad Color Frames the Animation uses
        /// </summary>
        public int GetAnimationFrameCount() { return animationFrames.Count; }



        /// <summary>
        /// Plays the animation with an overwritten fps paramter (CAUTION! This will delete the previous displayTime paramter saved in the LaunchpadColorFrames)
        /// </summary>
        /// <param name="fpsOverride">FPS overwrite</param>
        /// <param name="startFrame">Startframe of the animation</param>
        /// <param name="playMode">Play mode of the animation</param>
        public void PlayAnimation(float fpsOverride, int startFrame, AnimationPlayMode playMode)
        {
            foreach (var frame in animationFrames)
                frame.displayDuration = (int)(1000 / fpsOverride);

            PlayAnimation(startFrame, playMode);
        }


        /// <summary>
        /// Plays the Animation on the Launchpad
        /// </summary>
        /// <param name="startFrame">Startframe of the animation</param>
        /// <param name="playMode">Play mode of the animation</param>
        public void PlayAnimation(int startFrame, AnimationPlayMode playMode)
        {
            if(animationFrames?.Count > 0)
            {
                switch (playMode)
                {
                    case AnimationPlayMode.ForwardsOnce:
                        animationThread = new Thread(new ParameterizedThreadStart(delegate { PlayForward(startFrame, false, launchpad); }));
                        break;

                    case AnimationPlayMode.BackwardsOnce:
                        animationThread = new Thread(new ParameterizedThreadStart(delegate { PlayBackward(startFrame, false, launchpad); }));
                        break;

                    case AnimationPlayMode.PingPongOnce:
                        animationThread = new Thread(new ParameterizedThreadStart(delegate { PlayPingPong(startFrame, false, launchpad); }));
                        break;                    
                    
                    case AnimationPlayMode.LoopForwards:
                        animationThread = new Thread(new ParameterizedThreadStart(delegate { PlayForward(startFrame, true, launchpad); }));
                        break;

                    case AnimationPlayMode.LoopBackward:
                        animationThread = new Thread(new ParameterizedThreadStart(delegate { PlayBackward(startFrame, true, launchpad); }));
                        break;

                    case AnimationPlayMode.LoopPingPong:
                        animationThread = new Thread(new ParameterizedThreadStart(delegate { PlayPingPong(startFrame, true, launchpad); }));
                        break;
                }            
                
                animationThread.IsBackground = true;
                animationThread.Start();
            }
        }


        public void StopAnimation() { animationThread?.Abort(); }
        public void ClearAnimationFrames() { animationFrames.Clear(); }


        //TODO still buggy

        private void PlayForward(int startFrame, bool loop, MidiLaunchpadDevice launchpadDevice)
        {
            do
            {
                animationFrames[startFrame % animationFrames.Count].ApplyToLaunchpadUsingSysEx(launchpadDevice);
                Thread.Sleep(animationFrames[startFrame++ % animationFrames.Count].displayDuration);
            }
            while (loop || startFrame < animationFrames.Count);

            OnAnimationComplete?.Invoke();
        }

        private void PlayBackward(int startFrame, bool loop, MidiLaunchpadDevice launchpadDevice)
        {
            do
            {
                animationFrames[Math.Abs(startFrame) % animationFrames.Count].ApplyToLaunchpadUsingSysEx(launchpadDevice);
                Thread.Sleep(animationFrames[Math.Abs(startFrame--) % animationFrames.Count].displayDuration);
            }
            while (loop || startFrame > -1);

            OnAnimationComplete?.Invoke();
        }

        private void PlayPingPong(int startFrame, bool loop, MidiLaunchpadDevice launchpadDevice)
        {
            bool currentDir = true;
            do
            {
                animationFrames[Math.Abs(startFrame)].ApplyToLaunchpadUsingSysEx(launchpadDevice);
                Thread.Sleep(animationFrames[currentDir ? startFrame++ : startFrame--].displayDuration);
                if (startFrame == animationFrames.Count - 1) currentDir = false;
                if (startFrame == 0) currentDir = true;
            }
            while (loop || (startFrame > 0));

            OnAnimationComplete?.Invoke();
        }

        #endregion
    }
}
