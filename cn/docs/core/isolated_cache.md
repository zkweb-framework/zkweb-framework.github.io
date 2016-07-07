ZKWeb提供了一套隔离缓存的机制，可以简化缓存存取的操作。<br/>
例如缓存翻译结果时，一般应该使用`cache[当前语言][原文] = [译文]`，<br/>
但使用缓存隔离功能后，可以直接使用`cache[原文] = [译文]`。<br/>

### 缓存隔离的使用

隔离缓存的类型是`IsolatedMemoryCache<Key, Value>`，初始化时需要传入隔离策略。<br/>
以下创建了根据当前请求的语言和Url进行隔离的缓存。<br/>
``` csharp
var cache = new IsolatedMemoryCache<string, string>("Url", "Locale");
cache.Put(path, renderResult, TimeSpan.FromSeconds(100));
```

### 自定义隔离策略

继承`ICacheIsolationPolicy`并且注册到容器可以实现自定义的缓存策略。<br/>
缓存策略类似于Http协议中的`Vary`，可以根据不同的请求参数区分缓存的内容。<br/>
这里根据Http头中的Accept隔离。<br/>
``` csharp
[ExportMany(ContractKey = "Custom")]
public class CustomPolicy : ICacheIsolationPolicy {
	public object GetIsolationKey() {
		return HttpManager.CurrentContext.Request.GetHeader("Accept");
	}
}
```

### 提供的缓存隔离策略

框架默认提供了以下的缓存隔离策略<br/>

- `Device`
	- 按当前请求的设备(电脑或手机)隔离缓存
- `Locale`
	- 按当前的语言和时区隔离缓存
- `Url`
	- 按当前请求的地址，参数和表单参数隔离缓存
