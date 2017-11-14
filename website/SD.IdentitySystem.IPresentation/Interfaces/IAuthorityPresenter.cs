using SD.FormatModel.EasyUI;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.PresentationBase;
using System;
using System.Collections.Generic;

namespace SD.IdentitySystem.IPresentation.Interfaces
{
    /// <summary>
    /// 权限呈现器接口
    /// </summary>
    public interface IAuthorityPresenter : IPresenter
    {
        #region # 分页获取权限列表 —— PageModel<AuthorityView> GetAuthoritiesByPage(string systemNo...
        /// <summary>
        /// 分页获取权限列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>权限列表</returns>
        PageModel<AuthorityView> GetAuthoritiesByPage(string systemNo, string keywords, int pageIndex, int pageSize);
        #endregion

        #region # 根据信息系统获取权限列表 —— IEnumerable<AuthorityView> GetAuthoritiesBySystem(...
        /// <summary>
        /// 根据信息系统获取权限列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>权限列表</returns>
        IEnumerable<AuthorityView> GetAuthoritiesBySystem(string systemNo);
        #endregion

        #region # 根据菜单获取权限列表 —— IEnumerable<AuthorityView> GetAuthoritiesByMenu(...
        /// <summary>
        /// 根据菜单获取权限列表
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns>权限列表</returns>
        IEnumerable<AuthorityView> GetAuthoritiesByMenu(Guid menuId);
        #endregion

        #region # 根据角色获取权限列表 —— IEnumerable<AuthorityView> GetAuthoritiesByRole(...
        /// <summary>
        /// 根据角色获取权限列表
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>权限列表</returns>
        IEnumerable<AuthorityView> GetAuthoritiesByRole(Guid roleId);
        #endregion

        #region # 获取权限 —— AuthorityView GetAuthority(Guid authorityId)
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="authorityId">权限Id</param>
        /// <returns>权限视图模型</returns>
        AuthorityView GetAuthority(Guid authorityId);
        #endregion

        #region # 获取信息系统的权限树 —— Node GetAuthorityTree(string systemNo)
        /// <summary>
        /// 获取信息系统的权限树
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>权限树</returns>
        Node GetAuthorityTree(string systemNo);
        #endregion

        #region # 获取角色的权限树 —— Node GetAuthorityTreeByRole(Guid roleId)
        /// <summary>
        /// 获取角色的权限树
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>权限树</returns>
        Node GetAuthorityTreeByRole(Guid roleId);
        #endregion

        #region # 获取菜单的权限树 —— Node GetAuthorityTreeByMenu(Guid menuId)
        /// <summary>
        /// 获取菜单的权限树
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns>权限树</returns>
        Node GetAuthorityTreeByMenu(Guid menuId);
        #endregion
    }
}
