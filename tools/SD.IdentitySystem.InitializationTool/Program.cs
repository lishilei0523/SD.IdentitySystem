using Microsoft.Extensions.DependencyInjection;
using SD.IOC.Core.Mediators;
using SD.IOC.Extension.NetFx;
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
            //初始化容器
            InitContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(ResolveMediator.Resolve<MainWindow>());
        }

        /// <summary>
        /// 初始化容器
        /// </summary>
        static void InitContainer()
        {
            IServiceCollection serviceCollection = ResolveMediator.GetServiceCollection();
            serviceCollection.RegisterConfigs();

            ResolveMediator.Build();
        }
    }
}
