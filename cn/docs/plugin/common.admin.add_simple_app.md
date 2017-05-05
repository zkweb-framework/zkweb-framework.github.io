添加后台功能可以继承`SimpleAdminAppControllerBase`实现。<br/>

添加`src\Controllers\ExampleAdminController.cs`
``` csharp
/// <summary>
/// 示例的后台应用
/// </summary>
[ExportMany]
public class ExampleAdminController : SimpleAdminAppControllerBase {
	// Name和Url必须提供
	public override string Name { get { return "ExampleApp"; } }
	public override string Url { get { return "/admin/example_app"; } }
	// 可选，如果需要指定图标颜色和内容
	public override string TileClass { get { return "tile bg-navy"; } }
	public override string IconClass { get { return "fa fa-rocket"; } }
	// 可选，默认只要求管理员不要求指定权限
	public override Type RequiredUserType { get { return typeof(IAmAdmin); } }
	public override string[] RequiredPrivileges { get { return new[] { "ExampleApp:View" }; } }

	protected override IActionResult Action() {
		return new TemplateResult("example/example_admin_app.html");
	}
}
```

添加`templates\example\example_admin_app.html`
``` html
{% use_title "Example Admin App" -%}
{% include common.admin/header.html %}
<div class="portlet-title">
	<div class="caption">
		<i class="fa fa-rocket"></i>
		<span class="caption-subject">{{ "Example Admin App" | trans }}</span>
	</div>
</div>
<div class="portlet-body">
	hello admin app
</div>
{% include common.admin/footer.html %}
```

刷新后台可以看到多出了`ExampleApp`的图标<br/>
![](../img/example_admin_app.jpg)

进入后会显示`Action`返回的内容<br/>
![](../img/example_admin_app_page.jpg)
