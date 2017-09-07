为了让插件之间的组件交互更加简单, ZKWeb使用了IoC容器(也叫DI容器).<br/>
ZKWeb中使用的容器是单独开发的, 性能接近目前最快的`DryIoC`.

### 最简单的例子

对于一个IoC容器, 大致上可以把操作分为`注册`和`解决`.<br/>
注册指告诉容器解决一个服务类型的时候需要返回什么,<br/>
而解决则是让容器返回服务类型对应的实例.<br/>

以下是ZKWeb内置的IoC容器最简单的使用例子:

``` csharp
void Example() {
	// 创建一个新的容器
	IContainer container = new Container();
	
	// 注册类型
	container.Register<IAnimal, Dog>();
	container.Register<IAnimal, Cow>();
	container.Register<IAnimalManager, AnimalManager>(ReuseType.Singleton);
	
	// animals包含两个实例, 一个是Dog, 另外一个是Cow
	var animals = container.ResolveMany<IAnimal>()
	
	// animalManager的构造函数会自动传入包含两个实例的列表
	var animalManager = container.Resolve<IAnimalManager>();
	
	// 注册为单例(Singleton)的服务再次解决的时候会使用原来的实例
	// 在这里 animalManager == otherAnimalManager
	var otherAnimalManager = container.Resolve<IAnimalManager>();
}

public interface IAnimal { }
public class Dog : IAnimal { }
public class Cow : IAnimal { }

public interface IAnimalManager { }
public class AnimalManager : IAnimalManager {
	public AnimalManager(IEnumerable<IAnimal> animals) { }
}
```

### 全局的IoC容器

在上面的例子中容器是手动创建的.<br/>
实际开发ZKWeb程序时不需要手动创建容器, 因为ZKWeb中默认有一个全局的.<br/>
访问全局的IoC容器可以使用:

```
var someService = Application.Ioc.Resolve<ISomeService>();
```

使用全局容器会导致代码的可测试性变差,<br/>
如果您想编写更易于测试的代码可以使用构造函数注入策略.

### 构造函数注入

ZKWeb的IoC容器支持构造函数注入, 使用例子如下:<br/>

``` csharp
public interface IXmlWriter { }
public interface IAttributeProvider { }

public class XmlBuilder {
	public XmlBuilder(IXmlWriter writer, IEnumerable<IAttributeProvider> providers) { }
}

void Example() {
	// 创建XmlBuilder的实例，自动注入依赖项
	var xmlBuilder = Application.Ioc.Resolve<XmlBuilder>();
}
```

需要注意的是如果有多个构造函数, ZKWeb会按照以下策略来选择:<br/>

- 查找带`[Inject]`属性的构造函数, 如果找到则使用它
- 查找唯一的一个public构造函数, 如果找到则使用它
- 解决的时候查找`IMultiConstructorResolver`
	- 如果有注册`IMultiConstructorResolver`则使用它来解决
	- 如果未注册则报错提示类型有多个构造函数

目前`IMultiConstructorResolver`在`Asp.Net Core`项目中会默认注册,<br/>
在`Asp.Net`和`Owin`项目中则不会.<br/>
默认的`IMultiConstructorResolver`解决策略如下:<br/>

- 查找所有public构造函数, 按参数个数从多到少排序
- 枚举排序好的构造函数
	- 逐个进行解决
		- 如果解决成功则记录该构造函数, 下次解决时使用它

使用构造函数注入可以改善代码的可测试性, 您可以积极使用这项功能.

### 在插件中注册组件

在插件中可以通过标记属性来自动注册组件, 例如:

``` csharp
[ExportMany]
public class SomeService : ISomeService { }
```

或者

``` csharp
[Export(ServiceType = typeof(ISomeService))]
public class SomeService : ISomeService { }
```

`[ExportMany]`会同时注册`SomeService`和`ISomeService`,<br/>
`[Export]`只会注册`ISomeService`, 也就是`Resolve<SomeService>`会得不到结果.<br/>

这样的注册方式和`MEF`很相似.<br/>
ZKWeb目前不支持按接口类型来注册(扫描时间会很长),<br/>
但是您可以通过重载`Application`自己实现一套注册机制.<br/>

此外, 如果您想替换原有已注册的组件, 可以使用`ClearExists`属性:

``` csharp
[ExportMany(ClearExists = true)]
public class SomeService : ISomeService { }
```

标记`ClearExists`后相当于以下代码:

``` csharp
Application.Ioc.Unregister<ISomeService>();
Application.Ioc.Unregister<SomeService>();
Application.Ioc.RegisterMany<SomeService>();
```

### 组件的生命周期

ZKWeb中的使用的容器支持三种生命周期:

- Transient: 不共享对象, 解决时总是重新创建
- Singleton: 在所有环境共享对象, 解决是总是返回单个实例
- Scoped: 在同一区域(请求)中共享对象, 区域结束后自动释放(调用Dispose)

标记组件的生命周期可以使用`[SingletonReuse]`或者`[ScopedReuse]`<br/>

``` csharp
[ExportMany, SingletonReuse]
public class SomeService : ISomeService { }
```

### IServiceProvider整合

目前ZKWeb的容器实现了微软的DI接口, 并且支持替换Asp.Net Core中使用的容器.<br/>

注册`IServiceCollection`可以使用

``` csharp
Application.Ioc.RegisterFromServiceCollection(serviceCollection);
```

获取`IServiceProvider`可以使用

``` csharp
// 多次获取会返回同一个实例
var serviceProvider = Application.Ioc.AsServiceProvider();
```

目前项目生成器创建的Asp.Net Core项目都会替换默认的容器, 您不需要手动写上面的代码.
