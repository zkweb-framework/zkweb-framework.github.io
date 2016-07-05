using FluentNHibernate.Mapping;
using System;
using ZKWebStandard.Ioc;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Database {
	public class ExampleTable {
		public virtual long Id { get; set; }
		public virtual string Name { get; set; }
		public virtual DateTime CreateTime { get; set; }
		public virtual bool Deleted { get; set; }
	}

	[ExportMany]
	public class ExampleTableMap : ClassMap<ExampleTable> {
		public ExampleTableMap() {
			Id(e => e.Id);
			Map(e => e.Name).Length(0xffff); // 0xffff `== no limit, you can confirm later
			Map(e => e.CreateTime).Not.Nullable();
			Map(e => e.Deleted);
		}
	}
}
