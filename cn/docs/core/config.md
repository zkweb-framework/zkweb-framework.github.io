ZKWeb的网站配置内容都保存在`App_Data\config.json`中，不需要修改传统的`web.config`。<br/>

### <h2>配置内容</h2>
``` json
{
	"Database": 数据库名称,
	"ConnectionString": 数据库连接字符串,
	"PluginDirectories": [ 插件目录的列表 ],
	"Plugins": [ 插件列表 ],
	"Extra": { 附加配置 }
}
```

各配置的说明<br/>

- 数据库名称<br/>
  可以是"postgresql", "sqlite", "mssql", "mysql"
- 数据库连接字符串<br/>
  填写该数据库试用的标准的连接字符串即可
- 插件目录的列表<br/>
  相对于网站程序的路径，不指定时是`["App_Data/Plugins"]`
- 插件列表<br/>
  加载的列表，在插件目录下的文件夹名称<br/>
  注意这里的顺序会影响到插件的加载顺序，加载顺序会影响到组件注册的顺序和模板文件的读取顺序等
- 附加配置<br/>
  附加配置可以用来修改高级的配置，例如各项缓存时间等

### <h2>附加配置</h2>

核心框架提供了以下的附加配置，部分插件中也会有这样的附加配置。

- ZKWeb.TranslateCacheTime 翻译的缓存时间，单位是秒
- ZKWeb.TemplatePathCacheTime 模板路径缓存时间，单位是秒
- ZKWeb.ResourcePathCacheTime 资源路径的缓存时间，单位是秒
- ZKWeb.WidgetInfoCacheTime 模块信息的缓存时间，单位是秒
- ZKWeb.CustomWidgetsCacheTime 自定义模块列表的缓存时间，单位是秒
- ZKWeb.TemplateCacheTime 模板的缓存时间，单位是秒
- ZKWeb.DisplayFullExceptionForTemplate 是否在描画模板发生例外时显示完整信息
- ZKWeb.DisplayFullExceptionForRequest 是否在请求发生例外时显示完整信息
- ZKWeb.ClearCacheAfterUsedMemoryMoreThan 内存占用超过此数值时自动清理缓存，单位是MB
- ZKWeb.CleanCacheCheckInterval 缓存自动清理器的检查间隔，单位是秒

### <h2>配置的完整示例</h2>

以下是一个完整的配置文件的示例
``` json
{
	"Database": "postgresql",
	"ConnectionString": "Server=127.0.0.1;Port=5432;Database=zkweb;User Id=postgres;Password=123456;",
	"PluginDirectories": [ "../../ZKWeb.Plugins" ],
	"Plugins": [
		"Common.Base",
		"Common.Captcha",
		"Common.Admin"
	],
	"Extra": {
		"ZKWeb.DisplayFullExceptionForTemplate": false
	},
}
```
