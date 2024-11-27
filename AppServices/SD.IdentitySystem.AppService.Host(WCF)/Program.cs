using CoreWCF.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SD.Toolkits.AspNet;
using Serilog;
using System;
using System.IO;

namespace SD.IdentitySystem.AppService.Host
{
    class Program
    {
        static void Main()
        {
            IHostBuilder hostBuilder = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder();

            //WebHost配置
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel(options =>
                {
                    foreach (int httpPort in AspNetSetting.HttpPorts)
                    {
                        options.ListenAnyIP(httpPort);
                    }
                });
                foreach (int netTcpPort in AspNetSetting.NetTcpPorts)
                {
                    webBuilder.UseNetTcp(netTcpPort);
                }
                webBuilder.UseStartup<Startup>();
            });

            //依赖注入配置
            ServiceLocator serviceLocator = new ServiceLocator();
            hostBuilder.UseServiceProviderFactory(serviceLocator);

            //日志配置
            string logFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            Directory.CreateDirectory(logFolder);
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Async(config => config.File($"{logFolder}/Log_.txt", rollingInterval: RollingInterval.Day, buffered: true), 1000)
                .CreateLogger();
            hostBuilder.UseSerilog();
#if OS_LINUX
            //Linux系统服务配置
            hostBuilder.UseSystemd();
#endif
            IHost host = hostBuilder.Build();
            host.Run();
        }
    }
}
