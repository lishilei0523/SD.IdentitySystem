using System;
using System.Windows.Forms;
using SD.IOC.Core.Mediator;

namespace SD.IdentitySystem.InitializationTool
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(ResolveMediator.Resolve<MainWindow>());
        }
    }
}
