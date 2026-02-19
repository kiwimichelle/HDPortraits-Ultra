using System;
using System.Collections.Generic;
using System.Linq;

namespace HDPortraitsUltra
{
    public class FallbackResolver
    {
        /// <summary>
        /// 根据 fallback 链找到最接近的匹配肖像
        /// </summary>
        public Portrait ResolveFallback(
            List<Portrait> availablePortraits,
            Dictionary<string, object> originalConditions,
            Dictionary<string, List<string>> fallbackChain)
        {
            // 1. 尝试精确匹配
            var exact = this.TryExactMatch(availablePortraits, originalConditions);
            if (exact != null)
                return exact;

            // 2. 按 fallback 链逐步降级
            foreach (var fallback in fallbackChain)
            {
                var modified = new Dictionary<string, object>(originalConditions);
                
                foreach (var fallbackValue in fallback.Value)
                {
                    modified[fallback.Key] = fallbackValue;
                    var match = this.TryExactMatch(availablePortraits, modified);
                    if (match != null)
                        return match;
                }
            }

            // 3. 返回全局默认肖像
            return availablePortraits.FirstOrDefault(p => p.Conditions == null || p.Conditions.Count == 0);
        }

        private Portrait TryExactMatch(List<Portrait> portraits, Dictionary<string, object> conditions)
        {
            // 找到满足条件的最高优先级肖像
            var matches = portraits
                .Where(p => this.ConditionsMatch(p.Conditions, conditions))
                .OrderByDescending(p => p.Priority)
                .FirstOrDefault();

            return matches;
        }

        private bool ConditionsMatch(Dictionary<string, object> portraitConditions, Dictionary<string, object> checkConditions)
        {
            if (portraitConditions == null || portraitConditions.Count == 0)
                return true;

            foreach (var condition in portraitConditions)
            {
                if (!checkConditions.ContainsKey(condition.Key))
                    return false;

                if (!this.ValueMatches(condition.Value, checkConditions[condition.Key]))
                    return false;
            }

            return true;
        }

        private bool ValueMatches(object expected, object actual)
        {
            if (expected == null)
                return true;

            if (expected is List<string> list)
                return list.Any(v => v.ToString() == actual?.ToString());

            return expected.ToString() == actual?.ToString();
        }
    }
}
