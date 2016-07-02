using ZKWeb.Plugin;
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
		public Plugin() { }
	}
}
