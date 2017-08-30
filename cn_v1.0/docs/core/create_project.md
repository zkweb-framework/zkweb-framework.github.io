在创建项目前，请准备好以下的工具

- Visual Studio 2017
	- 在安装时请勾选Asp.Net Core支持
	- 选择Asp.net Core项目必须VS2017
	- 选择Asp.Net或Owin项目也可以使用VS2015
- 下载ZKWeb和ZKWeb.Plugins项目
	- https://github.com/zkweb-framework/ZKWeb
	- https://github.com/zkweb-framework/ZKWeb.Plugins

准备好以后，打开`ZKWeb\Tools\ProjectCreator.Gui.exe`，创建一个新的项目。<br/>
如果使用的是SQLite以外的数据库，请先创建好一个空的数据库，并点击`Test`测试数据库连接。<br/>
如果需要支持.Net Core请选择Asp.Net Core和NHibernate以外的ORM。<br/>
![项目创建器](../img/project_creator.jpg)

创建项目时，如果需要下图的效果，需要选择"默认的插件集"<br/>
点击"Use Default Plugins"右边的"Browse"，然后根据使用的ORM选中以下的文件<br/>
目前NHibernate支持的插件最多，其他的仅支持最基本的几个插件<br/>

- NHibernate: "ZKWeb.Plugins\src\ZKWeb.Plugins\plugin.collection.json"
- EFCore: "ZKWeb.Plugins\src\ZKWeb.Plugins\plugin.collection.ef.json"
- Dapper: "ZKWeb.Plugins\src\ZKWeb.Plugins\plugin.collection.dapper.json"
- MongoDB: "ZKWeb.Plugins\src\ZKWeb.Plugins\plugin.collection.mongodb.json"

创建好项目以后，使用Visual Studio打开并运行。<br/>
第一次打开需要编译所有插件，可能会耗时几分钟，请耐心等待。<br/>
![首次运行](../img/first_running.jpg)

### FAQ

<h4>我不想使用默认的插件集，怎么办？</h4>
默认的插件集中包含了比较多的功能，但不一定适合所有人的需要。<br/>
如果不想使用默认的插件集，可以在创建项目时把`Use Default Plugins`留空。<br/>
如果只想使用一部分，可以通过修改`App_Data\config.json`实现，详见[网站配置](website_config)。

<h4>使用了默认的插件集后，怎么进入管理后台？</h4>
后台地址是`/admin`，首次运行网站时应该先注册一个用户并用这个用户登录到后台。<br/>
网站创建后第一个登陆到后台的用户会成为超级管理员。

<h4>选择Dapper创建项目后显示找不到表，怎么解决？</h4>
Dapper不支持自动创建表，你需要手动创建表或者先使用其他ORM例如NHibernate创建表。

<h4>VS2017提示ZKWeb.System.Drawing和System.Drawing冲突怎么办？</h4>
使用旧版本创建的Asp.Net Core项目升级到VS2017时会出现这个错误, 解决办法可以右键点击项目编辑csproj<br/>
然后在`PropertyGroup`下添加`<DisableImplicitFrameworkReferences>true</DisableImplicitFrameworkReferences>`<br/>
