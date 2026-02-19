# HD Portraits Ultra

🎨 **超高分辨率多风格头像系统 | Stardew Valley**

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![SMAPI](https://img.shields.io/badge/SMAPI-3.0%2B-blue.svg)](https://smapi.io)

## ✨ 核心特性

- 🎬 **超高分辨率支持** - 2x / 4x / 8x / 任意分辨率
- 🔄 **自动缩放适配** - 智能自适应 UI 显示
- 📦 **多头像包同时存在** - 同一 NPC 支持无限头像包变体
- ⚡ **游戏内即时切换** - 无需重启游戏，立刻生效
- 🎮 **游戏内菜单** - 按键或菜单快速切换
- 🎨 **表情动画系统** - 支持帧序列动画
- 📋 **高级条件系统** - 天气/季节/时间/事件触发
- 📱 **游戏内菜单 UI** - 美观的切换界面
- ✅ **完全原版兼容** - 卸载自动回退
- 🔌 **Content Patcher 兼容** - 支持 CP NPC

## 🚀 快速开始

### 安装

1. 下载最新版本
2. 解压到 `Stardew Valley/Mods/` 目录
3. 启动游戏

### 使用

1. 按 `P` 键打开头像切换菜单
2. 选择 NPC
3. 选择你喜欢的头像包
4. 点击应用即刻生效 ✨

## 📖 文档

- **[头像包制作指南](./docs/PORTRAIT_PACK_FORMAT.md)** - 如何制作头像包
- **[条件系统文档](./docs/CONDITION_SYSTEM.md)** - 条件触发详解
- **[动画制作指南](./docs/ANIMATION_GUIDE.md)** - 动画创建教程
- **[API 参考](./docs/API.md)** - 模组 API 接口

## 📦 纯资源包制作

HD Portraits Ultra 的头像包完全是**纯资源**，无需任何编程！

### 文件夹结构

```
MyPortraitPack/
├── manifest.json              ← 模组声明
├── portraits.json             ← 头像配置
└── assets/
    └── NpcName/
        ├── default/
        │   └── NpcName_4x.png
        ├── happy/
        │   └── NpcName_happy.png
        └── animations/
            └── blink_frames/
                ├── frame_0.png
                └── frame_1.png
```

### manifest.json

```json
{
  "Name": "My Portrait Pack",
  "Author": "YourName",
  "Version": "1.0.0",
  "Description": "Custom portraits for HD Portraits Ultra",
  "UniqueID": "YourName.HDPortraits.MyPack",
  "ContentPackFor": {
    "UniqueID": "kiwimichelle.HDPortraits.Ultra"
  }
}
```

### portraits.json

```json
{
  "version": "1.0",
  "packs": {
    "Sam": {
      "npc": "Sam",
      "portraits": [
        {
          "id": "default",
          "baseFile": "assets/Sam/default/Sam",
          "resolutions": ["4x", "8x"],
          "conditions": {}
        }
      ]
    }
  }
}
```

## 🎮 配置

编辑 `config.json`：

```json
{
  "Hotkey": "P",                    // 打开菜单的快捷键
  "EnableAnimations": true,         // 启用动画
  "AutoScaleResolution": true,      // 自动缩放分辨率
  "DefaultResolution": "4x",        // 默认分辨率
  "PreloadPortraits": true,         // 预加载头像
  "CacheSize": 512,                 // 缓存大小 (MB)
  "Language": "en",                 // 语言
  "MaxAnimationFrames": 50,         // 最大动画帧数
  "AnimationLoopDelay": 3000        // 动画循环延迟 (ms)
}
```

## 💡 高级功能

### 条件肖像

根据游戏条件自动切换头像：

```json
{
  "id": "happy_summer",
  "baseFile": "assets/Sam/happy_summer",
  "conditions": {
    "emotion": "happy",
    "season": "summer",
    "hearts": { "min": 6, "max": 10 }
  }
}
```

### 动画肖像

支持帧序列动画：

```json
{
  "id": "blink",
  "animation": {
    "type": "frameSequence",
    "frames": [
      { "file": "assets/Sam/blink/frame_0", "duration": 100 },
      { "file": "assets/Sam/blink/frame_1", "duration": 50 }
    ],
    "loop": true,
    "loopDelay": 3000
  }
}
```

### Fallback 链

智能降级系统确保总是有肖像显示：

```json
{
  "fallback": {
    "emotion": ["happy", "neutral", "default"],
    "weather": ["sunny", "default"],
    "global": "default"
  }
}
```

## 🔧 系统需求

- **Stardew Valley 1.6+**
- **SMAPI 3.0+**
- **Windows / Mac / Linux**

## 🤝 贡献

欢迎提交 PR 和 Issue！

## 📝 许可证

MIT License - 详见 [LICENSE](./LICENSE)

## 🎯 路线图

- [ ] 游戏内肖像预览
- [ ] 肖像包热加载
- [ ] 性格头像自动匹配
- [ ] 多语言 UI 支持
- [ ] 更多条件类型

## 💬 支持

遇到问题？

1. 查看 [常见问题](./docs/FAQ.md)
2. 搜索 [已有 Issue](https://github.com/kiwimichelle/HDPortraits-Ultra/issues)
3. 创建新 Issue 并详细描述问题

## 📧 联系

- 作者: kiwimichelle
- GitHub: [@kiwimichelle](https://github.com/kiwimichelle)

---

**Made with ❤️ for Stardew Valley modding community**# HD Portraits Ultra

🎨 **超高分辨率多风格头像系统 | Stardew Valley**

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![SMAPI](https://img.shields.io/badge/SMAPI-3.0%2B-blue.svg)](https://smapi.io)

## ✨ 核心特性

- 🎬 **超高分辨率支持** - 2x / 4x / 8x / 任意分辨率
- 🔄 **自动缩放适配** - 智能自适应 UI 显示
- 📦 **多头像包同时存在** - 同一 NPC 支持无限头像包变体
- ⚡ **游戏内即时切换** - 无需重启游戏，立刻生效
- 🎮 **游戏内菜单** - 按键或菜单快速切换
- 🎨 **表情动画系统** - 支持帧序列动画
- 📋 **高级条件系统** - 天气/季节/时间/事件触发
- 📱 **游戏内菜单 UI** - 美观的切换界面
- ✅ **完全原版兼容** - 卸载自动回退
- 🔌 **Content Patcher 兼容** - 支持 CP NPC

## 🚀 快速开始

### 安装

1. 下载最新版本
2. 解压到 `Stardew Valley/Mods/` 目录
3. 启动游戏

### 使用

1. 按 `P` 键打开头像切换菜单
2. 选择 NPC
3. 选择你喜欢的头像包
4. 点击应用即刻生效 ✨

## 📖 文档

- **[头像包制作指南](./docs/PORTRAIT_PACK_FORMAT.md)** - 如何制作头像包
- **[条件系统文档](./docs/CONDITION_SYSTEM.md)** - 条件触发详解
- **[动画制作指南](./docs/ANIMATION_GUIDE.md)** - 动画创建教程
- **[API 参考](./docs/API.md)** - 模组 API 接口

## 📦 纯资源包制作

HD Portraits Ultra 的头像包完全是**纯资源**，无需任何编程！

### 文件夹结构

```
MyPortraitPack/
├── manifest.json              ← 模组声明
├── portraits.json             ← 头像配置
└── assets/
    └── NpcName/
        ├── default/
        │   └── NpcName_4x.png
        ├── happy/
        │   └── NpcName_happy.png
        └── animations/
            └── blink_frames/
                ├── frame_0.png
                └── frame_1.png
```

### manifest.json

```json
{
  "Name": "My Portrait Pack",
  "Author": "YourName",
  "Version": "1.0.0",
  "Description": "Custom portraits for HD Portraits Ultra",
  "UniqueID": "YourName.HDPortraits.MyPack",
  "ContentPackFor": {
    "UniqueID": "kiwimichelle.HDPortraits.Ultra"
  }
}
```

### portraits.json

```json
{
  "version": "1.0",
  "packs": {
    "Sam": {
      "npc": "Sam",
      "portraits": [
        {
          "id": "default",
          "baseFile": "assets/Sam/default/Sam",
          "resolutions": ["4x", "8x"],
          "conditions": {}
        }
      ]
    }
  }
}
```

## 🎮 配置

编辑 `config.json`：

```json
{
  "Hotkey": "P",                    // 打开菜单的快捷键
  "EnableAnimations": true,         // 启用动画
  "AutoScaleResolution": true,      // 自动缩放分辨率
  "DefaultResolution": "4x",        // 默认分辨率
  "PreloadPortraits": true,         // 预加载头像
  "CacheSize": 512,                 // 缓存大小 (MB)
  "Language": "en",                 // 语言
  "MaxAnimationFrames": 50,         // 最大动画帧数
  "AnimationLoopDelay": 3000        // 动画循环延迟 (ms)
}
```

## 💡 高级功能

### 条件肖像

根据游戏条件自动切换头像：

```json
{
  "id": "happy_summer",
  "baseFile": "assets/Sam/happy_summer",
  "conditions": {
    "emotion": "happy",
    "season": "summer",
    "hearts": { "min": 6, "max": 10 }
  }
}
```

### 动画肖像

支持帧序列动画：

```json
{
  "id": "blink",
  "animation": {
    "type": "frameSequence",
    "frames": [
      { "file": "assets/Sam/blink/frame_0", "duration": 100 },
      { "file": "assets/Sam/blink/frame_1", "duration": 50 }
    ],
    "loop": true,
    "loopDelay": 3000
  }
}
```

### Fallback 链

智能降级系统确保总是有肖像显示：

```json
{
  "fallback": {
    "emotion": ["happy", "neutral", "default"],
    "weather": ["sunny", "default"],
    "global": "default"
  }
}
```

## 🔧 系统需求

- **Stardew Valley 1.6+**
- **SMAPI 3.0+**
- **Windows / Mac / Linux**

## 🤝 贡献

欢迎提交 PR 和 Issue！

## 📝 许可证

MIT License - 详见 [LICENSE](./LICENSE)

## 🎯 路线图

- [ ] 游戏内肖像预览
- [ ] 肖像包热加载
- [ ] 性格头像自动匹配
- [ ] 多语言 UI 支持
- [ ] 更多条件类型

## 💬 支持

遇到问题？

1. 查看 [常见问题](./docs/FAQ.md)
2. 搜索 [已有 Issue](https://github.com/kiwimichelle/HDPortraits-Ultra/issues)
3. 创建新 Issue 并详细描述问题

## 📧 联系

- 作者: kiwimichelle
- GitHub: [@kiwimichelle](https://github.com/kiwimichelle)

---

**Made with ❤️ for Stardew Valley modding community**
