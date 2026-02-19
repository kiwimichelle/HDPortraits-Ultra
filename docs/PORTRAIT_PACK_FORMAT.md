# HD Portraits Ultra - Portrait Pack Format Specification

## 快速开始

创建一个头像包只需3步：

1. **创建文件夹结构**
2. **准备PNG肖像文件**
3. **编写JSON配置**

## 文件夹结构

```
HDPortraits-YourPackName/
├── manifest.json              # 必需：模组元数据
├── portraits.json             # 必需：头像声明文件
└── assets/
    └── Sam/
        ├── default/
        │   ├── Sam_2x.png
        │   ├── Sam_4x.png
        │   ├── Sam_8x.png
        │   └── metadata.json
        ├── happy/
        │   └── Sam_happy.png
        └── animations/
            └── blink_frames/
                ├── frame_0.png
                ├── frame_1.png
                └── ...
```

## manifest.json

```json
{
  "Name": "HDPortraits-Sam-PackA",
  "Author": "YourName",
  "Version": "1.0.0",
  "Description": "Ultra HD Sam portraits with animations",
  "UniqueID": "YourName.HDPortraits.Sam.PackA",
  "ContentPackFor": {
    "UniqueID": "kiwimichelle.HDPortraits.Ultra"
  }
}
```

## portraits.json 完整规范

### 基础结构

```json
{
  "version": "1.0",
  "packs": {
    "Sam": {
      "npc": "Sam",
      "defaultResolution": "4x",
      "portraits": [
        // Portrait definitions here
      ],
      "fallback": {
        // Fallback chain here
      }
    }
  }
}
```

### Portrait 定义详解

#### 1. 基础肖像

```json
{
  "id": "default",
  "baseFile": "assets/Sam/default/Sam",
  "resolutions": ["2x", "4x", "8x"],
  "conditions": {},
  "priority": 0,
  "metadata": {
    "offsetX": 0,
    "offsetY": 0,
    "scaleX": 1.0,
    "scaleY": 1.0,
    "cropMargin": 4,
    "preserveAspectRatio": true
  }
}
```

#### 2. 条件肖像（含多个条件）

```json
{
  "id": "happy_summer_highhearts",
  "baseFile": "assets/Sam/happy/Sam_summer_love",
  "resolutions": ["4x", "8x"],
  "conditions": {
    "emotion": "happy",
    "season": ["summer"],
    "hearts": {
      "min": 6,
      "max": 10
    }
  },
  "priority": 150,
  "metadata": {
    "offsetX": -5,
    "offsetY": 2,
    "scaleX": 1.0,
    "scaleY": 1.0
  }
}
```

#### 3. 动画肖像

```json
{
  "id": "animated_blink",
  "animation": {
    "type": "frameSequence",
    "frames": [
      {
        "file": "assets/Sam/animations/blink_frames/frame_0",
        "duration": 100,
        "resolutions": ["4x", "8x"]
      },
      {
        "file": "assets/Sam/animations/blink_frames/frame_1",
        "duration": 50
      },
      {
        "file": "assets/Sam/animations/blink_frames/frame_0",
        "duration": 100
      }
    ],
    "loop": true,
    "loopDelay": 3000,
    "triggers": {
      "onDialogue": true,
      "onEvent": [100, 200]
    }
  },
  "priority": 50
}
```

## 条件系统完整列表

| 条件字段 | 类型 | 说明 | 示例 |
|---------|------|------|------|
| `emotion` | string \| array | 情绪 | `"happy"` 或 `["happy", "neutral"]` |
| `season` | string \| array | 季节 | `["summer", "spring"]` |
| `weather` | string \| array | 天气 | `["rainy", "stormy"]` |
| `location` | string \| array | 地点 | `["farm", "town"]` |
| `hearts` | object | 好感度范围 | `{"min": 4, "max": 8}` |
| `eventId` | array | 事件ID | `[1234, 5678]` |
| `gameTime` | object | 游戏时间 | `{"min": 600, "max": 2600}` |
| `dayOfWeek` | string \| array | 星期几 | `["monday", "friday"]` |
| `isBirthday` | boolean | 是否生日 | `true` |
| `isMarried` | boolean | 是否已婚 | `true` |

## Fallback 链系统

```json
{
  "fallback": {
    "emotion": ["happy", "neutral", "default"],
    "weather": ["default"],
    "location": ["default"],
    "global": "default"
  }
}
```

**优先级链：**
1. 精确匹配所有条件
2. 按emotion fallback链匹配
3. 按season fallback链匹配
4. 按global fallback匹配
5. 使用全局default

## 分辨率支持

### 支持的分辨率
- `2x` - 96x96 像素（移动设备）
- `4x` - 192x192 像素（标准）
- `8x` - 384x384 像素（超高清）
- `custom` - 自定义分辨率

