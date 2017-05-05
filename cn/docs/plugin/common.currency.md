这个插件提供了货币的统一接口和常用的默认货币，用于支持多货币功能。<br/>

### 添加新的货币

添加货币类型需要继承`ICurrency`。<br/>
以下是人民币的源代码，可以参考添加新的货币，货币可以是实际的货币也可以是虚拟货币。<br/>

``` csharp
[ExportMany]
public class CNY : ICurrency {
	public string Type { get { return "CNY"; } }
	public string Prefix { get { return "¥"; } }
	public string Suffix { get { return null; } }
}
```

### 设置默认货币

后台可以设置网站默认的货币<br/>

![货币设置](../img/currency_settings.jpg)

### 货币相关的操作

**获取所有货币**

```csharp
var currencies = Application.Ioc.ResolveMany<ICurrency>();
```

**获取指定货币**

```csharp
var currencyManager = Application.Ioc.Resolve<CurrencyManager>();
var currency = currencyManager.GetCurrency("CNY");
```

**获取默认货币**

```csharp
var currencyManager = Application.Ioc.Resolve<CurrencyManager>();
var currency = currencyManager.GetDefaultCurrency();
```
