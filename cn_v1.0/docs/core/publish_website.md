ZKWeb发布网站时需要同时发布网站程序和插件。<br/>
为了简化发布流程，ZKWeb提供了发布网站的工具。<br/>

![网站发布工具](../img/website_publisher.jpg)

### 发布Asp.Net或Owin项目的步骤

- 使用Visual Studio打开项目
- 切换配置到**Release**并编辑网站项目
- 运行一次网站以确保所有插件均已编译到最新
- 打开网站发布工具`ZKWeb\Tools\WebsitePublisher.Gui.exe`
	- `Website Root` 选择网站项目的路径，例如`D:\Projects\Hello.World\Hello.World.AspNet`
	- `Output Name` 会自动填写，如果需要发布到其他名称的文件夹可以进行修改
	- `Output Directory` 选择发布网站到的路径，最终会发布到`Output Directory\Output Name`下
- 点击 `Publish Website`，看到`Success`就是发布成功了

### 发布Asp.Net Core项目的步骤

- 如果你需要以net461框架发布，跟随上面的步骤即可
- 如果你需要以netcoreapp1.1框架发布，在打开网站发布工具之前还需要执行以下命令
	- `dotnet publish -f netcoreapp1.1 -c Release -r win10-x64`

### 托管到IIS
托管到IIS不需要特别的设置，创建网站并选择发布的文件夹即可。<br/>

### 自宿主
Asp.Net Core和Owin支持自宿主运行网站。<br/>
Asp.Net Core打开主程序即可。<br/>
Owin请修改Console项目并把`项目名.Console.exe`复制到发布文件夹。<br/>

### 通过命令行发布网站
需要自动化发布或有大量网站时，可以使用命令行版本的网站发布器。<br/>
项目路径需要选择包含web.config的目录<br/>
```
ZKWeb\Tools\WebsitePublisher.Cmd.exe -r "项目路径" -n "发布名称" -o "发布路径"
```

### 部署Asp.Net Core版到IIS时需要的设置

部署Asp.Net Core版到IIS时需要进行以下设置才可以正常运行<br/>

- 设置进程池的.NET版本到"无托管代码(No Managed Code)"
- 安装IIS模块
	- 从[微软官网](https://www.microsoft.com/net/download/core#/runtime)下载安装**.Net Core Windows Server Hosting**
	- 安装后需要重启

### 如何用批处理实现自动编译和发布

如果创建的项目类型是Asp.Net Core，可以编写批处理实现自动编译主项目 + 所有插件项目 + 发布<br/>
以我的示例项目为例:<br/>
注意，如果你需要基于`netcoreapp1.1`框架发布需要在`dotnet build`下面加`dotnet publish`命令

```
@echo off

echo building project...
cd src\ZKWeb.Demo.AspNetCore
dotnet build -c Release -f net461
cd ..\..

echo building plugins...
cd src\ZKWeb.Demo.Console
dotnet run -c Release -f net461
cd ..\..

echo publishing website...
..\ZKWeb\Tools\WebsitePublisher.Cmd.exe -r src\ZKWeb.Demo.AspNetCore -n "zkweb" -o "..\..\publish"
pause
```
