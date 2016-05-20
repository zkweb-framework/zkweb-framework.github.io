ZKWeb核心框架支持多语言和多时区，<br/>
语言的翻译由`ITranslateProvider`实现。<br/>
每个线程都可以指定不同的语言和时区。<br/>
使用插件`Common.Base`时可以自动设置当前线程语言和时区的机制，请参考该插件的文档。<br/>

### <h2>添加翻译</h2>

添加`Example\src\Translates\zh_CN.cs`，内容如下

``` csharp
/// <summary>
/// 中文翻译
/// </summary>
[ExportMany, SingletonReuse]
public class zh_CN : ITranslateProvider {
	private static HashSet<string> Codes = new HashSet<string>() { "zh-CN" };
	private static Dictionary<string, string> Translates = new Dictionary<string, string>()
	{
		{ "Example", "示例" }
	};

	public bool CanTranslate(string code) {
		return Codes.Contains(code);
	}

	public string Translate(string text) {
		return Translates.GetOrDefault(text);
	}
}
```

翻译需要实现`ITranslateProvider`接口并使用`[ExportMany]`注册到容器中。<br/>
这里保存翻译使用了`Dictionary`，如果有需要还可以通过其他途径翻译。<br/>

### <h2>使用翻译</h2>

在代码中使用翻译<br/>
类似于gettext，但是gettext使用名称`_`，这里使用`T`（Translate的T）。<br/>
``` csharp
string translated = new T("Example"); // 当前语言是中文时，translated等于"示例"
```

在模板中使用翻译，通过过滤器翻译字符串或变量<br/>
``` html
<p>{{ "Example" | trans }}</p>
<p>{{ stringVariable | trans }}</p>
```

### <h2>转换时区</h2>

转换到客户端的时间，`ToClientTime`是一个扩展函数。<br/>
如果客户端指定了时区则使用该时区，否则使用服务器本地时间。<br/>
``` csharp
var utcTime = DateTime.UtcNow;
var clientTime = utcTime.ToClientTime();
```

从客户端时间转换到utc时间，常用于客户端的时间控件提交值到服务器。
``` csharp
var clientTime = DateTime.Parse(timeParam);
var utcTime = clientTime.FromClientTime();
```

### <h2>指定当前语言</h2>

直接设置指定语言<br/>
``` csharp
LocaleUtils.SetThreadLanguage("zh-CN");
```

自动设置语言（Cookies => 浏览器语言 => 默认语言）<br/>
第一个参数决定是否检测浏览器语言<br/>
``` csharp
LocaleUtils.SetThreadLanguageAutomatic(true, "en-US");
```

### <h2>指定当前时区</h2>

直接设置指定时区
``` csharp
LocaleUtils.SetThreadTimezone("China Standard Time");
```

自动设置时区（Cookies => 默认时区）
``` csharp
LocaleUtils.SetThreadTimezoneAutomatic("GMT Standard Time");
```
