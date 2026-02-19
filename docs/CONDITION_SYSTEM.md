# 条件系统完整文档

## 概述

HD Portraits Ultra 的条件系统允许头像在特定游戏条件下自动切换，而无需玩家手动操作。

## 条件类型

### 1. 情绪（Emotion）

**字段名:** `emotion`  
**类型:** `string` 或 `array`  
**可用值:** `happy`, `sad`, `neutral`, `angry`, `love`, `surprise`

```json
{
  "conditions": {
    "emotion": "happy"
  }
}
```

或使用数组（满足任意一个）：

```json
{
  "conditions": {
    "emotion": ["happy", "love"]
  }
}
```

### 2. 季节（Season）

**字段名:** `season`  
**类型:** `string` 或 `array`  
**可用值:** `spring`, `summer`, `fall`, `winter`

```json
{
  "conditions": {
    "season": ["summer", "spring"]
  }
}
```

### 3. 天气（Weather）

**字段名:** `weather`  
**类型:** `string` 或 `array`  
**可用值:** `sunny`, `rainy`, `snow`, `stormy`, `hail`

```json
{
  "conditions": {
    "weather": ["rainy", "stormy"]
  }
}
```

### 4. 地点（Location）

**字段名:** `location`  
**类型:** `string` 或 `array`  
**可用值:** `farm`, `town`, `mines`, `skull_cavern`, `beach`, `forest`, `mountain`, 等

```json
{
  "conditions": {
    "location": ["mines", "skull_cavern"]
  }
}
```

### 5. 好感度（Hearts）

**字段名:** `hearts`  
**类型:** `object`  
**字段:** `min` (最小值), `max` (最大值)  
**范围:** 0-10 (8 heart 为最满)

```json
{
  "conditions": {
    "hearts": {
      "min": 4,
      "max": 8
    }
  }
}
```

### 6. 事件ID（Event ID）

**字段名:** `eventId`  
**类型:** `array`  
**说明:** 特定事件触发时显示该肖像

```json
{
  "conditions": {
    "eventId": [1234, 5678, 9012]
  }
}
```

### 7. 游戏时间（Game Time）

**字段名:** `gameTime`  
**类型:** `object`  
**字段:** `min` (最早时间), `max` (最晚时间)  
**格式:** 游戏时间（600 = 6:00AM, 2600 = 26:00 = 2:00AM）

```json
{
  "conditions": {
    "gameTime": {
      "min": 600,
      "max": 2200
    }
  }
}
```

### 8. 星期几（Day Of Week）

**字段名:** `dayOfWeek`  
**类型:** `string` 或 `array`  
**可用值:** `monday`, `tuesday`, `wednesday`, `thursday`, `friday`, `saturday`, `sunday`

```json
{
  "conditions": {
    "dayOfWeek": ["saturday", "sunday"]
  }
}
```

### 9. 生日（Is Birthday）

**字段名:** `isBirthday`  
**类型:** `boolean`  
**说明:** NPC生日时显示

```json
{
  "conditions": {
    "isBirthday": true
  }
}
```

### 10. 已婚（Is Married）

**字段名:** `isMarried`  
**类型:** `boolean`  
**说明:** NPC已与玩家结婚时显示

```json
{
  "conditions": {
    "isMarried": true
  }
}
```

## 条件组合逻辑

### AND 逻辑

多个条件字段之间是 **AND** 关系：

```json
{
  "conditions": {
    "emotion": "happy",           // AND
    "season": "summer",           // AND
    "hearts": { "min": 6, "max": 10 }
  }
}
```

**结果:** 只有当 **同时满足** 所有三个条件时，才显示该肖像。

### OR 逻辑

单个条件字段内的多个值是 **OR** 关系：

```json
{
  "conditions": {
    "weather": ["rainy", "stormy", "snow"]
  }
}
```

**结果:** 当天气为 **任意一个** 时显示该肖像。

## 优先级系统（Priority）

当多个头像满足条件时，系统按 `priority` 从高到低选择。

```json
[
  {
    "id": "happy_summer_highhearts",
    "conditions": {
      "emotion": "happy",
      "season": "summer",
      "hearts": { "min": 8, "max": 10 }
    },
    "priority": 300
  },
  {
    "id": "happy_summer",
    "conditions": {
      "emotion": "happy",
      "season": "summer"
    },
    "priority": 100
  },
  {
    "id": "default",
    "conditions": {},
    "priority": 0
  }
]
```

