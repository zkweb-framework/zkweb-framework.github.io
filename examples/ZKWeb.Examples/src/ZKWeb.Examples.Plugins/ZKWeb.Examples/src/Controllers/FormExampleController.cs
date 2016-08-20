#if TODO
using System;
using ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Forms;
using ZKWeb.Web;
using ZKWeb.Web.ActionResults;
using ZKWebStandard.Ioc;
using ZKWebStandard.Web;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Controllers {
	[ExportMany]
	public class FormExampleController : IController {
		[Action("example/form")]
		[Action("example/form", HttpMethods.POST)]
		public IActionResult Form() {
			var form = new ExampleForm();
			if (HttpManager.CurrentContext.Request.Method == HttpMethods.POST) {
				return new JsonResult(form.Submit());
			} else {
				form.Bind();
				return new TemplateResult("zkweb.examples/form.html", new { form });
			}
		}
	}
}
#endif
