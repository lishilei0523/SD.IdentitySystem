using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using SD.Toolkits.AspNet;
using System;
using System.IO;

namespace SD.IdentitySystem.UpdateService
{
    /// <summary>
    /// 应用程序启动器
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 配置应用程序
        /// </summary>
        public void Configure(IApplicationBuilder appBuilder)
        {
            //配置服务器
            string staticFilesRoot = Path.Combine(AppContext.BaseDirectory, AspNetSetting.StaticFilesPath);
            string fileServerRoot = Path.Combine(AppContext.BaseDirectory, AspNetSetting.FileServerPath);
            Directory.CreateDirectory(staticFilesRoot);
            Directory.CreateDirectory(fileServerRoot);
            StaticFileOptions staticFileOptions = new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticFilesRoot)
            };
            FileServerOptions fileServerOptions = new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(fileServerRoot),
                EnableDirectoryBrowsing = true
            };
            FileExtensionContentTypeProvider contentTypeProvider = (FileExtensionContentTypeProvider)fileServerOptions.StaticFileOptions.ContentTypeProvider;
            contentTypeProvider.Mappings.Add(".apk", "application/vnd.android.package-archive");

            appBuilder.UseStaticFiles(staticFileOptions);
            appBuilder.UseFileServer(fileServerOptions);
        }
    }
}
