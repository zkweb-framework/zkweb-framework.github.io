管理员插件使用了以下的特征接口

- `IHaveOwner`
	- 类型是否有所属用户的特征

### 类型是否有所属用户的特征

使用特征
```
var haveOwner = OwnerTypeTrait<TEntity>.HaveOwner;
```

这个特征用于标记数据是否有所属的用户, <br/>
例如每个用户只能看到自己的短信息, 不能看到别人的短信息.<br/>

### 使用过滤器过滤只属于自身的数据

``` csharp
var uow = Application.Ioc.Resolve<IUnitOfWork>();
var filter = new OwnerFilter();
using (uow.Scope())
using (uow.EnableQueryFilter(filter))
using (uow.EnableOperationFilter(filter)) {
	// 在这里只能查询属于自己的数据
	// 尝试写入不属于自己的数据时会抛出错误
}
```
