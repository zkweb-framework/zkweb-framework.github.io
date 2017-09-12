日期选择控件可以用来选择日期或者日期范围, 做成单独插件是因为js文件较大不适合放在基础插件中

### 日期选择控件的效果

选择单个日期的效果<br/>
![选择单个日期的效果](../images/plugins/common.datepicker.datepicker_datefield.jpg)

选择日期范围的效果<br/>
![选择日期范围的效果](../images/plugins/common.datepicker.datepicker_daterangefield.jpg)

### 日期选择控件的使用

插件提供了以下的表单字段属性

- DateFieldAttribute
- DateRangeFieldAttribute

使用时添加到表单类中即可, 如下

``` csharp
[DateField("DateExample")]
public DateTime? DateExample { get; set; }

[DateRangeField("DateRangeExample")]
public DateRange DateRangeExample { get; set; }
```
