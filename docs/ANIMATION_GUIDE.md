# 动画制作完整指南

## 快速开始

### 步骤1：准备动画帧

创建多个PNG文件作为动画帧。建议分辨率：384x384 (8x)

```
assets/Sam/animations/blink/
├── frame_0.png
├── frame_1.png
├── frame_2.png
└── ...
```

### 步骤2：配置 portraits.json

```json
{
  "id": "blink_animation",
  "animation": {
    "type": "frameSequence",
    "frames": [
      {
        "file": "assets/Sam/animations/blink/frame_0",
        "duration": 100
      },
      {
        "file": "assets/Sam/animations/blink/frame_1",
        "duration": 50
      },
      {
        "file": "assets/Sam/animations/blink/frame_0",
        "duration": 100
      }
    ],
    "loop": true,
    "loopDelay": 3000
  }
}
```

### 步骤3：测试

放入游戏的 `Mods/` 目录，启动游戏查看效果。

## 动画类型

### frameSequence（帧序列）- 最常用

**说明:** 按顺序播放多个图像帧

```json
{
  "type": "frameSequence",
  "frames": [
    { "file": "path/to/frame_0", "duration": 100 },
    { "file": "path/to/frame_1", "duration": 150 },
    { "file": "path/to/frame_2", "duration": 100 }
  ],
  "loop": true,
  "loopDelay": 2000
}
```

**参数：**
- `frames` - 帧列表
- `loop` - 是否循环播放
- `loopDelay` - 循环延迟（毫秒）

## 帧参数详解

### duration（帧持续时间）

每一帧显示的时长（毫秒）

```json
{
  "file": "path/to/frame",
  "duration": 100  // 显示 100 毫秒
}
```

**推荐值：**
- 快速动画（眨眼）: 50-100ms
- 正常动画（说话）: 100-150ms
- 缓慢动画（转身）: 150-300ms

### resolutions（特定分辨率）

指定该帧适用的分辨率。如果不指定，所有分辨率使用同一帧。

```json
{
  "file": "path/to/frame_4x",
  "duration": 100,
  "resolutions": ["4x", "8x"]
}
```

## 常见动画

### 1. 眨眼动画

```json
{
  "id": "blink",
  "animation": {
    "type": "frameSequence",
    "frames": [
      { "file": "assets/Sam/animations/blink/open", "duration": 100 },
      { "file": "assets/Sam/animations/blink/half", "duration": 50 },
      { "file": "assets/Sam/animations/blink/closed", "duration": 80 },
      { "file": "assets/Sam/animations/blink/half", "duration": 50 },
      { "file": "assets/Sam/animations/blink/open", "duration": 100 }
    ],
    "loop": true,
    "loopDelay": 4000
  }
}
```

### 2. 说话动画

```json
{
  "id": "talk",
  "animation": {
    "type": "frameSequence",
    "frames": [
      { "file": "assets/Sam/animations/talk/mouth_closed", "duration": 100 },
      { "file": "assets/Sam/animations/talk/mouth_open", "duration": 100 },
      { "file": "assets/Sam/animations/talk/mouth_wide", "duration": 100 },
      { "file": "assets/Sam/animations/talk/mouth_open", "duration": 100 }
    ],
    "loop": true,
    "loopDelay": 0
  },
  "conditions": {
    "isDialogue": true
  }
}
```

### 3. 心情变化动画

```json
{
  "id": "happy_jump",
  "animation": {
    "type": "frameSequence",
    "frames": [
      { "file": "assets/Sam/animations/happy/frame_0", "duration": 50 },
      { "file": "assets/Sam/animations/happy/frame_1", "duration": 50 },
      { "file": "assets/Sam/animations/happy/frame_2", "duration": 100 },
      { "file": "assets/Sam/animations/happy/frame_1", "duration": 50 },
      { "file": "assets/Sam/animations/happy/frame_0", "duration": 50 }
    ],
    "loop": true,
    "loopDelay": 2000
  },
  "conditions": {
    "emotion": "happy"
  }
}
```

## 高级技巧

### 1. 分辨率特定帧

为不同分辨率提供不同的帧：

```json
{
  "frames": [
    {
      "file": "assets/Sam/blink/high_res_frame",
      "duration": 100,
      "resolutions": ["8x"]
    },
    {
      "file": "assets/Sam/blink/standard_frame",
      "duration": 100,
      "resolutions": ["4x", "2x"]
    }
  ]
}
```

### 2. 条件触发的动画

只在特定条件下播放：

```json
{
  "id": "winter_shiver",
  "animation": { /* ... */ },
  "conditions": {
    "season": "winter",
    "weather": ["snow", "stormy"]
  },
  "priority": 200
}
```

