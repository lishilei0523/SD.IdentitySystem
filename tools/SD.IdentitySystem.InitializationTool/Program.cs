using SD.IOC.Core.Mediators;
using System;
using System.Windows.Forms;

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
