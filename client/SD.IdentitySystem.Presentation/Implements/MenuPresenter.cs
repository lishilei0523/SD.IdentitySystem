using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.ViewModels.Formats.EasyUI;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using SD.IdentitySystem.Presentation.Maps;
using SD.Infrastructure.DTOBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Presentation.Implements
{
    /// <summary>
    /// 菜单呈现器实现
    /// </summary>
    public class MenuPresenter : IMenuPresenter
    {
        #region # 字段及构造器

        /// <summary>
        /// 用户服务接口
        /// </summary>
        private readonly IUserContract _userContract;

        /// <summary>
        /// 权限服务接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="userContract">用户服务接口</param>
        /// <param name="authorizationContract">权限服务接口</param>
        public MenuPresenter(IUserContract userContract, IAuthorizationContract authorizationContract)
        {
            this._userContract = userContract;
            this._authorizationContract = authorizationContract;
        }

        #endregion

        #region # 分页获取菜单列表 —— PageModel<MenuView> GetMenusByPage(string keywords...
        /// <summary>
        /// 分页获取菜单列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>菜单列表</returns>
        public PageModel<MenuView> GetMenusByPage(string keywords, string systemNo, int pageIndex, int pageSize)
        {
            PageModel<MenuInfo> pageModel = this._authorizationContract.GetMenusByPage(keywords, systemNo, pageIndex, pageSize);

            IEnumerable<MenuView> menuViews = pageModel.Datas.Select(x => x.ToViewModel());

            return new PageModel<MenuView>(menuViews, pageIndex, pageSize, pageModel.PageCount, pageModel.RowCount);
        }
        #endregion

        #region # 获取菜单列表 —— IEnumerable<MenuView> GetMenus(string systemNo)
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>菜单列表</returns>
        public IEnumerable<MenuView> GetMenus(string systemNo)
        {
            IEnumerable<MenuInfo> menuInfos = this._authorizationContract.GetMenus(systemNo);
            IEnumerable<MenuView> menuViews = menuInfos.OrderBy(x => x.Sort).Select(x => x.ToViewModel());

            return menuViews;
        }
        #endregion

        #region # 获取用户菜单树 —— IEnumerable<Node> GetMenuTree(string loginId, string systemNo)
        /// <summary>
        /// 获取用户菜单树
        /// </summary>
        /// <param name="loginId">用户登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>菜单树</returns>
        public IEnumerable<Node> GetMenuTree(string loginId, string systemNo)
        {
            IEnumerable<MenuInfo> menuInfos = this._userContract.GetMenus(loginId, systemNo);
            IEnumerable<MenuView> menuViews = menuInfos.Select(x => x.ToViewModel());

            ICollection<Node> menuTree = menuViews.ToTree(null);

            return menuTree;
        }
        #endregion

        #region # 获取菜单树 —— IEnumerable<Node> GetMenuTree(string systemNo)
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>菜单树</returns>
        public IEnumerable<Node> GetMenuTree(string systemNo)
        {
            IEnumerable<MenuView> menuViews = this.GetMenus(systemNo);

            ICollection<Node> menuTree = menuViews.ToTree(null);

            return menuTree;
        }
        #endregion

        #region # 获取菜单 —— MenuView GetMenu(Guid menuId)
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns>菜单</returns>
        public MenuView GetMenu(Guid menuId)
        {
            MenuInfo menuInfo = this._authorizationContract.GetMenu(menuId);

            return menuInfo.ToViewModel();
        }
        #endregion

        #region # 获取菜单TreeGrid —— IEnumerable<MenuView> GetMenuTreeGrid(string systemNo)
        /// <summary>
        /// 获取菜单TreeGrid
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>菜单TreeGrid</returns>
        public IEnumerable<MenuView> GetMenuTreeGrid(string systemNo)
        {
            IEnumerable<MenuView> menus = this.GetMenus(systemNo);

            return menus.ToTreeGrid();
        }
        #endregion

    }
}
