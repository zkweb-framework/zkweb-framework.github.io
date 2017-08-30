ZKWeb支持多时区功能。<br/>

### 设置当前线程的时区

设置指定时区
``` csharp
LocaleUtils.SetThreadTimezone("Asia/Shanghai");
```

自动设置时区（Cookies => 默认时区）
``` csharp
LocaleUtils.SetThreadTimezoneAutomatic("America/New_York");
```

更多支持的时区可以[查看源代码](https://github.com/zkweb-framework/ZKWeb/blob/master/ZKWeb/ZKWebStandard/Utils/LocaleUtils.cs)

### 根据时区转换时间

转换到客户端的本地时间，`ToClientTime`是一个扩展函数。<br/>
如果客户端指定了时区则使用该时区，否则使用服务器本地时间。<br/>
``` csharp
var utcTime = DateTime.UtcNow;
var clientTime = utcTime.ToClientTime();
```

从客户端时间转换到utc时间，常用于解析客户端的时间控件提交的值。
``` csharp
var clientTime = DateTime.Parse(timeParam);
var utcTime = clientTime.FromClientTime();
```
