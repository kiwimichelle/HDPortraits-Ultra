namespace HDPortraitsUltra
{
    public class ModConfig
    {
        public string Hotkey { get; set; } = "P";
        public bool EnableAnimations { get; set; } = true;
        public bool AutoScaleResolution { get; set; } = true;
        public string DefaultResolution { get; set; } = "4x";
        public bool PreloadPortraits { get; set; } = true;
        public int CacheSize { get; set; } = 512;
        public string Language { get; set; } = "en";
        public int MaxAnimationFrames { get; set; } = 50;
        public int AnimationLoopDelay { get; set; } = 3000;
    }
}
