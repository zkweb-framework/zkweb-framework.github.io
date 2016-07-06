特征类`Trait`用于在外部标记指定类型的某些特征，但不需要修改原有的类型。<br/>
特征类一般用于泛型类型的处理，<br/>
例如`GenericRepository`需要通过`EntityTrait`来获取实体类型的主键名称和类型。<br/>

### 基础插件提供的特征类<br/>

- EntityTrait (主键名称和类型)
- RecyclableTrait (是否可回收)

### 使用特征类的例子

以下是生成TData主键比较表达式的代码。<br/>
``` csharp
var entityTrait = EntityTrait.For<TData>();
var expression = ExpressionUtils.MakeMemberEqualiventExpression<TData>(entityTrait.PrimaryKey, id);
```

## 提供自定义特征类

在插件初始化时可以注册自定义的特征到容器中。<br/>
``` csharp
Application.Ioc.RegisterInstance(
	new EntityTrait() { PrimaryKey = "Id", PrimaryKeyType = typeof(Guid) },
	serviceKey: typeof(ExampleTableUseGuidPrimaryKey));
```
