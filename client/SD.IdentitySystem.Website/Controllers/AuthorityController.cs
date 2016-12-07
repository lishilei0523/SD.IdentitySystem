using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.ViewModels.Formats.EasyUI;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using ShSoft.Infrastructure.DTOBase;
using ShSoft.Infrastructure.MVC;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SD.IdentitySystem.Website.Controllers
{
    /// <summary>
    /// 权限控制器
    /// </summary>
    public class AuthorityController : BaseController
    {
        #region # 字段及构造器

        /// <summary>
        /// 信息系统呈现器接口
        /// </summary>
        private readonly IInfoSystemPresenter _systemPresenter;

        /// <summary>
        /// 权限呈现器接口
        /// </summary>
        private readonly IAuthorityPresenter _authorityPresenter;

        /// <summary>
        /// 角色呈现器接口
        /// </summary>
        private readonly IRolePresenter _rolePresenter;

        /// <summary>
        /// 权限服务接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="systemPresenter">信息系统呈现器接口</param>
        /// <param name="authorityPresenter">权限呈现器接口</param>
        /// <param name="authorizationContract">权限服务接口</param>
        /// <param name="rolePresenter">角色呈现器接口</param>
        public AuthorityController(IInfoSystemPresenter systemPresenter, IAuthorityPresenter authorityPresenter, IAuthorizationContract authorizationContract, IRolePresenter rolePresenter)
        {
            this._systemPresenter = systemPresenter;
            this._authorityPresenter = authorityPresenter;
            this._authorizationContract = authorizationContract;
            this._rolePresenter = rolePresenter;
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
            IEnumerable<InfoSystemView> systems = this._systemPresenter.GetInfoSystems();
            base.ViewBag.InfoSystems = systems;

            return base.View();
        }
        #endregion


        //命令部分


        //查询部分

        #region # 分页获取权限列表 —— JsonResult GetAuthorities(string systemNo...
        /// <summary>
        /// 分页获取权限列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="page">页码</param>
        /// <param name="rows">页容量</param>
        /// <returns>权限列表</returns>
        public JsonResult GetAuthorities(string systemNo, string keywords, int page, int rows)
        {
            PageModel<AuthorityView> pageModel = this._authorityPresenter.GetAuthoritiesByPage(systemNo, keywords, page, rows);

            Grid<AuthorityView> grid = new Grid<AuthorityView>(pageModel.RowCount, pageModel.Datas);

            return base.Json(grid, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region # 获取信息系统/权限树 —— JsonResult GetAuthorityTree(string id)
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="id">信息系统编号</param>
        /// <returns>权限列表</returns>
        public JsonResult GetAuthorityTree(string id)
        {
            Node node = this._authorityPresenter.GetAuthorityTree(id);

            IEnumerable<Node> tree = new[] { node };

            return base.Json(tree, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region # 获取角色的权限树 —— JsonResult GetAuthorityTreeByRole(Guid id)
        /// <summary>
        /// 获取角色的权限树
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns>权限列表</returns>
        public JsonResult GetAuthorityTreeByRole(Guid id)
        {
            Node node = this._authorityPresenter.GetAuthorityTreeByRole(id);

            IEnumerable<Node> tree = new[] { node };

            return base.Json(tree, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region # 根据菜单的权限树 —— JsonResult GetAuthorityTreeByMenu(Guid id)
        /// <summary>
        /// 根据菜单的权限树
        /// </summary>
        /// <param name="id">菜单Id</param>
        /// <returns>权限列表</returns>
        public JsonResult GetAuthorityTreeByMenu(Guid id)
        {
            Node node = this._authorityPresenter.GetAuthorityTreeByMenu(id);

            IEnumerable<Node> tree = new[] { node };

            return base.Json(tree, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
