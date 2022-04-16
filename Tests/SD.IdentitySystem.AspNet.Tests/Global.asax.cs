using SD.Infrastructure;
using System;
using System.Configuration;
using System.Web;
using System.Web.Configuration;

namespace SD.IdentitySystem.AspNet.Tests
{
    /// <summary>
    /// 全局应用程序类
    /// </summary>
    public class Global : HttpApplication
    {
        /// <summary>
        /// 应用程序启动事件
        /// </summary>
        protected void Application_Start(object sender, EventArgs eventArgs)
        {
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");
            FrameworkSection.Initialize(configuration);
        }
    }
}