using Newtonsoft.Json;
using System;
using System.Linq;
using ZKWeb.Database;
using ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Database;
using ZKWeb.Web;
using ZKWebStandard.Ioc;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Controllers {
	[ExportMany]
	public class DatabaseExampleController : IController {
		[Action("example/add_data")]
		public string AddData() {
			var databaseManager = Application.Ioc.Resolve<DatabaseManager>();
			using (var context = databaseManager.CreateContext()) {
				var data = new ExampleTable() {
					Name = "test",
					CreateTime = DateTime.UtcNow,
					Deleted = false
				};
				context.Save(ref data);
			}
			return "success";
		}

		[Action("example/update_data")]
		public string UpdateData() {
			var databaseManager = Application.Ioc.Resolve<DatabaseManager>();
			using (var context = databaseManager.CreateContext()) {
				foreach (var data in context.Query<ExampleTable>()) {
					var localData = data;
					context.Save(ref localData, d => d.Name = "updated");
				}
			}
			return "success";
		}

		[Action("example/query_data")]
		public string QueryData() {
			var databaseManager = Application.Ioc.Resolve<DatabaseManager>();
			using (var context = databaseManager.CreateContext()) {
				var notUpdated = context.Query<ExampleTable>()
					.Where(t => t.Name != "updated").ToList();
				return string.Format("these objects are not updated:\r\n{0}",
					JsonConvert.SerializeObject(notUpdated, Formatting.Indented));
			}
		}

		[Action("example/remove_data")]
		public string RemoveData() {
			var databaseManager = Application.Ioc.Resolve<DatabaseManager>();
			using (var context = databaseManager.CreateContext()) {
				long deleted = context.BatchDelete<ExampleTable>(d => d.Name == "updated");
				return string.Format("{0} objects are removed", deleted);
			}
		}
	}
}
