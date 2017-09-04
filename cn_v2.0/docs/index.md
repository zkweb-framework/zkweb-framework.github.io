ZKWeb是一个轻量的, 支持模块化的网页框架, 支持.Net Framework和.Net Core.

### 为什么创建这个框架

- 我需要更简单和灵活的插件系统, 和Django一样, 创建文件夹就可以作为一个新的模块
- 我需要一个独立的MVC框架, 可以保证即使微软的MVC框架再次换代也不需要重写业务代码
- 我需要一个强大的模板系统, 足够让我实现网页的可视化编辑

### ZKWeb的主要功能

- 同时支持.Net Framework和.Net Core
- 提供简单和灵活的插件系统
	- 一个文件夹一个插件, 文件夹下包含该插件需要的所有代码和资源文件
	- 插件下的源代码改变后可以自动重新编译和加载
	- 基于Roslyn实现
- 提供强大的模板系统
	- 可以像Django一样, 在一个插件重载其他插件的模板
	- 可以根据访问来源是PC或者移动端使用不同的模板
	- 提供区域-部件风格的动态内容系统, 可以基于这个功能实现页面的可视化编辑
	- 支持缓存页面中某个部分的描绘结果, 而不需要缓存整个页面
	- 基于DotLiquid实现
- 提供自主开发的IoC容器
	- 性能非常快, 接近DryIoC
	- 提供微软DI抽象层的整合, 可以跟Asp.Net Core融合在一起
- 支持多个托管环境
	- 支持基于Asp.Net, Asp.Net Core, Owin运行
	- 同一份代码使用抽象层可兼容以上的所有运行环境, 代码不再需要跟着框架重写
- 支持多个ORM
	- 支持Dapper, EntityFramework Core, InMemory, MongoDB, NHibernate
	- 其中EntityFramework Core和NHibernate支持全自动的数据表迁移
	- 代码可以使用抽象层, 但仍需注意ORM之间的功能差异(例如是否支持导航属性和懒加载)
- 提供多语言和多时区支持
- 提供缓存的抽象层, 可用于对接Redis
	- 还支持按当前访问设备, 请求URL等策略隔离缓存内容
- 提供文件系统的抽象层, 可用于对接分布式文件系统
- 提供测试支持
	- 支持从控制台或者网页执行单元或集成测试
	- 支持重载当前使用的IoC容器和Http上下文
	- 支持使用临时数据库
- 提供创建项目和发布项目的工具, 支持命令行和跨平台
- 提供Linux支持, 已在多个发行版上测试可用

### 演示站点

ZKWeb目前有两个Demo项目, 一个是多页面的商城系统, 另一个是单页面的管理系统.

##### 多页面的商城系统:<br/>
[http://demo.zkweb.org/admin/login.html](http://demo.zkweb.org/admin/login.html)<br/>
用户名是demo, 密码是123456.

##### 单页面的管理系统:<br/>
[http://mvvmdemo.zkweb.org/admin](http://mvvmdemo.zkweb.org/admin)<br/>
用户名是admin, 密码是123456.

### 项目和文档链接

##### ZKWeb的项目首页:<br/>
[http://www.zkweb.org](http://www.zkweb.org)<br/>
包含了ZKWeb的项目介绍.

##### ZKWeb的源代码:<br/>
[https://github.com/zkweb-framework/ZKWeb](https://github.com/zkweb-framework/ZKWeb)<br/>
这个项目仅包含类库, 如何创建一个ZKWeb项目并启动请参考[创建项目](core/create_project/index.html).

##### 多页面商城系统的源代码:<br/>
启动项目: [http://github.com/zkweb-framework/ZKWeb.Demo](http://github.com/zkweb-framework/ZKWeb.Demo)<br/>
插件项目: [http://github.com/zkweb-framework/ZKWeb.Plugins](http://github.com/zkweb-framework/ZKWeb.Plugins)<br/>
启动项目仅用于启动网站, 插件项目包含业务代码, 您可以创建自己的项目然后引用插件项目.

##### 单页面管理系统的源代码:<br/>
[http://github.com/zkweb-framework/ZKWeb.MVVMDemo](http://github.com/zkweb-framework/ZKWeb.MVVMDemo)<br/>
这个项目同时包含了网站项目和插件, 启动前需要使用NodeJS编译前端页面.

##### ZKWeb 2.0的文档地址:<br/>
[http://zkweb-framework.github.io](http://zkweb-framework.github.io)<br/>
就是您现在看的文档, 内容还在补充, 如果有需要可以查看下面1.0的文档.

##### ZKWeb 1.0的文档地址:<br/>
[http://zkweb-framework.github.io/cn_v1.0/site/index.html](http://zkweb-framework.github.io/cn_v1.0/site/index.html)<br/>
ZKWeb 1.0~1.9的文档.

##### 单页面管理系统的文档:<br/>
[https://github.com/zkweb-framework/ZKWeb.MVVMDemo/tree/master/docs/cn](https://github.com/zkweb-framework/ZKWeb.MVVMDemo/tree/master/docs/cn)<br/>
单页面管理系统(MVVMDemo)的文档不在这里, 请参考上面的地址.<br/>

##### ZKWeb的索引文档:<br/>
TODO, 针对2.0的索引文档尚未生成.

##### ZKWeb.Plugins的索引文档:<br/>
TODO, 针对2.0的索引文档尚未生成.

### 开源协议

ZKWeb基于MIT协议开源, 可以自由的修改源代码并免费用于商业项目.

### QQ群

欢迎加入ZKWeb的官方QQ群: 522083886