### 3. 多个动画组合

```json
{
  "portraits": [
    {
      "id": "happy_talk",
      "baseFile": "assets/Sam/happy",
      "animation": {
        "type": "frameSequence",
        "frames": [
          { "file": "assets/Sam/happy_talk/frame_0", "duration": 80 },
          { "file": "assets/Sam/happy_talk/frame_1", "duration": 80 }
        ],
        "loop": true
      },
      "conditions": {
        "emotion": "happy",
        "isDialogue": true
      },
      "priority": 250
    },
    {
      "id": "happy_idle",
      "baseFile": "assets/Sam/happy",
      "animation": {
        "type": "frameSequence",
        "frames": [
          { "file": "assets/Sam/happy_idle/frame_0", "duration": 200 },
          { "file": "assets/Sam/happy_idle/frame_1", "duration": 200 }
        ],
        "loop": true,
        "loopDelay": 1000
      },
      "conditions": {
        "emotion": "happy"
      },
      "priority": 100
    }
  ]
}
```

## 性能最佳实践

### 推荐配置

- **帧数上限:** 50帧（可在 config.json 配置）
- **帧尺寸:** 最大 384x384 (8x)
- **总缓存:** 512MB（可配置）

### 优化建议

1. **使用适当的持续时间**
   - ❌ 过短: 低于 50ms 会显示过快
   - ✅ 推荐: 80-150ms

2. **减少帧数**
   - ❌ 避免超过 20 帧
   - ✅ 使用循环而非多帧

3. **重用帧**
   ```json
   {
     "frames": [
       { "file": "frame_0", "duration": 100 },
       { "file": "frame_1", "duration": 100 },
       { "file": "frame_0", "duration": 100 }  // 重用 frame_0
     ]
   }
   ```

## 故障排除

### 动画不播放
- 检查文件路径是否正确
- 确保文件名没有大小写错误
- 验证 PNG 文件格式有效

### 动画卡顿
- 减少帧数
- 增加帧间隔（duration）
- 检查文件大小是否过大

### 内存占用过高
- 减少预加载的肖像数量
- 降低目标分辨率
- 清理旧的动画文件

---

更多帮助见 GitHub: https://github.com/kiwimichelle/HDPortraits-Ultra# 动画制作完整指南

## 快速开始

### 步骤1：准备动画帧

创建多个PNG文件作为动画帧。建议分辨率：384x384 (8x)

```
assets/Sam/animations/blink/
├── frame_0.png
├── frame_1.png
├── frame_2.png
└── ...
```

### 步骤2：配置 portraits.json

```json
{
  "id": "blink_animation",
  "animation": {
    "type": "frameSequence",
    "frames": [
      {
        "file": "assets/Sam/animations/blink/frame_0",
        "duration": 100
      },
      {
        "file": "assets/Sam/animations/blink/frame_1",
        "duration": 50
      },
      {
        "file": "assets/Sam/animations/blink/frame_0",
        "duration": 100
      }
    ],
    "loop": true,
    "loopDelay": 3000
  }
}
```

### 步骤3：测试

放入游戏的 `Mods/` 目录，启动游戏查看效果。

## 动画类型

### frameSequence（帧序列）- 最常用

**说明:** 按顺序播放多个图像帧

```json
{
  "type": "frameSequence",
  "frames": [
    { "file": "path/to/frame_0", "duration": 100 },
    { "file": "path/to/frame_1", "duration": 150 },
    { "file": "path/to/frame_2", "duration": 100 }
  ],
  "loop": true,
  "loopDelay": 2000
}
```

**参数：**
- `frames` - 帧列表
- `loop` - 是否循环播放
- `loopDelay` - 循环延迟（毫秒）

## 帧参数详解

### duration（帧持续时间）

每一帧显示的时长（毫秒）

```json
{
  "file": "path/to/frame",
  "duration": 100  // 显示 100 毫秒
}
```

**推荐值：**
- 快速动画（眨眼）: 50-100ms
- 正常动画（说话）: 100-150ms
- 缓慢动画（转身）: 150-300ms

### resolutions（特定分辨率）

指定该帧适用的分辨率。如果不指定，所有分辨率使用同一帧。

```json
{
  "file": "path/to/frame_4x",
  "duration": 100,
  "resolutions": ["4x", "8x"]
}
```

## 常见动画

### 1. 眨眼动画

