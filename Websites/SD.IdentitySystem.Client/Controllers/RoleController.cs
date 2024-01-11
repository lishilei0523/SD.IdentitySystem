using Microsoft.AspNetCore.Mvc;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.Presentation.Models;
using SD.IdentitySystem.Presentation.Presenters;
using SD.Infrastructure.DTOBase;
using SD.Toolkits.EasyUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Client.Controllers
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    public class RoleController : Controller
    {
        #region # 字段及构造器

        /// <summary>
        /// 角色呈现器
        /// </summary>
        private readonly RolePresenter _rolePresenter;

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
        public RoleController(RolePresenter rolePresenter, InfoSystemPresenter infoSystemPresenter, IAuthorizationContract authorizationContract)
        {
            this._rolePresenter = rolePresenter;
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
        public ViewResult Index()
        {
            IEnumerable<InfoSystem> infoSystems = this._infoSystemPresenter.GetInfoSystems();
            base.ViewBag.InfoSystems = infoSystems;

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
            IEnumerable<InfoSystem> infoSystems = this._infoSystemPresenter.GetInfoSystems();
            base.ViewBag.InfoSystems = infoSystems;

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
            Role currentRole = this._rolePresenter.GetRole(id);

            return base.View(currentRole);
        }
        #endregion


        //命令部分

        #region # 创建角色 —— void CreateRole(string infoSystemNo, string roleName...
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="description">角色描述</param>
        /// <param name="authorityIds">权限Id集</param>
        [HttpPost]
        public void CreateRole(string infoSystemNo, string roleName, string description, IEnumerable<Guid> authorityIds)
        {
            authorityIds = authorityIds?.ToArray() ?? Array.Empty<Guid>();

            this._authorizationContract.CreateRole(infoSystemNo, roleName, description, authorityIds);
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
            authorityIds = authorityIds?.ToArray() ?? Array.Empty<Guid>();

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
            roleIds = roleIds?.ToArray() ?? Array.Empty<Guid>();
            foreach (Guid roleId in roleIds)
            {
                this._authorizationContract.RemoveRole(roleId);
            }
        }
        #endregion


        //查询部分

        #region # 获取用户的信息系统/角色树 —— JsonResult GetUserInfoSystemRoleTree(string id)
        /// <summary>
        /// 获取用户的信息系统/角色树
        /// </summary>
        /// <param name="id">用户名</param>
        /// <returns>信息系统/角色树</returns>
        [HttpGet]
        [HttpPost]
        public JsonResult GetUserInfoSystemRoleTree(string id)
        {
            string loginId = id;
            IEnumerable<Node> tree = this._rolePresenter.GetUserInfoSystemRoleTree(loginId);

            return base.Json(tree);
        }
        #endregion

        #region # 分页获取角色列表 —— JsonResult GetRolesByPage(string keywords...
        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">页容量</param>
        /// <returns>角色列表</returns>
        [HttpGet]
        [HttpPost]
        public JsonResult GetRolesByPage(string keywords, string infoSystemNo, int page, int rows)
        {
            PageModel<Role> pageModel = this._rolePresenter.GetRolesByPage(keywords, infoSystemNo, page, rows);
            Grid<Role> grid = new Grid<Role>(pageModel.RowCount, pageModel.Datas);

            return base.Json(grid);
        }
        #endregion
    }
}
