using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.ViewModels.Formats.EasyUI;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using SD.Infrastructure.Attributes;
using SD.Infrastructure.Constants;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.MVC;
using SD.Infrastructure.MVC.Filters;
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
        [RequireAuthorization("信息系统管理首页视图")]
        public ViewResult Index()
        {
            return base.View();
        }
        #endregion

        #region # 加载创建信息系统视图 —— ViewResult Add()
        /// <summary>
        /// 加载创建信息系统视图
        /// </summary>
        /// <returns>创建信息系统视图</returns>
        [HttpGet]
        [RequireAuthorization("创建信息系统视图")]
        public ViewResult Add()
        {
            return base.View();
        }
        #endregion

        #region # 加载初始化信息系统视图 —— ViewResult Init(string id)
        /// <summary>
        /// 加载初始化信息系统视图
        /// </summary>
        /// <param name="id">信息系统编号</param>
        /// <returns>初始化信息系统视图</returns>
        [HttpGet]
        [RequireAuthorization("初始化信息系统视图")]
        public ViewResult Init(string id)
        {
            InfoSystemView currentSystem = this._systemPresenter.GetInfoSystem(id);

            return base.View(currentSystem);
        }
        #endregion


        //命令部分

        #region # 创建信息系统 —— void CreateInfoSystem(string systemNo, string systemName...
        /// <summary>
        /// 创建信息系统
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="systemName">信息系统名称</param>
        /// <param name="adminLoginId">系统管理员账号</param>
        /// <param name="applicationType">应用程序类型</param>
        [HttpPost]
        [RequireAuthorization("创建信息系统")]
        public void CreateInfoSystem(string systemNo, string systemName, string adminLoginId, ApplicationType applicationType)
        {
            this._authorizationContract.CreateInfoSystem(systemNo, systemName, adminLoginId, applicationType);
        }
        #endregion

        #region # 初始化信息系统 —— void InitInfoSystem(string systemNo, string host...
        /// <summary>
        /// 初始化信息系统
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="host">主机名称</param>
        /// <param name="port">端口</param>
        /// <param name="index">首页</param>
        [HttpPost]
        [RequireAuthorization("初始化信息系统")]
        public void InitInfoSystem(string systemNo, string host, int port, string index)
        {
            this._authorizationContract.InitInfoSystem(systemNo, host, port, index);
        }
        #endregion


        //查询部分

        #region # 获取信息系统列表 —— JsonResult GetInfoSystems(string keywords, int page...
        /// <summary>
        /// 获取信息系统列表
        /// </summary>
        /// <returns>信息系统列表</returns>
        [RequireAuthorization("获取信息系统列表")]
        public JsonResult GetInfoSystems(string keywords, int page, int rows)
        {
            PageModel<InfoSystemView> pageModel = this._systemPresenter.GetInfoSystems(keywords, page, rows);

            Grid<InfoSystemView> grid = new Grid<InfoSystemView>(pageModel.RowCount, pageModel.Datas);

            return base.Json(grid, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
