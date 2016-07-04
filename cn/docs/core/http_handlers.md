在ZKWeb控制器只能处理固定的Url，如果需要灵活处理Url可以使用Http处理器。<br/>
控制器(ControllerManager)也是基于IHttpRequestHandler实现的。<br/>

### Http请求处理器的例子

以下的处理器可以在访问`/example/handler/xxx`时显示"the path is xxx"
``` csharp
[ExportMany]
public class HttpRequestHandlerExample : IHttpRequestHandler {
	public const string Prefix = "/example/handler/";
	public void OnRequest() {
		var request = HttpManager.CurrentContext.Request;
		var response = HttpManager.CurrentContext.Response;
		if (request.Path.StartsWith(Prefix)) {
			new PlainResult("the path is " + request.Path.Substring(Prefix.Length))
				.WriteResponse(response);
			response.End();
		}
	}
}
```

### Http错误处理器的例子

以下的处理器可以定义自定义的404页面
``` csharp
[ExportMany]
public class HttpErrorHandlerExample : IHttpRequestErrorHandler {
	public void OnError(Exception ex) {
		var httpEx = ex as HttpException; // HttpException in ZKWebStandard.Web
		if (httpEx != null && httpEx.StatusCode == 404) {
			var response = HttpManager.CurrentContext.Response;
			new PlainResult("custom 404 page").WriteResponse(response);
			response.End();
		}
	}
}
```

### Http处理器的介绍

在ZKWeb中，有以下类型的Http处理器：<br/>

- IHttpRequestPreHandler
	- Http请求的预处理器
	- 这类处理器不应该返回结果，只应该用来设置数据，例如当前请求的语言
- IHttpRequestHandler
	- Http请求的处理器
	- 这类处理器在请求的路径匹配时可以直接返回结果
- IHttpRequestErrorHandler
	- 请求错误的处理器
	- 用于处理请求中发生的错误，例如自定义404页面

ZKWeb处理Http请求的流程如下<br/>
``` csharp
try {
	foreach (按插件注册顺序调用预处理器) {
		调用IHttpRequestPreHandler.OnRequest
	}
	foreach (按插件注册顺序反序调用处理器) {
		调用IHttpRequestHandler.OnRequest
	}
	throw new HttpException(404, "Not Found");
} catch (代表请求结束的例外) {
} catch (Exception ex) {
	foreach (按插件注册顺序反序调用错误处理器) {
		调用IHttpRequestErrorHandler.OnError
	}
}
```
