#if TODO
using ZKWeb.Plugins.Common.Admin.src.Model;
using ZKWeb.Plugins.Common.Admin.src.Scaffolding;
using ZKWeb.Web;
using ZKWeb.Web.ActionResults;
using ZKWebStandard.Ioc;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.AdminApps {
	[ExportMany]
	public class ExampleApp : SimpleAdminAppBuilder {
		// Name和Url必须提供
		public override string Name { get { return "ExampleApp"; } }
		public override string Url { get { return "/admin/example_app"; } }
		// 可选，如果需要指定图标颜色和内容
		public override string TileClass { get { return "tile bg-navy"; } }
		public override string IconClass { get { return "fa fa-rocket"; } }
		// 可选，默认只要求管理员不要求指定权限
		public override UserTypes[] AllowedUserTypes { get { return UserTypesGroup.Admin; } }
		public override string[] RequiredPrivileges { get { return new[] { "ExampleApp:View" }; } }

		protected override IActionResult Action() {
			return new TemplateResult("example/example_admin_app.html");
		}
	}
}
#endif
