using System.ComponentModel.DataAnnotations;
using ZKWeb.Plugins.Common.Base.src.UIComponents.Forms;
using ZKWeb.Plugins.Common.Base.src.UIComponents.Forms.Attributes;

namespace ZKWeb.Examples.Plugins.ZKWeb.Examples.src.UIComponents.Forms {
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
