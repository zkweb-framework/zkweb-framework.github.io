插件包含了通用性高的最基本的功能和资源，可以满足大部分网站的需求。<br/>
在示例项目中使用这个插件时请先引用`Common.Base\bin\Common.Base.dll`，否则VS会提示错误（但不影响ZKWeb加载）。<br/>

----------------------------------------------------------

### 通用配置

通用配置可以保存网站全局使用的参数，例如网站名称和默认语言等。<br/>
添加`Example\src\config\ExampleConfig.cs`，内容如下<br/>
``` csharp
/// <summary>
/// 示例配置
/// </summary>
[GenericConfig("Example.ExampleConfig", CacheTime = 15)]
public class ExampleConfig {
	public string ExampleName { get; set; }
	public int ExampleCount { get; set; }
}
```

配置需要添加`GenericConfig`属性，第一个参数是保存到数据库时使用的键名，CacheTime可以指定缓存时间（秒）。<br/>
缓存时间不会影响到网站部署到单个进程时的读取，单个进程时读取配置总能读到最新的配置。<br/>
但会影响网站部署到多个进程或服务器时的读取，缓存时间不应该指定太长。<br/>

读取和写入配置<br/>
注意首次获取`ExampleConfig`或之前保存了null值时会返回一个新的实例，GetData不会返回null。<br/>
在`ExampleController`下添加以下内容<br/>
``` csharp
[Action("example/read_config")]
public string ReadConfig() {
	var configManager = Application.Ioc.Resolve<GenericConfigManager>();
	var config = configManager.GetData<ExampleConfig>();
	return JsonConvert.SerializeObject(config);
}

[Action("example/write_config")]
public string WriteConfig() {
	var configManager = Application.Ioc.Resolve<GenericConfigManager>();
	var config = configManager.GetData<ExampleConfig>();
	config.ExampleName = "updated";
	config.ExampleCount++;
	configManager.PutData(config);
	return "success";
}
```

### 定时任务

定时任务可以用于在网站后台执行定时处理。<br/>
注意网站被IIS回收后，将不会定时执行这些任务（其他的网站内嵌定时任务框架也一样），<br/>
如果需要定时任务必须在指定的时间运行，请设置IIS程序池常驻或使用独立的进程处理。<br/>
定时任务已考虑到部署到多个服务器时的情况，部署到多个服务器仍可以保证任务不被重复执行。<br/>

添加`Example\src\ScheduledTasks\ExampleTask.cs`，内容如下<br/>
这个任务每15分钟写入一次日志<br/>
``` csharp
/// <summary>
/// 示例任务
/// </summary>
[ExportMany, SingletonReuse]
public class ExampleTask : IScheduledTaskExecutor {
	public string Key { get { return "Example.ExampleTask"; } }

	public bool ShouldExecuteNow(DateTime lastExecuted) {
		return ((DateTime.UtcNow - lastExecuted).TotalMinutes > 15);
	}

	public void Execute() {
		var logManager = Application.Ioc.Resolve<LogManager>();
		logManager.LogDebug("Example task executed");
	}
}
```

### 会话

考虑到网站部署到多个服务器，会话应该保存到数据库中。<br/>
会话会在当前请求期间共享同一个对象，但不会缓存到这个期间外。<br/>

获取和保存会话<br/>
在`ExampleController`下添加以下内容<br/>
调用`SaveSession`可以保存之前使用`GetSession`获取的会话。<br/>
``` csharp
[Action("example/get_session")]
public string GetSession() {
	var sessionManager = Application.Ioc.Resolve<SessionManager>();
	var session = sessionManager.GetSession();
	return JsonConvert.SerializeObject(session, Formatting.Indented);
}

[Action("example/save_session")]
public string SaveSession() {
	var sessionManager = Application.Ioc.Resolve<SessionManager>();
	var session = sessionManager.GetSession();
	session.Items["ExampleKey"] = DateTime.UtcNow;
	sessionManager.SaveSession();
	return "success";
}
```

会话有过期时间，需要延长过期时间时可以使用`SetExpiresAtLeast`函数。<br/>
以下代码设置会话最少有效一个小时<br/>
``` csharp
session.SetExpiresAtLeast(TimeSpan.FromHours(1));
```

### css样式

这个插件使用了以下css样式，具体用法可以参考他们的官网<br/>

- Bootstrap 3.3.2
- AdminLTE 2.3.0

### js脚本

这个插件使用了以下js脚本，具体用法可以参考他们的官网<br/>

- jQuery 1.11.2
	- jquery-migrate
	- jquery-form
	- jquery-mobile
	- jquery-toast
	- jquery-validate
	- jquery-validate-unobtrusive
- Bootstrap 3.3.2
	- context-menu
	- dialog
	- hover-dropdown
- Switchery 0.8.1
- jsUri 1.3.1
- Underscore 1.8.3

这个插件还带了很多零碎的附加功能，具体可以参考`Common.Base\static\common.base.js\custom`。

### 图标字体

这个插件使用了以下图标字体，具体用法可以参考他们的官网<br/>

- Font Awesome 4.5.0

### 表格构建器

表格构建器可以用于构建从远程载入内容的Ajax表格，并带分页等支持。<br/>
使用例子<br/>

编写中

### 表格搜索栏构建器

### 表单构建器

### 数据库操作类
(GenericRepository, UnitOfWork)<br/>

### 前台首页

### 前台头部和底部

### 静态文件处理器

### 语言时区处理器
