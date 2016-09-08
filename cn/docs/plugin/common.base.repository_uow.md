为了简化和规范数据库操作，基础插件提供了工作单元,仓储和领域服务的基础类。<br/>
工作单元负责数据库上下文的管理和事务的提交，支持嵌套使用。<br/>
仓储负责数据的查询和修改，领域服务负责提供各种业务功能。<br/>

### 添加仓储

仓储需要继承`IRepository<TEntity, TPrimaryKey>`，<br/>
一般情况推荐继承`RepositoryBase`，这个基础类提供了增删查改函数和对工作单元过滤器的支持。<br/>

添加`src\Domain\Repositories\ExampleRepository.cs`<br/>
``` csharp
[ExportMany]
public class ExampleRepository : RepositoryBase<ExampleTable, long> {
}
```

### 添加领域服务

领域服务需要继承`IDomainService<TEntity, TPrimaryKey>`，<br/>
一般情况推荐继承`DomainServiceBase`，这个基础类提供了基础的增删查改函数。<br/>

添加`src\Domain\Services\ExampleService.cs`<br/>
```csharp
[ExportMany]
public class ExampleService : DomainServiceBase<ExampleTable, long> {
}
```

### 工作单元的使用

先使用Scope创建工作单元，然后在using的范围内使用仓储或领域服务即可。<br/>
工作单元可以嵌套使用，事务默认不开启，开启可以使用`Context.BeginTransaction`和`Context.FinishTransaction`。<br/>
工作单元还支持全局的查询和操作过滤器，请参考下面的说明。<br/>

``` csharp
/// <summary>
/// 工作单元的接口
/// </summary>
public interface IUnitOfWork {
	/// <summary>
	/// 当前的数据库上下文
	/// 不存在时抛出错误
	/// </summary>
	IDatabaseContext Context { get; }

	/// <summary>
	/// 当前的查询过滤器列表
	/// 不存在时抛出错误
	/// </summary>
	IList<IEntityQueryFilter> QueryFilters { get; set; }

	/// <summary>
	/// 当前的操作过滤器列表
	/// 不存在时抛出错误
	/// </summary>
	IList<IEntityOperationFilter> OperationFilters { get; set; }

	/// <summary>
	/// 在指定的范围内使用工作单元
	/// 工作单元中可以使用相同的上下文和过滤器，并且和其他工作单元隔离
	/// 这个函数可以嵌套使用，嵌套使用时都使用最上层的数据库上下文
	/// </summary>
	/// <returns></returns>
	IDisposable Scope();
}
```

### 在工作单元中调用领域服务

添加`src\Controllers\UowExampleController.cs`<br/>
``` csharp
[ExportMany]
public class UowExampleController : ControllerBase {
	[Action("example/uow")]
	public string Uow() {
		// insert data
		var uow = Application.Ioc.Resolve<IUnitOfWork>();
		var service = Application.Ioc.Resolve<IDomainService<ExampleTable, long>>();
		string name = RandomUtils.RandomString(5);
		using (uow.Scope()) {
			var data = new ExampleTable() { Name = name };
			service.Save(ref data);
		}
		// read inserted data
		using (uow.Scope()) {
			var readData = service.Get(t => t.Name == name);
			return JsonConvert.SerializeObject(new { readData });
		}
	}
}
```

### 工作单元过滤器

工作单元支持全局和局部指定过滤器。<br/>
过滤器分查询过滤器`IEntityQueryFilter`和操作过滤器`IEntityOperationFilter`。<br/>
需要提供全局过滤器时可以继承过滤器的接口并使用`[ExportMany]`注册到容器。

局部启用和禁用过滤器的例子
```
using (uow.Scope())
using (uow.DisableQueryFilter(typeof(DeletedFilter)))
using (uow.EnableQueryFilter(new DeletedFilter(true))) {
	// 在这里只能查询到Deleted为true的数据
}
```

基础插件默认提供了以下的过滤器，以下的过滤器默认全局有效

- CreateTimeFilter: 继承了`IHaveCreateTime`的实体创建时自动设置创建时间
- UpdateTimeFilter: 继承了`IHaveUpdateTime`的实体保存时自动设置更新时间
- DeletedFilter: 继承了`IHaveDeleted`的实体查询时只能查到`Deleted`等于`false`的数据
- GuidEntityFilter: 继承了`IEnity<Guid>`的实体创建时自动调用`GuidUtils.SequentialGuid`设置主键
