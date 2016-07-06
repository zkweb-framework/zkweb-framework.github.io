using ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Database;
using ZKWeb.Plugins.Common.Base.src.Repositories;
using ZKWebStandard.Ioc;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Repositories {
	[ExportMany]
	public class ExampleRepository : GenericRepository<ExampleTable> {
		public virtual long CountNotDeleted() {
			return Count(t => !t.Deleted);
		}
	}
}