**选择顺序：**
1. 优先匹配 priority 300 的头像（最具体）
2. 如果不匹配，尝试 priority 100
3. 最后使用 priority 0 的默认头像

## Fallback 链（降级系统）

当条件不完全匹配时，系统会按 fallback 链查找替代方案。

```json
{
  "fallback": {
    "emotion": ["happy", "neutral", "default"],
    "weather": ["sunny", "default"],
    "location": ["default"],
    "global": "default"
  }
}
```

**降级流程示例：**

假设需要显示 `emotion: "angry"` 的肖像：

1. 查找 `emotion: "angry"` 的头像 → 找不到
2. 按 fallback 链尝试：
   - 寻找 `emotion: "happy"` → 找不到
   - 寻找 `emotion: "neutral"` → 找不到
   - 寻找 `emotion: "default"` → 找到！使用

## 实践示例

### 示例 1：季节性头像

```json
{
  "id": "summer_beach_outfit",
  "baseFile": "assets/Sam/summer_beach",
  "conditions": {
    "season": "summer",
    "location": "beach"
  },
  "priority": 200
}
```

**触发条件:** 当前季节是夏季 **且** NPC在海滩

### 示例 2：时间限制的表情

```json
{
  "id": "sleepy_morning",
  "baseFile": "assets/Sam/sleepy",
  "conditions": {
    "gameTime": {
      "min": 400,
      "max": 700
    },
    "emotion": "sad"
  },
  "priority": 150
}
```

**触发条件:** 游戏时间在 4:00AM - 7:00AM 之间 **且** 情绪是悲伤

### 示例 3：节日特殊头像

```json
{
  "id": "festival_special",
  "baseFile": "assets/Sam/festival",
  "conditions": {
    "eventId": [20, 34, 55]
  },
  "priority": 500
}
```

**触发条件:** 发生了特定事件

### 示例 4：多季节多天气

```json
{
  "id": "stormy_autumn",
  "baseFile": "assets/Sam/stormy",
  "conditions": {
    "season": ["fall", "winter"],
    "weather": ["stormy", "snow"]
  },
  "priority": 250
}
```

**触发条件:** 季节是秋冬 **且** 天气是暴雨或下雪

### 示例 5：好感度阶段

```json
[
  {
    "id": "love_max",
    "baseFile": "assets/Sam/love_max",
    "conditions": {
      "hearts": { "min": 10, "max": 10 }
    },
    "priority": 300
  },
  {
    "id": "love_high",
    "baseFile": "assets/Sam/love_high",
    "conditions": {
      "hearts": { "min": 7, "max": 9 }
    },
    "priority": 200
  },
  {
    "id": "love_medium",
    "baseFile": "assets/Sam/love_medium",
    "conditions": {
      "hearts": { "min": 4, "max": 6 }
    },
    "priority": 100
  }
]
```

## 性能优化

### 条件评估缓存

系统会缓存条件评估结果，以提高性能：

```
游戏时间变化
    ↓
检查是否有条件需要重新评估
    ↓
仅评估受影响的条件
    ↓
使用缓存加速
```

### 最佳实践

1. **避免过于复杂的条件组合**
   - ❌ 避免 10+ 个条件字段
   - ✅ 使用优先级而非过度嵌套

2. **合理设置 priority**
   - ❌ 避免相似优先级堆积
   - ✅ 使用 0, 100, 200, 300 等间隔清晰的值

3. **使用 fallback 链**
   - 确保总有一个默认肖像可用
   - 避免显示黑屏

## 调试条件

### 查看当前条件状态

在游戏中按 `P` 打开菜单时，会显示当前评估的条件：

```
当前条件:
- 季节: summer
- 天气: sunny
- 时间: 1200 (12:00PM)
- 情绪: happy
- 好感度: 5
- 地点: farm
```

### 日志输出

模组会在 SMAPI 日志中输出条件评估结果（仅在调试模式）。

---

更多帮助见 GitHub: https://github.com/kiwimichelle/HDPortraits-Ultra
