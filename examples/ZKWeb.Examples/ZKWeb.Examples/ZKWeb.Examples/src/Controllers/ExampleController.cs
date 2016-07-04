using ZKWeb.Web;
using ZKWeb.Web.ActionResults;
using ZKWebStandard.Ioc;

namespace ZKWeb.Examples.ZKWeb.Examples.src.Controllers {
	[ExportMany]
	public class ExampleController : IController {
		[Action("example/plain_text")]
		public IActionResult PlainText() {
			return new PlainResult("some plain text");
		}

		[Action("example/plain_string")]
		public string PlainString() {
			return "some plain string";
		}

		[Action("example/template")]
		public IActionResult Template() {
			return new TemplateResult("zkweb.examples/hello.html", new { text = "World" });
		}

		[Action("example/file")]
		public IActionResult File() {
			return new FileResult("D:\\1.txt");
		}
	}
}
