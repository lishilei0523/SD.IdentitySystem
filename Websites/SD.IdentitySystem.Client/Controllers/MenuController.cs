using SD.Common;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.Models;
using SD.Infrastructure.Attributes;
using SD.Infrastructure.Constants;
using SD.Toolkits.EasyUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SD.IdentitySystem.Client.Controllers
{
    /// <summary>
    /// 菜单控制器
    /// </summary>
    public class MenuController : Controller
    {
        #region # 字段及构造器

        /// <summary>
        /// 菜单呈现器
        /// </summary>
        private readonly IMenuPresenter _menuPresenter;

        /// <summary>
        /// 信息系统呈现器
        /// </summary>
        private readonly IInfoSystemPresenter _systemPresenter;

        /// <summary>
        /// 权限服务接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="menuPresenter">菜单呈现器</param>
        /// <param name="systemPresenter">信息系统呈现器</param>
        /// <param name="authorizationContract">权限服务接口</param>
        public MenuController(IMenuPresenter menuPresenter, IInfoSystemPresenter systemPresenter, IAuthorizationContract authorizationContract)
        {
            this._menuPresenter = menuPresenter;
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
        [RequireAuthorization("菜单管理首页视图")]
        public ViewResult Index()
        {
            IEnumerable<InfoSystem> systems = this._systemPresenter.GetInfoSystems();
            IDictionary<int, string> applicationTypeDescriptions = typeof(ApplicationType).GetEnumDictionary();

            base.ViewBag.InfoSystems = systems;
            base.ViewBag.ApplicationTypeDescriptions = applicationTypeDescriptions;

            return base.View();
        }
        #endregion

        #region # 加载创建菜单视图 —— ViewResult Add()
        /// <summary>
        /// 加载创建菜单视图
        /// </summary>
        /// <returns>创建菜单视图</returns>
        [HttpGet]
        [RequireAuthorization("创建菜单视图")]
        public ViewResult Add()
        {
            IEnumerable<InfoSystem> systems = this._systemPresenter.GetInfoSystems();
            IDictionary<int, string> applicationTypeDescriptions = typeof(ApplicationType).GetEnumDictionary();

            base.ViewBag.InfoSystems = systems;
            base.ViewBag.ApplicationTypeDescriptions = applicationTypeDescriptions;

            return base.View();
        }
        #endregion

        #region # 加载修改菜单视图 —— ViewResult Update(Guid id)
        /// <summary>
        /// 加载修改菜单视图
        /// </summary>
        /// <param name="id">菜单Id</param>
        /// <returns>修改菜单视图</returns>
        [HttpGet]
        [RequireAuthorization("修改菜单视图")]
        public ViewResult Update(Guid id)
        {
            Menu currentMenu = this._menuPresenter.GetMenu(id);
            Menu parentMenu = currentMenu.ParentMenuId == null ? null : this._menuPresenter.GetMenu(currentMenu.ParentMenuId.Value);

            base.ViewBag.ParentName = parentMenu == null ? string.Empty : parentMenu.Name;

            return base.View(currentMenu);
        }
        #endregion

        #region # 加载关联权限视图 —— ViewResult RelateAuthority(Guid id)
        /// <summary>
        /// 加载关联权限视图
        /// </summary>
        /// <param name="id">菜单Id</param>
        /// <returns>关联权限视图</returns>
        [HttpGet]
        [RequireAuthorization("关联权限视图")]
        public ViewResult RelateAuthority(Guid id)
        {
            Menu currentMenu = this._menuPresenter.GetMenu(id);

            return base.View(currentMenu);
        }
        #endregion


        //命令部分

        #region # 创建菜单 —— void CreateMenu(string systemNo, ApplicationType applicationType...
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="menuName">菜单名称</param>
        /// <param name="sort">排序</param>
        /// <param name="url">链接地址</param>
        /// <param name="path">路径</param>
        /// <param name="icon">图标</param>
        /// <param name="parentId">父级菜单Id</param>
        [HttpPost]
        [RequireAuthorization("创建菜单")]
        public void CreateMenu(string systemNo, ApplicationType applicationType, string menuName, int sort, string url, string path, string icon, Guid? parentId)
        {
            this._authorizationContract.CreateMenu(systemNo, applicationType, menuName, sort, url, path, icon, parentId);
        }
        #endregion

        #region # 修改菜单 —— void UpdateMenu(Guid menuId, string menuName...
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <param name="menuName">菜单名称</param>
        /// <param name="sort">排序</param>
        /// <param name="url">链接地址</param>
        /// <param name="path">路径</param>
        /// <param name="icon">图标</param>
        [HttpPost]
        [RequireAuthorization("修改菜单")]
        public void UpdateMenu(Guid menuId, string menuName, int sort, string url, string path, string icon)
        {
            this._authorizationContract.UpdateMenu(menuId, menuName, sort, url, path, icon);
        }
        #endregion

        #region # 删除菜单 —— void RemoveMenu(Guid id)
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id">菜单Id</param>
        [HttpPost]
        [RequireAuthorization("删除菜单")]
        public void RemoveMenu(Guid id)
        {
            this._authorizationContract.RemoveMenu(id);
        }
        #endregion

        #region # 批量删除菜单 —— void RemoveMenus(IEnumerable<Guid> menuIds)
        /// <summary>
        /// 批量删除菜单
        /// </summary>
        /// <param name="menuIds">菜单Id集</param>
        [HttpPost]
        [RequireAuthorization("批量删除菜单")]
        public void RemoveMenus(IEnumerable<Guid> menuIds)
        {
            menuIds = menuIds ?? new Guid[0];

            foreach (Guid menuId in menuIds)
            {
                this._authorizationContract.RemoveMenu(menuId);
            }
        }
        #endregion

        #region # 关联权限 —— void RelateAuthorities(Guid menuId...
        /// <summary>
        /// 关联权限
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <param name="authorityIds">权限Id集</param>
        [HttpPost]
        [RequireAuthorization("关联权限")]
        public void RelateAuthorities(Guid menuId, IEnumerable<Guid> authorityIds)
        {
            authorityIds = authorityIds ?? new Guid[0];

            this._authorizationContract.RelateAuthoritiesToMenu(menuId, authorityIds);
        }
        #endregion


        //查询部分

        #region # 获取菜单树 —— JsonResult GetMenuTree(string systemNo...
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单树</returns>
        [RequireAuthorization("获取菜单树")]
        public JsonResult GetMenuTree(string systemNo, ApplicationType? applicationType)
        {
            IEnumerable<Node> menuTree = this._menuPresenter.GetMenuTree(systemNo, applicationType);

            return base.Json(menuTree, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region # 获取用户菜单树 —— JsonResult GetUserMenuTree(string loginId, string systemNo...
        /// <summary>
        /// 获取用户菜单树
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单树</returns>
        [RequireAuthorization("获取用户菜单树")]
        public JsonResult GetUserMenuTree(string loginId, string systemNo, ApplicationType? applicationType)
        {
            IEnumerable<Node> menuTree = loginId == CommonConstants.AdminLoginId
                ? this._menuPresenter.GetMenuTree(systemNo, applicationType)
                : this._menuPresenter.GetUserMenuTree(loginId, systemNo, applicationType);

            return base.Json(menuTree, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region # 获取菜单TreeGrid —— JsonResult GetMenuTreeGrid(string systemNo...
        /// <summary>
        /// 获取菜单TreeGrid
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单TreeGrid</returns>
        [RequireAuthorization("获取菜单树形表格")]
        public JsonResult GetMenuTreeGrid(string systemNo, ApplicationType? applicationType)
        {
            IEnumerable<Menu> menus = this._menuPresenter.GetMenuTreeGrid(systemNo, applicationType).ToArray();
            Grid<Menu> grid = new Grid<Menu>(menus.Count(), menus);

            return base.Json(grid, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
