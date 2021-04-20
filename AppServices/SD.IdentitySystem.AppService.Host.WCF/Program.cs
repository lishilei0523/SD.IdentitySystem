using System.Configuration;
using Topshelf;

namespace SD.IdentitySystem.AppService.Host
{
    class Program
    {
        private static readonly string _ServiceName = ConfigurationManager.AppSettings["ServiceName"];
        private static readonly string _ServiceDisplayName = ConfigurationManager.AppSettings["ServiceDisplayName"];
        private static readonly string _ServiceDescription = ConfigurationManager.AppSettings["ServiceDescription"];


        static void Main(string[] args)
        {
            HostFactory.Run(config =>
            {
                config.Service<ServiceLauncher>(host =>
                {
                    host.ConstructUsing(name => new ServiceLauncher());
                    host.WhenStarted(launcher => launcher.Start());
                    host.WhenStopped(launcher => launcher.Stop());
                });
                config.RunAsLocalSystem();

                config.SetServiceName(_ServiceName);
                config.SetDisplayName(_ServiceDisplayName);
                config.SetDescription(_ServiceDescription);
            });
        }
    }
}
