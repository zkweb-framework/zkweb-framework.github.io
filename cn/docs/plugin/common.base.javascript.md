基础插件引用了以下的javascript脚本<br/>

- jQuery 1.11.2
	- jquery-migrate
	- jquery-form
	- jquery-mobile
	- jquery-toast
	- jquery-validate
	- jquery-validate-unobtrusive
- Bootstrap 3.3.2
	- context-menu
	- dialog
	- hover-dropdown
- Switchery 0.8.1
- jsUri 1.3.1
- Underscore 1.8.3

基础插件的javascript脚本已整合到一个文件，引用时可以使用

``` html
{% include_js_here "/static/common.base.js/components.min.js" %}
```

### 额外的脚本功能

基础插件的脚本还提供了一些额外的功能，这些功能大多数已经过代码的包装，不需要全部了解。

** 弹出对话框显示远程内容**<br/>
``` javascript
BootstrapDialog.showRemote("title", "/example/contents");
```

**初始化动态表格**<br/>
详见[动态表格构建器](common.base.ajax_table)
``` javascript
$("#Table").ajaxTable({ target: "/example/search_table" });
```

**多选框组，支持全选/取消全选**<br/>
``` html
<div data-toggle="checkbox-group">
	<input type="checkbox" class="select-all">
	<input type="checkbox">
	<input type="checkbox">
</div>
```

**储存多个多选框的值到一个控件中**<br/>
``` html
<input type="checkbox" value="A" merge-to="input[name=ExampleList]">
<input type="checkbox" value="B" merge-to="input[name=ExampleList]">
<input type="hidden" name="ExampleList" value="[]" />
```

**设置通用的Ajax表单**<br/>
在提交期间为表单添加loading类，并触发`beforeSubmit`, `success`, `error`事件。<br/>
``` javascript
$("#Form").commonAjaxForm();
```

**处理ajax提交的结果**<br/>
支持显示消息和执行结果中的脚本。<br/>
``` javascript
$.handleAjaxResult(data);
// data: { message: "example message", allowHtmlText: false, script: "alert(1)" }
```

**自动显示ajax提交错误的信息**<br/>
服务器对于ajax请求返回了错误时，会自动弹出错误的信息。<br/>
需要禁用这个功能请设置`$.toast.extra.showAjaxError = false;`<br/>

**可编辑的表格**<br/>
支持添加和删除行，定义收集和绑定事件等功能。
``` javascript
var $table = $("#Editor").editableTable({
	columns: [ "A", "B", $("<div>") ],
	tableClass: "table table-bordered table-hover",
	tableHeaderClass: "heading"
});
$table.on("addRow.editableTable", function (e, data) { ... });
$table.on("collect.editableTable", function () { ... });
$table.on("bind.editableTable", function () { ... });
```

**全屏portlet的内容**<br/>
在以下html结构中点击`.fullscreen`时可以全屏`portlet`的内容。
``` html
<div class="portlet">
	<a class="fullscreen">FullScreen</a>
</div>
```

**数字文本框**<br/>
点击上下按钮时改变文本框中的数值，文本框中只能填写指定类型的数值。<br/>
``` html
<div data-toggle='number-input'
	data-allow-decimal='false' data-allow-negative='false' data-delta='1'>
	<input type='text' class='number' />
	<span class='up'></span>
	<span class='down'></span>
</div>
```

**支持post数据的链接**<br/>
跳转post到指定的地址或通过ajax post到指定的地址，<br/>
通过ajax post时返回结果会使用$.handleAjaxResult处理。<br/>
``` html
<a post-href="/user/logout"></a>
<a post-href="/api/example_api" ajax="true"></a>
```

**刷新图片内容**<br/>
会在参数中添加timestamp来跳过浏览器缓存，获取最新的图片内容。
``` javascript
$("#Img").refreshImage();
```

**延迟加载脚本**<br/>
常用于模板模块中引用脚本，同一路径只会引用一次。
``` html
<div require-script="/static/demo/demo.js"></div>
```

**延迟加载样式**<br/>
常用于模板模块中引用样式，同一路径只会引用一次。
``` html
<div require-style="/statis/demo/demo.css"></div>
```

**可搜索的下拉框**<br/>
可以在选项列表中搜索，不支持远程搜索。<br/>
``` html
<div class="advance-select" data-toggle="advance-select">
	<button class="btn btn-default form-control dropdown-toggle" data-toggle="dropdown">
		<span class="option-text"></span><span class="caret"></span>
	</button>
	<div class="dropdown-menu">
		<input type="text" class="form-control keyword" placeholder="" />
		<select></select>
	</div>
</div>
```

**可编辑文本的下拉框**<br/>
可以选中选项列表中的值或填写自定义值，注意提交时应该把文本框中的内容也提交到服务器。<br/>
``` html
<div class="advance-select" data-toggle="advance-select">
	<div class="input-group dropdown-toggle" data-toggle="dropdown">
		<input class="form-control option-text-editable" type="text" />
		<span class="input-group-btn">
			<button class="btn btn-default"><span class="caret"></span></button>
		</span>
	</div>
	<div class="dropdown-menu">
		<select></select>
	</div>
</div>
```
