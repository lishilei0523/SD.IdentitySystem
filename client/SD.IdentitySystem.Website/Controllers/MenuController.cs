using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.ViewModels.Formats.EasyUI;
using ShSoft.Infrastructure.MVC;
using ShSoft.Infrastructure.MVC.Filters;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using SD.IdentitySystem.Presentation.Maps;
using ShSoft.Infrastructure.DTOBase;

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
        /// 依赖注入构造器
        /// </summary>
        /// <param name="menuPresenter">菜单呈现器</param>
        /// <param name="systemPresenter">信息系统呈现器</param>
        public MenuController(IMenuPresenter menuPresenter, IInfoSystemPresenter systemPresenter)
        {
            this._menuPresenter = menuPresenter;
            this._systemPresenter = systemPresenter;
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


        //命令部分


        //查询部分

        #region # 获取菜单树 —— JsonResult GetMenuTree()
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns>菜单树</returns>
        public JsonResult GetMenuTree()
        {
            IEnumerable<Node> menuTree = this._menuPresenter.GetMenuTree("00");

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
