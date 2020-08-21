using System.Web.Mvc;
using System.Web.Routing;

namespace SD.IdentitySystem.Website
{
    /// <summary>
    /// 全局应用程序类
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}