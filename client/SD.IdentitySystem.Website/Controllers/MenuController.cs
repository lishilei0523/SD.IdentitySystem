using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.ViewModels.Formats.EasyUI;
using ShSoft.Infrastructure.MVC;
using ShSoft.Infrastructure.MVC.Filters;
using System.Collections.Generic;
using System.Web.Mvc;

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
        /// 依赖注入构造器
        /// </summary>
        /// <param name="menuPresenter">菜单呈现器</param>
        public MenuController(IMenuPresenter menuPresenter)
        {
            this._menuPresenter = menuPresenter;
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
    }
}
