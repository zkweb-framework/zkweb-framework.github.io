#if TODO
using System.Collections.Generic;
using System.Linq;
using ZKWeb.Database;
using ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Database;
using ZKWeb.Plugins.Common.Base.src.Model;
using ZKWebStandard.Extensions;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.StaticTableCallbacks {
	public class ExampleStaticTableCallback : IStaticTableCallback<ExampleTable> {
		public void OnQuery(
			StaticTableSearchRequest request, DatabaseContext context, ref IQueryable<ExampleTable> query) {
			if (!string.IsNullOrEmpty(request.Keyword)) {
				query = query.Where(q => q.Name.Contains(request.Keyword));
			}
			query = query.Where(q => !q.Deleted);
		}

		public void OnSort(
			StaticTableSearchRequest request, DatabaseContext context, ref IQueryable<ExampleTable> query) {
			query = query.OrderByDescending(q => q.Id);
		}

		public void OnSelect(
			StaticTableSearchRequest request, List<EntityToTableRow<ExampleTable>> pairs) {
			foreach (var pair in pairs) {
				pair.Row["Id"] = pair.Entity.Id;
				pair.Row["Name"] = pair.Entity.Name;
				pair.Row["CreateTime"] = pair.Entity.CreateTime.ToClientTimeString();
			}
		}
	}
}
#endif
