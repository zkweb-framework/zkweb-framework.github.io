using Newtonsoft.Json;
using System;
using ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Database;
using ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Repositories;
using ZKWeb.Plugins.Common.Base.src.Repositories;
using ZKWeb.Web;
using ZKWebStandard.Ioc;
using ZKWebStandard.Utils;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Controllers {
	[ExportMany]
	public class UowExampleController : IController {
		[Action("example/uow")]
		public string Uow() {
			// insert data
			string name = RandomUtils.RandomString(5);
			UnitOfWork.WriteData<ExampleTable>(r => {
				var data = new ExampleTable() { Name = name };
				r.Save(ref data);
			});
			// read inserted data
			var readData = UnitOfWork.ReadData<ExampleTable, ExampleTable>(
				r => r.Get(t => t.Name == name));
			var notDeleted = UnitOfWork.ReadRepository<ExampleRepository, long>(
				r => r.CountNotDeleted());
			return JsonConvert.SerializeObject(new { readData, notDeleted });
		}
	}
}
