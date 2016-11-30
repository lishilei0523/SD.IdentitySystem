using ShSoft.Infrastructure.MVC.Filters;
using System.Web.Mvc;

namespace SD.IdentitySystem.Website.Controllers
{
    /// <summary>
    /// 主页控制器
    /// </summary>
    [ExceptionFilter]
    public class HomeController : Controller
    {
        /// <summary>
        /// 主页视图
        /// </summary>
        /// <returns>主页视图</returns>
        public ActionResult Index()
        {
            return this.View();
        }

    }
}
