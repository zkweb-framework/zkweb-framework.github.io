ZKWeb的模板系统使用了DotLiquid.</br>
您可以先参考DotLiquid的文档: [http://dotliquidmarkup.org](http://dotliquidmarkup.org)</br>

### 为什么不使用Razor

- DotLiquid的模板不要求拥有基类等复杂的环境, 只需要传参数即可编译
- DotLiquid的模板只编译成语法树而不是IL, 页面第一次打开速度远比Razor快
- DotLiquid的文件系统容易扩展, ZKWeb中已实现了类似Django的透过式文件系统

### 在控制器中返回模板

这里以项目创建器创建的空白项目为例, 首先文件结构是这样的:

- 插件文件夹
	- src 源代码
	- static 静态文件
		- hello.world.css
			- hello.css
		- hello.world.js
			- hello.js
	- templates 模板文件
		- hello.world
			- hello.html

控制器返回模板的代码如下:

``` csharp
[ExportMany]
public class HelloController : IController {
	[Action("/")]
	public IActionResult Index() {
		// 第一个参数是模板路径, 第二个参数是传给模板的参数
		return new TemplateResult("hello.world/hello.html", new { text = "World" });
	}
}
```

模板`hello.html`的内容如下:

``` html
<!DOCTYPE html>
<html>
<head>
	<title>Hello</title>
	<link href="/static/hello.world.css/hello.css" rel="stylesheet" type="text/css" />
</head>
<body>
	<h1>Hello {{ text }}!</h1>
	<hr />
	<div>
		<p>If you're seeing this, that mean you didn't use default plugin collections.</p>
		<p>So this is an empty project.</p>
		<p>You can open src\Controllers\HelloController.cs and add something to it.</p>
	</div>
	<script src="/static/hello.world.js/hello.js" type="text/javascript"></script>
</body>
</html> 
```

我们可以看到模板中有`{{ text }}`用于绑定传入的参数, 这个就是最简单的例子了.<br/>

您可能会奇怪为什么需要`hello.world`, `hello.world.js`, `hello.world.css`这些文件夹,<br/>
这是因为在ZKWeb中资源文件的命名空间在各个插件中是共享的,<br/>
您可以参考[插件系统](plugin/index.html)中对透过式文件系统的介绍.<br/>

### 模板的语法

#### 变量

`{{ }}`可以用于描画变量或者常量到所在的位置:

``` html
Hello {{ name }}
Hello {{ user.name }}
Hello {{ 'tobi' }}
```

#### 过滤器

在`{{ }}`中使用`|`可以对绑定的值做出过滤.

``` html
Hello {{ 'tobi' | upcase }}
Hello tobi has {{ 'tobi' | size }} letters!
Hello {{ '*tobi*' | textilize | upcase }}
Hello {{ 'now' | date: "%Y %h" }}
```

#### 标签

`{% %}`可以使用预定义的标签:

``` html
{% if user %}
  Hello {{ user.name }}
{% endif %}

{% if user.name == 'bob' and user.age > 45 %}
  Hello old bob
{% endif %}
```

更多的语法可以参考[Liquid的官方文档](https://github.com/Shopify/liquid/wiki/Liquid-for-Designers).

### 安全绑值

使用DotLiquid绑值时并不是所有值都可以绑定,<br/>
内部会有一套安全机制让未经批准的值不能绑定到模板上.<br/>

需要允许绑定, 请让类型实现`ILiquidizable`, 或者标记类型为`[LiquidType]`,<br/>
或者调用`Template.RegisterSafeType(Type type, string[] allowedMembers)`注册,<br/>
更详细的说明请[查看这里](https://github.com/dotliquid/dotliquid/wiki/DotLiquid-for-Developers#rules-for-template-rendering-parameters).

### DotLiquid内置的标签

DotLiquid内置了以下的标签:

#### assign

源代码: [点击查看](https://github.com/dotliquid/dotliquid/blob/master/src/DotLiquid/Tags/Assign.cs)

``` html
{% assign foo = 'monkey' %}
```

#### capture

源代码: [点击查看](https://github.com/dotliquid/dotliquid/blob/master/src/DotLiquid/Tags/Capture.cs)

``` html
{% capture heading %}
Monkeys!
{% endcapture %}

<h1>{{ heading }}</h1>
```

#### case

源代码: [点击查看](https://github.com/dotliquid/dotliquid/blob/master/src/DotLiquid/Tags/Case.cs)

``` html
{% case condition %}{% when 1 %} its 1 {% when 2 %} its 2 {% endcase %}
```

#### comment

源代码: [点击查看](https://github.com/dotliquid/dotliquid/blob/master/src/DotLiquid/Tags/Comment.cs)

``` html
{%comment%} comment {%endcomment%}
```

#### continue

源代码: [点击查看](https://github.com/dotliquid/dotliquid/blob/master/src/DotLiquid/Tags/Continue.cs)

``` html
{% for i in array.items %}
  {% if i == 3 %}
    {% continue %}
  {% endif %}
  {{ i }}
{% endfor %}
```

#### cycle

源代码: [点击查看](https://github.com/dotliquid/dotliquid/blob/master/src/DotLiquid/Tags/Cycle.cs)

``` html
{% for item in items %}
  <div class="{% cycle 'red', 'green', 'blue' %}">{{ item }}</div>
{% endfor %}
```

#### extend

源代码: [点击查看](https://github.com/dotliquid/dotliquid/blob/master/src/DotLiquid/Tags/Extends.cs)

用于继承模板, 具体例子请看上面代码中的注释.<br/>

#### for

源代码: [点击查看](https://github.com/dotliquid/dotliquid/blob/master/src/DotLiquid/Tags/For.cs)

``` html
{% for item in collection %}
  {{ forloop.index }}: {{ item.name }}
{% endfor %}
```

#### if

源代码: [点击查看](https://github.com/dotliquid/dotliquid/blob/master/src/DotLiquid/Tags/If.cs)

``` html
{% if user.admin %}
  Admin user!
{% else %}
  Not admin user
{% endif %}
```

#### include

源代码: [点击查看](https://github.com/dotliquid/dotliquid/blob/master/src/DotLiquid/Tags/Include.cs)

``` html
{% include common.base/header.html %}
```

#### literal

源代码: [点击查看](https://github.com/dotliquid/dotliquid/blob/master/src/DotLiquid/Tags/Literal.cs)

``` html
{% literal %}{% if user = 'tobi' %}hi{% endif %}{% endliteral %}
```

#### raw

源代码: [点击查看](https://github.com/dotliquid/dotliquid/blob/master/src/DotLiquid/Tags/Raw.cs)

``` html
{% raw %}{% if user = 'tobi' %}hi{% endif %}{% endraw %}
```

#### unless

源代码: [点击查看](https://github.com/dotliquid/dotliquid/blob/master/src/DotLiquid/Tags/Unless.cs)

``` html
{% unless x < 0 %} x is greater than zero {% end %}
```

### ZKWeb内置的标签

ZKWeb在DotLiquid的基础上内置了以下的标签:

#### area

参考[动态内容](dynamic_contents/index.html).

``` html
{% area test_area %}
```

#### fetch

把指定路径的执行结果设置到变量, 适用于JsonResult和PlainResult.

``` html
{% fetch /api/login_info > login_info %}
```

#### html_lang

显示当前的请求的语言代号.

``` html
{% html_lang %}
```

#### raw_html

描画原始的内容, 不经过html编码.

``` html
{% assign variable = '<br/>' %}
{% raw_html variable %}
```

### DotLiquid内置的过滤器

DotLiquid内置了以下的过滤器, 源代码可以[查看这里](https://github.com/dotliquid/dotliquid/blob/master/src/DotLiquid/StandardFilters.cs).

#### size

`{{ 'abc' | size }}` == `3`

#### slice

`{{ 'abc' | slice 1,2 }}` == `bc`

#### downcase

`{{ 'Abc' | downcase }}` == `abc`

#### url_encode

`{{ '/' | url_encode }}` == `%2F`

#### capitalize

`{{ 'notThing' | url_encode }}` == `Notthing`

#### escape, h(别名)

`{{ '<strong>' | h }}` == `&lt;strong&gt;`

#### truncate

`{{ '1234567890' | truncate: 7 }}` == `1234...`

#### truncate_words

`{{ 'one two three' | truncate_words: 2 }}` == `one two...`

#### split

`{{ 'This is a sentence' | split: ' ' }}` == `列表对象`

#### strip_html

`{{ '<div>abc</div>' | strip_html }}` == `abc`

#### currency

`{{ 6.72 | currency }}` == `$6.72 (根据当前区域)`

#### strip_newlines

`{{ 'a\r\nb\r\nc' | strip_newlines }}` == `abc`

#### join

`{{ list_123 | join }}` == `1 2 3`

#### sort

`{{ list_321 | sort | join }}` == `1 2 3`

#### map

`{{ list_dogs | map: 'name' }}` == `包含dog.name的列表`

#### replace

`{{ 'a a a' | replace: 'a', 'b' }}` == `b b b`

#### replace_first

`{{ 'a a a' | replace_first: 'a', 'b' }}` == `b a a`

#### remove

`{{ 'aabc' | remove: 'a' }}` == `bc`

#### remove_first

`{{ 'aabc' | remove_first: 'a' }}` == `abc`

#### append

`{{ 'a' | append: 'b' }}` == `ab`

#### prepend

`{{ 'a' | prepend: 'b' }}` == `ba`

#### newline_to_br

`{{ 'a\r\nb' | newline_to_br }}` == `a<br />\r\nb`

#### date

`{{ 'now' | date: 'MM/dd/yyyy' }}` == `当前时间的MM/dd/yyyy格式`

#### first

`{{ list_123 | first }}` == `1`

#### last

`{{ list_123 | last }}` == `3`

#### plus

`{{ 1 | plus: 2 }}` == `3`

#### minus

`{{ 1 | minus: 2 }}` == `-1`

#### times

`{{ 3 | times: 4 }}"` == `12`

#### round

`{{ 1.234678 | round: 3 }}` == `1.235`

#### divided_by

`{{ 15 | divided_by: 3 }}` == `5`

#### modulo

`{{ 3 | modulo: 2 }}` == `1`

#### default

`{{ not_exist | default: '123' }}` == `123`

### ZKWeb内置的过滤器

ZKWeb在DotLiquid的基础上内置了以下的过滤器:<br/>

#### trans

翻译指定的文本，参考[多语言](multi_language/index.html).

`{{ text | trans }}, {{ "fixed text" | trans }}`

#### format

格式化字符串，最多可支持8个参数.

`{{ "name is [0], age is [1]" | format: name, age }}`

#### raw_html

描画原始的内容，不经过html编码.

`{{ variable | raw_html }}`

各个插件中还会有更多的自定义标签和过滤器, 请参考插件的文档说明.
