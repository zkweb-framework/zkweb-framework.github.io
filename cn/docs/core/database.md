ZKWeb使用了NHibernate来管理数据库和查询数据。<br/>
目前支持的数据库服务器有PostgreSQL, SQLite, MSSQL, MySQL。<br/>
使用NHibernate的理由有<br/>

- 不保存数据库状态和更新历史，不会像EF一样容易出现metadata相关的错误
- 可以实现自动更新数据库，添加字段后不需要任何额外操作就可以应用到数据表
- 更好的支持MySQL等非微软的数据库，不会像EF只对MSSQL支持好。

ZKWeb同时使用了FluentNHibernate来定义数据结构，避免使用繁杂的xml。<br/>

### <h2>添加数据表</h2>

添加`Example\src\Database\ExampleTable.cs`，内容如下
``` csharp
/// <summary>
/// 示例数据
/// </summary>
public class ExampleTable {
	public virtual long Id { get; set; }
	public virtual string Name { get; set; }
	public virtual DateTime CreateTime { get; set; }
}

/// <summary>
/// 示例数据表的结构
/// </summary>
[ExportMany]
public class ExampleTableMap : ClassMap<ExampleTable> {
	public ExampleTableMap() {
		Id(e => e.Id);
		Map(e => e.Name).Length(0xffff); // 0xffff `== no limit, you can confirm later
		Map(e => e.CreateTime).Not.Nullable();
	}
}
```

添加完毕后刷新浏览器可以看到数据库中多出这个表。<br/>
更多用法可以参考FluentNHibernte的文档。<br/>
https://github.com/jagregory/fluent-nhibernate/wiki/Getting-started<br/>
在ZKWeb中已有插件正常使用一对多(Reference, HasMany)，多对多(ManyToMany)的功能。<br/>

### <h2>升级数据表</h2>

在ZKWeb中升级数据库只需要修改类并且刷新浏览器即可。<br/>
在ExampleTable中添加以下成员<br/>
``` csharp
public virtual bool Deleted { get; set; }
```

在ExampleTableMap的构造函数中添加以下行<br/>
``` csharp
Map(e => e.Deleted);
```

保存后刷新浏览器即可看到效果。<br/>
注意ZKWeb中可以自动添加新增的字段，但是不能修改或删除原有字段。<br/>
![](../img/example_table.jpg)

### <h2>增删查改</h2>

通过`DatabaseManager.GetContext`获取数据库上下文可以进行增删查改操作<br/>
如果使用了`Common.Base`插件可以使用`GenericRepository`和`UnitOfWork`类进行更简单的操作，<br/>
详细请参考`Common.Base`的文档。<br/>

以下函数可以添加到`ExampleController`用于测试是否正常工作。<br/>

新增数据<br/>
使用`ref data`的原因是因为NHibernate插入数据时会返回另外一个对象。<br/>
操作完毕后需要使用`SaveChanges`提交事务，为了保证数据一致性ZKWeb中的操作都会默认开启事务。<br/>
``` csharp
[Action("example/add_data")]
public string AddData() {
	var databaseManager = Application.Ioc.Resolve<DatabaseManager>();
	using (var context = databaseManager.GetContext()) {
		var data = new ExampleTable() {
			Name = "test",
			CreateTime = DateTime.UtcNow,
			Deleted = false
		};
		context.Save(ref data);
		context.SaveChanges(); // don't forget this
	}
	return "success";
}
```

修改数据<br/>
这里会修改表中的所有数据。<br/>
和新增数据一样，修改数据也会使用`Save`函数，但是修改操作应该在后面的参数中实现，<br/>
这样使用数据库事件可以捕捉到修改前和修改后的数据。<br/>
``` csharp
[Action("example/update_data")]
public string UpdateData() {
	var databaseManager = Application.Ioc.Resolve<DatabaseManager>();
	using (var context = databaseManager.GetContext()) {
		foreach (var data in context.Query<ExampleTable>()) {
			var localData = data;
			context.Save(ref localData, d => d.Name = "updated");
		}
		context.SaveChanges(); // don't forget this
	}
	return "success";
}
```

查询数据<br/>
这里返回没有更新的数据内容。<br/>
查询时不需要调用`SaveChanges`函数。<br/>
``` csharp
[Action("example/query_data")]
public string QueryData() {
	var databaseManager = Application.Ioc.Resolve<DatabaseManager>();
	using (var context = databaseManager.GetContext()) {
		var notUpdated = context.Query<ExampleTable>()
			.Where(t => t.Name != "updated").ToList();
		return string.Format("these objects are not updated:\r\n{0}",
			JsonConvert.SerializeObject(notUpdated, Formatting.Indented));
	}
}
```

删除数据<br/>
``` csharp
[Action("example/remove_data")]
public string RemoveData() {
	var databaseManager = Application.Ioc.Resolve<DatabaseManager>();
	using (var context = databaseManager.GetContext()) {
		long deleted = context.DeleteWhere<ExampleTable>(d => d.Name == "updated");
		context.SaveChanges(); // don't forget this
		return string.Format("{0} objects are removed", deleted);
	}
}
```

### <h2>数据事件</h2>

ZKWeb支持定义事件监听数据的增删查改<br/>
其中增加和修改使用`IDataSaveCallback`，删除使用`IDataDeleteCallback`。<br/>
在Before或After函数中可以通过传入的`context`参数修改关联的数据，<br/>
在Before或After函数中抛出例外可以阻止事务提交。<br/>

保存事件的示例
添加`Example\src\DataCallbacks\ExampleDataSaveCallback.cs`，内容如下<br/>
这个处理器会在数据插入或名称改变时记录到日志。<br/>
``` csharp
/// <summary>
/// 保存事件的示例
/// </summary>
[ExportMany]
public class ExampleDataSaveCallback : IDataSaveCallback<ExampleTable> {
	private long IdBeforeSave { get; set; }
	private string NameBeforeSave { get; set; }

