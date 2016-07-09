管理员插件使用了以下的特征类

- `UserOwnedTrait`
	- 类型是否有所属用户的特征

### 类型是否有所属用户的特征

使用特征
```
var trait = UserOwnedTrait.For<TypeName>();
if (trait.IsUserOwned) {
	var property = trait.PropertyName;
}
```

这个特征用于标记数据是否有所属的用户，<br/>
例如每个用户只能看到自己的短信息，不能看到别人的短信息。<br/>
特征默认会检测类型是否有`Owner`或`User`属性，如果有则标记这个类型有所属的用户。<br/>
特征可以通过容器修改，请参考基础插件的[特征类](common.base.traits)
