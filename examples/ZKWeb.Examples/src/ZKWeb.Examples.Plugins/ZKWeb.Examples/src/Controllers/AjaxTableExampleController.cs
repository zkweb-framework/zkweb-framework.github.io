using System;
using ZKWeb.Examples.Plugins.ZKWeb.Examples.src.UIComponents.AjaxTableHandlers;
using ZKWeb.Plugins.Common.Base.src.Controllers.Bases;
using ZKWeb.Plugins.Common.Base.src.UIComponents.AjaxTable;
using ZKWeb.Plugins.Common.Base.src.UIComponents.AjaxTable.Extensions;
using ZKWeb.Plugins.Common.Base.src.UIComponents.Forms;
using ZKWeb.Plugins.Common.Base.src.UIComponents.Forms.Attributes;
using ZKWeb.Web;
using ZKWeb.Web.ActionResults;
using ZKWebStandard.Extensions;
using ZKWebStandard.Ioc;
using ZKWebStandard.Web;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Controllers {
	[ExportMany]
	public class AjaxTableExampleController : ControllerBase {
		[Action("example/ajax_table")]
		public IActionResult AjaxTable() {
			var table = new AjaxTableBuilder();
			table.Id = "ExampleTable";
			table.Target = "/example/ajax_table_search";
			var searchBar = new AjaxTableSearchBarBuilder();
			searchBar.TableId = table.Id;
			searchBar.Conditions.Add(new FormField(new CheckBoxFieldAttribute("Deleted")));
			return new TemplateResult("zkweb.examples/ajax_table.html", new { table, searchBar });
		}

		[Action("example/ajax_table_search", HttpMethods.POST)]
		public IActionResult AjaxTableSearch() {
			var json = Request.Get<string>("json");
			var request = AjaxTableSearchRequest.FromJson(json);
			var callbacks = new ExampleAjaxTableHandler().WithExtraHandlers();
			var response = request.BuildResponse(callbacks);
			return new JsonResult(response);
		}
	}
}
