using Android.App;
using Android.Runtime;
using Caliburn.Micro;
using Microsoft.Extensions.DependencyInjection;
using SD.Common;
using SD.Infrastructure;
using SD.Infrastructure.Xamarin.Caliburn.Extensions;
using SD.IOC.Core;
using SD.IOC.Core.Mediators;
using SD.IOC.Extension.NetCore;
using SD.IOC.Extension.Xamarin.ServiceModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.ServiceModel;

namespace SD.IdentitySystem.Mobile.Host
{
    /// <summary>
    /// Xamarin.Android应用程序
    /// </summary>
    [Application]
    public class Application : CaliburnApplication
    {
        #region # 字段及构造器

        /// <summary>
        /// 创建Android应用程序构造器
        /// </summary>
        public Application(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {

        }

        #endregion

        #region # 应用程序创建事件 —— override void OnCreate()
        /// <summary>
        /// 应用程序创建事件
        /// </summary>
        public override void OnCreate()
        {
            base.OnCreate();
            base.Initialize();
        }
        #endregion

        #region # 配置应用程序 —— override void Configure()
        /// <summary>
        /// 配置应用程序
        /// </summary>
        protected override void Configure()
        {
            //初始化配置文件
            Assembly entryAssembly = Assembly.GetExecutingAssembly();
            Configuration configuration = ConfigurationExtension.GetConfigurationFromAssembly(entryAssembly);
            ServiceModelSectionGroup.Initialize(configuration);
            FrameworkSection.Initialize(configuration);
            DependencyInjectionSection.Initialize(configuration);

            //初始化依赖注入容器
            if (!ResolveMediator.ContainerBuilt)
            {
                IServiceCollection serviceCollection = ResolveMediator.GetServiceCollection();
                serviceCollection.RegisterConfigs();
                serviceCollection.RegisterServiceModels();
                serviceCollection.RegisterNavigationService();

                ResolveMediator.Build();
            }
        }
        #endregion

        #region # 选择视图模型程序集 —— override IEnumerable<Assembly> SelectAssemblies()
        /// <summary>
        /// 选择视图模型程序集
        /// </summary>
        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            Assembly[] assemblies =
            {
                typeof(Startup).Assembly
            };

            return assemblies;
        }
        #endregion

        #region # 解析服务实例 —— override object GetInstance(Type service...
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

        #region # 解析服务实例列表 —— override IEnumerable<object> GetAllInstances(Type service)
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
    }
}
