ZKWeb是一个用于快速开发网站的框架。<br/>
提供了编辑即编译，和全自动管理数据库结构的功能。<br/>
模板系统和自动生成组件参考了Django的做法，并遵从Don't repeat yourself原则。<br/>

框架更注重功能的分离而不是层次的分离，<br/>
每个插件程序集可以包含功能所需要的业务处理，数据库处理，控制器和模板。<br/>
插件系统可以实现相同的业务代码提供给不同的客户时，只需要写一次并只管理一份代码。<br/>

框架和插件编写时均考虑了实现可视化编辑（可视化编辑功能目前还没有开始编写）。<br/>

### <h2>项目地址</h2>

<a href="https://github.com/zkweb-framework/ZKWeb" target="_blank">https://github.com/zkweb-framework/ZKWeb</a></br>
<a href="https://github.com/zkweb-framework/ZKWeb.Plugins" target="_blank">https://github.com/zkweb-framework/ZKWeb.Plugins</a>

### <h2>DEMO</h2>

地址: <a href="http://www.zkwebsite.com" target="_blank">http://www.zkwebsite.com</a><br/>
用户名: demo<br/>
密码: 123456<br/>

### <h2>索引文档</h2>

最终更新: 2016-05-26<br/>
<a href="../references/zkweb/html/annotated.html" target="_blank">ZKWeb索引文档</a></br>
<a href="../references/zkweb.plugins/html/annotated.html" target="_blank">ZKWeb.Plugins索引文档</a>

### <h2>主要功能</h2>

* 支持动态载入和编辑的插件系统
	* 插件代码编辑保存后可直接刷新浏览器
* 使用Ioc容器对各项功能进行扩展
	* 功能的可扩展性强
	* 可支持单元测试时覆盖任意组件
* 支持从代码自动更新数据库
	* 添加数据表或字段后不需要运行任何命令，刷新浏览器即可更新到数据库
* 支持数据库事件
	* 允许添加回调，在数据修改或删除前后在同一个事务中进行操作
	* 支持对比修改前后的数据
* Django风格的模板系统
	* 不需预编译成dll，载入速度快且没有内存泄漏问题
	* 允许新的插件对原有插件的模板进行覆盖（规则同Django）
	* 支持区域和针对区域的动态内容，可以在这基础上实现可视化编辑
	* 支持对页面中的部分内容进行单独缓存，可以大幅提升页面的响应速度
* 多语言支持
	* 在一个插件中翻译好的内容另外一个插件不需要翻译
	* 翻译接口支持自定义翻译逻辑（允许智能翻译）
	* 可以检测并使用浏览器语言或Cookies指定的语言
* 多时区支持
	* 可以检测并使用Cookies指定的时区
* 自动生成表单（需要Common.Base插件）
	* 支持从类型的成员自动生成表单
	* 支持客户端和服务端的表单验证
	* 支持防跨站攻击验证，默认开启
	* 支持自定义复杂的表单类型
	* 支持在其他插件中扩展现有的表单
* 管理员后台（需要使用Common.Admin插件）
	* 基于AdminLTE
	* 同时支持电脑和手机，所有页面自适应
* 自动生成增删查改（需要使用Common.Admin插件）
	* 允许生成管理员使用的增删查改页面
	* 支持批量操作和高级搜索
	* 支持回收站
	* 支持自动生成和检查权限
	* 不通过代码生成器，减少程序的代码量和内存占用

### <h2>项目进度</h2>

核心框架已开发完毕，但仍处于测试阶段。</br>
正在逐步完善业务插件，目标是发布一个完整的商城网站。

讨论QQ群：522083886。