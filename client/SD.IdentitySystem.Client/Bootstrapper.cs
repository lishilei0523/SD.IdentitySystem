using Caliburn.Micro;
using SD.IOC.Core.Mediator;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using SD.IdentitySystem.Client.ViewModels;

namespace SD.IdentitySystem.Client
{
    /// <summary>
    /// 引导程序
    /// </summary>
    public class Bootstrapper : BootstrapperBase
    {
        /// <summary>
        /// 导航服务
        /// </summary>
        private static INavigationService _NavigationService;

        /// <summary>
        /// 导航服务
        /// </summary>
        public static INavigationService NavigationService
        {
            get { return _NavigationService; }
        }

        /// <summary>
        /// 初始化导航服务
        /// </summary>
        /// <param name="navigationService"></param>
        public static void InitNavigationService(INavigationService navigationService)
        {
            _NavigationService = navigationService;
        }

        /// <summary>
        /// 构造器
        /// </summary>
        public Bootstrapper()
        {
            this.Initialize();
        }

        /// <summary>
        /// 应用程序启动事件
        /// </summary>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            this.DisplayRootViewFor<ShellViewModel>();
        }

        /// <summary>
        /// 异常事件
        /// </summary>
        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show(e.Exception.Message, "An error as occurred", MessageBoxButton.OK);
        }

        /// <summary>
        /// 解析服务实例
        /// </summary>
        protected override object GetInstance(Type service, string key)
        {
            object instance = ResolveMediator.Resolve(service);
            return instance;
        }

        /// <summary>
        /// 解析服务实例列表
        /// </summary>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            IEnumerable<object> instances = ResolveMediator.ResolveAll(service);
            return instances;
        }
    }
}
