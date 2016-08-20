#if TODO
using System;
using ZKWeb.Examples.Plugins.ZKWeb.Examples.src.StaticTableCallbacks;
using ZKWeb.Plugins.Common.Base.src.Extensions;
using ZKWeb.Plugins.Common.Base.src.Model;
using ZKWeb.Web;
using ZKWeb.Web.ActionResults;
using ZKWebStandard.Ioc;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Controllers {
	[ExportMany]
	public class StaticTableExampleController : IController {
		[Action("example/static_table")]
		public IActionResult StaticTable() {
			var request = StaticTableSearchRequest.FromHttpRequest();
			var response = request.BuildResponseFromDatabase(new[] { new ExampleStaticTableCallback() });
			return new TemplateResult("zkweb.examples/static_table.html", new { response });
		}
	}
}
#endif