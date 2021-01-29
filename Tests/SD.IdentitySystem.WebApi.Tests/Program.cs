using Topshelf;

namespace SD.IdentitySystem.WebApi.Tests
{
    class Program
    {
        static void Main()
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

                config.SetServiceName("ASP.NET WebApi SelfHost");
                config.SetDisplayName("ASP.NET WebApi SelfHost");
                config.SetDescription("ASP.NET WebApi SelfHost");
            });
        }
    }
}