```json
{
  "id": "blink",
  "animation": {
    "type": "frameSequence",
    "frames": [
      { "file": "assets/Sam/animations/blink/open", "duration": 100 },
      { "file": "assets/Sam/animations/blink/half", "duration": 50 },
      { "file": "assets/Sam/animations/blink/closed", "duration": 80 },
      { "file": "assets/Sam/animations/blink/half", "duration": 50 },
      { "file": "assets/Sam/animations/blink/open", "duration": 100 }
    ],
    "loop": true,
    "loopDelay": 4000
  }
}
```

### 2. 说话动画

```json
{
  "id": "talk",
  "animation": {
    "type": "frameSequence",
    "frames": [
      { "file": "assets/Sam/animations/talk/mouth_closed", "duration": 100 },
      { "file": "assets/Sam/animations/talk/mouth_open", "duration": 100 },
      { "file": "assets/Sam/animations/talk/mouth_wide", "duration": 100 },
      { "file": "assets/Sam/animations/talk/mouth_open", "duration": 100 }
    ],
    "loop": true,
    "loopDelay": 0
  },
  "conditions": {
    "isDialogue": true
  }
}
```

### 3. 心情变化动画

```json
{
  "id": "happy_jump",
  "animation": {
    "type": "frameSequence",
    "frames": [
      { "file": "assets/Sam/animations/happy/frame_0", "duration": 50 },
      { "file": "assets/Sam/animations/happy/frame_1", "duration": 50 },
      { "file": "assets/Sam/animations/happy/frame_2", "duration": 100 },
      { "file": "assets/Sam/animations/happy/frame_1", "duration": 50 },
      { "file": "assets/Sam/animations/happy/frame_0", "duration": 50 }
    ],
    "loop": true,
    "loopDelay": 2000
  },
  "conditions": {
    "emotion": "happy"
  }
}
```

## 高级技巧

### 1. 分辨率特定帧

为不同分辨率提供不同的帧：

```json
{
  "frames": [
    {
      "file": "assets/Sam/blink/high_res_frame",
      "duration": 100,
      "resolutions": ["8x"]
    },
    {
      "file": "assets/Sam/blink/standard_frame",
      "duration": 100,
      "resolutions": ["4x", "2x"]
    }
  ]
}
```

### 2. 条件触发的动画

只在特定条件下播放：

```json
{
  "id": "winter_shiver",
  "animation": { /* ... */ },
  "conditions": {
    "season": "winter",
    "weather": ["snow", "stormy"]
  },
  "priority": 200
}
```

### 3. 多个动画组合

```json
{
  "portraits": [
    {
      "id": "happy_talk",
      "baseFile": "assets/Sam/happy",
      "animation": {
        "type": "frameSequence",
        "frames": [
          { "file": "assets/Sam/happy_talk/frame_0", "duration": 80 },
          { "file": "assets/Sam/happy_talk/frame_1", "duration": 80 }
        ],
        "loop": true
      },
      "conditions": {
        "emotion": "happy",
        "isDialogue": true
      },
      "priority": 250
    },
    {
      "id": "happy_idle",
      "baseFile": "assets/Sam/happy",
      "animation": {
        "type": "frameSequence",
        "frames": [
          { "file": "assets/Sam/happy_idle/frame_0", "duration": 200 },
          { "file": "assets/Sam/happy_idle/frame_1", "duration": 200 }
        ],
        "loop": true,
        "loopDelay": 1000
      },
      "conditions": {
        "emotion": "happy"
      },
      "priority": 100
    }
  ]
}
```

## 性能最佳实践

### 推荐配置

- **帧数上限:** 50帧（可在 config.json 配置）
- **帧尺寸:** 最大 384x384 (8x)
- **总缓存:** 512MB（可配置）

### 优化建议

1. **使用适当的持续时间**
   - ❌ 过短: 低于 50ms 会显示过快
   - ✅ 推荐: 80-150ms

2. **减少帧数**
   - ❌ 避免超过 20 帧
   - ✅ 使用循环而非多帧

3. **重用帧**
   ```json
   {
     "frames": [
       { "file": "frame_0", "duration": 100 },
       { "file": "frame_1", "duration": 100 },
       { "file": "frame_0", "duration": 100 }  // 重用 frame_0
     ]
   }
   ```

## 故障排除

### 动画不播放
- 检查文件路径是否正确
- 确保文件名没有大小写错误
- 验证 PNG 文件格式有效

### 动画卡顿
- 减少帧数
- 增加帧间隔（duration）
- 检查文件大小是否过大

### 内存占用过高
- 减少预加载的肖像数量
- 降低目标分辨率
- 清理旧的动画文件

---

更多帮助见 GitHub: https://github.com/kiwimichelle/HDPortraits-Ultra
