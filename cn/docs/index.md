<small>最终更新: 2016-12-21</small>

ZKWeb是一个着重快速开发和模块开发的网站框架。<br/>
实现了独自的Mvc系统，同时提供动态插件和全自动更新数据库结构等功能。<br/>
模板系统参考了Django的做法，并遵从Don't repeat yourself原则。<br/>

### 主要功能

- .Net Core支持
	- 支持运行在.Net Framework和.Net Core上
- 插件系统
	- 使用Roslyn
	- 支持动态加载插件
	- 支持修改插件源代码后自动重新编译和加载
- 模板系统
	- 使用DotLiquid
	- 支持Django风格的模板重载
	- 支持手机版专用模板（优先从templates.mobile读取模板内容）
	- 支持区域和针对区域的动态内容，可以在这基础上实现可视化编辑
	- 支持对页面中的部分内容进行单独缓存，可以大幅提升页面的响应速度
- IoC容器
	- 轻量且快速
	- 默认支持使用属性注册程序集中的类型到容器
	- 支持构造函数注入
- 支持多种框架的托管
	- 支持托管在Asp.Net
	- 支持托管在Asp.Net Core
	- 支持托管在Owin
	- 插件不需要理会托管在哪个框架，使用抽象层即可
- 支持多种ORM
	- 支持Dapper
	- 支持EntityFramework Core
	- 支持InMemory
	- 支持MongoDB
	- 支持NHibernate
		- NHibernate还不能运行在.Net Core上
	- NHibernate和EFCore支持运行时自动更新数据表结构，不需要手动迁移
	- ORM有统一的抽象层，一份代码可以同时在所有ORM上运行，但不能实现完全兼容
- 本地化
	- 支持多语言
	- 支持多时区
	- 提供了gettext风格的翻译函数
- 缓存处理
	- 支持按策略隔离缓存，例如按客户端的设备或请求Url
	- 缓存有统一的抽象层，可以自己提供分布式缓存的实现
- 文件储存
	- 文件储存有统一的抽象层，可以自己提供分布式储存的实现
- 测试
	- 支持在控制台和网页运行测试
	- 支持在测试中重载IoC容器
	- 支持在测试中重载Http上下文
	- 支持在测试中使用临时数据库
- 项目工具
	- 提供创建项目使用的工具
	- 提供发布项目使用的工具

### 默认插件集中的主要功能

- 自动生成和验证表单
- 自动生成Ajax表格
- 自动生成CRUD页面
- 定时任务
- 验证码
- 管理后台（使用AdminLTE）
- 全自动伪静态，几乎没有额外开销
- 多货币和多国家支持
- 更多功能请查看各插件的文档

### 项目地址

<a href="https://github.com/zkweb-framework/ZKWeb" target="_blank">https://github.com/zkweb-framework/ZKWeb</a></br>
<a href="https://github.com/zkweb-framework/ZKWeb.Plugins" target="_blank">https://github.com/zkweb-framework/ZKWeb.Plugins</a>

### DEMO

地址: <a href="http://www.zkwebsite.com" target="_blank">http://www.zkwebsite.com</a><br/>
用户名: demo<br/>
密码: 123456<br/>

### 示例项目

这里的文档使用的代码都可以在示例项目中找到，<br/>
地址是<a href="https://github.com/zkweb-framework/zkweb-framework.github.io/tree/master/examples/ZKWeb.Examples" target="_blank">https://github.com/zkweb-framework/zkweb-framework.github.io/tree/master/examples/ZKWeb.Examples</a>

### 索引文档

最终更新: 2016-10-01<br/>
<a href="../references/zkweb/index.html" target="_blank">ZKWeb索引文档</a></br>
<a href="../references/zkweb.plugins/index.html" target="_blank">ZKWeb.Plugins索引文档</a>

### 性能数据

- 2016-06-17 (i7 Q720 1.6Ghz x 4 core 8 threads, ab -n 8000 -c 16, real machine, asp.net core)
	- 首页 0.852ms, 0.799ms, 0.801ms
	- 商品列表页 0.879ms, 0.740ms, 0.742ms
	- 商品详情页 0.941ms, 0.877ms, 0.879ms
- 2016-06-17 (i7 Q720 1.6Ghz x 4 core 8 threads, ab -n 8000 -c 16, real machine, asp.net)
	- 首页 0.674ms, 0.688ms, 0.672ms
	- 商品列表页 0.648ms, 0.770ms, 0.645ms
	- 商品详情页 0.736ms, 0.756ms, 0.775ms
- 2016-07-06 (i7 Q720 1.6Ghz x 4 core 8 threads, ab -n 8000 -c 16, real machine, asp.net)
	- 首页 0.447ms, 0.430ms, 0.432ms
	- 商品列表页 0.402ms, 0.408ms, 0.422ms
	- 商品详情页 0.504ms, 0.506ms, 0.502ms
- 2016-09-29 (i7 Q720 1.6Ghz x 4 core 8 threads, ab -n 8000 -c 16, real machine, asp.net core)
	- 首页 0.986ms, 0.983ms, 0.998ms
	- 商品列表页 1.006ms, 0.977ms, 0.996ms
	- 商品详情页 1.111ms, 1.082ms, 1.080ms

### 项目进度

核心框架已发布正式的版本。</br>
业务插件仍在编写，目标是使用这套框架做一个开源的商城系统。</br>

讨论QQ群：522083886
