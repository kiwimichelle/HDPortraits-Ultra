using StardewModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HDPortraitsUltra
{
    public class AnimationController
    {
        private readonly IMonitor monitor;
        private Dictionary<string, AnimationState> activeAnimations = new();
        private int maxFrames = 50;

        public AnimationController(IMonitor monitor, int maxFrames = 50)
        {
            this.monitor = monitor;
            this.maxFrames = maxFrames;
        }

        public void PlayAnimation(string npcName, List<AnimationFrame> frames, bool loop = true, int loopDelay = 3000)
        {
            if (frames.Count > this.maxFrames)
            {
                this.monitor.Log($"Animation for {npcName} exceeds max frames ({frames.Count} > {this.maxFrames})", LogLevel.Warn);
                frames = frames.Take(this.maxFrames).ToList();
            }

            var state = new AnimationState
            {
                NPC = npcName,
                Frames = frames,
                CurrentFrame = 0,
                Loop = loop,
                LoopDelay = loopDelay,
                IsPlaying = true,
                LastFrameTime = DateTime.Now
            };

            if (this.activeAnimations.ContainsKey(npcName))
                this.activeAnimations[npcName] = state;
            else
                this.activeAnimations.Add(npcName, state);
        }

        public void StopAnimation(string npcName)
        {
            if (this.activeAnimations.ContainsKey(npcName))
            {
                this.activeAnimations[npcName].IsPlaying = false;
            }
        }

        public AnimationFrame GetCurrentFrame(string npcName)
        {
            if (!this.activeAnimations.ContainsKey(npcName))
                return null;

            var state = this.activeAnimations[npcName];
            if (!state.IsPlaying || state.Frames.Count == 0)
                return null;

            var elapsed = (DateTime.Now - state.LastFrameTime).TotalMilliseconds;
            
            if (elapsed >= state.Frames[state.CurrentFrame].Duration)
            {
                state.CurrentFrame++;
                state.LastFrameTime = DateTime.Now;

                if (state.CurrentFrame >= state.Frames.Count)
                {
                    if (state.Loop)
                    {
                        state.CurrentFrame = 0;
                        System.Threading.Thread.Sleep(state.LoopDelay);
                    }
                    else
                    {
                        state.IsPlaying = false;
                        return null;
                    }
                }
            }

            return state.Frames[state.CurrentFrame];
        }

        public class AnimationState
        {
            public string NPC { get; set; }
            public List<AnimationFrame> Frames { get; set; }
            public int CurrentFrame { get; set; }
            public bool Loop { get; set; }
            public int LoopDelay { get; set; }
            public bool IsPlaying { get; set; }
            public DateTime LastFrameTime { get; set; }
        }
    }

    public class AnimationFrame
    {
        public string File { get; set; }
        public int Duration { get; set; }
        public List<string> Resolutions { get; set; }
    }
}
