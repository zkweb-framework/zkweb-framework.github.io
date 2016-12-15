ZKWeb使用了独自编写的控制器和模板系统，不依赖于Asp.Net Webform和Mvc。<br/>
ZKWeb提供了对Http的抽象封装，不依赖于Asp.Net和Asp.Net Core。<br/>
获取当前Http上下文应该使用`HttpManager.CurrentContext`。<br/>

### 和Asp.Net Mvc控制器的区别

ZKWeb的控制器不拥有状态，不像Mvc的控制器会带当前Http上下文和路由信息。<br/>
ZKweb的控制器不决定路径，路径需要通过各个函数的Action属性指定，Action属性可以指定完整的Url。<br/>

### 控制器的示例

Action属性需要指定完整的Url，Url前如果没有"/"会自动补上。</br>
`[ExportMany]`属性在[IoC容器](ioc_container)提到过，用于注册组件到全局的容器。<br/>
``` csharp
[ExportMany]
public class ExampleController : IController {
	[Action("example/plain_text")]
	public IActionResult PlainText() {
		// 返回文本
		return new PlainResult("some plain text");
	}

	[Action("example/plain_string")]
	public string PlainString() {
		// 返回文本，返回类型是string时会自动使用PlainResult包装
		return "some plain string";
	}
	
	[Action("example/json")]
	public object Json(string name, int age) {
		// 有参数时会自动获取传入参数
		// 返回json，返回类型不是IActionResult或string时会自动使用JsonResult包装
		return new { name, age };
	}

	[Action("example/template")]
	public IActionResult Template() {
		// 返回模板
		return new TemplateResult("zkweb.examples/hello.html", new { text = "World" });
	}

	[Action("example/file")]
	public IActionResult File() {
		// 返回文件
		return new FileResult("D:\\1.txt");
	}
}
```
![控制器的示例](../img/controller_example.jpg)

### 提供的返回类型

ZKWeb提供了以下的返回类型，</br>
如果需要返回其他类型的结果，可以自己编写继承`IActionResult`的类。</br>

- 返回文件
	- `FileResult(string path, DateTime? ifModifiedSince = null)`
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

如果action函数带有参数，会自动按参数的名称进行获取。<br/>
需要手动获取时可以使用`HttpManager.CurrentContext.Request.Get<T>`。

``` csharp
[ExportMany]
public class PostExampleController : IController {
	[Action("example/post", HttpMethods.POST)]
	public IActionResult Post() {
		var request = HttpManager.CurrentContext.Request;
		var paramInUrl = request.GetQueryValue("paramInUrl");
		var paramInBody = request.GetFormValue("paramInBody");
		var name = request.Get<string>("name");
		var age = request.Get<int>("age");
		return new JsonResult(new { name, age });
	}
}
```
![获取传入参数的示例](../img/post_controller_example.jpg)

### 重载Action

ZKWeb支持替换现有的Action，例如添加一个插件把原有的页面处理全部重写。<br/>
替换Action时需要指定Action属性的OverrideExists参数。<br/>
替换Action的插件加载顺序需要在原插件的后面（且不能是同一个插件）。<br/>
``` csharp
[Action("/", OverrideExists = true)]
public IActionResult CustomIndexPage() {
	return new PlainResult("hello overridden action");
}
```

### Action过滤器

Action过滤器的接口的定义如下
``` csharp
public interface IActionFilter {
	Func<IActionResult> Filter(Func<IActionResult> action);
}
```
过滤器可以分为全局过滤器和属性过滤器，<br/>
全局过滤器对所有Action有效，<br/>
属性过滤器只对标记的Action有效。<br/>

添加全局过滤器可以继承IActionFilter并标记[ExportMany]注册到容器中。<br/>
添加属性过滤器可以继承ActionFilterAttribute并标记继承的属性到Action上。<br/>
