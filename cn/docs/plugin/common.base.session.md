基础插件提供的提供了独自编写的会话管理功能，不依赖于Asp.Net和Asp.Net Core。<br/>
考虑到网站可能会部署到多个服务器，会话保存在数据库。<br/>

### 获取和保存会话

获取和保存会话需要使用`SessionManager`。<br/>
调用`SaveSession`可以保存之前使用`GetSession`获取的会话。<br/>

**获取和保存会话的例子**<br/>
添加`src\Controllers\SessionExampleController.cs`<br/>
``` csharp
[ExportMany]
public class SessionExampleController : IController {
	[Action("example/get_session")]
	public IActionResult GetSession() {
		var sessionManager = Application.Ioc.Resolve<SessionManager>();
		var session = sessionManager.GetSession();
		return new JsonResult(session, Formatting.Indented);
	}

	[Action("example/save_session")]
	public string SaveSession() {
		var sessionManager = Application.Ioc.Resolve<SessionManager>();
		var session = sessionManager.GetSession();
		session.Items["ExampleKey"] = DateTime.UtcNow;
		sessionManager.SaveSession();
		return "success";
	}
}
```

会话有过期时间，需要延长过期时间时可以使用`SetExpiresAtLeast`函数。<br/>
以下代码设置会话最少有效一个小时<br/>
``` csharp
session.SetExpiresAtLeast(TimeSpan.FromHours(1));
```