using Topshelf;

namespace SD.IdentitySystem.StubWCF.Client
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

                config.SetServiceName("SD.IdentitySystem.StubWCF.Client");
                config.SetDisplayName("SD.IdentitySystem.StubWCF.Client");
                config.SetDescription("SD.IdentitySystem.StubWCF.Client");
            });
        }
    }
}
