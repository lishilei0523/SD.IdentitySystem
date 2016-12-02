using SD.IdentitySystem.IPresentation.ViewModels.Formats.EasyUI;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using ShSoft.Infrastructure;
using System;
using System.Collections.Generic;

namespace SD.IdentitySystem.IPresentation.Interfaces
{
    /// <summary>
    /// 菜单呈现器接口
    /// </summary>
    public interface IMenuPresenter : IPresenter
    {
        #region # 获取菜单列表 —— IEnumerable<MenuView> GetMenus(string systemNo)
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>菜单列表</returns>
        IEnumerable<MenuView> GetMenus(string systemNo);
        #endregion

        #region # 获取菜单树 —— IEnumerable<Node> GetMenuTree(string systemNo)
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="systemNo">信息系统类别编号</param>
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
    }
}
