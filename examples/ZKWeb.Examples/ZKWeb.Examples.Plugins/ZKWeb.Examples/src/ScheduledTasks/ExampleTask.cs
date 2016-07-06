using System;
using ZKWeb.Logging;
using ZKWeb.Plugins.Common.Base.src.Model;
using ZKWebStandard.Ioc;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.ScheduledTasks {
	[ExportMany, SingletonReuse]
	public class ExampleTask : IScheduledTaskExecutor {
		public string Key { get { return "ZKWeb.Example.ExampleTask"; } }

		public bool ShouldExecuteNow(DateTime lastExecuted) {
			return ((DateTime.UtcNow - lastExecuted).TotalMinutes > 15);
		}

		public void Execute() {
			var logManager = Application.Ioc.Resolve<LogManager>();
			logManager.LogDebug("Example task executed");
		}
	}
}
