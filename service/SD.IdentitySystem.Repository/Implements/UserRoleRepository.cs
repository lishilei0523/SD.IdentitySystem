using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using SD.Toolkits.Recursion.Tree;
using ShSoft.Infrastructure.Repository.EntityFramework;

namespace SD.IdentitySystem.Repository.Implements
{
    /// <summary>
    /// 用户角色仓储实现
    /// </summary>
    public class UserRoleRepository : EFRepositoryProvider<UserRole>, IUserRoleRepository
    {
        #region # 获取用户角色列表 —— override IQueryable<UserRole> FindAllInner()
        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <returns>用户角色列表</returns>
        protected override IQueryable<UserRole> FindAllInner()
        {
            Expression<Func<UserRole, bool>> condition =
                x =>
                    x.User != null &&
                    x.Role != null &&
                    x.SystemNo != null &&
                    !x.User.Deleted &&
                    !x.Role.Deleted;

            return base.FindAllInner().Where(condition);
        }
        #endregion

        #region # 获取角色列表 —— IEnumerable<Role> GetRoles(string loginId, string systemNo)
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>角色列表</returns>
        public IEnumerable<Role> GetRoles(string loginId, string systemNo)
        {
            return this.Find(x => x.SystemNo == systemNo && x.User.Number == loginId).Select(x => x.Role).AsEnumerable();
        }
        #endregion

        #region # 获取权限列表 —— IEnumerable<Authority> GetAuthorities(string loginId, string systemNo)
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>权限列表</returns>
        public IEnumerable<Authority> GetAuthorities(string loginId, string systemNo)
        {
            IQueryable<Authority> authorities = this.Find(x => x.SystemNo == systemNo && x.User.Number == loginId)
                 .Select(x => x.Role)
                 .SelectMany(x => x.Authorities)
                 .Where(x => x != null && !x.Deleted)
                 .Distinct();

            return authorities.AsEnumerable();
        }
        #endregion

        #region # 获取菜单列表（递归） —— IEnumerable<Menu> GetMenus(string loginId, string systemNo)
        /// <summary>
        /// 获取菜单列表（递归）
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>菜单列表</returns>
        public IEnumerable<Menu> GetMenus(string loginId, string systemNo)
        {
            IQueryable<Menu> menuLeaves = this.Find(x => x.SystemNo == systemNo && x.User.Number == loginId)
                .Select(x => x.Role)
                .SelectMany(x => x.Authorities)
                .Where(x => x != null && !x.Deleted)
                .SelectMany(x => x.MenuLeaves)
                .Where(x => x != null && !x.Deleted)
                .Distinct()
                .OrderBy(x => x.Sort);

            return menuLeaves.TailRecurseParentNodes().OrderBy(x => x.Sort);
        }
        #endregion

        #region # 获取信息系统编号列表 —— IEnumerable<string> GetInfoSystemNos(string loginId)
        /// <summary>
        /// 获取信息系统编号列表
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <returns>信息系统编号列表</returns>
        public IEnumerable<string> GetInfoSystemNos(string loginId)
        {
            return this.Find(x => x.User.Number == loginId).Select(x => x.SystemNo).Distinct().AsEnumerable();
        }
        #endregion
    }
}
