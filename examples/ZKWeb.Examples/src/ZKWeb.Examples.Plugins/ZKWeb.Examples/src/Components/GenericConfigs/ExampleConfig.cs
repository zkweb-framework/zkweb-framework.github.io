using ZKWeb.Plugins.Common.Base.src.Components.GenericConfigs.Attributes;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Components.GenericConfigs {
	/// <summary>
	/// 示例使用的通用配置
	/// </summary>
	[GenericConfig("ZKWeb.Example.ExampleConfig", CacheTime = 15)]
	public class ExampleConfig {
		public string ExampleName { get; set; }
		public int ExampleCount { get; set; }
	}
}
