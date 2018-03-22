因为需要同时兼顾到Asp.Net, Asp.Net Core和Owin,<br/>
ZKWeb程序的配置并不保存在`web.config`或者`appsettings.json`里面.<br/>
ZKWeb程序的配置统一保存在`App_Data\config.json`.

### 配置结构

`config.json`的结构大致如下:

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

#### ORM
ORM名称, 可以是`NHibernate`, `EFCore`, `Dapper`, `MongoDB`, `InMemory`

#### Database

数据库名称, 可以是`MSSQL`, `MySQL`, `SQLite`, `PostgreSQL`, `InMemory`, `MongoDB`

#### ConnectionString

数据库连接字符串, 填写标准的连接字符串即可, 选择InMemory时可以随便填.

#### PluginDirectories

用于查找插件的目录列表, 会从这些目录中查找插件, <br/>
应该填写相对于网站程序的路径，不填写时使用`["App_Data/Plugins"]`.<br/>

#### PluginDirectories

插件列表, 应该填写插件文件夹的名称.<br/>
这里的顺序等于插件的加载顺序, 加载顺序会影响组件注册的顺序和资源文件的读取顺序.<br/>

#### Extra

附加配置, 可以用来修改高级的配置, 例如各项缓存时间等.

### 获取配置

在代码中获取配置可以使用:

``` csharp
var configManager = Application.Ioc.Resolve<WebsiteConfigManager>();
var value = configManager.WebsiteConfig.Extra.GetOrDefault<bool>("SomeExtraConfiguration");
```

### 附加配置

以下是ZKWeb内部使用的附加配置, 它们保存在`Extra`节下.<br/>
部分插件中也有额外的配置项, 请参考插件的说明.<br/>

- `ZKWeb.TemporaryDatabaseORM` 临时数据库使用的ORM, 默认是InMemory
- `ZKWeb.TemporaryDatabaseType `临时数据库使用的数据库类型
- `ZKWeb.TemporaryDatabaseConnectionString` 临时数据库试用的连接字符串
- `ZKWeb.TranslateCacheTime` 翻译的缓存时间, 单位是秒
- `ZKWeb.TemplatePathCacheTime` 模板路径缓存时间, 单位是秒
- `ZKWeb.ResourcePathCacheTime` 资源路径的缓存时间, 单位是秒
- `ZKWeb.WidgetInfoCacheTime` 模块信息的缓存时间, 单位是秒
- `ZKWeb.CustomWidgetsCacheTime` 自定义模块列表的缓存时间, 单位是秒
- `ZKWeb.TemplateCacheTime` 模板的缓存时间, 单位是秒
- `ZKWeb.DisplayFullExceptionForTemplate` 是否在描画模板发生例外时显示完整信息
- `ZKWeb.DisplayFullExceptionForRequest` 是否在请求发生例外时显示完整信息
- `ZKWeb.ClearCacheAfterUsedMemoryMoreThan` 内存占用超过值时自动清理缓存, 单位是MB
- `ZKWeb.CleanCacheCheckInterval` 缓存自动清理器的检查间隔, 单位是秒
- `ZKWeb.CompilePluginsWithReleaseConfiguration` 使用Release配置编译插件，默认是false
- `ZKWeb.DisableCaseSensitiveRouting` 禁止大小写敏感的路由
- `ZKWeb.DisableAutomaticPluginReloading` 禁止自动重新加载插件
