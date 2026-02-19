# HD Portraits Ultra - API 参考

## 概述

HD Portraits Ultra 提供了一个简洁的 API，允许其他模组与之交互。

## 导入模组

```csharp
var hdPortraits = helper.ModRegistry.GetApi<IHDPortraitsAPI>("kiwimichelle.HDPortraits.Ultra");
```

## API 接口

### IHDPortraitsAPI

```csharp
公共 interface IHDPortraitsAPI
{
    /// <summary>
    /// 获取 NPC 当前使用的头像包
    /// </summary>
    string GetNPCCurrentPack(string npcName);

    /// <summary>
    /// 设置 NPC 使用的头像包
    /// </summary>
    void SetNPCPortraitPack(string npcName, string packName);

    /// <summary>
    /// 获取 NPC 所有可用的头像包
    /// </summary>
    List<string> GetAvailablePacksForNPC(string npcName);

    /// <summary>
    /// 刷新所有头像
    /// </summary>
    void RefreshPortraits();

    /// <summary>
    /// 播放动画
    /// </summary>
    void PlayAnimation(string npcName, string animationId);

    /// <summary>
    /// 停止动画
    /// </summary>
    void StopAnimation(string npcName);

    /// <summary>
    /// 注册自定义条件评估器
    /// </summary>
    void RegisterConditionEvaluator(string conditionKey, Func<object, bool> evaluator);
}
```

## 使用示例

### 示例1：获取当前头像包

```csharp
var currentPack = hdPortraits.GetNPCCurrentPack("Sam");
this.Monitor.Log($"Sam's current portrait pack: {currentPack}", LogLevel.Info);
```

### 示例2：切换头像包

```csharp
hdPortraits.SetNPCPortraitPack("Sam", "Sam_PackA");
hdPortraits.RefreshPortraits();
```

### 示例3：列出可用头像包

```csharp
var packs = hdPortraits.GetAvailablePacksForNPC("Abigail");
foreach (var pack in packs)
{
    this.Monitor.Log($"Available pack: {pack}", LogLevel.Info);
}
```

### 示例4：播放动画

```csharp
hdPortraits.PlayAnimation("Sam", "blink_animation");
```

### 示例5：注册自定义条件

```csharp
hdPortraits.RegisterConditionEvaluator("customWeather", (value) =>
{
    // 自定义天气评估逻辑
    return Game1.weatherForTomorrow == Game1.weather_snow;
});
```

## 头像包规范（供其他模组创建包）

### manifest.json

```json
{
  "Name": "My Custom Portraits",
  "Author": "YourName",
  "Version": "1.0.0",
  "Description": "Custom portrait pack for HD Portraits Ultra",
  "UniqueID": "YourName.HDPortraits.Custom",
  "ContentPackFor": {
    "UniqueID": "kiwimichelle.HDPortraits.Ultra"
  }
}
```

### portraits.json

详见 `PORTRAIT_PACK_FORMAT.md`

## 事件系统（计划中）

未来将支持以下事件：

```csharp
// 当头像包被切换时
helper.Events.GenericEvents.ExecuteWhenWorldReady(() =>
{
    // 监听头像包切换事件
});
```

## 最佳实践

### 1. 版本检查

```csharp
if (helper.ModRegistry.IsLoaded("kiwimichelle.HDPortraits.Ultra"))
{
    var hdPortraits = helper.ModRegistry.GetApi<IHDPortraitsAPI>(
        "kiwimichelle.HDPortraits.Ultra");
    
    if (hdPortraits != null)
    {
        // 使用 API
    }
}
```

### 2. 错误处理

```csharp
try
{
    hdPortraits.SetNPCPortraitPack("Sam", "NonExistentPack");
}
catch (ArgumentException ex)
{
    this.Monitor.Log($"Portrait pack not found: {ex.Message}", LogLevel.Warn);
}
```

### 3. 延迟调用

在游戏完全加载后再调用 API：

```csharp
helper.Events.GameLoop.SaveLoaded += (s, e) =>
{
    hdPortraits.RefreshPortraits();
};
```

## 常见问题

### Q: 如何检查 HD Portraits Ultra 是否已安装？

```csharp
bool isInstalled = helper.ModRegistry.IsLoaded("kiwimichelle.HDPortraits.Ultra");
```

### Q: 可以创建运行时头像包吗？

不推荐。建议通过 Content Patcher 或创建独立的模组包。

### Q: 如何调试 API 调用？

启用 SMAPI 的调试模式，使用 `this.Monitor.Log()` 输出日志。

---

更多帮助见 GitHub: https://github.com/kiwimichelle/HDPortraits-Ultra
