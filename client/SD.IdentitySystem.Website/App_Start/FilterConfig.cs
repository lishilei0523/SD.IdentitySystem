using System.Web.Mvc;
using ShSoft.Infrastructure.MVC.Filters;

namespace SD.IdentitySystem.Website
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionFilterAttribute());
        }
    }
}