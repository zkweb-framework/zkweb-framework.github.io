using ZKWeb.Plugin;
using ZKWeb.Templating.DynamicContents;
using ZKWebStandard.Ioc;

namespace ZKWeb.Examples.ZKWeb.Examples.src {
	/// <summary>
	/// Plugin Entry Point
	/// </summary>
	[ExportMany]
	public class Plugin : IPlugin {
		/// <summary>
		/// Here will execute after plugin loaded
		/// </summary>
		public Plugin() {
			var areaManager = Application.Ioc.Resolve<TemplateAreaManager>();
			areaManager.GetArea("header_menubar").DefaultWidgets.Add("example.widgets/example_nav");
		}
	}
}
