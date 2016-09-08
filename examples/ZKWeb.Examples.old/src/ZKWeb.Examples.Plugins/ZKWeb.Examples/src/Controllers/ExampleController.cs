using ZKWeb.Web;
using ZKWeb.Web.ActionResults;
using ZKWebStandard.Ioc;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Controllers {
	[ExportMany]
	public class ExampleController : IController {
		[Action("example/plain_text")]
		public IActionResult PlainText() {
			// 返回文本
			return new PlainResult("some plain text");
		}

		[Action("example/plain_string")]
		public string PlainString() {
			// 返回文本，返回类型是string时会自动使用PlainResult包装
			return "some plain string";
		}

		[Action("example/json")]
		public object Json(string name, int age) {
			// 有参数时会自动获取传入参数
			// 返回json，返回类型不是IActionResult或string时会自动使用JsonResult包装
			return new { name, age };
		}

		[Action("example/template")]
		public IActionResult Template() {
			// 返回模板
			return new TemplateResult("zkweb.examples/hello.html", new { text = "World" });
		}

		[Action("example/file")]
		public IActionResult File() {
			// 返回文件
			return new FileResult("D:\\1.txt");
		}
	}
}
