基础插件是默认插件集中最基础的插件，提供了核心框架中没有但是大多数网站都需要的功能。<br/>
使用默认插件集时，为了Visual Studio编辑时的代码提示，<br/>
需要从nuget安装`ZKWeb.Plugins`包到`Plugins`项目，或手动引用`Common.Base.dll`等程序集。

`Common.Base`插件包含了以下的功能

- [通用配置](common.base.generic_config)
	- 读取和保存整个网站中使用的配置，例如网站标题等
- [定时任务](common.base.scheduled_task)
	- 提供定时执行指定处理的功能
- [会话](common.base.session)
	- 提供会话存取的功能
- [基础css样式](common.base.css)
	- 引用了Bootstrap和AdminLTE样式
- [基础javascript脚本](common.base.javascript)
	- 引用了jQuery, Bootstrap, Underscore等脚本
- [基础图标字体](common.base.font)
	- 引用了Font Awesome图标字体
- [动态表格构建器](common.base.ajax_table)
	- 提供在浏览器绑定数据的表格，数据通过ajax获取
- [静态表格构建器](common.base.static_table)
	- 提供在服务端绑定数据的表格
- [表单构建器](common.base.form_builder)
	- 提供表单构建和解析的功能
- [仓储和工作单元](common.base.repository_uow)
	- 提供更简单和规范的数据库访问功能
- [静态文件处理器](common.base.static_handler)
	- 提供返回静态文件的功能，静态文件保存在`static`目录下
- [语言时区处理器](common.base.locale_handler)
	- 提供自动设置当前请求的语言和时区的功能
- [实体特征](common.base.traits)
	- 提供数据库实体的特征接口，例如是否可以软删除
- [提供的标签和过滤器](common.base.tag_and_filter)
	- 提供了一系列模板使用的标签和过滤器
- [提供的隔离策略](common.base.cache_policy)
	- 提供了一系列缓存隔离策略

### 层次结构

默认插件的层次结构如下，主要按业务组件,控制器,领域层,测试和界面组件区分

- `src` 源代码
	- `Components` 各种业务组件
	- `Controllers` 控制器
	- `Domain` 领域层
		- `Entities` 数据库实体
		- `Filters` 查询或操作过滤器
		- `Repositories` 仓储
		- `Services` 服务
		- `Structs` 实体或服务使用的结构类
	- `Tests` 测试
	- `UIComponents` 各种界面组件
