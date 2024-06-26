using Avalonia;
using Avalonia.Markup.Xaml;

namespace SD.IdentitySystem.Client
{
    /// <summary>
    /// Avalonia应用程序
    /// </summary>
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            base.OnFrameworkInitializationCompleted();

            //Caliburn启动
            Startup startup = new Startup();
            startup.Initialize();
        }
    }
}
