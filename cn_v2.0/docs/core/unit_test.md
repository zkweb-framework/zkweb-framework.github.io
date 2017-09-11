ZKWeb使用了独自编写的测试框架, 编写时参考了XUnit.<br/>
测试可以在控制台中运行, 也可以使用默认的插件集在网页上运行.<br/>
因为相当于在实际环境中运行, 创建的测试可以是单元测试也可以是集成测试.

### 创建测试

测试类需要标记`Tests`属性, 标记后该类下面的所有公开函数都会成为测试函数.<br/>
添加`src\Tests\ExampleTest.cs`, 内容如下<br/>
使用`Assert`可以测试条件是否成立, 如果测试失败会抛出`AssertException`.<br/>
``` csharp
[Tests]
class ExampleTest {
	public void MethodA() {
		Assert.IsTrue(1 == 1);
		Assert.IsTrueWith(1 == 1, "if failed this item will be outputed");
		Assert.Equals(true, true);
		Assert.Throws<ArgumentException>(() => { throw new ArgumentException(); });
	}
}
```

### 运行测试

**在控制台中运行**<br/>
设置主项目到`您的项目名.Console`并运行.<br/>

**在网页上运行**<br/>
参考`UnitTest.WebTester`插件的文档.<br/>

### 在测试中重载Http上下文

在测试时, 有时需要重载Http上下文, <br/>
可以使用`HttpManager.OverrideContext`函数重载当前的上下文.<br/>
在`ExampleTest`类中添加以下函数<br/>
``` csharp
public void MethodB() {
	using (HttpManager.OverrideContext("/?a=1", "POST")) {
		var request = (HttpRequestMock)HttpManager.CurrentContext.Request;
		request.remoteIpAddress = IPAddress.Parse("192.168.168.168");

		Assert.Equals(request.Get<string>("a"), "1");
		Assert.Equals(request.RemoteIpAddress, IPAddress.Parse("192.168.168.168"));
	}
}
```

### 在测试中模拟IoC容器

在测试时, 有时需要影响容器返回的组件, <br/>
可以使用`Application.OverrideContainer`重载当前的IoC容器.<br/>
在`ExampleTest`类中添加以下函数<br/>
``` csharp
public void MethodC() {
	using (Application.OverrideIoc()) {
		Application.Ioc.Unregister<IEntityOperationHandler<ExampleTable>>();
		Assert.IsTrue(!Application.Ioc.ResolveMany<IEntityOperationHandler<ExampleTable>>().Any());
	}
	// override is finished
	Assert.IsTrue(Application.Ioc.ResolveMany<IEntityOperationHandler<ExampleTable>>().Any());
}
```

### 在测试中使用临时数据库

在测试时, 有时需要做涉及到数据库的测试, <br/>
可以使用`TestManager.UseTemporaryDatabase`来让指定范围内的代码使用临时数据库.<br/>
临时数据库默认使用内存数据库, 也可以通过修改网站配置使用指定的数据库.<br/>
在`ExampleTest`类中添加以下函数<br/>
``` csharp
public void MethodD() {
	var testManager = Application.Ioc.Resolve<TestManager>();
	using (testManager.UseTemporaryDatabase()) {
		var databaseManager = Application.Ioc.Resolve<DatabaseManager>();
		using (var context = databaseManager.CreateContext()) {
			var obj = new ExampleTable() { Name = "obj in temporary database" };
			context.Save(ref obj);
		}
	}
}
```