### 自适应裁剪

系统会自动裁剪中心区域，从高分辨率适配低分辨率显示。

```
原始肖像: 8x (384x384)
    ↓
需要 4x 显示
    ↓
自动中心裁剪 (192x192)
    ↓
应用于UI
```

## 动画帧配置

### 基础帧序列

```json
{
  "animation": {
    "type": "frameSequence",
    "frames": [
      {
        "file": "assets/Sam/anim/frame_0",
        "duration": 100
      },
      {
        "file": "assets/Sam/anim/frame_1",
        "duration": 150
      }
    ],
    "loop": true,
    "loopDelay": 2000
  }
}
```

### 帧参数

- `file` - 帧图片路径（无扩展名）
- `duration` - 显示时长（毫秒）
- `resolutions` - 该帧适用的分辨率（可选）

## Portrait 偏移和缩放

### Metadata 字段

```json
{
  "metadata": {
    "offsetX": -10,              // 水平偏移（像素，负数向左）
    "offsetY": 5,                // 垂直偏移（像素，负数向上）
    "scaleX": 1.05,              // 水平缩放比例
    "scaleY": 0.98,              // 垂直缩放比例
    "cropMargin": 4,             // 自适应裁剪边距（像素）
    "preserveAspectRatio": true  // 保持宽高比
  }
}
```

## Content Patcher NPC 支持

```json
{
  "id": "cp_npc",
  "baseFile": "assets/CustomNPC/portrait",
  "conditions": {
    "isContentPatcherNPC": true,
    "npcName": "CustomNPC"
  }
}
```

## 命名规范

### 文件命名

- 肖像文件: `{NPC}_{emotion}_{condition}.png`
- 动画帧: `frame_{index}.png`
- 示例: `Sam_happy_summer.png`, `Abigail_sad_winter.png`

### ID 命名

- 使用小写 + 下划线: `happy_summer_highhearts`
- 避免特殊字符

## 完整示例

```json
{
  "version": "1.0",
  "packs": {
    "Sam": {
      "npc": "Sam",
      "defaultResolution": "4x",
      "portraits": [
        {
          "id": "default",
          "baseFile": "assets/Sam/default/Sam",
          "resolutions": ["2x", "4x", "8x"],
          "conditions": {},
          "priority": 0,
          "metadata": {
            "offsetX": 0,
            "offsetY": 0,
            "scaleX": 1.0,
            "scaleY": 1.0
          }
        },
        {
          "id": "happy_summer",
          "baseFile": "assets/Sam/happy/Sam_summer",
          "resolutions": ["4x", "8x"],
          "conditions": {
            "emotion": "happy",
            "season": ["summer"],
            "hearts": { "min": 4, "max": 8 }
          },
          "priority": 100
        },
        {
          "id": "rainy_sad",
          "baseFile": "assets/Sam/sad/Sam_rainy",
          "resolutions": ["4x"],
          "conditions": {
            "weather": ["rainy", "stormy"],
            "emotion": "sad"
          },
          "priority": 80
        },
        {
          "id": "blink_animation",
          "animation": {
            "type": "frameSequence",
            "frames": [
              { "file": "assets/Sam/animations/blink/frame_0", "duration": 100 },
              { "file": "assets/Sam/animations/blink/frame_1", "duration": 50 },
              { "file": "assets/Sam/animations/blink/frame_0", "duration": 100 }
            ],
            "loop": true,
            "loopDelay": 3000
          },
          "priority": 10
        }
      ],
      "fallback": {
        "emotion": ["happy", "neutral", "default"],
        "weather": ["default"],
        "global": "default"
      }
    },
    "Abigail": {
      "npc": "Abigail",
      "defaultResolution": "4x",
      "portraits": [
        {
          "id": "default",
          "baseFile": "assets/Abigail/default/Abigail",
          "resolutions": ["4x", "8x"],
          "conditions": {}
        }
      ],
      "fallback": {
        "global": "default"
      }
    }
  }
}
```

## 常见问题

### Q: 我的PNG文件需要多大？
A: 建议至少 384x384 (8x)，系统会自动缩放到较小分辨率。

### Q: 可以不提供所有分辨率吗？
A: 可以。提供 4x 即可，系统会自动生成缩略版本。

### Q: 条件不匹配会怎样？
A: 系统按 fallback 链寻找最接近的匹配头像。

### Q: 可以混合多个 NPC 吗？
A: 可以。一个包可以包含多个 NPC 的头像。

### Q: 对Content Patcher NPC 的支持如何？
A: 完全支持。在conditions中设置 `isContentPatcherNPC: true` 即可。

---

更多信息见 GitHub: https://github.com/kiwimichelle/HDPortraits-Ultra
