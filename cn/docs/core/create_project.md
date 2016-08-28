在创建项目前，请准备好以下的工具

- Visual Studio 2015 update 3
- 如果你需要运行在Asp.Net Core上，请安装
	- DotNetCore 1.0.0 Runtime
	- DotNetCore 1.0.0 VS2015 Tooling Preview 2
- 下载ZKWeb和ZKWeb.Plugins项目
	- https://github.com/zkweb-framework/ZKWeb
	- https://github.com/zkweb-framework/ZKWeb.Plugins

准备好以后，打开`ZKWeb\Tools\ProjectCreator.Gui.exe`，创建一个新的项目。<br/>
如果使用的是SQLite以外的数据库，请先创建好一个空的数据库，并点击`Test`测试数据库连接。<br/>
如果需要支持.Net Core请选择Asp.Net Core和NHibernate以外的ORM。<br/>
![项目创建器](../img/project_creator.jpg)

创建好项目以后，使用Visual Studio打开并运行。<br/>
第一次打开需要编译所有插件，可能会耗时几分钟，请耐心等待。<br/>
![首次运行](../img/first_running.jpg)

### FAQ

<h4>我不想使用默认的插件集，怎么办？</h4>
默认的插件集中包含了比较多的功能，但不一定适合所有人的需要。<br/>
如果不想使用默认的插件集，可以在创建项目时把`Use Default Plugins`留空。<br/>
如果只想使用一部分，可以通过修改`App_Data\config.json`实现，详见[网站配置](website_config)。

<h4>创建时没有选择默认的插件集，打开首页是404，怎么办？</h4>
项目模板没有定义首页的内容（因为首页有可能已在插件集中定义），<br/>
找到HelloController，把`[Action("hello")]`改为`[Action("/")]`即可看到首页的内容。<br/>
同时访问静态文件也需要一个处理器，可以参考`Common.Base`插件中的实现。

<h4>使用了默认的插件集后，怎么进入管理后台？</h4>
后台地址是`/admin`，首次运行网站时应该先注册一个用户并用这个用户登录到后台。<br/>
网站创建后第一个登陆到后台的用户会成为超级管理员。