	public void BeforeSave(DatabaseContext context, ExampleTable data) {
		IdBeforeSave = data.Id;
		NameBeforeSave = data.Name;
	}

	public void AfterSave(DatabaseContext context, ExampleTable data) {
		var logManager = Application.Ioc.Resolve<LogManager>();
		if (IdBeforeSave <= 0) {
			logManager.LogDebug(string.Format("example data inserted, id is {0}", data.Id));
		} else if (NameBeforeSave != data.Name) {
			logManager.LogDebug(string.Format("example data name changed, id is {0}", data.Id));
		}
	}
}
```

添加或更新数据后可以查看`ZKWeb\App_Data\Logs`下的日志是否记录成功。<br/>

删除事件的示例<br/>
添加`Example\src\DataCallbacks\ExampleDataDeleteCallback.cs`，内容如下<br/>
这个处理器会在数据删除时记录到日志。<br/>
``` csharp
/// <summary>
/// 删除事件的示例
/// </summary>
[ExportMany]
public class ExampleDataDeleteCallback : IDataDeleteCallback<ExampleTable> {
	public void BeforeDelete(DatabaseContext context, ExampleTable data) {
	}

	public void AfterDelete(DatabaseContext context, ExampleTable data) {
		var logManager = Application.Ioc.Resolve<LogManager>();
		logManager.LogDebug(string.Format("example data deleted, id is {0}", data.Id));
	}
}
```

删除数据后可以查看`ZKWeb\App_Data\Logs`下的日志是否记录成功。<br/>

### <h2>原生查询</h2>

ZKWeb在支持数据事件时牺牲了一定的性能，包括不能实现真正的批量操作。<br/>
但你可以通过原生查询实现，使用原生查询时将不能支持数据事件等高级功能。<br/>

NHibernate的会话可以通过`context.Session`获取到。
``` csharp
context.Session.Save(new ExampleTable());
```

执行储存过程（添加、更新或删除）
``` csharp
var query = context.Session.CreateSQLQuery("exec some_update_sp @arg");
query.SetParameter("arg", 1);
int affected = query.ExecuteUpdate();
```

执行储存过程（查询）
``` csharp
var query = context.Session.CreateSQLQuery("exec some_query_sp @arg");
query.SetParameter("arg", 1);
var result = query.Enumerable<ExampleTable>();
```
