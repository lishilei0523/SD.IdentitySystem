using System;
using System.Collections.Generic;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using ShSoft.Infrastructure;
using ShSoft.Infrastructure.DTOBase;

namespace SD.IdentitySystem.IPresentation.Interfaces
{
    /// <summary>
    /// 权限呈现器接口
    /// </summary>
    public interface IAuthorityPresenter : IPresenter
    {
        #region # 分页获取权限列表 —— PageModel<AuthorityView> GetAuthoritiesByPage(string systemKindNo...
        /// <summary>
        /// 分页获取权限列表
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>权限列表</returns>
        PageModel<AuthorityView> GetAuthoritiesByPage(string systemKindNo, string keywords, int pageIndex, int pageSize);
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

        #region # 是否存在权限 —— bool ExistsAuthority(string assemblyName, string @namespace...
        /// <summary>
        /// 是否存在权限
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="namespace">命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        /// <returns>是否存在</returns>
        bool ExistsAuthority(string assemblyName, string @namespace, string className, string methodName);
        #endregion
    }
}
