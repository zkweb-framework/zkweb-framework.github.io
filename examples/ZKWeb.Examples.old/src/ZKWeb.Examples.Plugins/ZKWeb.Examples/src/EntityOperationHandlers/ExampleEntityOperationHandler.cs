using ZKWeb.Database;
using ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Database;
using ZKWeb.Logging;
using ZKWebStandard.Ioc;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.DataCallbacks {
	[ExportMany]
	public class ExampleEntityOperationHandler : IEntityOperationHandler<ExampleTable> {
		private long IdBeforeSave { get; set; }
		private string NameBeforeSave { get; set; }

		public void BeforeSave(IDatabaseContext context, ExampleTable data) {
			IdBeforeSave = data.Id;
			NameBeforeSave = data.Name;
		}

		public void AfterSave(IDatabaseContext context, ExampleTable data) {
			var logManager = Application.Ioc.Resolve<LogManager>();
			if (IdBeforeSave <= 0) {
				logManager.LogDebug(string.Format("example data inserted, id is {0}", data.Id));
			} else if (NameBeforeSave != data.Name) {
				logManager.LogDebug(string.Format("example data name changed, id is {0}", data.Id));
			}
		}

		public void BeforeDelete(IDatabaseContext context, ExampleTable data) {
		}

		public void AfterDelete(IDatabaseContext context, ExampleTable data) {
			var logManager = Application.Ioc.Resolve<LogManager>();
			logManager.LogDebug(string.Format("example data deleted, id is {0}", data.Id));
		}
	}
}
