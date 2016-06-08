ZKWeb使用了独自编写的单元测试框架，编写时参考了XUnit。<br/>
使用默认的插件可以在后台网页上运行单元测试，也可以在控制台上运行。<br/>
ZKWeb的单元测试支持模拟Http上下文和IoC容器。<br/>

测试可以直接保存在对应的插件文件夹下，也可以单独一个插件保存。<br/>
为了便于管理，ZKWeb的插件测试都直接保存在对应插件文件夹下。<br/>

### <h2>创建测试</h2>

添加`Example\src\Tests\ExampleTest.cs`，内容如下<br/>
使用`Assert`类测试条件是否成立，如果测试失败会抛出`AssertException`。<br/>
``` csharp
[UnitTest]
class ExampleTest {
	public void MethodA() {
		Assert.IsTrue(1 == 1);
		Assert.IsTrueWith(1 == 1, "if failed this item will be outputed");
		Assert.Equals(true, true);
		Assert.Throws<ArgumentException>(() => { throw new ArgumentException(); });
	}
}
```

### <h2>运行测试</h2>

在控制台中运行:<br/>
设置主项目到`ZKWeb.Console`，并运行。<br/>

在后台中运行:<br/>
需要`UnitTest.WebTester`插件，登陆到后台点击"单元测试"并点击"运行"或"全部运行"即可。<br/>

### <h2>在测试中模拟Http上下文</h2>

在测试部分功能时，有时需要模拟Http上下文，<br/>
可以使用`HttpContextUtils.OverrideContext`函数重载当前的上下文。<br/>
在`ExampleTest`类中添加以下函数<br/>
``` csharp
public void MethodB() {
	using (HttpContextUtils.OverrideContext("/?a=1", "POST")) {
		var request = (HttpRequestMock)HttpContextUtils.CurrentContext.Request;
		request.userHostAddress = "192.168.168.168";

		Assert.Equals(request.Get<string>("a"), "1");
		Assert.Equals(request.UserHostAddress, "192.168.168.168");
	}
}
```

### <h2>在测试中模拟IoC容器</h2>

在测试部分功能时，有时需要影响容器返回的组件，
可以使用`Application.OverrideContainer`重载当前的IoC容器。<br/>
在`ExampleTest`类中添加以下函数<br/>
``` csharp
public void MethodC() {
	using (Application.OverrideIoc()) {
		Application.Ioc.Unregister<IDataDeleteCallback<ExampleTable>>();
		Assert.IsTrue(!Application.Ioc.ResolveMany<IDataDeleteCallback<ExampleTable>>().Any());
	}
	// override is finished
	Assert.IsTrue(Application.Ioc.ResolveMany<IDataDeleteCallback<ExampleTable>>().Any());
}
```

模拟Ioc容器时注意旧的容器只是作为`fallback`使用，新的容器有注册的组件时，只会返回新的容器注册的组件。<br/>

### <h2>在测试中使用临时数据库</h2>

单元测试时可以在一定范围使用一个空白的临时数据库。<br/>
临时数据库使用了sqlite + 临时文件，结束后会自动删除该文件。<br/>
如果需要查看临时数据库的内容，可以在using结束之前下一个断点，然后用数据库浏览器打开临时文件夹下的数据库文件。<br/>
``` csharp
var unitTestManager = Application.Ioc.Resolve<UnitTestManager>();
using (unitTestManager.UseTemporaryDatabase()) {
	// 数据库操作代码
}
```
