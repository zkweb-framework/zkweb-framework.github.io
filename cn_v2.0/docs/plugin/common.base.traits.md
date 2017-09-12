让实体类型继承一些特征接口可以提供自动的处理.<br/>
基础插件中提供了以下的特征接口<br/>

- IHaveCreateTime: 实体有创建时间, 创建时自动设置
- IHaveUpdateTime: 实体有更新时间, 更新时自动设置
- IHaveDeleted: 实体有标记删除字段, 默认查询标记未删除的数据

特征接口一般配合工作单元过滤器使用, 请参考[仓储和工作单元](common.base.repository_uow)

### 使用帮助类获取实体的特征

```csharp
var haveDeleted = DeletedTypeTrait<TEntity>.HaveDeleted;
```
