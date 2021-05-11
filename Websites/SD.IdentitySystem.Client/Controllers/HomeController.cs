using SD.Infrastructure.Attributes;
using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using System.Web.Mvc;

namespace SD.IdentitySystem.Client.Controllers
{
    /// <summary>
    /// 主页控制器
    /// </summary>
    public class HomeController : Controller
    {
        #region # 加载主页视图 —— ViewResult Index()
        /// <summary>
        /// 加载主页视图
        /// </summary>
        /// <returns>主页视图</returns>
        [RequireAuthorization("主页视图")]
        public ViewResult Index()
        {
            LoginInfo loginInfo = HttpContext.Session[SessionKey.CurrentUser] as LoginInfo;

            base.ViewBag.LoginId = loginInfo?.LoginId;
            base.ViewBag.RealName = loginInfo?.RealName;

            return this.View();
        }
        #endregion
    }
}
