using Topshelf;

namespace SD.IdentitySystem.StubWCF.Server
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

                config.SetServiceName("SD.IdentitySystem.StubWCF.Server");
                config.SetDisplayName("SD.IdentitySystem.StubWCF.Server");
                config.SetDescription("SD.IdentitySystem.StubWCF.Server");
            });
        }
    }
}
