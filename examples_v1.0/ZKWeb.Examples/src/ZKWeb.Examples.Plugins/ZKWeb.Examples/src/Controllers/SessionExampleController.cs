﻿using Newtonsoft.Json;
using System;
using ZKWeb.Plugins.Common.Base.src.Controllers.Bases;
using ZKWeb.Plugins.Common.Base.src.Domain.Services;
using ZKWeb.Web;
using ZKWeb.Web.ActionResults;
using ZKWebStandard.Ioc;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Controllers {
	[ExportMany]
	public class SessionExampleController : ControllerBase {
		[Action("example/get_session")]
		public IActionResult GetSession() {
			var sessionManager = Application.Ioc.Resolve<SessionManager>();
			var session = sessionManager.GetSession();
			return new JsonResult(session, Formatting.Indented);
		}

		[Action("example/save_session")]
		public string SaveSession() {
			var sessionManager = Application.Ioc.Resolve<SessionManager>();
			var session = sessionManager.GetSession();
			session.Items["ExampleKey"] = DateTime.UtcNow;
			sessionManager.SaveSession();
			return "success";
		}
	}
}
