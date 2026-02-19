using StardewValley;
using System;
using System.Collections.Generic;

namespace HDPortraitsUltra
{
    public class ResolutionScaler
    {
        private readonly IModHelper helper;

        public ResolutionScaler(IModHelper helper)
        {
            this.helper = helper;
        }

        /// <summary>
        /// 获取最优分辨率
        /// </summary>
        public string GetOptimalResolution(List<string> availableResolutions)
        {
            // 优先级: 8x > 4x > 2x
            if (availableResolutions.Contains("8x"))
                return "8x";
            if (availableResolutions.Contains("4x"))
                return "4x";
            if (availableResolutions.Contains("2x"))
                return "2x";
            
            return availableResolutions.Count > 0 ? availableResolutions[0] : "4x";
        }

        /// <summary>
        /// 自动裁剪高分辨率到目标分辨率
        /// </summary>
        public void AutoCropToResolution(string sourceResolution, string targetResolution)
        {
            var cropRatio = this.GetCropRatio(sourceResolution, targetResolution);
            // 实现裁剪逻辑
        }

        private float GetCropRatio(string from, string to)
        {
            var fromSize = this.ParseResolution(from);
            var toSize = this.ParseResolution(to);
            return (float)toSize / fromSize;
        }

        private int ParseResolution(string resolution)
        {
            return resolution switch
            {
                "2x" => 96,
                "4x" => 192,
                "8x" => 384,
                _ => 192
            };
        }
    }
}
