ZKWeb支持多时区功能。<br/>
时区依赖于当前系统中的时区列表，而不同平台上的时区列表有不同的名称，跨平台时需要注意这点。<br/>

### 设置当前线程的时区

设置指定时区
``` csharp
LocaleUtils.SetThreadTimezone("China Standard Time");
```

自动设置时区（Cookies => 默认时区）
``` csharp
LocaleUtils.SetThreadTimezoneAutomatic("GMT Standard Time");
```

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
