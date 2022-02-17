using Autofac;
using SD.IOC.Core.Extensions;
using SD.IOC.Core.Mediators;
using System;
using System.Windows.Forms;

namespace SD.IdentitySystem.AuthorityImporter
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

            //初始化依赖注入容器
            InitContainer();

            MainWindow mainWindow = ResolveMediator.Resolve<MainWindow>();
            Application.Run(mainWindow);
        }

        /// <summary>
        /// 初始化依赖注入容器
        /// </summary>
        static void InitContainer()
        {
            if (!ResolveMediator.ContainerBuilt)
            {
                ContainerBuilder builder = ResolveMediator.GetContainerBuilder();
                builder.RegisterConfigs();

                ResolveMediator.Build();
            }
        }
    }
}
