ZKWeb的插件系统以文件夹为单位，每个文件夹等于一个插件。<br/>
插件的编译由核心框架完成，不依赖VS。<br/>
下面使用VS创建插件是因为编写插件时可以有代码提示和调试支持。<br/>

### 插件的目录结构

- ZKWeb.Plugins (默认的插件根目录)
	- Common.Base (插件文件夹)
	- Common.Admin (插件文件夹)
	- ...
- ZKWeb.Example (示例的插件根目录)
	- ExampleForDocument (插件文件夹)
		- bin (储存这个插件编译出来的dll文件)
		- src (储存代码文件，发布时可以把这个文件夹除外)
		- static (储存静态文件)
		- templates (储存模板文件)
	- OtherExample (插件文件夹)
		- 同上

在`config.json`的`PluginDirectories`可以指定一个或多个插件根目录。

### 创建插件
步骤

- 打开Visual Studio，创建项目，类型选“类库”，名称填`ExampleForDocument`，不要勾选为解决方案创建文件夹。</p>
- 在项目中引用`ZKWeb\bin`下的所有dll
- 在项目下添加文件夹`Example`

创建后的目录结构应该如下:<br/>
ZKWeb.Example文件夹需要自己创建，或者使用github上现成项目。

- ZKWeb
- ZKWeb.Example
	- ExampleForDocument
		- Example

![](../img/project.jpg)

### 引用插件
步骤

- 打开`ZKWeb\App_Data\config.json`
- 修改`PluginDirectories`，在后面添加`"../../ZKWeb.Examples/ExampleForDocument"`
- 修改`Plugins`，在后面添加`"Example"`
- 刷新浏览器，在后台中查看"关于网站"是否显示了这个插件，点击右上角图标会显示关于网站

引用插件时需要注意依赖顺序，在前面的插件会先加载。
这里的顺序也会影响到IoC容器中的注册顺序和模板文件的查找顺序。

![](../img/add_plugin.jpg)

### 添加插件说明

在`Example`文件夹下创建`plugin.json`，内容如下
```
{
	"Name": "Example",
	"Description": "Example plugin for document"
}
```

刷新关于网站即可看到新的名称和说明。

### 添加代码

插件的代码都保存在`src`文件夹下，可以参考文档顶部的目录结构。<br/>

这里添加在插件加载时输出日志的处理:<br/>
在src文件夹中创建Plugin.cs，内容如下（引用和命名空间省略，后面的文档也如此）<br/>
如果提示找不到Resolve函数请手动添加`using DryIoc;`到代码顶部。<br/>

``` csharp
/// <summary>
/// 插件加载时输出一行日志
/// </summary>
[ExportMany]
public class Plugin : IPlugin {
	public Plugin() {
		var logManager = Application.Ioc.Resolve<LogManager>();
		logManager.LogDebug("Example plugin loaded");
	}
}
```

刷新浏览器，在App_Data/Logs下可以看到输出的日志。<br/>
ExportMany和Ioc将在下一节解释。<br/>

### Ioc容器

ZKWeb使用了DryIoc，请先阅读DryIoc的文档:<br/>
https://bitbucket.org/dadhi/dryioc/wiki/Home<br/>

ZKWeb全局使用的容器在`Application.Ioc`。<br/>
（为什么不使用构造函数注入请参考`global.asax.cs`中的说明，使用全局变量仍然很好的可以支持单元测试）<br/>
插件可以使用`[ExportMany]`注册组件（参考"添加代码"节），也可以使用`IPlugin`接口在网站启动时注册到`Application.Ioc`中。<br/>
指定`[SingletonReuse]`属性时可以实现单例。<br/>

DryIoc的性能非常高，可以查看以下的对比:<br/>
http://www.palmmedia.de/Blog/2011/8/30/ioc-container-benchmark-performance-comparison<br/>

### Ioc容器的使用例子
以下是一个简单的使用例子。

``` csharp
void Example() {
	var animals = Application.Ioc.ResolveMany<IAnimal>()
	// animals contains instances of Dog and Cow
	
	var animalManager = Application.Ioc.Resolve<IAnimalManager>();
	// animalManager is AnimalManager
	
	var otherAnimalManager = Application.Ioc.Resolve<IAnimalManager>();
	// animalManager only create once, otherAnimalManager == animalManager
}
public interface IAnimal { }
[ExportMany] public class Dog : IAnimal { }
[ExportMany] public class Cow : IAnimal { }

public interface IAnimalManager { }
[ExportMany, SingletonUsage] public class AnimalManager : IAnimalManager { }
```

### 组件注册顺序

插件的定义顺序会影响到组件注册顺序，例如`Plugins: [ "A", "B" ]`，<br/>
插件A有`class ExampleHandlerA : IExampleHandler { }`，<br/>
插件B有`class ExampleHandlerB : IExampleHandler { }`，<br/>
这时使用`Application.Ioc.ResolveMany<IExampleHandler>()`会获取到`[ExampleHandlerA, ExampleHandlerB]`。

### 调试插件

调试插件有两种办法，<br/>
第一种是在插件的VS中选择调试-挂载到进程并选择IIS进程挂载。<br/>
第二种是把需要调试的文件拉到打开ZKWeb的VS中，然后在里面下断点。<br/>
![](../img/attach_process.jpg)
