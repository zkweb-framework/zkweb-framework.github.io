基础插件提供了访问静态文件的功能, 使用`/static/文件路径`可以访问到对应的静态文件.<br/>
静态文件没有修改时支持返回`304 Not Modified`.<br/>

### 静态文件的读取顺序

静态文件同样属于资源文件, 读取顺序如下<br/>

```
"App_Data/static/路径"
foreach (按加载顺序反序枚举插件) {
	"插件目录/static/路径"
}
```

例如注册了插件PluginA和PluginB, 读取`/static/example/homepage.js`时会按以下顺序<br/>

- `App_Data\static\example\homepage.js`
- `PluginB\static\example\homepage.js`
- `PluginA\static\example\homepage.js`
