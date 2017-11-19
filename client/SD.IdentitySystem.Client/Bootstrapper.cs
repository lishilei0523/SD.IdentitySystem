using Caliburn.Micro;
using SD.IdentitySystem.Client.Commons;
using SD.IdentitySystem.Client.ViewModels;
using SD.IOC.Core.Mediator;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace SD.IdentitySystem.Client
{
    /// <summary>
    /// 引导程序
    /// </summary>
    public class Bootstrapper : BootstrapperBase
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public Bootstrapper()
        {
            base.Initialize();
        }

        /// <summary>
        /// 应用程序启动事件
        /// </summary>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            base.DisplayRootViewFor<LoginViewModel>();
        }

        /// <summary>
        /// 异常事件
        /// </summary>
        protected override async void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            Exception exception = e.Exception.GetInnerException();
            string errorMessage = exception == null ? null : exception.GetErrorMessage();

            await ElementManager.ShowMessage("错误", errorMessage);
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
