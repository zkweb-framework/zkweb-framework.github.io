using System;
using ZKWeb.Examples.Plugins.ZKWeb.Examples.src.UIComponents.Forms;
using ZKWeb.Plugins.Common.Base.src.Controllers.Bases;
using ZKWeb.Web;
using ZKWeb.Web.ActionResults;
using ZKWebStandard.Ioc;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Controllers {
	[ExportMany]
	public class FormExampleController : ControllerBase {
		[Action("example/form")]
		[Action("example/form", HttpMethods.POST)]
		public IActionResult Form() {
			var form = new ExampleForm();
			if (Request.Method == HttpMethods.POST) {
				return new JsonResult(form.Submit());
			} else {
				form.Bind();
				return new TemplateResult("zkweb.examples/form.html", new { form });
			}
		}
	}
}
