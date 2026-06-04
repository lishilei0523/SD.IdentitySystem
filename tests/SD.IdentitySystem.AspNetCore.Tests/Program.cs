using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SD.Toolkits.AspNet;

namespace SD.IdentitySystem.AspNetCore.Tests
{
    class Program
    {
        static void Main()
        {
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder();

            //WebHost≈‰÷√
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel(options =>
                {
                    foreach (int httpPort in AspNetSetting.HttpPorts)
                    {
                        options.ListenAnyIP(httpPort);
                    }
                });
                webBuilder.UseStartup<Startup>();
            });

            //“¿¿µ◊¢»Î≈‰÷√
            hostBuilder.UseServiceProviderFactory(new ServiceLocator());

            IHost host = hostBuilder.Build();
            host.Run();
        }
    }
}
