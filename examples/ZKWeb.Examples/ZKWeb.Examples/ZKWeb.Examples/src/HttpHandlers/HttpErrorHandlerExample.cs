using System;
using ZKWeb.Web;
using ZKWeb.Web.ActionResults;
using ZKWebStandard.Ioc;
using ZKWebStandard.Web;

namespace ZKWeb.Examples.ZKWeb.Examples.src.HttpHandlers {
	[ExportMany]
	public class HttpErrorHandlerExample : IHttpRequestErrorHandler {
		public void OnError(Exception ex) {
			var httpEx = ex as HttpException; // HttpException in ZKWebStandard.Web
			if (httpEx != null && httpEx.StatusCode == 404) {
				var response = HttpManager.CurrentContext.Response;
				new PlainResult("custom 404 page").WriteResponse(response);
				response.End();
			}
		}
	}
}
