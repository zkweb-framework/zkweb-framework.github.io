ZKWeb提供了一套独立实现的MVC框架, 不依赖于Asp.Net和Asp.Net Core.<br/>
自己实现MVC框架的好处是可以不受微软技术更新换代的影响,<br/>
即使以后微软淘汰了Asp.Net Core, ZKWeb也可以迅速兼容新的框架并且无需改动业务代码.<br/>

### 控制器的示例

这是一个简单的控制器:

``` csharp
[ExportMany]
public class ExampleController : IController {
	public IActionResult PlainText() {
		// 返回文本
		return new PlainResult("some plain text");
	}

	public string PlainString() {
		// 返回文本，返回类型是string时会自动使用PlainResult包装
		return "some plain string";
	}

	public object Json(string name, int age) {
		// 有参数时会自动获取传入参数
		// 返回json，返回类型不是IActionResult或string时会自动使用JsonResult包装
		return new { name, age };
	}

	public IActionResult Template() {
		// 返回模板
		return new TemplateResult("zkweb.examples/hello.html", new { text = "World" });
	}

	public IActionResult File() {
		// 返回文件
		var fileStorage = Application.Ioc.Resolve<IFileStorage>();
		var file = fileStorage.GetResourceFile("static", "1.txt");
		return new FileEntryResult(file);
	}
}
```

建立后在浏览器打开`http://localhost:端口/Example/PlainText`即可看到内容.

在ZKWeb中添加控制器只需要继承`IController`接口并标记`ExportMany`即可,<br/>
控制器和其他组件一样支持依赖注入, 您可以在构造函数中写需要的服务.<br/>

### 控制访问路径

通过在控制器类上标记`[ActionBase]`属性， 或者在方法上标记`[Action]`属性可以控制访问路径.<br/>
例如:

``` c#
[ExportMany]
public class ExampleController : IController {
	// 不标记[ActionBase]也不标记[Action]
	// 访问路径是 "/Example/PlainText"
	public IActionResult PlainText() {
		return new PlainResult("some plain text");
	}
}
```

``` c#
[ExportMany]
[ActionBase("/MyExample")]
public class ExampleController : IController {
	// 标记[ActionBase]但不标记[Action]
	// 访问路径是 "/MyExample/PlainText"
	public IActionResult PlainText() {
		return new PlainResult("some plain text");
	}
}
```

``` c#
[ExportMany]
[ActionBase("/MyExample")]
public class ExampleController : IController {
	// 同时标记[ActionBase]和[Action]
	// 访问路径是 "/MyExample/MyPlainText"
	[Action("MyPlainText")]
	public IActionResult PlainText() {
		return new PlainResult("some plain text");
	}
}
```

``` c#
[ExportMany]
public class ExampleController : IController {
	// 不标记[ActionBase], 只标记[Action] (兼容2.0之前的版本)
	// [Action]标记的就是完整路径
	// 访问路径是 "/MyPlainText"
	[Action("/MyPlainText")]
	public IActionResult PlainText() {
		return new PlainResult("some plain text");
	}
}
```

### Action的返回类型

Action的返回类型可以是`IActionResult`, `string`或者其他引用类型.<br/>
如果返回类型是string, 则会自动包装为`PlainResult`,<br/>
如果返回类型是其他引用类型(例如`SomeOutputDto`), 则会自动包装为`JsonResult`.<br/>

ZKWeb内置了以下的返回类型,</br>
如果需要返回其他类型的结果, 可以自己编写继承`IActionResult`的类.</br>

- 返回文件
	- `FileEntryResult(IFileEntry fileEntry, DateTime? ifModifiedSince = null)`
- 返回图片
	- `ImageResult(Image image, ImageFormat format = null)`
- 返回Json序列化的结果
	- `JsonResult(object obj, Formatting formatting = Formatting.None)`
- 返回纯文本
	- `PlainResult(object obj)`
- 重定向到指定地址
	- `RedirectResult(string url, bool permanent = false)`
- 返回数据流
	- `StreamResult(Stream stream, string contentType = null)`
- 返回模板
	- `TemplateResult(string path, object argument = null)`

### 获取传入参数

如果action函数带有参数, 会自动按参数的名称进行获取.<br/>
需要手动获取时可以使用`HttpManager.CurrentContext.Request.Get<T>`<br/>
下面两种写法可以获取到一样的参数<br/>

