动态表单支持从json数据构建表单内容，可以用于实现可视化表单<br/>

### 构建动态表单的json数据结构

``` json
[
	{
		"Type": "TextBox",
		"Name": "Width",
		"PlaceHolder": "Example: 100px",
		"Validators": [
			{
				"Type": "RegularExpression",
				"Pattern": "^(\\d+px)?$"
			}
		]
	},
	{
		"Type": "TextBox",
		"Name": "Height",
		"PlaceHolder": "Example: 100px",
		"Validators": [
			{
				"Type": "RegularExpression",
				"Pattern": "^(\\d+px)?$"
			}
		]
	}
]
```

### 如何构建动态表单

``` csharp
var fields = JsonConvert.Deserialize<IList<IDictionary<string, object>>>(json);
var dynamicFormBuilder = new DynamicFormBuilder();
dynamicFormBuilder.AddFields(fields);
var form = dynamicFormBuilder.ToForm<TabFormBuilder>();
// 接下来可以调用BindValues绑定表单或ParseValues解析提交的参数
```

### 支持在动态表单中使用现成的表单字段属性

这里以`TextBoxFieldAttribute`为例
``` csharp
/// <summary>
/// 文本框生成器
/// </summary>
[ExportMany(ContractKey = "TextBox")]
public class TextBoxFieldFactory : IDynamicFormFieldFactory {
	/// <summary>
	/// 创建表单字段属性
	/// </summary>
	public FormFieldAttribute Create(IDictionary<string, object> fieldData) {
		var name = fieldData.GetOrDefault<string>("Name");
		var placeHolder = fieldData.GetOrDefault<string>("PlaceHolder");
		var group = fieldData.GetOrDefault<string>("Group");
		return new TextBoxFieldAttribute(name, placeHolder) { Group = group };
	}
}
```

### 支持在动态表单中使用现成的表单字段验证属性

这里以`StringLengthAttribute`为例
``` csharp
/// <summary>
/// 生成长度验证属性
/// </summary>
[ExportMany(ContractKey = "StringLength")]
public class StringLengthValidatorFactory : IDynamicFormFieldValidatorFactory {
	/// <summary>
	/// 生成长度验证属性
	/// </summary>
	public Attribute Create(IDictionary<string, object> validatorData) {
		var maximumLength = validatorData.GetOrDefault<int>("MaximumLength");
		var minimumLength = validatorData.GetOrDefault<int>("MinimumLength");
		return new StringLengthAttribute(maximumLength) { MinimumLength = minimumLength };
	}
}
```

### 各个动态表单字段和它们的参数

**AlertHtml: 提示Html生成器**

- Name: 字段名称
- Type: 类型, 例如"alert-info"
- Group: 分组

**CheckBox: 勾选框生成器**

- Name: 字段名称
- Group: 分组

**CheckBoxGroup: 勾选框组生成器**

- Name: 字段名称
- Type: 类型, 例如"全名, 程序集名称"
- Group: 分组

**CheckBoxGroups: 勾选框组列表生成器**

- Name: 字段名称
- Type: 类型, 例如"全名, 程序集名称"
- Group: 分组

**CheckBoxTree: 勾选框树生成器**

- Name: 字段名称
- Type: 类型, 例如"全名, 程序集名称"
- Group: 分组

**DropdownList: 下拉框生成器**

- Name: 字段名称
- Type: 类型, 例如"全名, 程序集名称"
- Group: 分组

**FileUploader: 文件上传生成器**

- Name: 字段名称
- Extensions: 扩展名, 例如"png,jpg,jpeg,gif"
- MaxContentsLength: 允许上传的最大长度, 单位是字节, 例如1MB是"1048576"
- Group: 分组

**Hidden: 隐藏字段生成器**

- Name: 字段名称
- Group: 分组

**Html: Html内容生成器**

- Name: 字段名称
- Group: 分组

**Json: Json字段生成器**

- Name: 字段名称
- Type: 类型, 例如"全名, 程序集名称"
- Group: 分组

**Password: 密码框生成器**

- Name: 字段名称
- Group: 分组

**RadioButtons: 单选按钮组生成器**

- Name: 字段名称
- Type: 类型, 例如"全名, 程序集名称"
- Group: 分组

**RichTextEditor: 富文本编辑器生成器**

- Name: 字段名称
- Config: 自定义配置，格式是Json
- ImageBrowserUrl: 图片管理器地址，指定时可以启用图片上传功能
- Group: 分组

**SearchableDropdownList: 可搜索的下拉框生成器**

- Name: 字段名称
- Type: 类型, 例如"全名, 程序集名称"
- Group: 分组

**TemplateHtml: 模板Html生成器**

- Name: 字段名称
- Path: 模板路径
- Group: 分组

**TextArea: 文本区域生成器**

- Name: 字段名称
- Rows: 行数
- PlaceHolder: 预置文本
- Group: 分组

**TextBox: 文本框生成器**

- Name: 字段名称
- PlaceHolder: 预置文本
- Group: 分组

### 各个动态表单验证属性和它们的参数

**RegularExpression: 生成表达式验证属性**

- Pattern: 正则表达式

**Required: 生成必填项属性**

无参数

**StringLength: 生成长度验证属性**

- MaximumLength: 最大长度
- MinimumLength: 最小长度
