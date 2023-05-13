using Microsoft.AspNetCore.Mvc;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.Presentation.Models;
using SD.IdentitySystem.Presentation.Presenters;
using SD.Infrastructure.Attributes;
using SD.Infrastructure.Constants;
using SD.Infrastructure.DTOBase;
using SD.Toolkits.EasyUI;

namespace SD.IdentitySystem.Client.Controllers
{
    /// <summary>
    /// 信息系统控制器
    /// </summary>
    public class InfoSystemController : Controller
    {
        #region # 字段及构造器

        /// <summary>
        /// 信息系统呈现器
        /// </summary>
        private readonly InfoSystemPresenter _infoSystemPresenter;

        /// <summary>
        /// 权限管理服务契约接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public InfoSystemController(InfoSystemPresenter infoSystemPresenter, IAuthorizationContract authorizationContract)
        {
            this._infoSystemPresenter = infoSystemPresenter;
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
            InfoSystem currentSystem = this._infoSystemPresenter.GetInfoSystem(id);

            return base.View(currentSystem);
        }
        #endregion


        //命令部分

        #region # 创建信息系统 —— void CreateInfoSystem(string infoSystemNo, string infoSystemName...
        /// <summary>
        /// 创建信息系统
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="infoSystemName">信息系统名称</param>
        /// <param name="adminLoginId">系统管理员账号</param>
        /// <param name="applicationType">应用程序类型</param>
        [HttpPost]
        [RequireAuthorization("创建信息系统")]
        public void CreateInfoSystem(string infoSystemNo, string infoSystemName, string adminLoginId, ApplicationType applicationType)
        {
            this._authorizationContract.CreateInfoSystem(infoSystemNo, infoSystemName, adminLoginId, applicationType);
        }
        #endregion

        #region # 初始化信息系统 —— void InitInfoSystem(string infoSystemNo, string host...
        /// <summary>
        /// 初始化信息系统
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="host">主机名称</param>
        /// <param name="port">端口</param>
        /// <param name="index">首页</param>
        [HttpPost]
        [RequireAuthorization("初始化信息系统")]
        public void InitInfoSystem(string infoSystemNo, string host, int port, string index)
        {
            this._authorizationContract.InitInfoSystem(infoSystemNo, host, port, index);
        }
        #endregion


        //查询部分

        #region # 分页获取信息系统列表 —— JsonResult GetInfoSystemsByPage(string keywords, int page...
        /// <summary>
        /// 分页获取信息系统列表
        /// </summary>
        /// <returns>信息系统列表</returns>
        [RequireAuthorization("分页获取信息系统列表")]
        public JsonResult GetInfoSystemsByPage(string keywords, int page, int rows)
        {
            PageModel<InfoSystem> pageModel = this._infoSystemPresenter.GetInfoSystemsByPage(keywords, page, rows);
            Grid<InfoSystem> grid = new Grid<InfoSystem>(pageModel.RowCount, pageModel.Datas);

            return base.Json(grid);
        }
        #endregion
    }
}
