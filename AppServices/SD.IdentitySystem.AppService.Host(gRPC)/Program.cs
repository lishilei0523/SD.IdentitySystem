using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using SD.Toolkits.AspNet;
#if RELEASE
using Serilog;
using System;
using System.IO;
#endif

namespace SD.IdentitySystem.AppService.Host
{
    class Program
    {
        static void Main()
        {
            IHostBuilder hostBuilder = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder();

            //WebHost����
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel(options =>
                {
                    foreach (int httpsPort in AspNetSetting.HttpsPorts)
                    {
                        options.ListenAnyIP(httpsPort, config =>
                        {
                            config.UseHttps();
                            config.Protocols = HttpProtocols.Http2;
                        });
                    }
                });
                webBuilder.UseStartup<Startup>();
            });

            //����ע������
            ServiceLocator serviceLocator = new ServiceLocator();
            hostBuilder.UseServiceProviderFactory(serviceLocator);
#if RELEASE
            //��־����
            string logFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            Directory.CreateDirectory(logFolder);
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Async(config => config.File($"{logFolder}/Log_.txt", rollingInterval: RollingInterval.Day, buffered: true), 1000)
                .CreateLogger();
            hostBuilder.UseSerilog();
#endif
#if OS_LINUX
            //Linuxϵͳ��������
            hostBuilder.UseSystemd();
#endif
            IHost host = hostBuilder.Build();
            host.Run();
        }
    }
}