``` csharp
[ExportMany]
public class PostExampleController : IController {
	[Action("example/post_a", HttpMethods.POST)]
	public IActionResult PostA(string paramInUrl, string paramInBody, string name, int age) {
		return new JsonResult(new { name, age });
	}
	
	[Action("example/post_b", HttpMethods.POST)]
	public IActionResult PostB() {
		var request = HttpManager.CurrentContext.Request;
		var paramInUrl = request.GetQueryValue("paramInUrl");
		var paramInBody = request.GetFormValue("paramInBody");
		var name = request.Get<string>("name");
		var age = request.Get<int>("age");
		return new JsonResult(new { name, age });
	}
}
```

和`Asp.Net`不一样, 您不需要指定`[FromQuery]`等属性, ZKWeb会自动根据以下的规则获取:<br/>

- 从自定义参数获取(HttpManager.CurrentContext.Request.CustomParameters)
- 从表单内容获取
- 从Url参数获取
- 从Json Body中的单个字段获取(例如传入`{a:1}`, 参数a的值等于1)
- 从提交文件获取(类型需要是IHttpPostedFile)
- 从Json Body整体获取(例如传入`{a:1}`, 可以绑到`class A {int a;}`上)

除此之外您还可以替换默认的`IActionParameterProvider`实现自己的获取逻辑,<br/>
也可以在里面实现自动验证参数.<br/>
参考`MVVMDemo`的[`ValidatedActionParameterProvider`](https://github.com/zkweb-framework/ZKWeb.MVVMDemo/blob/master/src/ZKWeb.MVVMPlugins/MVVM.Common.Base/src/Components/ActionParameterProviders/ValidatedActionParameterProvider.cs).

### 通过Url传入参数

从ZKWeb 2.0开始, Action支持通过Url传入参数, 例如:<br/>

``` csharp
[ExportMany]
public class PostExampleController : IController {
	[Action("example/get/{id}")]
	public IActionResult PostA(int id) {
		return new JsonResult(new { id });
	}
}
```

访问`http://localhost:端口/example/get/1`时, `id`参数的值将会是`1`.

### 替换现有的Action

如果您想替换在其他插件中定义的页面, 可以使用`OverrideExists`属性, 例如:<br/>

``` csharp
[Action("/", OverrideExists = true)]
public IActionResult CustomIndexPage() {
	return new PlainResult("hello overridden action");
}
```

要注意使用`OverrideExists`的插件必须在原来的插件之后加载,<br/>
也就是要放在`App_Data\config.json`的`Plugins`列表的后面.

### Action过滤器

ZKWeb支持在Action的前后执行自定义的代码, 可以看作是一种Aop机制.<br/>
Action过滤器的接口的定义如下:<br/>

``` csharp
public interface IActionFilter {
	Func<IActionResult> Filter(Func<IActionResult> action);
}
```

过滤器分`全局过滤器`和`属性过滤器`,<br/>
全局过滤器对所有Action都有效, 属性过滤器只对标记的Action有效.

添加全局过滤器需要继承`IActionFilter`并标记`[ExportMany]`, 例如:<br/>

``` csharp
[ExportMany]
public class MyActionFilter : IActionFilter {
	public Func<IActionResult> Filter(Func<IActionResult> action) {
		var logManager = Application.Ioc.Resolve<LogManager>();
		return () => {
			var path = HttpManager.CurrentContext.Request.Path;
			logManager.LogDebug("received request: " + path);
			return action();
		};
	}
}
```

添加属性过滤器需要继承`ActionFilterAttribute`, 但不能标记`[ExportMany]`, 例如:<br/>

``` csharp
public class MyActionFilterAttribute : ActionFilterAttribute {
	public override Func<IActionResult> Filter(Func<IActionResult> action) {
		var logManager = Application.Ioc.Resolve<LogManager>();
		return () => {
			var path = HttpManager.CurrentContext.Request.Path;
			logManager.LogDebug("received request: " + path);
			return action();
		};
	}
}
```

使用时在Action上标记即可, 例如:<br/>

``` csharp
[ExportMany]
public class ExampleController : IController {
	[MyActionFilter]
	[Action("example/plain_text")]
	public IActionResult PlainText() {
		return new PlainResult("some plain text");
	}
}
```

### 忽略大小写

ZKWeb默认会区分请求路径的大小写, 如果你不想区分可以添加以下的选项到`App_Data\config.json`:

``` text
{
	"Extra": { "ZKWeb.DisableCaseSensitiveRouting": true }
}
```
