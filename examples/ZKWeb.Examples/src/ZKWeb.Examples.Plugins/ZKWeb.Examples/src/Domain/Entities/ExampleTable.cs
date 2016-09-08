using System;
using ZKWeb.Database;
using ZKWeb.Plugins.Common.Base.src.Domain.Entities.Interfaces;
using ZKWebStandard.Ioc;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Database {
	[ExportMany]
	public class ExampleTable :
		IHaveCreateTime, IHaveUpdateTime,
		IEntity<long>, IEntityMappingProvider<ExampleTable> {
		public virtual long Id { get; set; }
		public virtual string Name { get; set; }
		public virtual DateTime CreateTime { get; set; }
		public virtual DateTime UpdateTime { get; set; }
		public virtual bool Deleted { get; set; }

		public virtual void Configure(IEntityMappingBuilder<ExampleTable> builder) {
			builder.Id(e => e.Id);
			builder.Map(e => e.Name);
			builder.Map(e => e.CreateTime);
			builder.Map(e => e.UpdateTime);
			builder.Map(e => e.Deleted);
		}
	}
}
