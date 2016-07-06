using System.ComponentModel.DataAnnotations;
using ZKWeb.Plugins.Common.Base.src.Model;
using ZKWeb.Plugins.Common.Base.src.Scaffolding;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.Forms {
	public class ExampleForm : ModelFormBuilder {
		[Required]
		[StringLength(100)]
		[TextBoxField("Name", "Please enter name")]
		public string Name { get; set; }
		[Required]
		[TextBoxField("Age", "Please enter age")]
		public int Age { get; set; }

		protected override void OnBind() {
			Name = "Tom";
			Age = 25;
		}

		protected override object OnSubmit() {
			var message = string.Format("Hello, {0} ({1})", Name, Age);
			return new { message };
		}
	}
}
