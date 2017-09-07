使用ZKWeb的项目通常会带有不同路径的插件, 发布时除了要发布网站文件还要同时复制插件.

为了简化这个流程, ZKWeb提供了一个专门的发布工具.

![发布工具](../images/core/publish_tool.png)

### 发布Asp.Net或Owin项目的步骤

首先使用Visual Studio打开项目, 切换配置到**Release**并编辑网站项目.

然后运行一次网站以确保所有插件均已编译到最新版本.

最后打开网站发布工具<br/>
"ZKWeb\Tools\WebsitePublisher.Gui.Windows\ZKWeb.Toolkits.WebsitePublisher.Gui.exe"

选择网站根目录和输出文件夹后, 点击"发布网站"即可.

托管到IIS时使用默认的设置(4.0+集成)即可, 不需要做其他修改.

### 发布Asp.Net Core项目的步骤

首先使用Visual Studio打开项目, 切换配置到**Release**并编辑网站项目.

然后运行一次网站以确保所有插件均已编译到最新版本.

再执行以下的命令发布网站程序本身, 您可能需要把"netcoreapp2.0"替换为"net461".

``` text
dotnet publish -f netcoreapp2.0 -c Release -r win10-x64
```

最后打开网站发布工具<br/>
"ZKWeb\Tools\WebsitePublisher.Gui.Windows\ZKWeb.Toolkits.WebsitePublisher.Gui.exe"

选择网站根目录和输出文件夹后, 点击"发布网站"即可.

发布出来的网站可以直接点击exe执行(自宿主), 也可以托管到IIS.

托管到IIS需要在服务器安装"Windows Server Hosting", 请到官网[下载安装](https://www.microsoft.com/net/download/core#/runtime).

### 批处理发布网站

如果您需要频繁的发布网站, 可以选择批处理发布减轻工作量.

以"ZKWeb.Demo"为例:

``` bat
@echo off
echo this script is for build and publish demo site
echo please ensure you have this directory layout
echo - zkweb
echo   - tools
echo - zkweb.demo
echo   - publish.bat
echo.

echo building project...
cd src\ZKWeb.Demo.AspNetCore
dotnet restore
dotnet build -c Release -f net461
cd ..\..

echo building plugins...
cd src\ZKWeb.Demo.Console
dotnet restore
dotnet run -c Release -f net461
cd ..\..

echo publishing website...
..\ZKWeb\Tools\WebsitePublisher.Cmd.Windows\ZKWeb.Toolkits.WebsitePublisher.Cmd.exe -r src\ZKWeb.Demo.AspNetCore -n "zkweb.demo" -o "..\..\publish"
pause
```

以"ZKWeb.MVVMDemo"为例

``` bat
@echo off
echo this script is for build and publish mvvm demo site
echo please ensure you have this directory layout
echo - zkweb
echo   - tools
echo - zkweb.mvvmdemo
echo   - publish.bat
echo.

echo building project...
cd src\ZKWeb.MVVMDemo.AspNetCore
dotnet restore
dotnet build -c Release -f netcoreapp2.0
dotnet publish -c Release -f netcoreapp2.0 -r win10-x64
cd ..\..

echo building plugins...
cd src\ZKWeb.MVVMDemo.Console
dotnet restore
dotnet run -c Release -f netcoreapp2.0
cd ..\..

echo publishing website...
..\ZKWeb\Tools\WebsitePublisher.Cmd.Windows\ZKWeb.Toolkits.WebsitePublisher.Cmd.exe -f netcoreapp2.0 -x ".*node_modules.*" -r src\ZKWeb.MVVMDemo.AspNetCore -n "zkweb.mvvm" -o "..\..\publish"
pause
```
模仿上面的批处理编写bat脚本即可.

### 在Linux上发布网站

目前只有`AspNet.Core`类型, 且使用了.Net Core的项目可以在Linux上发布.

请参考`ZKWeb.MVVMDemo`的[publish_ubuntu.sh](https://github.com/zkweb-framework/ZKWeb.MVVMDemo/blob/master/publish_ubuntu.sh).

### 发布到Docker

发布到Docker需要在镜像内安装libgdiplus(用于ZKWeb.System.Drawing),<br/>
其他和普通的Asp.Net Core网站一样.

请参考`ZKWeb.MVVMDemo`的[Dockerfile](https://github.com/zkweb-framework/ZKWeb.MVVMDemo/blob/master/Dockerfile).
