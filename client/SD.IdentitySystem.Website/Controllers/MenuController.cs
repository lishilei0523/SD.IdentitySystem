using System;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.ViewModels.Formats.EasyUI;
using ShSoft.Infrastructure.MVC;
using ShSoft.Infrastructure.MVC.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;

namespace SD.IdentitySystem.Website.Controllers
{
    /// <summary>
    /// 菜单控制器
    /// </summary>
    [ExceptionFilter]
    public class MenuController : BaseController
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
        /// <returns></returns>
        public ViewResult Index()
        {
            IEnumerable<InfoSystemView> systems = this._systemPresenter.GetInfoSystems();
            base.ViewBag.InfoSystems = systems;

            return base.View();
        }
        #endregion

        #region # 加载创建菜单视图 —— ViewResult Add()
        /// <summary>
        /// 加载创建菜单视图
        /// </summary>
        /// <returns>创建菜单视图</returns>
        [HttpGet]
        public ViewResult Add()
        {
            IEnumerable<InfoSystemView> systems = this._systemPresenter.GetInfoSystems();
            base.ViewBag.InfoSystems = systems;

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
        public ViewResult Update(Guid id)
        {
            MenuView currentMenu = this._menuPresenter.GetMenu(id);

            return base.View(currentMenu);
        }
        #endregion


        //命令部分

        #region # 创建菜单 —— void CreateMenu(string systemNo, string menuName...
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="menuName">菜单名称</param>
        /// <param name="sort">排序</param>
        /// <param name="url">链接地址</param>
        /// <param name="icon">图标</param>
        /// <param name="parentId">父级菜单Id</param>
        [HttpPost]
        public void CreateMenu(string systemNo, string menuName, int sort, string url, string icon, Guid? parentId)
        {
            this._authorizationContract.CreateMenu(systemNo, menuName, sort, url, icon, parentId);
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
        /// <param name="icon">图标</param>
        [HttpPost]
        public void UpdateMenu(Guid menuId, string menuName, int sort, string url, string icon)
        {
            this._authorizationContract.UpdateMenu(menuId, menuName, sort, url, icon);
        }
        #endregion

        #region # 删除菜单 —— void RemoveMenu(Guid id)
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id">菜单Id</param>
        [HttpPost]
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
        public void RemoveMenus(IEnumerable<Guid> menuIds)
        {
            menuIds = menuIds ?? new Guid[0];

            foreach (Guid menuId in menuIds)
            {
                this._authorizationContract.RemoveMenu(menuId);
            }
        }
        #endregion


        //查询部分

        #region # 获取菜单树 —— JsonResult GetMenuTree(string id)
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="id">信息系统编号</param>
        /// <returns>菜单树</returns>
        public JsonResult GetMenuTree(string id)
        {
            string systemNo = id;

            IEnumerable<Node> menuTree = this._menuPresenter.GetMenuTree(systemNo);

            return base.Json(menuTree, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region # 获取菜单TreeGrid —— PageModel<MenuView> GetMenuTreeGrid(string keywords...
        /// <summary>
        /// 获取菜单TreeGrid
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>菜单TreeGrid</returns>
        public JsonResult GetMenuTreeGrid(string systemNo)
        {
            IEnumerable<MenuView> menus = this._menuPresenter.GetMenuTreeGrid(systemNo).ToArray();

            Grid<MenuView> grid = new Grid<MenuView>(menus.Count(), menus);

            return base.Json(grid, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
