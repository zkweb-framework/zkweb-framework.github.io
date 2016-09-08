using ZKWeb.Web;
using ZKWeb.Web.ActionResults;
using ZKWebStandard.Ioc;
using ZKWebStandard.Web;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.HttpHandlers {
	[ExportMany]
	public class HttpRequestHandlerExample : IHttpRequestHandler {
		public const string Prefix = "/example/handler/";
		public void OnRequest() {
			var request = HttpManager.CurrentContext.Request;
			var response = HttpManager.CurrentContext.Response;
			if (request.Path.StartsWith(Prefix)) {
				new PlainResult("the path is " + request.Path.Substring(Prefix.Length))
					.WriteResponse(response);
				response.End();
			}
		}
	}
}
