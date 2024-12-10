## 框架地址：https://gitee.com/lishilei0523/SD.Framework

#### 项目说明
> 专注于信息系统的单点登录、身份认证、机器许可、页面授权；

> SD.Framework框架项目试验田；

#### 开发目的：
    1、作为一个使用SD.Framework框架开发的项目样板；
        
    2、探索基于ASP.NET Core MVC的前端架构；

    3、探索Angular + NG-Zorro前端架构；

    4、探索基于WPF MVVM的前端架构；

    5、做一套统一身份认证系统；

#### 主要涉及技术：
    Web端1：ASP.NET Core MVC、jQuery.EasyUI；

    Web端2：Angular、NG-Zorro；

    客户端：WPF、Caliburn.Micro、MahApps.Metro、FluentWPF；

    服务端：基于SD.Framework开发，支持ASP.NET Core WebApi、CoreWCF、gRPC承载；

#### 主要功能模块：
    配置部分：
        信息系统管理、用户管理、角色管理、菜单管理、权限管理等；

    身份认证部分：
        ASP.NET Core、WCF、gRPC、SignalR、WPF、Avalonia、MAUI；

    机器许可部分：
        机器码生成、License发放、License许可等；

    页面授权部分：
        Angular、WPF、Avalonia、MAUI；

#### 注意事项：
        请先修改服务端Host项目配置文件中连接字符串，然后运行，数据库会自动生成，服务端Repository项目也有数据库脚本

        先运行服务端，再运行客户端

        如果服务端运行失败，请检查是不是没装Redis（附件中有）

        默认账号：admin，密码：888888

## EasyUI版页面预览
##### 登录
![登录](https://git.oschina.net/uploads/images/2017/0529/175100_85d0afeb_372841.png "登录")
##### 首页
![首页](https://git.oschina.net/uploads/images/2017/0529/175138_4d6d131a_372841.jpeg "首页")
##### 菜单管理
![菜单管理](https://git.oschina.net/uploads/images/2017/0529/175208_075c0a11_372841.png "菜单管理")
##### 菜单关联权限
![菜单关联权限](https://git.oschina.net/uploads/images/2017/0529/175233_1c16ae98_372841.png "菜单关联权限")
##### 用户管理
![用户管理](https://git.oschina.net/uploads/images/2017/0529/175251_14e3c127_372841.png "用户管理")
##### 用户分配角色
![用户分配角色](https://git.oschina.net/uploads/images/2017/0529/175306_fd5487f1_372841.png "用户分配角色")
##### 创建角色
![创建角色](https://git.oschina.net/uploads/images/2017/0529/175358_a0e006c2_372841.png "创建角色")