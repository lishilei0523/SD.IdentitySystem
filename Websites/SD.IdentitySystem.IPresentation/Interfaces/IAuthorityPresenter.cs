using SD.IdentitySystem.IPresentation.Models;
using SD.Infrastructure.Constants;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.PresentationBase;
using SD.Toolkits.EasyUI;
using System;

namespace SD.IdentitySystem.IPresentation.Interfaces
{
    /// <summary>
    /// 权限呈现器接口
    /// </summary>
    public interface IAuthorityPresenter : IPresenter
    {
        #region # 获取权限 —— Authority GetAuthority(Guid authorityId)
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="authorityId">权限Id</param>
        /// <returns>权限模型</returns>
        Authority GetAuthority(Guid authorityId);
        #endregion

        #region # 获取信息系统的权限树 —— Node GetAuthorityTree(string systemNo...
        /// <summary>
        /// 获取信息系统的权限树
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>权限树</returns>
        Node GetAuthorityTree(string systemNo, ApplicationType? applicationType);
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

        #region # 分页获取权限列表 —— PageModel<Authority> GetAuthoritiesByPage(string keywords...
        /// <summary>
        /// 分页获取权限列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>权限列表</returns>
        PageModel<Authority> GetAuthoritiesByPage(string keywords, string systemNo, ApplicationType? applicationType, int pageIndex, int pageSize);
        #endregion
    }
}
