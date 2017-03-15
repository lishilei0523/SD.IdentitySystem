using SD.Infrastructure.MVC;
using SD.ValueObjects.Attributes;
using System.Web.Mvc;
using SD.Infrastructure.MVC.Filters;

namespace SD.IdentitySystem.Website.Controllers
{
    /// <summary>
    /// 主页控制器
    /// </summary>
    [ExceptionFilter]
    public class HomeController : BaseController
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
            base.ViewBag.LoginId = base.LoginInfo == null ? null : base.LoginInfo.LoginId;
            base.ViewBag.RealName = base.LoginInfo == null ? null : base.LoginInfo.RealName;

            return this.View();
        }
        #endregion
    }
}
