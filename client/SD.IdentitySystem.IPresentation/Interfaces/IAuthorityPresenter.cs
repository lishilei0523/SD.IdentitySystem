using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using ShSoft.Infrastructure.DTOBase;
using ShSoft.Infrastructure.MVC;
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
    }
}
