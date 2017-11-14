using SD.FormatModel.EasyUI;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.PresentationBase;
using System;
using System.Collections.Generic;

namespace SD.IdentitySystem.IPresentation.Interfaces
{
    /// <summary>
    /// 菜单呈现器接口
    /// </summary>
    public interface IMenuPresenter : IPresenter
    {
        #region # 分页获取菜单列表 —— PageModel<MenuView> GetMenusByPage(string keywords...
        /// <summary>
        /// 分页获取菜单列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>菜单列表</returns>
        PageModel<MenuView> GetMenusByPage(string keywords, string systemNo, int pageIndex, int pageSize);
        #endregion

        #region # 获取菜单列表 —— IEnumerable<MenuView> GetMenus(string systemNo)
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>菜单列表</returns>
        IEnumerable<MenuView> GetMenus(string systemNo);
        #endregion

        #region # 获取用户菜单树 —— IEnumerable<Node> GetMenuTree(string loginId, string systemNo)
        /// <summary>
        /// 获取用户菜单树
        /// </summary>
        /// <param name="loginId">用户登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>菜单树</returns>
        IEnumerable<Node> GetMenuTree(string loginId, string systemNo);
        #endregion

        #region # 获取菜单树 —— IEnumerable<Node> GetMenuTree(string systemNo)
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>菜单树</returns>
        IEnumerable<Node> GetMenuTree(string systemNo);
        #endregion

        #region # 获取菜单 —— MenuView GetMenu(Guid menuId)
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns>菜单</returns>
        MenuView GetMenu(Guid menuId);
        #endregion

        #region # 获取菜单TreeGrid —— IEnumerable<MenuView> GetMenuTreeGrid(string systemNo)
        /// <summary>
        /// 获取菜单TreeGrid
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>菜单TreeGrid</returns>
        IEnumerable<MenuView> GetMenuTreeGrid(string systemNo);
        #endregion
    }
}
