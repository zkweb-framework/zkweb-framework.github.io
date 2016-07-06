using System;
using ZKWeb.Examples.Plugins.ZKWeb.Examples.src.AjaxTableCallbacks;
using ZKWeb.Plugins.Common.Base.src.Extensions;
using ZKWeb.Plugins.Common.Base.src.Model;
using ZKWeb.Plugins.Common.Base.src.Scaffolding;
using ZKWeb.Web;
using ZKWeb.Web.ActionResults;
using ZKWebStandard.Extensions;
using ZKWebStandard.Ioc;
using ZKWebStandard.Web;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Controllers {
	[ExportMany]
	public class AjaxTableExampleController : IController {
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
			var json = HttpManager.CurrentContext.Request.Get<string>("json");
			var request = AjaxTableSearchRequest.FromJson(json);
			var callbacks = new ExampleAjaxTableCallback().WithExtensions();
			var response = request.BuildResponseFromDatabase(callbacks);
			return new JsonResult(response);
		}
	}
}