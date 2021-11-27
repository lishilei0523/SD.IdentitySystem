using SD.Infrastructure.Attributes;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
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

            return base.View();
        }
        #endregion

        #region # 加载异常视图 —— ViewResult Exception(string message)
        /// <summary>
        /// 加载异常视图
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <returns>异常视图</returns>
        [RequireAuthorization("异常视图")]
        public ViewResult Exception(string message)
        {
            base.ViewBag.ErrorMessage = message;

            return base.View();
        }
        #endregion
    }
}
