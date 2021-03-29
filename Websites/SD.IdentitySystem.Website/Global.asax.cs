using SD.IdentitySystem.MVC.Authentication.Filters;
using SD.Toolkits.MVC.Filters;
using System.Web.Mvc;
using System.Web.Routing;

namespace SD.IdentitySystem.Website
{
    /// <summary>
    /// 全局应用程序类
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 应用程序启动事件
        /// </summary>
        protected void Application_Start()
        {
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