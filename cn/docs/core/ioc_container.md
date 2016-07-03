ZKWeb需要使用IoC容器来管理各个插件中的组件。<br/>
ZKWeb提供的IoC容器比较简单，为了性能并不支持对构造函数和成员的依赖注入。<br/>

<h4>简单的使用例子</h4>

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

<h4>全局的IoC容器</h4>

ZKWeb使用了一个全局变量储存IoC容器，这个全局变量在`Application.Ioc`。<br/>
虽然使用了全局变量，但这个全局变量可以在线程范围内使用`Application.OverrideIoc`进行重载，以便测试。<br/>

<h4>在插件中注册组件</h4>

插件可以标记`[ExportMany]`属性注册组件，同时标记`[SingletonReuse]`属性可以注册成单例。<br/>
请注意标记该属性的类需要是公开类，私有类不会被扫描。<br/>
如果不想使用属性注册组件，可以实现`IPlugin`接口在插件载入时手动注册到`Application.Ioc`
