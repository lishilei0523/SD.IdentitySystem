using SD.IdentitySystem.IPresentation.Models.Outputs;
using SD.Infrastructure.Constants;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.PresentationBase;
using SD.Toolkits.EasyUI;
using System;
using System.Collections.Generic;

namespace SD.IdentitySystem.IPresentation.Interfaces
{
    /// <summary>
    /// 菜单呈现器接口
    /// </summary>
    public interface IMenuPresenter : IPresenter
    {
        #region # 获取菜单 —— Menu GetMenu(Guid menuId)
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns>菜单</returns>
        Menu GetMenu(Guid menuId);
        #endregion

        #region # 获取菜单列表 —— IEnumerable<Menu> GetMenus(string systemNo...
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单列表</returns>
        IEnumerable<Menu> GetMenus(string systemNo, ApplicationType? applicationType);
        #endregion

        #region # 获取菜单树 —— IEnumerable<Node> GetMenuTree(string systemNo...
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单树</returns>
        IEnumerable<Node> GetMenuTree(string systemNo, ApplicationType? applicationType);
        #endregion

        #region # 获取用户菜单树 —— IEnumerable<Node> GetUserMenuTree(string loginId, string systemNo...
        /// <summary>
        /// 获取用户菜单树
        /// </summary>
        /// <param name="loginId">用户登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单树</returns>
        IEnumerable<Node> GetUserMenuTree(string loginId, string systemNo, ApplicationType? applicationType);
        #endregion

        #region # 获取菜单TreeGrid —— IEnumerable<Menu> GetMenuTreeGrid(string systemNo...
        /// <summary>
        /// 获取菜单TreeGrid
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单TreeGrid</returns>
        IEnumerable<Menu> GetMenuTreeGrid(string systemNo, ApplicationType? applicationType);
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
        PageModel<Menu> GetMenusByPage(string keywords, string systemNo, ApplicationType? applicationType, int pageIndex, int pageSize);
        #endregion
    }
}
