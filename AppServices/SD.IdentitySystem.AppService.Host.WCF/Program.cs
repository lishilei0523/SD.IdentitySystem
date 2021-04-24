using SD.Infrastructure;
using Topshelf;

namespace SD.IdentitySystem.AppService.Host
{
    class Program
    {
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

                config.SetServiceName(FrameworkSection.Setting.ServiceName.Value);
                config.SetDisplayName(FrameworkSection.Setting.ServiceDisplayName.Value);
                config.SetDescription(FrameworkSection.Setting.ServiceDescription.Value);
            });
        }
    }
}
