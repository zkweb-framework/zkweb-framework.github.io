代码编辑器封装了CoreMirror到表单字段, 提供了代码高亮提示和检测错误等功能

### 代码编辑器的效果

![效果图](../images/plugins/common.codeeditor.code_editor.jpg)

### 代码编辑器的使用

使用时给表单中的字段标记CodeEditorAttribute<br/>

``` html
[CodeEditor("CustomHtml", 5, "html")]
public string CustomHtml { get; set; }
```
