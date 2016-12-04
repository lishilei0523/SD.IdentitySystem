using System.Collections.Generic;
using SD.IdentitySystem.Domain.Entities;
using ShSoft.Infrastructure.RepositoryBase;

namespace SD.IdentitySystem.Domain.IRepositories.Interfaces
{
    /// <summary>
    /// 用户角色仓储接口
    /// </summary>
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        #region # 获取角色列表 —— IEnumerable<Role> GetRoles(string loginId, string systemNo)
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>角色列表</returns>
        IEnumerable<Role> GetRoles(string loginId, string systemNo);
        #endregion

        #region # 获取权限列表 —— IEnumerable<Authority> GetAuthorities(string loginId, string systemNo)
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>权限列表</returns>
        IEnumerable<Authority> GetAuthorities(string loginId, string systemNo);
        #endregion

        #region # 获取菜单列表（递归） —— IEnumerable<Menu> GetMenus(string loginId, string systemNo)
        /// <summary>
        /// 获取菜单列表（递归）
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>菜单列表</returns>
        IEnumerable<Menu> GetMenus(string loginId, string systemNo);
        #endregion

        #region # 获取信息系统编号列表 —— IEnumerable<string> GetInfoSystemNos(string loginId)
        /// <summary>
        /// 获取信息系统编号列表
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <returns>信息系统编号列表</returns>
        IEnumerable<string> GetInfoSystemNos(string loginId);
        #endregion
    }
}
