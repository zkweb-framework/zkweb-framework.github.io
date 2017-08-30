using ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Components.GenericConfigs;
using ZKWeb.Plugins.Common.Base.src.Controllers.Bases;
using ZKWeb.Plugins.Common.Base.src.Domain.Services;
using ZKWeb.Web;
using ZKWeb.Web.ActionResults;
using ZKWebStandard.Ioc;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Controllers {
	[ExportMany]
	public class ConfigExampleController : ControllerBase {
		[Action("example/read_config")]
		public IActionResult ReadConfig() {
			var configManager = Application.Ioc.Resolve<GenericConfigManager>();
			var config = configManager.GetData<ExampleConfig>();
			return new JsonResult(config);
		}

		[Action("example/write_config")]
		public string WriteConfig() {
			var configManager = Application.Ioc.Resolve<GenericConfigManager>();
			var config = configManager.GetData<ExampleConfig>();
			config.ExampleName = "updated";
			config.ExampleCount++;
			configManager.PutData(config);
			return "success";
		}
	}
}
