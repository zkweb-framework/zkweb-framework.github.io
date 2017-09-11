ZKWeb内置提供了多语言支持, 不使用.Net自带的资源文件机制.<br/>
语言的翻译由`ITranslateProvider`实现, 每个线程都可以指定不同的语言和时区.<br/>
默认的插件集提供了自动设置当前线程语言和时区的功能, 请参考`Common.Base`插件的文档.<br/>

### 设置当前线程的语言

设置指定语言<br/>

``` csharp
LocaleUtils.SetThreadLanguage("zh-CN");
```

自动设置语言（Cookies => 浏览器语言 => 默认语言）<br/>
第一个参数决定是否检测浏览器语言<br/>

``` csharp
LocaleUtils.SetThreadLanguageAutomatic(true, "en-US");
```

### 添加翻译

翻译需要实现`ITranslateProvider`接口并使用`[ExportMany]`注册到容器中.<br/>
添加`src\Components\Translates\zh_CN.cs`, 内容如下:

``` csharp
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

### 使用翻译

在代码中使用翻译:

``` csharp
string translated = new T("Example"); // 当前语言是中文时, translated等于"示例"
```

在模板中使用翻译, 通过过滤器翻译字符串或变量:

``` html
<p>{{ "Example" | trans }}</p>
<p>{{ stringVariable | trans }}</p>
```
