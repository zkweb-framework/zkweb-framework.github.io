using System;
using ZKWeb.Examples.Plugins.ZKWeb.Examples.src.UIComponents.StaticTableHandlers;
using ZKWeb.Plugins.Common.Base.src.Controllers.Bases;
using ZKWeb.Plugins.Common.Base.src.UIComponents.StaticTable;
using ZKWeb.Plugins.Common.Base.src.UIComponents.StaticTable.Extensions;
using ZKWeb.Web;
using ZKWeb.Web.ActionResults;
using ZKWebStandard.Ioc;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Controllers {
	[ExportMany]
	public class StaticTableExampleController : ControllerBase {
		[Action("example/static_table")]
		public IActionResult StaticTable() {
			var request = StaticTableSearchRequest.FromHttpRequest();
			var handlers = new ExampleStaticTableHandler().WithExtraHandlers();
			var response = request.BuildResponse(handlers);
			return new TemplateResult("zkweb.examples/static_table.html", new { response });
		}
	}
}
