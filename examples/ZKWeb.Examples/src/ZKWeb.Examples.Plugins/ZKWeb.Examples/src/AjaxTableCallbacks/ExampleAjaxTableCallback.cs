#if TODO
using System.Collections.Generic;
using System.Linq;
using ZKWeb.Database;
using ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Database;
using ZKWeb.Plugins.Common.Base.src.Extensions;
using ZKWeb.Plugins.Common.Base.src.Model;
using ZKWeb.Plugins.Common.Base.src.Scaffolding;
using ZKWebStandard.Extensions;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.AjaxTableCallbacks {
	public class ExampleAjaxTableCallback : IAjaxTableCallback<ExampleTable> {
		public void OnBuildTable(AjaxTableBuilder table, AjaxTableSearchBarBuilder searchBar) { }

		public void OnQuery(
			AjaxTableSearchRequest request, DatabaseContext context, ref IQueryable<ExampleTable> query) {
			if (!string.IsNullOrEmpty(request.Keyword)) {
				query = query.Where(q => q.Name.Contains(request.Keyword));
			}
			bool deleted = request.Conditions.GetOrDefault<string>("Deleted") == "on";
			query = query.Where(q => q.Deleted == deleted);
		}

		public void OnSort(
			AjaxTableSearchRequest request, DatabaseContext context, ref IQueryable<ExampleTable> query) {
			query = query.OrderByDescending(q => q.Id);
		}

		public void OnSelect(
			AjaxTableSearchRequest request, List<EntityToTableRow<ExampleTable>> pairs) {
			foreach (var pair in pairs) {
				pair.Row["Id"] = pair.Entity.Id;
				pair.Row["Name"] = pair.Entity.Name;
				pair.Row["CreateTime"] = pair.Entity.CreateTime.ToClientTimeString();
			}
		}

		public void OnResponse(AjaxTableSearchRequest request, AjaxTableSearchResponse response) {
			response.Columns.AddIdColumn("Id");
			response.Columns.AddMemberColumn("Name");
			response.Columns.AddMemberColumn("CreateTime");
		}
	}
}
#endif
