using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.Presentation.Maps;
using SD.IdentitySystem.Presentation.Models;
using SD.Infrastructure.Constants;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Presentation.Presentors
{
    /// <summary>
    /// 菜单呈现器
    /// </summary>
    public class MenuPresenter
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

        #region # 获取菜单 —— Menu GetMenu(Guid menuId)
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns>菜单</returns>
        public Menu GetMenu(Guid menuId)
        {
            MenuInfo menuInfo = this._authorizationContract.GetMenu(menuId);
            Menu menu = menuInfo.ToModel();

            return menu;
        }
        #endregion

        #region # 获取菜单列表 —— IEnumerable<Menu> GetMenus(string systemNo...
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单列表</returns>
        public IEnumerable<Menu> GetMenus(string systemNo, ApplicationType? applicationType)
        {
            IEnumerable<MenuInfo> menuInfos = this._authorizationContract.GetMenus(systemNo, applicationType);
            IEnumerable<Menu> menus = menuInfos.OrderBy(x => x.Sort).Select(x => x.ToModel());

            return menus;
        }
        #endregion

        #region # 获取菜单树 —— IEnumerable<Node> GetMenuTree(string systemNo...
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单树</returns>
        public IEnumerable<Node> GetMenuTree(string systemNo, ApplicationType? applicationType)
        {
            IEnumerable<Menu> menus = this.GetMenus(systemNo, applicationType);
            ICollection<Node> menuTree = menus.ToTree(null);

            return menuTree;
        }
        #endregion

        #region # 获取用户菜单树 —— IEnumerable<Node> GetUserMenuTree(string loginId, string systemNo...
        /// <summary>
        /// 获取用户菜单树
        /// </summary>
        /// <param name="loginId">用户登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单树</returns>
        public IEnumerable<Node> GetUserMenuTree(string loginId, string systemNo, ApplicationType? applicationType)
        {
            IEnumerable<MenuInfo> menuInfos = this._userContract.GetUserMenus(loginId, systemNo, applicationType);
            IEnumerable<Menu> menus = menuInfos.Select(x => x.ToModel());
            ICollection<Node> menuTree = menus.ToTree(null);

            return menuTree;
        }
        #endregion

        #region # 获取菜单树列表 —— IEnumerable<Menu> GetMenuTreeList(string systemNo...
        /// <summary>
        /// 获取菜单树列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单TreeGrid</returns>
        public IEnumerable<Menu> GetMenuTreeList(string systemNo, ApplicationType? applicationType)
        {
            IEnumerable<Menu> menus = this.GetMenus(systemNo, applicationType);
            IEnumerable<Menu> menuTreeList = menus.ToTreeList();

            return menuTreeList;
        }
        #endregion

        #region # 分页获取菜单列表 —— PageModel<Menu> GetMenusByPage(string keywords...
        /// <summary>
        /// 分页获取菜单列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>菜单列表</returns>
        public PageModel<Menu> GetMenusByPage(string keywords, string systemNo, ApplicationType? applicationType, int pageIndex, int pageSize)
        {
            PageModel<MenuInfo> pageModel = this._authorizationContract.GetMenusByPage(keywords, systemNo, applicationType, pageIndex, pageSize);
            IEnumerable<Menu> menus = pageModel.Datas.Select(x => x.ToModel());

            return new PageModel<Menu>(menus, pageIndex, pageSize, pageModel.PageCount, pageModel.RowCount);
        }
        #endregion
    }
}
