CKEditor插件提供了基础插件中的`RichTextEditor`表单字段的实现。<br/>

### CKEditor字段的效果

![CKEditor字段的效果](../img/ckeditor.jpg)

### 如何使用CKEditor字段

并添加`CMS.CKEditor`到插件列表，并在表单的成员上添加`RichTextEditor`属性即可。<br/>

``` csharp
[RichTextEditor("Remark")]
public string Remark { get; set; }
```

### 使用CKEditor需要注意的点

- CKEditor保存到数据库时，会使用经过html编码后的值
- 提交后会对标签进行白名单过滤，不在白名单中的标签和属性将被过滤掉
- CKEditor允许在内容中嵌入图片(base64)，但是为了更快的加载速度推荐使用[图片管理器](cms.imagebrowser)插件。
