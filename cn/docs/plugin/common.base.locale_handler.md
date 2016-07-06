基础插件可以自动设置当前请求的语言和时区。<br/>
设置语言和时区的处理器基于`IHttpRequestPreHandler`实现。<br/>

### 设置语言的顺序

- 客户端传入的Cookies (LocaleUtils.LanguageKey: "ZKWeb.Language")
- 客户端浏览器语言，如果`LocaleSettings`设置允许检测浏览器语言
- `LocaleSettings`中的默认语言

### 设置时区的顺序

- 客户端传入的Cookies (LocaleUtils.TimeZoneKey: "ZKWeb.TimeZone")
- `LocaleSettings`中的默认时区

改变默认语言和时区可以修改通用配置`LocaleSettings`。<br/>
如果不手动设置则使用服务器的本地语言和时区。<br/>
