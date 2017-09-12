基础插件提供的提供了独自编写的会话管理功能, 不依赖于Asp.Net和Asp.Net Core.<br/>
考虑到网站可能会部署到多个服务器, 会话保存在数据库.<br/>

### 会话的数据结构

![会话的ER图](../images/plugins/common.base.session.jpg)

会话中可以保存自定义的数据, 自定义的数据使用json存储.

### 获取和保存会话

获取和保存会话需要使用`SessionManager`.<br/>
调用`SaveSession`可以保存之前使用`GetSession`获取的会话.<br/>

**获取和保存会话的例子**<br/>
添加`src\Controllers\SessionExampleController.cs`<br/>
``` csharp
[ExportMany]
public class SessionExampleController : ControllerBase {
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

### 过期时间

会话有过期时间, 需要延长过期时间时请使用`SetExpiresAtLeast`函数.<br/>
这个函数确保保存会话时, 浏览器中的过期时间也会相应延长.<br/>
以下代码设置会话最少有效一个小时<br/>
``` csharp
session.SetExpiresAtLeast(TimeSpan.FromHours(1));
```

### 关联用户

会话可以关联用户, 关联时需要设置用户Id到`ReleatedId`成员.<br/>
基础插件不提供用户功能, 所以只留了关联`Id`的成员, 成员的类型是`Guid`.<br/>

### 会话Id的储存

会话Id默认保存在Cookies中, 如果有需要请注册自定义的`ISessionIdStore`.
