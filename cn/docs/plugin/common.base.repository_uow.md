为了简化和规范数据库操作，基础插件提供了通用仓储`GenericRepository`和工作单元`UnitOfWork`。<br/>
`GenericRepository`提供了通用的操作函数，`UnitOfWork`负责管理数据库上下文和事务的提交。<br/>

### 实现自定义的仓储

通用仓储已经实现增删查改等最通用的功能，如果还需要其他功能则需要添加自定义的仓储类型。<br/>
自定义仓储推荐继承`GenericRepository`，直接继承`IRepository`虽然可以使用但是不能重载增删查改等操作。

**添加自定义仓储的例子**<br/>
添加`src\Repositories\ExampleRepository.cs`<br/>
``` csharp
[ExportMany]
public class ExampleRepository : GenericRepository<ExampleTable> {
	public virtual long CountNotDeleted() {
		return Count(t => !t.Deleted);
	}
}
```

### 使用工作单元和仓储

工作单元的职责是负责管理数据库上下文和提交事务，仓储的职责是负责操作数据，<br/>
仓储使用的数据库上下文应该由工作单元传入。<br/>
为了保证数据一致性，工作单元总是会获取包含事务的数据库上下文。<br/>

工作单元`UnitOfWork`类提供了以下的函数

- `void Read(Action<DatabaseContext> func)`
	- 读取数据，数据库上下文在函数执行完毕后自动注销
	- 要求函数返回指定类型的对象时可以使用以下版本
	- `TResult Read<TResult>(Func<DatabaseContext, TResult> func)`
- `void Write(Action<DatabaseContext> func)`
	- 写入数据，数据库上下文在函数执行完毕后自动保存修改，提交事务和注销
	- 要求函数返回指定类型的对象时可以使用以下版本
	- `TResult Write<TResult>(Func<DatabaseContext, TResult> func)`
- `void ReadData<TData>(Action<GenericRepository<TData>> func)`
	- 自动获取`TData`类型对应的仓储对象并读取数据
	- 要求函数返回指定类型的对象时可以使用以下版本
	- `TResult ReadData<TData, TResult>(Func<GenericRepository<TData>, TResult> func)`
- `void WriteData<TData>(Action<GenericRepository<TData>> func)`
	- 自动获取`TData`类型对应的仓储对象写入数据
	- 要求函数返回指定类型的对象时可以使用以下版本
	- `TResult WriteData<TData, TResult>(Func<GenericRepository<TData>, TResult> func)`
- `void ReadRepository<TRepository>(Action<TRepository> func)`
	- 自动获取`TRepository`类型的仓储并读取数据
	- 要求函数返回指定类型的对象时可以使用以下版本
	- `TResult ReadRepository<TRepository, TResult>(Func<TRepository, TResult> func)`
- `void WriteRepository<TRepository>(Action<TRepository> func)`
	- 自动获取`TRepository`类型的仓储并写入数据
	- 要求函数返回指定类型的对象时可以使用以下版本
	- `TResult WriteRepository<TRepository, TResult>(Func<TRepository, TResult> func)`

在同一个工作单元中使用多个仓储时需要手动使用`RepositoryResolver`。
``` csharp
UnitOfWork.Read(context => {
	var repository = RepositoryResolver.Resolve<ExampleTable>(context);
	var otherRepository = RepositoryResolver.Resolve<OtherTable>(context);
});
```

### 使用仓储和工作单元的例子

这个例子在控制器中添加了工作单元的操作函数是为了演示功能，<br/>
实际推荐建立`Manager`或`Service`类来存放业务代码，不推荐在控制器中写业务处理。<br/>

添加`src\Controllers\UowExampleController.cs`<br/>
``` csharp
[Action("example/uow")]
public string Uow() {
	// insert data
	string name = RandomUtils.RandomString(5);
	UnitOfWork.WriteData<ExampleTable>(r => {
		var data = new ExampleTable() { Name = name };
		r.Save(ref data);
	});
	// read inserted data
	var readData = UnitOfWork.ReadData<ExampleTable, ExampleTable>(
		r => r.Get(t => t.Name == name));
	return JsonConvert.SerializeObject(readData);
}
```
