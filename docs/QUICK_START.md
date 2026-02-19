# 快速开始指南

## 安装 HD Portraits Ultra

### 前提条件
- Stardew Valley 1.6 或更高版本
- SMAPI 3.0 或更高版本

### 安装步骤

1. **下载模组**
   - 从 [Releases](https://github.com/kiwimichelle/HDPortraits-Ultra/releases) 下载最新版本
   - 或从 [Nexus Mods](https://www.nexusmods.com/) 下载

2. **解压文件**
   ```
   Stardew Valley/
   └── Mods/
       └── HDPortraits-Ultra/
           ├── manifest.json
           ├── HDPortraitsUltra.dll
           ├── config.json
           └── ...
   ```

3. **启动游戏**
   - 启动 SMAPI
   - 等待模组加载完成

## 基础使用

### 打开菜单

在游戏中按 `P` 键打开头像切换菜单。

### 切换头像

1. 从左侧 NPC 列表选择一个角色
2. 从右侧查看该 NPC 的所有可用头像包
3. 点击你想使用的头像包
4. 点击 `Apply`（应用）按钮

✨ **更改立即生效，无需重启游戏！**

## 安装头像包

### 方式1：从模组管理器

1. 打开 Vortex 或其他模组管理器
2. 搜索 `HDPortraits` 头像包
3. 点击 Install（安装）

### 方式2：手动安装

1. 下载头像包（.zip 文件）
2. 解压到 `Stardew Valley/Mods/` 目录
3. 重启 SMAPI（或使用 `reload` 命令）

## 配置

编辑 `HDPortraits-Ultra/config.json`：

```json
{
  "Hotkey": "P",                    // 打开菜单的按键
  "EnableAnimations": true,         // 是否启用动画
  "AutoScaleResolution": true,      // 是否自动缩放分辨率
  "DefaultResolution": "4x",        // 默认显示分辨率
  "Language": "en"                  // 语言（en/zh）
}
```

## 第一个头像包

### 创建文件夹结构

```
MyPortraits/
├── manifest.json
├── portraits.json
└── assets/
    └── Sam/
        └── default/
            └── Sam_4x.png
```

### 创建 manifest.json

```json
{
  "Name": "My Portrait Pack",
  "Author": "YourName",
  "Version": "1.0.0",
  "Description": "Custom portraits",
  "UniqueID": "YourName.HDPortraits.MyPack",
  "ContentPackFor": {
    "UniqueID": "kiwimichelle.HDPortraits.Ultra"
  }
}
```

### 创建 portraits.json

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
          "resolutions": ["4x"],
          "conditions": {}
        }
      ]
    }
  }
}
```

### 测试

1. 将文件夹放入 `Mods/` 目录
2. 启动游戏
3. 按 `P` 打开菜单
4. 找到 Sam
5. 看你的新头像包是否出现

## 常见问题

### Q: 菜单快捷键不工作？
A: 检查 `config.json` 中的 `Hotkey` 是否正确设置。

### Q: 头像包没有显示？
A: 
- 检查 `manifest.json` 中的 `ContentPackFor` 是否正确
- 确保文件路径没有大小写错误
- 查看 SMAPI 日志是否有错误信息

### Q: 如何卸载？
A: 
- 从 `Mods/` 文件夹删除 `HDPortraits-Ultra` 文件夹
- 游戏会自动使用原版肖像

### Q: 可以同时使用多个头像包吗？
A: 完全可以！每个头像包都可以独立安装和切换。

## 下一步

- 📖 阅读 [头像包制作指南](./PORTRAIT_PACK_FORMAT.md)
- 🎨 学习 [条件系统](./CONDITION_SYSTEM.md)
- 🎬 探索 [动画制作](./ANIMATION_GUIDE.md)
- 📚 查看 [API 参考](./API.md)

---

需要帮助？提交 [Issue](https://github.com/kiwimichelle/HDPortraits-Ultra/issues)
