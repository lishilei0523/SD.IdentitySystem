using SD.Infrastructure.MVC.Filters;
using System.Web.Mvc;

namespace SD.IdentitySystem.Website
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionFilterAttribute());
        }
    }
}
