using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using SD.Infrastructure.Repository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SD.IdentitySystem.Repository.Implements
{
    /// <summary>
    /// 权限仓储实现
    /// </summary>
    public class AuthorityRepository : EFAggRootRepositoryProvider<Authority>, IAuthorityRepository
    {
        #region # 分页获取权限列表 —— ICollection<Authority> FindByPage(string keywords...
        /// <summary>
        /// 分页获取权限列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录条数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>权限列表</returns>
        public ICollection<Authority> FindByPage(string keywords, string systemNo, int pageIndex, int pageSize, out int rowCount, out int pageCount)
        {
            Expression<Func<Authority, bool>> condition =
                x =>
                    (string.IsNullOrEmpty(keywords) || x.Keywords.Contains(keywords)) &&
                    (string.IsNullOrEmpty(systemNo) || x.SystemNo == systemNo);

            IQueryable<Authority> authorities = base.FindByPage(condition, pageIndex, pageSize, out rowCount, out pageCount);

            return authorities.ToList();
        }
        #endregion

        #region # 获取权限列表 —— ICollection<Authority> Find(string keywords, string systemNo...
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="menuId">菜单Id</param>
        /// <param name="roleId">角色Id</param>
        /// <returns>权限列表</returns>
        public ICollection<Authority> Find(string keywords, string systemNo, Guid? menuId, Guid? roleId)
        {
            IQueryable<Authority> authorities = base.Find(x => string.IsNullOrEmpty(keywords) || x.Keywords.Contains(keywords));

            if (!string.IsNullOrWhiteSpace(systemNo))
            {
                authorities = authorities.Where(x => x.SystemNo == systemNo);
            }
            if (menuId.HasValue)
            {
                authorities = authorities.Where(x => x.MenuLeaves.Any(y => y.Id == menuId));
            }
            if (roleId.HasValue)
            {
                authorities = authorities.Where(x => x.Roles.Any(y => y.Id == roleId));
            }

            return authorities.ToList();
        }
        #endregion

        #region # 根据角色获取权限列表 —— ICollection<Authority> FindByRole(IEnumerable<Guid> roleIds)
        /// <summary>
        /// 根据角色获取权限列表
        /// </summary>
        /// <param name="roleIds">角色Id集</param>
        /// <returns>权限列表</returns>
        public ICollection<Authority> FindByRole(IEnumerable<Guid> roleIds)
        {
            Guid[] roleIds_ = roleIds?.Distinct().ToArray() ?? new Guid[0];
            if (!roleIds_.Any())
            {
                return new List<Authority>();
            }

            Expression<Func<Authority, bool>> condition =
                x =>
                    x.Roles.Any(y => roleIds_.Contains(y.Id));

            IQueryable<Authority> authorities = base.Find(condition);

            return authorities.ToList();
        }
        #endregion

        #region # 根据角色获取权限Id列表 —— ICollection<Guid> FindIdsByRole(IEnumerable<Guid> roleIds)
        /// <summary>
        /// 根据角色获取权限Id列表
        /// </summary>
        /// <param name="roleIds">角色Id集</param>
        /// <returns>权限Id列表</returns>
        public ICollection<Guid> FindIdsByRole(IEnumerable<Guid> roleIds)
        {
            Expression<Func<Authority, bool>> condition =
                x =>
                    x.Roles.Any(y => roleIds.Contains(y.Id));

            IQueryable<Guid> authorityIds = base.FindIds(condition);

            return authorityIds.ToList();
        }
        #endregion

        #region # 是否存在给定权限 —— bool ExistsPath(string authorityPath)
        /// <summary>
        /// 是否存在给定权限
        /// </summary>
        /// <param name="authorityPath">权限路径</param>
        /// <returns>是否存在</returns>
        public bool ExistsPath(string authorityPath)
        {
            return base.Exists(x => x.AuthorityPath == authorityPath);
        }
        #endregion

        #region # 是否存在给定权限 ——  bool ExistsPath(string assemblyName, string @namespace...
        /// <summary>
        /// 是否存在给定权限
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="namespace">命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        /// <returns>是否存在</returns>
        public bool ExistsPath(string assemblyName, string @namespace, string className, string methodName)
        {
            Expression<Func<Authority, bool>> condition =
                x =>
                    x.AssemblyName == assemblyName &&
                    x.Namespace == @namespace &&
                    x.ClassName == className &&
                    x.MethodName == methodName;

            return base.Exists(condition);
        }
        #endregion
    }
}
