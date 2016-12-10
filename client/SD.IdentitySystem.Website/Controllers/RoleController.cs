using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.ViewModels.Formats.EasyUI;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using ShSoft.Infrastructure.DTOBase;
using ShSoft.Infrastructure.MVC;
using ShSoft.Infrastructure.MVC.Filters;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SD.IdentitySystem.Website.Controllers
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    [ExceptionFilter]
    public class RoleController : BaseController
    {
        #region # 字段及构造器

        /// <summary>
        /// 角色呈现器接口
        /// </summary>
        private readonly IRolePresenter _rolePresenter;

        /// <summary>
        /// 信息系统呈现器接口
        /// </summary>
        private readonly IInfoSystemPresenter _systemPresenter;

        /// <summary>
        /// 权限呈现器接口
        /// </summary>
        private readonly IAuthorityPresenter _authorityPresenter;

        /// <summary>
        /// 权限服务接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="rolePresenter">角色呈现器接口</param>
        /// <param name="systemPresenter">信息系统呈现器接口</param>
        /// <param name="authorityPresenter">权限呈现器接口</param>
        /// <param name="authorizationContract">权限服务接口</param>
        public RoleController(IRolePresenter rolePresenter, IInfoSystemPresenter systemPresenter, IAuthorityPresenter authorityPresenter, IAuthorizationContract authorizationContract)
        {
            this._rolePresenter = rolePresenter;
            this._systemPresenter = systemPresenter;
            this._authorityPresenter = authorityPresenter;
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
            IEnumerable<InfoSystemView> systems = this._systemPresenter.GetInfoSystems();
            base.ViewBag.InfoSystems = systems;

            return base.View();
        }
        #endregion

        #region # 加载创建角色视图 —— ViewResult Add()
        /// <summary>
        /// 加载创建角色视图
        /// </summary>
        /// <returns>创建角色视图</returns>
        [HttpGet]
        public ViewResult Add()
        {
            IEnumerable<InfoSystemView> systems = this._systemPresenter.GetInfoSystems();
            base.ViewBag.InfoSystems = systems;

            return base.View();
        }
        #endregion

        #region # 加载修改角色视图 —— ViewResult Update(Guid id)
        /// <summary>
        /// 加载修改角色视图
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns>修改角色视图</returns>
        [HttpGet]
        public ViewResult Update(Guid id)
        {
            RoleView currentRole = this._rolePresenter.GetRole(id);

            return base.View(currentRole);
        }
        #endregion


        //命令部分

        #region # 创建角色 —— void CreateRole(string systemNo, string roleName...
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="description">角色描述</param>
        /// <param name="authorityIds">权限Id集</param>
        [HttpPost]
        public void CreateRole(string systemNo, string roleName, string description, IEnumerable<Guid> authorityIds)
        {
            authorityIds = authorityIds ?? new Guid[0];

            this._authorizationContract.CreateRole(systemNo, roleName, description, authorityIds);
        }
        #endregion

        #region # 修改角色 —— void UpdateRole(Guid roleId, string roleName...
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="description">角色描述</param>
        /// <param name="authorityIds">权限Id集</param>
        [HttpPost]
        public void UpdateRole(Guid roleId, string roleName, string description, IEnumerable<Guid> authorityIds)
        {
            authorityIds = authorityIds ?? new Guid[0];

            this._authorizationContract.UpdateRole(roleId, roleName, description, authorityIds);
        }
        #endregion

        #region # 删除角色 —— void RemoveRole(Guid id)
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">角色Id</param>
        [HttpPost]
        public void RemoveRole(Guid id)
        {
            this._authorizationContract.RemoveRole(id);
        }
        #endregion

        #region # 批量删除角色 —— void RemoveRoles(IEnumerable<Guid> roleIds)
        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="roleIds">角色Id集</param>
        [HttpPost]
        public void RemoveRoles(IEnumerable<Guid> roleIds)
        {
            roleIds = roleIds ?? new Guid[0];

            foreach (Guid roleId in roleIds)
            {
                this._authorizationContract.RemoveRole(roleId);
            }
        }
        #endregion


        //查询部分

        #region # 获取角色列表 —— JsonResult GetRoles(string systemNo, string keywords...
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="page">页码</param>
        /// <param name="rows">页容量</param>
        /// <returns>角色列表</returns>
        public JsonResult GetRoles(string systemNo, string keywords, int page, int rows)
        {
            PageModel<RoleView> pageModel = this._rolePresenter.GetRolesByPage(systemNo, keywords, page, rows);

            Grid<RoleView> grid = new Grid<RoleView>(pageModel.RowCount, pageModel.Datas);

            return base.Json(grid, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region # 获取用户的信息系统/角色树 —— JsonResult GetRoleTree(string id)
        /// <summary>
        /// 获取用户的信息系统/角色树
        /// </summary>
        /// <param name="id">用户登录名</param>
        /// <returns>信息系统/角色树</returns>
        public JsonResult GetRoleTree(string id)
        {
            string loginId = id;

            IEnumerable<Node> tree = this._rolePresenter.GetRoleTreeByUser(loginId);

            return base.Json(tree, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
