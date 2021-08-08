using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using SD.Toolkits.AspNet;
using System.IO;
using System.Web.Http;

namespace SD.IdentitySystem.UpdateService
{
    /// <summary>
    /// OWIN启动器
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 配置应用程序
        /// </summary>
        /// <param name="appBuilder">应用程序建造者</param>
        public void Configuration(IAppBuilder appBuilder)
        {
            //配置Web服务器
            HttpConfiguration httpConfiguration = new HttpConfiguration();
            httpConfiguration.Formatters.Remove(httpConfiguration.Formatters.XmlFormatter);

            //配置文件服务器
            Directory.CreateDirectory(AspNetSection.Setting.StaticFiles.Value);
            Directory.CreateDirectory(AspNetSection.Setting.FileServer.Value);
            StaticFileOptions staticFileOptions = new StaticFileOptions
            {
                FileSystem = new PhysicalFileSystem(AspNetSection.Setting.StaticFiles.Value)
            };
            FileServerOptions fileServerOptions = new FileServerOptions
            {
                FileSystem = new PhysicalFileSystem(AspNetSection.Setting.FileServer.Value),
                EnableDirectoryBrowsing = true
            };
            appBuilder.UseStaticFiles(staticFileOptions);
            appBuilder.UseFileServer(fileServerOptions);
            appBuilder.UseWebApi(httpConfiguration);
        }
    }
}
