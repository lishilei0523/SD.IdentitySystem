using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SD.Toolkits.AspNet;

namespace SD.IdentitySystem.UpdateService
{
    public class Program
    {
        public static void Main()
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

            IHost host = hostBuilder.Build();
            host.Run();
        }
    }
}
