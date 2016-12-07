using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using ShSoft.Infrastructure.MVC;
using ShSoft.Infrastructure.MVC.Filters;
using System.Web.Mvc;

namespace SD.IdentitySystem.Website.Controllers
{
    /// <summary>
    /// 信息系统控制器
    /// </summary>
    [ExceptionFilter]
    public class InfoSystemController : BaseController
    {
        #region # 字段及构造器

        /// <summary>
        /// 信息系统呈现器接口
        /// </summary>
        private readonly IInfoSystemPresenter _systemPresenter;

        /// <summary>
        /// 权限服务接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="systemPresenter">信息系统呈现器接口</param>
        /// <param name="authorizationContract">权限服务接口</param>
        public InfoSystemController(IInfoSystemPresenter systemPresenter, IAuthorizationContract authorizationContract)
        {
            this._systemPresenter = systemPresenter;
            this._authorizationContract = authorizationContract;
        }

        #endregion


        //视图部分

        #region # 加载首页视图 —— ViewResult Index()
        /// <summary>
        /// 加载首页视图
        /// </summary>
        /// <returns>首页视图</returns>
        [HttpGet]
        public ViewResult Index()
        {
            return base.View();
        }
        #endregion
    }
}
