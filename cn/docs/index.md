ZKWeb是一个着重快速开发和模块开发的网站框架。<br/>
提供了动态插件和全自动管理数据库结构的功能。<br/>
模板系统和自动生成页面参考了Django的做法，并遵从Don't repeat yourself原则。<br/>

ZKWeb目前同时提供Asp.Net, Asp.Net Core, Owin版本。<br/>
所有版本都兼容相同的插件。<br/>
编写插件应该使用ZKWebStandard提供的抽象层而不去依赖Asp.Net或Asp.Net Core。<br/>

### 项目地址

<a href="https://github.com/zkweb-framework/ZKWeb" target="_blank">https://github.com/zkweb-framework/ZKWeb</a></br>
<a href="https://github.com/zkweb-framework/ZKWeb.Plugins" target="_blank">https://github.com/zkweb-framework/ZKWeb.Plugins</a>

### DEMO

地址: <a href="http://www.zkwebsite.com" target="_blank">http://www.zkwebsite.com</a><br/>
用户名: demo<br/>
密码: 123456<br/>

### 索引文档

最终更新: 2016-06-19<br/>
<a href="../references/zkweb/index.html" target="_blank">ZKWeb索引文档</a></br>
<a href="../references/zkweb.plugins/index.html" target="_blank">ZKWeb.Plugins索引文档</a>

### 主要功能

- 支持动态载入和编辑的插件系统
	- 使用Csscript + Codedom
	- 编辑后自动重新编译
- 支持从代码自动更新数据库
	- 使用FluentNHibernate
	- 添加数据表或字段后不需要运行任何命令，刷新浏览器即可更新到数据库	
- 使用简单和高性能的Ioc容器
- 支持数据库事件
	- 允许添加回调，在数据修改或删除前后在同一个事务中进行操作
	- 支持对比修改前后的数据
- Django风格的模板系统
	- 使用DotLiquid
	- 支持模板重载
	- 支持手机版专用模板（优先从templates.mobile读取模板内容）
	- 支持区域和针对区域的动态内容，可以在这基础上实现可视化编辑
	- 支持对页面中的部分内容进行单独缓存，可以大幅提升页面的响应速度
- 多语言支持
	- 在一个插件中翻译好的内容另外一个插件不需要翻译
	- 翻译接口支持自定义翻译逻辑（允许智能翻译）
	- 可以检测并使用浏览器语言或Cookies指定的语言
- 多时区支持
	- 可以检测并使用Cookies指定的时区
- 自动生成表单（需要使用默认插件集）
	- 支持从类型的成员自动生成表单
	- 支持客户端和服务端的表单验证
	- 支持防跨站攻击验证，默认开启
	- 支持自定义复杂的表单类型
	- 支持在其他插件中扩展现有的表单
- 管理员后台（需要使用默认插件集）
	- 基于AdminLTE
	- 同时支持电脑和手机，所有页面自适应
- 自动生成增删查改（需要使用默认插件集）
	- 允许生成管理员使用的增删查改页面
	- 允许生成会员中心使用的增删查改页面
	- 支持批量操作和高级搜索
	- 支持回收站
	- 支持自动生成和检查权限
	- 不通过代码生成器，减少程序的代码量和内存占用
- 支持伪静态（需要使用默认插件集）
	- 全局自动生成和处理伪静态url，不需要编写正则
	- 几乎没有额外的性能开销

### 性能数据

- 2016-06-17 (i7 Q720 1.6Ghz x 4 core 8 threads, ab -n 8000 -c 16, real machine, asp.net core)
	- 首页 0.852ms, 0.799ms, 0.801ms
	- 商品列表页 0.879ms, 0.740ms, 0.742ms
	- 商品详情页 0.941ms, 0.877ms, 0.879ms
- 2016-06-17 (i7 Q720 1.6Ghz x 4 core 8 threads, ab -n 8000 -c 16, real machine, asp.net)
	- 首页 0.674ms, 0.688ms, 0.672ms
	- 商品列表页 0.648ms, 0.770ms, 0.645ms
	- 商品详情页 0.736ms, 0.756ms, 0.775ms

### 项目进度

核心框架已开发完毕，正在进行正式版本的测试。</br>
业务插件仍在编写，目标是使用这套框架做一个开源的商城系统。</br>

讨论QQ群：522083886
