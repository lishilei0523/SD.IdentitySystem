using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SD.Toolkits.AspNet;
using Serilog;
using System;
using System.IO;

namespace SD.IdentitySystem.UpdateService
{
    class Program
    {
        static void Main()
        {
            IHostBuilder hostBuilder = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder();

            //WebHostÅäÖÃ
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel(options =>
                {
                    foreach (int httpPort in AspNetSetting.HttpPorts)
                    {
                        options.ListenAnyIP(httpPort);
                    }

                    options.Limits.MaxRequestLineSize = short.MaxValue;
                    options.Limits.MaxRequestBodySize = int.MaxValue;
                    options.Limits.MaxRequestBufferSize = int.MaxValue;
                    options.Limits.MaxResponseBufferSize = int.MaxValue;
                });

                webBuilder.UseWebRoot(AspNetSetting.StaticFilesPath);
                webBuilder.UseStartup<Startup>();
            });

            //ÈÕÖ¾ÅäÖÃ
            string logFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            Directory.CreateDirectory(logFolder);
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Async(config => config.File($"{logFolder}/Log_.txt", rollingInterval: RollingInterval.Day, buffered: true), 1000)
                .CreateLogger();
            hostBuilder.UseSerilog();

            IHost host = hostBuilder.Build();
            host.Run();
        }
    }
}
