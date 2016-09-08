using System;
using System.Collections.Generic;
using System.Linq;
using ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Database;
using ZKWeb.Plugins.Common.Base.src.UIComponents.AjaxTable;
using ZKWeb.Plugins.Common.Base.src.UIComponents.AjaxTable.Bases;
using ZKWeb.Plugins.Common.Base.src.UIComponents.AjaxTable.Extensions;
using ZKWeb.Plugins.Common.Base.src.UIComponents.BaseTable;
using ZKWebStandard.Extensions;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.UIComponents.AjaxTableHandlers {
	public class ExampleAjaxTableHandler : AjaxTableHandlerBase<ExampleTable, long> {
		public override void OnQuery(
			AjaxTableSearchRequest request, ref IQueryable<ExampleTable> query) {
			if (!string.IsNullOrEmpty(request.Keyword)) {
				query = query.Where(q => q.Name.Contains(request.Keyword));
			}
		}

		public override void OnSort(
			AjaxTableSearchRequest request, ref IQueryable<ExampleTable> query) {
			query = query.OrderByDescending(q => q.Id);
		}

		public override void OnSelect(
			AjaxTableSearchRequest request, IList<EntityToTableRow<ExampleTable>> pairs) {
			foreach (var pair in pairs) {
				pair.Row["Id"] = pair.Entity.Id;
				pair.Row["Name"] = pair.Entity.Name;
				pair.Row["CreateTime"] = pair.Entity.CreateTime.ToClientTimeString();
				pair.Row["UpdateTime"] = pair.Entity.UpdateTime.ToClientTimeString();
			}
		}

		public override void OnResponse(AjaxTableSearchRequest request, AjaxTableSearchResponse response) {
			response.Columns.AddIdColumn("Id");
			response.Columns.AddMemberColumn("Name");
			response.Columns.AddMemberColumn("CreateTime");
			response.Columns.AddMemberColumn("UpdateTime");
		}
	}
}
