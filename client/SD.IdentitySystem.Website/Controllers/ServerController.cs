using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using SD.Infrastructure.Attributes;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.MVC.Filters;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SD.FormatModel.EasyUI;

namespace SD.IdentitySystem.Website.Controllers
{
    /// <summary>
    /// 服务器控制器
    /// </summary>
    [ExceptionFilter]
    [AuthorizationFilter]
    public class ServerController : Controller
    {
        #region # 字段及构造器

        /// <summary>
        /// 服务器呈现器接口
        /// </summary>
        private readonly IServerPresenter _serverPresenter;

        /// <summary>
        /// 权限服务接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="serverPresenter">服务器呈现器接口</param>
        /// <param name="authorizationContract">权限服务接口</param>
        public ServerController(IServerPresenter serverPresenter, IAuthorizationContract authorizationContract)
        {
            this._serverPresenter = serverPresenter;
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
        [RequireAuthorization("服务器管理首页视图")]
        public ViewResult Index()
        {
            return base.View();
        }
        #endregion

        #region # 加载创建服务器视图 —— ViewResult Add()
        /// <summary>
        /// 加载创建服务器视图
        /// </summary>
        /// <returns>创建服务器视图</returns>
        [HttpGet]
        [RequireAuthorization("创建服务器视图")]
        public ViewResult Add()
        {
            return base.View();
        }
        #endregion

        #region # 加载修改服务器视图 —— ViewResult Update(Guid id)
        /// <summary>
        /// 加载修改服务器视图
        /// </summary>
        /// <param name="id">服务器Id</param>
        /// <returns>修改服务器视图</returns>
        [HttpGet]
        [RequireAuthorization("修改服务器视图")]
        public ViewResult Update(Guid id)
        {
            ServerView currentServer = this._serverPresenter.GetServer(id);

            return base.View(currentServer);
        }
        #endregion


        //命令部分

        #region # 创建服务器 —— void CreateServer(string uniqueCode, string hostName...
        /// <summary>
        /// 创建服务器
        /// </summary>
        /// <param name="uniqueCode">唯一码</param>
        /// <param name="hostName">主机名</param>
        /// <param name="serviceOverDate">服务停止日期</param>
        [HttpPost]
        [RequireAuthorization("创建服务器")]
        public void CreateServer(string uniqueCode, string hostName, DateTime serviceOverDate)
        {
            this._authorizationContract.CreateServer(uniqueCode, hostName, serviceOverDate);
        }
        #endregion

        #region # 修改服务器 —— void UpdateServer(Guid serverId, DateTime serviceOverDate)
        /// <summary>
        /// 修改服务器
        /// </summary>
        /// <param name="serverId">服务器Id</param>
        /// <param name="serviceOverDate">服务停止日期</param>
        [HttpPost]
        [RequireAuthorization("修改服务器")]
        public void UpdateServer(Guid serverId, DateTime serviceOverDate)
        {
            this._authorizationContract.UpdateServiceOverDate(serverId, serviceOverDate);
        }
        #endregion

        #region # 删除服务器 —— void RemoveServer(Guid id)
        /// <summary>
        /// 删除服务器
        /// </summary>
        /// <param name="id">服务器Id</param>
        [HttpPost]
        [RequireAuthorization("删除服务器")]
        public void RemoveServer(Guid id)
        {
            this._authorizationContract.RemoveServer(id);
        }
        #endregion

        #region # 批量删除服务器 —— void RemoveServers(IEnumerable<Guid> serverIds)
        /// <summary>
        /// 批量删除服务器
        /// </summary>
        /// <param name="serverIds">服务器Id集</param>
        [HttpPost]
        [RequireAuthorization("批量删除服务器")]
        public void RemoveServers(IEnumerable<Guid> serverIds)
        {
            serverIds = serverIds ?? new Guid[0];

            foreach (Guid serverId in serverIds)
            {
                this._authorizationContract.RemoveServer(serverId);
            }
        }
        #endregion


        //查询部分

        #region # 获取服务器列表 —— JsonResult GetServers(string keywords, int page, int rows)
        /// <summary>
        /// 获取服务器列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="page">页码</param>
        /// <param name="rows">页容量</param>
        /// <returns>服务器列表</returns>
        [RequireAuthorization("获取服务器列表")]
        public JsonResult GetServers(string keywords, int page, int rows)
        {
            PageModel<ServerView> pageModel = this._serverPresenter.GetServersByPage(keywords, page, rows);
            Grid<ServerView> grid = new Grid<ServerView>(pageModel.RowCount, pageModel.Datas);

            return base.Json(grid, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
