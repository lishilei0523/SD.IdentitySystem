using System.Web.Mvc;
using ShSoft.Infrastructure.MVC;
using ShSoft.Infrastructure.MVC.Filters;

namespace SD.IdentitySystem.Website.Controllers
{
    /// <summary>
    /// 主页控制器
    /// </summary>
    [ExceptionFilter]
    public class HomeController : BaseController
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
