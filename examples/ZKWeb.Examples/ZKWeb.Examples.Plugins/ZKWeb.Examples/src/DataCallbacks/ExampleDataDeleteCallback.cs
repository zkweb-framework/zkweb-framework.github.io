using ZKWeb.Database;
using ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Database;
using ZKWeb.Logging;
using ZKWebStandard.Ioc;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.DataCallbacks {
	[ExportMany]
	public class ExampleDataDeleteCallback : IDataDeleteCallback<ExampleTable> {
		public void BeforeDelete(DatabaseContext context, ExampleTable data) {
		}

		public void AfterDelete(DatabaseContext context, ExampleTable data) {
			var logManager = Application.Ioc.Resolve<LogManager>();
			logManager.LogDebug(string.Format("example data deleted, id is {0}", data.Id));
		}
	}
}
