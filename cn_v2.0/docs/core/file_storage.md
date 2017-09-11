ZKWeb提供了标准的文件储存接口, 抽象化了文件的读取保存和列出.<br/>
默认文件储存使用了本地的文件系统, 可以通过自己实现`IFileStorage`实现存取分布式的文件系统.<br/>
`IFileStorage`的接口如下

``` csharp
public interface IFileStorage {
	IFileEntry GetTemplateFile(string path);
	IFileEntry GetResourceFile(params string[] pathParts);
	IFileEntry GetStorageFile(params string[] pathParts);
	IDirectoryEntry GetStorageDirectory(params string[] pathParts);
}
```

`GetTemplateFile`用于获取Html模板文件, 查找顺序应该为`自定义储存 => 插件文件夹下的templates`<br/>
`GetResourceFile`用于获取资源文件, 查找顺序应该为`自定义储存 => 插件文件夹`<br/>
`GetStorageFile`用于获取储存文件, 可以读取或写入<br/>
`GetStorageDirectory`用于获取储存文件夹, 可以列出文件夹下的文件和子文件夹

### 使用文件储存

** 获取模板文件的例子 **

``` csharp
var fileStorage = Application.Ioc.Resolve<IFileStorage>();
var fileEntry = fileStorage.GetTemplateFile("abc.html");
var contents = fileEntry.ReadAllText();
```

** 获取资源文件的例子 **

``` csharp
var fileStorage = Application.Ioc.Resolve<IFileStorage>();
var fileEntry = fileStorage.GetResourceFile("abc.jpg");
using (var stream = fileEntry.OpenRead()) { }
```

** 保存储存文件的例子 **

``` csharp
var fileStorage = Application.Ioc.Resolve<IFileStorage>();
var fileEntry = fileStorage.GetStorageFile("static", "abc.txt");
fileEntry.WriteAllText("test storage file");
```

** 列出储存文件夹下的文件的例子(非递归) **

``` csharp
var fileStorage = Application.Ioc.Resolve<IFileStorage>();
var directoryEntry = fileStorage.GetStorageDirectory("static");
var childFiles = directoryEntry.EnumerateFiles();
foreach (var file in childFiles) {
	console.WriteLine($"{file.Filename}: {file.ReadAllText()}")
}
```
