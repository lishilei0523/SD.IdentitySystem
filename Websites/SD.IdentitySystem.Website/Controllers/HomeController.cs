using SD.Infrastructure.Attributes;
using System.Web.Mvc;

namespace SD.IdentitySystem.Website.Controllers
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
            //TODO 实现
            //base.ViewBag.LoginId = Membership.LoginInfo == null ? null : OperationContext.LoginInfo.LoginId;
            //base.ViewBag.RealName = OperationContext.LoginInfo == null ? null : OperationContext.LoginInfo.RealName;

            return this.View();
        }
        #endregion
    }
}
