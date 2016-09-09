using System;
using System.Collections.Generic;
using System.Linq;
using ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Database;
using ZKWeb.Plugins.Common.Base.src.UIComponents.BaseTable;
using ZKWeb.Plugins.Common.Base.src.UIComponents.StaticTable;
using ZKWeb.Plugins.Common.Base.src.UIComponents.StaticTable.Bases;
using ZKWebStandard.Extensions;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.UIComponents.StaticTableHandlers {
	public class ExampleStaticTableHandler : StaticTableHandlerBase<ExampleTable, long> {
		public override void OnQuery(
			StaticTableSearchRequest request, ref IQueryable<ExampleTable> query) {
			if (!string.IsNullOrEmpty(request.Keyword)) {
				query = query.Where(q => q.Name.Contains(request.Keyword));
			}
		}

		public override void OnSort(
			StaticTableSearchRequest request, ref IQueryable<ExampleTable> query) {
			query = query.OrderByDescending(q => q.Id);
		}

		public override void OnSelect(
			StaticTableSearchRequest request, IList<EntityToTableRow<ExampleTable>> pairs) {
			foreach (var pair in pairs) {
				pair.Row["Id"] = pair.Entity.Id;
				pair.Row["Name"] = pair.Entity.Name;
				pair.Row["CreateTime"] = pair.Entity.CreateTime.ToClientTimeString();
			}
		}
	}
}
