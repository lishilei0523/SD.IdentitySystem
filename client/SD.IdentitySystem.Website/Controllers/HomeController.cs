using SD.Infrastructure.Attributes;
using SD.Infrastructure.MVC;
using SD.Infrastructure.MVC.Filters;
using System.Web.Mvc;

namespace SD.IdentitySystem.Website.Controllers
{
    /// <summary>
    /// 主页控制器
    /// </summary>
    [ExceptionFilter]
    [AuthorizationFilter]
    public class HomeController : Controller
    {
        //视图部分

        #region # 加载主页视图 —— ViewResult Index()
        /// <summary>
        /// 加载主页视图
        /// </summary>
        /// <returns>主页视图</returns>
        [RequireAuthorization("主页视图")]
        public ViewResult Index()
        {
            base.ViewBag.LoginId = OperationContext.LoginInfo == null ? null : OperationContext.LoginInfo.LoginId;
            base.ViewBag.RealName = OperationContext.LoginInfo == null ? null : OperationContext.LoginInfo.RealName;

            return this.View();
        }
        #endregion
    }
}
