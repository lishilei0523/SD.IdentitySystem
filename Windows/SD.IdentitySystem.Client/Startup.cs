using Caliburn.Micro;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SD.Common;
using SD.IdentitySystem.Client.ViewModels.Home;
using SD.IOC.Core.Mediators;
using SD.IOC.Extension.NetFx;
using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

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
            this.Initialize();
        }
        #endregion

        #endregion

        #region # 事件

        #region 应用程序启动事件 —— override void OnStartup(object sender...
        /// <summary>
        /// 应用程序启动事件
        /// </summary>
        protected override void OnStartup(object sender, StartupEventArgs eventArgs)
        {
            base.DisplayRootViewFor<LoginViewModel>();
        }
        #endregion

        #region 应用程序异常事件 —— override void OnUnhandledException(object sender...
        /// <summary>
        /// 应用程序异常事件
        /// </summary>
        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs eventArgs)
        {
            Exception exception = eventArgs.Exception;
            eventArgs.Handled = true;

            //提示消息
            string errorMessage = string.Empty;
            errorMessage = GetErrorMessage(exception.Message, ref errorMessage);
            MessageBox.Show(errorMessage, "错误", MessageBoxButton.OK, MessageBoxImage.Error);

            #region # 身份认证异常处理

            if (exception is FaultException faultException && faultException.Code.Name == HttpStatusCode.Unauthorized.ToString())
            {
                IList<Window> activeWindows = new List<Window>();
                foreach (Window window in Application.Current.Windows)
                {
                    activeWindows.Add(window);
                }

                base.DisplayRootViewFor<LoginViewModel>();
                activeWindows.ForEach(window => window.Close());
            }

            #endregion

            //记录日志
            WriteLog(exception);
        }
        #endregion

        #region 应用程序退出事件 —— override void OnExit(object sender...
        /// <summary>
        /// 应用程序退出事件
        /// </summary>
        protected override void OnExit(object sender, EventArgs eventArgs)
        {
            try
            {
                ResolveMediator.Dispose();
            }
            catch (Exception exception)
            {
                WriteLog(exception);
            }
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

        #region 获取错误消息 —— static string GetErrorMessage(string exceptionMessage...
        /// <summary>
        /// 获取错误消息
        /// </summary>
        /// <param name="exceptionMessage">异常消息</param>
        /// <param name="errorMessage">错误消息</param>
        /// <returns>错误消息</returns>
        private static string GetErrorMessage(string exceptionMessage, ref string errorMessage)
        {
            try
            {
                const string errorMessageKey = "ErrorMessage";
                JObject jObject = (JObject)JsonConvert.DeserializeObject(exceptionMessage);
                if (jObject != null && jObject.ContainsKey(errorMessageKey))
                {
                    errorMessage = jObject.GetValue(errorMessageKey)?.ToString();
                }
                else
                {
                    errorMessage = exceptionMessage;
                }

                GetErrorMessage(errorMessage, ref errorMessage);

                return errorMessage;
            }
            catch
            {
                return exceptionMessage;
            }
        }
        #endregion

        #region 记录日志 —— static void WriteLog(Exception exception)
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="exception">异常</param>
        private static void WriteLog(Exception exception)
        {
            string exceptionLogPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\ExceptionLogs\\{{0:yyyy-MM-dd}}.txt";
            Task.Run(() =>
            {
                FileExtension.WriteFile(string.Format(exceptionLogPath, DateTime.Today),
                    "===================================WPF运行异常, 详细信息如下==================================="
                    + Environment.NewLine + "［异常时间］" + DateTime.Now
                    + Environment.NewLine + "［异常消息］" + exception.Message
                    + Environment.NewLine + "［异常明细］" + exception
                    + Environment.NewLine + "［内部异常］" + exception.InnerException
                    + Environment.NewLine + "［堆栈信息］" + exception.StackTrace
                    + Environment.NewLine + Environment.NewLine);
            });
        }
        #endregion

        #endregion
    }
}
