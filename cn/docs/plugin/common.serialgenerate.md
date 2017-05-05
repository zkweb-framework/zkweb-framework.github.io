流水号插件提供了统一的流水号生成功能。<br/>
支持在插件中重载原有对象的流水号生成规则。<br/>

### 给指定的对象生成流水号

```csharp
transaction.Serial = SerialGenerator.GenerateFor(transaction);
```

### 流水号的生成规则

默认流水号的格式是"yyyyMMdd + 8位随机数字"<br/>
如果需要指定自定义的格式可以继承`ISerialGenerateHandler`，在这个处理器中判断对象类型并生成自定义的流水号。<br/>
