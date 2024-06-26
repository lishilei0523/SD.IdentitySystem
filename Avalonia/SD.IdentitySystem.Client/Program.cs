using Avalonia;
using System;

namespace SD.IdentitySystem.Client
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            AppBuilder appBuilder = BuildAvaloniaApp();
            appBuilder.StartWithClassicDesktopLifetime(args);
        }

        static AppBuilder BuildAvaloniaApp()
        {
            AppBuilder appBuilder = AppBuilder.Configure<App>();
            appBuilder.UsePlatformDetect();
            appBuilder.UseSkia();

            return appBuilder;
        }
    }
}
