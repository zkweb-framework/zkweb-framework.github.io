ZKWeb内置了多时区的支持, 可以让不同的客户端显示不同的时间.<br/>

### 设置当前线程的时区

设置指定时区:

``` csharp
LocaleUtils.SetThreadTimezone("Asia/Shanghai");
```

自动设置时区(Cookies => 默认时区):

``` csharp
LocaleUtils.SetThreadTimezoneAutomatic("America/New_York");
```

更多支持的时区可以[查看源代码](https://github.com/zkweb-framework/ZKWeb/blob/master/ZKWeb/ZKWebStandard/Utils/LocaleUtils.cs)

### 根据时区转换时间

转换到客户端的本地时间, `ToClientTime`是一个扩展函数.<br/>
如果客户端指定了时区则使用该时区, 否则使用服务器本地时间.

``` csharp
var utcTime = DateTime.UtcNow;
var clientTime = utcTime.ToClientTime();
```

从客户端时间转换到utc时间, 常用于解析客户端的时间控件提交的值.

``` csharp
var clientTime = DateTime.Parse(timeParam);
var utcTime = clientTime.FromClientTime();
```

### 数据库中保存的时间

如果需要使用多时区支持, 请在数据库中保存UTC时间,<br/>
例如获取当前时间使用`DateTime.UtcNow`而不是`DateTime.Now`.<br/>

从数据库取出的时间应该使用`ToClientTime`转换后发给客户端.<br/>
从客户端接收的时间应该使用`FromClientTime`转换后保存到数据库.<br/>
