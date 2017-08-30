using Newtonsoft.Json;
using System;
using ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Database;
using ZKWeb.Plugins.Common.Base.src.Domain.Services.Interfaces;
using ZKWeb.Plugins.Common.Base.src.Domain.Uow.Interfaces;
using ZKWeb.Web;
using ZKWebStandard.Ioc;
using ZKWebStandard.Utils;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Controllers {
	[ExportMany]
	public class UowExampleController : IController {
		[Action("example/uow")]
		public string Uow() {
			// insert data
			var uow = Application.Ioc.Resolve<IUnitOfWork>();
			var service = Application.Ioc.Resolve<IDomainService<ExampleTable, long>>();
			string name = RandomUtils.RandomString(5);
			using (uow.Scope()) {
				var data = new ExampleTable() { Name = name };
				service.Save(ref data);
			}
			// read inserted data
			using (uow.Scope()) {
				var readData = service.Get(t => t.Name == name);
				return JsonConvert.SerializeObject(new { readData });
			}
		}
	}
}
