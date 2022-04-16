using SD.IdentitySystem.AspNetMvc.Authentication.Filters;
using SD.Infrastructure;
using SD.Toolkits.AspNetMvc.Filters;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace SD.IdentitySystem.Client
{
    /// <summary>
    /// 全局应用程序类
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// 应用程序启动事件
        /// </summary>
        protected void Application_Start()
        {
            //初始化配置文件
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");
            FrameworkSection.Initialize(configuration);

            //注册区域
            AreaRegistration.RegisterAllAreas();

            //注册过滤器
            GlobalFilters.Filters.Add(new MvcExceptionFilter());
            GlobalFilters.Filters.Add(new MvcAuthenticationFilter());

            //注册路由
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
