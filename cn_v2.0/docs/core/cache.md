ZKWeb提供了标准的缓存接口, 可以统一缓存的操作.<br/>
默认缓存对象都保存在内存中, 可以自己实现`ICacheFactory`来支持分布式的缓存.<br/>
缓存接口的类型是`IKeyValueCache<TKey, TValue>`, 通过`ICacheFactory`可以创建新的缓存对象.

``` csharp
public interface IKeyValueCache<TKey, TValue> {
	void Put(TKey key, TValue value, TimeSpan keepTime);
	bool TryGetValue(TKey key, out TValue value);
	void Remove(TKey key);
	int Count();
	void Clear();
}
```

### 创建缓存

创建缓存需要调用`ICacheFactory.CreateCache`, 可以指定创建的参数`CacheFactoryOptions`<br/>
`CacheFactoryOptions.Lifetime`为缓存的生命周期, 有持久和跟随Http请求销毁.<br/>
`CacheFactoryOptions.IsolationPolicies`为隔离策略, 请参考下面的说明.

``` csharp
var cacheFactory = Application.Ioc.Resolve<ICacheFactory>();
var cache = cacheFactory.CreateCache<int, string>();
```

### 缓存的隔离

ZKWeb提供了一套隔离缓存的机制, 可以按当前的上下文隔离缓存.<br/>
例如缓存翻译结果时, 一般应该使用`cache[当前语言][原文] = [译文]`, <br/>
但使用缓存隔离功能后, 可以直接使用`cache[原文] = [译文]`.<br/>

### 创建带隔离功能的缓存

以下创建了根据当前请求的语言和Url进行隔离的缓存.<br/>
``` csharp
var cacheFactory = Application.Ioc.Resolve<ICacheFactory>();
var cache = cacheFactory.CreateCache<string, string>(
	CacheFactoryOptions.Default.WithIsolationPolicies("Url", "Locale"));
cache.Put(path, renderResult, TimeSpan.FromSeconds(100));
```

### 自定义隔离策略

继承`ICacheIsolationPolicy`并且注册到容器可以实现自定义的缓存策略.<br/>
缓存策略类似于Http协议中的`Vary`, 可以根据不同的请求参数区分缓存的内容.<br/>
这里根据Http头中的Accept隔离.<br/>
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
	- 按当前请求的地址, 参数和表单参数隔离缓存
