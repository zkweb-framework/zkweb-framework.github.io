ZKWeb是一个用于快速开发网站的框架。<br/>
提供了编辑即编译，和全自动管理数据库结构的功能。<br/>
模板系统和自动生成组件参考了Django的做法，并遵从**Don't repeat yourself**原则。<br/>

框架更注重功能的分离而不是层次的分离，<br/>
每个插件程序集可以包含功能所需要的业务处理，数据库处理，控制器和模板。<br/>
插件系统可以实现相同的业务代码提供给不同的客户时，只需要写一次并只管理一份代码。<br/>

ZKWeb目前同时提供**Asp.Net**, **Asp.Net Core**, **Owin**版本。<br/>
所有版本都兼容相同的插件。<br/>
编写插件应该使用**ZKWebStandard**提供的抽象层而不去依赖**Asp.Net**或**Asp.Net Core**。<br/>

### <h2>项目地址</h2>

<a href="https://github.com/zkweb-framework/ZKWeb" target="_blank">https://github.com/zkweb-framework/ZKWeb</a></br>
<a href="https://github.com/zkweb-framework/ZKWeb.Plugins" target="_blank">https://github.com/zkweb-framework/ZKWeb.Plugins</a>

### <h2>DEMO</h2>

地址: <a href="http://www.zkwebsite.com" target="_blank">http://www.zkwebsite.com</a><br/>
用户名: demo<br/>
密码: 123456<br/>

### <h2>索引文档</h2>

最终更新: 2016-06-19<br/>
<a href="../references/zkweb/index.html" target="_blank">ZKWeb索引文档</a></br>
<a href="../references/zkweb.plugins/index.html" target="_blank">ZKWeb.Plugins索引文档</a>

### <h2>主要功能</h2>

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
- 自动生成表单（需要Common.Base插件）
	- 支持从类型的成员自动生成表单
	- 支持客户端和服务端的表单验证
	- 支持防跨站攻击验证，默认开启
	- 支持自定义复杂的表单类型
	- 支持在其他插件中扩展现有的表单
- 管理员后台（需要使用Common.Admin插件）
	- 基于AdminLTE
	- 同时支持电脑和手机，所有页面自适应
- 自动生成增删查改（需要使用Common.Admin插件）
	- 允许生成管理员使用的增删查改页面
	- 支持批量操作和高级搜索
	- 支持回收站
	- 支持自动生成和检查权限
	- 不通过代码生成器，减少程序的代码量和内存占用
- 支持伪静态
	- 全局自动生成和处理伪静态url，不需要编写正则
	- 几乎没有额外的性能开销

### <h2>层次结构</h2>

当前核心 + 默认插件的层次结构如下<br/>
![](img/architecture.jpg)

### <h2>性能数据</h2>

- 2016-05-27 (i7 Q720 1.6Ghz x 4 core 4 threads, ab -n 2000 -c 8, vmware player 12)
	- 首页 2.240ms, 2.165ms, 2.123ms
	- 商品列表页 2.131ms, 2.013ms, 2.132ms
	- 商品详情页 2.165ms, 2.136ms, 2.193ms
- 2016-06-08 (i7 Q720 1.6Ghz x 4 core 8 threads, ab -n 4000 -c 8, vmware player 12)
	- 首页 1.406ms, 1.441ms, 1.453ms
	- 商品列表页 1.457ms, 1.297ms, 1.207ms
	- 商品详情页 1.723ms, 1.695ms, 1.719ms
- 2016-06-08 (i7 Q720 1.6Ghz x 4 core 8 threads, ab -n 4000 -c 16, real machine)
	- 首页 0.793ms, 0.824ms, 0.809ms
	- 商品列表页 0.708ms, 0.672ms, 0.694ms
	- 商品详情页 0.853ms, 0.827ms, 0.827ms
- 2016-06-17 (i7 Q720 1.6Ghz x 4 core 8 threads, ab -n 8000 -c 16, real machine, asp.net core)
	- 首页 0.852ms, 0.799ms, 0.801ms
	- 商品列表页 0.879ms, 0.740ms, 0.742ms
	- 商品详情页 0.941ms, 0.877ms, 0.879ms
- 2016-06-17 (i7 Q720 1.6Ghz x 4 core 8 threads, ab -n 8000 -c 16, real machine, asp.net)
	- 首页 0.674ms, 0.688ms, 0.672ms
	- 商品列表页 0.648ms, 0.770ms, 0.645ms
	- 商品详情页 0.736ms, 0.756ms, 0.775ms

### <h2>项目进度</h2>

核心框架已开发完毕，正在进行正式版本的测试。</br>
业务插件仍在编写，目标是使用这套框架做一个开源的商城系统。</br>

讨论QQ群：522083886。
