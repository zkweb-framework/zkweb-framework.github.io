ZKWeb的网站配置内容都保存在`App_Data\config.json`中。<br/>

### 配置内容
``` json
{
	"ORM": ORM名称,
	"Database": 数据库名称,
	"ConnectionString": 数据库连接字符串,
	"PluginDirectories": [ 用于查找插件的目录列表 ],
	"Plugins": [ 插件列表 ],
	"Extra": { 附加配置 }
}
```

### 各配置的说明

- ORM ORM名称
	- 可以是"NHibernate", "EFCore", "Dapper", "MongoDB"
- Database 数据库名称
	- 可以是"MSSQL", "MySQL", "SQLite", "PostgreSQL", "InMemory", "MongoDB"
- ConnectionString 数据库连接字符串
	- 填写标准的连接字符串即可
	- 选择InMemory时可以随便填
- PluginDirectories 用于查找插件的目录列表
	- 会从这些目录中查找插件
	- 应该填写相对于网站程序的路径，不填写时使用`["App_Data/Plugins"]`
- PluginDirectories 插件列表
	- 插件文件夹的名称
	- 这里的顺序会影响到插件的加载顺序，加载顺序会影响到组件注册的顺序和资源文件的读取顺序等
- Extra 附加配置
	- 附加配置可以用来修改高级的配置，例如各项缓存时间等

### 获取配置

获取网站配置可以使用`ConfigManager`。
``` csharp
var configManager = Application.Ioc.Resolve<ConfigManager>();
var value = configManager.WebsiteConfig.Extra.GetOrDefault<bool>("SomeExtraConfiguration");
```

### 附加配置

核心框架提供了以下的附加配置，部分插件中也会有这样的附加配置。

- ZKWeb.TemporaryDatabaseORM 临时数据库使用的ORM，默认是InMemory
- ZKWeb.TemporaryDatabaseType 临时数据库使用的数据库类型
- ZKWeb.TemporaryDatabaseConnectionString 临时数据库试用的连接字符串
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
- ZKWeb.CompilePluginsWithReleaseConfiguration 是否使用Release配置编译插件，默认是false
