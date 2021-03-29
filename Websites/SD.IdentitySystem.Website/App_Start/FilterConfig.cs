using SD.IdentitySystem.MVC.Authentication.Filters;
using SD.Toolkits.MVC.Filters;
using System.Web.Mvc;

namespace SD.IdentitySystem.Website
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MvcExceptionFilter());
            filters.Add(new MvcAuthenticationFilter());
        }
    }
}