using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.Presentation.EasyUI;
using SD.IdentitySystem.Presentation.Maps;
using SD.IdentitySystem.Presentation.Models;
using SD.Infrastructure.Constants;
using SD.Infrastructure.PresentationBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Presentation.Presenters
{
    /// <summary>
    /// 菜单呈现器
    /// </summary>
    public class MenuPresenter : IPresenter
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限管理服务契约接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 用户管理服务契约接口
        /// </summary>
        private readonly IUserContract _userContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public MenuPresenter(IAuthorizationContract authorizationContract, IUserContract userContract)
        {
            this._authorizationContract = authorizationContract;
            this._userContract = userContract;
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

            return menuInfo.ToModel();
        }
        #endregion

        #region # 获取菜单树 —— IEnumerable<Node> GetMenuTree(string infoSystemNo...
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单树</returns>
        public IEnumerable<Node> GetMenuTree(string infoSystemNo, ApplicationType? applicationType)
        {
            IEnumerable<Menu> menus = this.GetMenus(infoSystemNo, applicationType);

            ICollection<Node> menuTree = menus.ToTree(null);

            return menuTree;
        }
        #endregion

        #region # 获取用户菜单树 —— IEnumerable<Node> GetUserMenuTree(string loginId, string infoSystemNo...
        /// <summary>
        /// 获取用户菜单树
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单树</returns>
        public IEnumerable<Node> GetUserMenuTree(string loginId, string infoSystemNo, ApplicationType? applicationType)
        {
            IEnumerable<MenuInfo> menuInfos = this._userContract.GetUserMenus(loginId, infoSystemNo, applicationType);
            IEnumerable<Menu> menus = menuInfos.Select(x => x.ToModel());

            ICollection<Node> menuTree = menus.ToTree(null);

            return menuTree;
        }
        #endregion

        #region # 获取菜单TreeGrid —— IEnumerable<Menu> GetMenuTreeGrid(string infoSystemNo...
        /// <summary>
        /// 获取菜单TreeGrid
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单TreeGrid</returns>
        public IEnumerable<Menu> GetMenuTreeGrid(string infoSystemNo, ApplicationType? applicationType)
        {
            IEnumerable<Menu> menus = this.GetMenus(infoSystemNo, applicationType);

            return menus.ToTreeGrid();
        }
        #endregion


        //Private

        #region # 获取菜单列表 —— IEnumerable<Menu> GetMenus(string infoSystemNo...
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单列表</returns>
        private IEnumerable<Menu> GetMenus(string infoSystemNo, ApplicationType? applicationType)
        {
            IEnumerable<MenuInfo> menuInfos = this._authorizationContract.GetMenus(null, infoSystemNo, applicationType);
            IEnumerable<Menu> menus = menuInfos.OrderBy(x => x.Sort).Select(x => x.ToModel());

            return menus;
        }
        #endregion
    }
}
