using System;
using System.Linq;
using System.Net;
using ZKWeb.Database;
using ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Database;
using ZKWeb.Testing;
using ZKWebStandard.Extensions;
using ZKWebStandard.Testing;
using ZKWebStandard.Web;
using ZKWebStandard.Web.Mock;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Tests {
	[Tests]
	class ExampleTest {
		public void MethodA() {
			Assert.IsTrue(1 == 1);
			Assert.IsTrueWith(1 == 1, "if failed this item will be outputed");
			Assert.Equals(true, true);
			Assert.Throws<ArgumentException>(() => { throw new ArgumentException(); });
		}

		public void MethodB() {
			using (HttpManager.OverrideContext("/?a=1", "POST")) {
				var request = (HttpRequestMock)HttpManager.CurrentContext.Request;
				request.remoteIpAddress = IPAddress.Parse("192.168.168.168");

				Assert.Equals(request.Get<string>("a"), "1");
				Assert.Equals(request.RemoteIpAddress, IPAddress.Parse("192.168.168.168"));
			}
		}

		public void MethodC() {
			using (Application.OverrideIoc()) {
				Application.Ioc.Unregister<IEntityOperationHandler<ExampleTable>>();
				Assert.IsTrue(!Application.Ioc.ResolveMany<IEntityOperationHandler<ExampleTable>>().Any());
			}
			// override is finished
			Assert.IsTrue(Application.Ioc.ResolveMany<IEntityOperationHandler<ExampleTable>>().Any());
		}

		public void MethodD() {
			var testManager = Application.Ioc.Resolve<TestManager>();
			using (testManager.UseTemporaryDatabase()) {
				var databaseManager = Application.Ioc.Resolve<DatabaseManager>();
				using (var context = databaseManager.CreateContext()) {
					var obj = new ExampleTable() { Name = "obj in temporary database" };
					context.Save(ref obj);
				}
			}
		}
	}
}
