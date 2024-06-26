using Avalonia.Controls.ApplicationLifetimes;
using Caliburn.Micro;
using Microsoft.Extensions.DependencyInjection;
using SD.IdentitySystem.Client.ViewModels.HomeContext;
using SD.IOC.Core.Mediators;
using SD.IOC.Extension.NetCore;
using System;
using System.Collections.Generic;

namespace SD.IdentitySystem.Client
{
    /// <summary>
    /// Caliburn启动器
    /// </summary>
    public class Startup : BootstrapperBase
    {
        #region # 构造器

        #region 00.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        public Startup()
        {

        }
        #endregion

        #endregion

        #region # 事件

        #region 应用程序启动事件 —— override void OnStartup(object sender...
        /// <summary>
        /// 应用程序启动事件
        /// </summary>
        protected override async void OnStartup(object sender, ControlledApplicationLifetimeStartupEventArgs eventArgs)
        {
            await base.DisplayRootViewFor<FormViewModel>();
        }
        #endregion

        #region 应用程序退出事件 —— override void OnExit(object sender...
        /// <summary>
        /// 应用程序退出事件
        /// </summary>
        protected override void OnExit(object sender, ControlledApplicationLifetimeExitEventArgs eventArgs)
        {
            ResolveMediator.Dispose();
        }
        #endregion

        #endregion

        #region # 方法

        #region 配置应用程序 —— override void Configure()
        /// <summary>
        /// 配置应用程序
        /// </summary>
        protected override void Configure()
        {
            //初始化依赖注入容器
            if (!ResolveMediator.ContainerBuilt)
            {
                IServiceCollection serviceCollection = ResolveMediator.GetServiceCollection();
                serviceCollection.RegisterConfigs();
                ResolveMediator.Build();
            }
        }
        #endregion

        #region 解析服务实例 —— override object GetInstance(Type service...
        /// <summary>
        /// 解析服务实例
        /// </summary>
        /// <param name="service">服务类型</param>
        /// <param name="key">键</param>
        /// <returns>服务实例</returns>
        protected override object GetInstance(Type service, string key)
        {
            object instance = ResolveMediator.Resolve(service);

            return instance;
        }
        #endregion

        #region 解析服务实例列表 —— override IEnumerable<object> GetAllInstances(Type service)
        /// <summary>
        /// 解析服务实例列表
        /// </summary>
        /// <param name="service">服务类型</param>
        /// <returns>服务实例列表</returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            IEnumerable<object> instances = ResolveMediator.ResolveAll(service);

            return instances;
        }
        #endregion

        #endregion
    }
}
