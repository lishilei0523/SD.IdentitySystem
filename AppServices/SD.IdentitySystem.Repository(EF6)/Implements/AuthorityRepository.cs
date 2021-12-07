using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Repository.EntityFramework;
using SD.Toolkits.EntityFramework.Extensions;
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
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>权限列表</returns>
        public ICollection<Authority> FindByPage(string keywords, string infoSystemNo, ApplicationType? applicationType, int pageIndex, int pageSize, out int rowCount, out int pageCount)
        {
            QueryBuilder<Authority> queryBuilder = QueryBuilder<Authority>.Affirm();
            if (!string.IsNullOrWhiteSpace(keywords))
            {
                queryBuilder.And(x => x.Keywords.Contains(keywords));
            }
            if (!string.IsNullOrWhiteSpace(infoSystemNo))
            {
                queryBuilder.And(x => x.InfoSystemNo == infoSystemNo);
            }
            if (applicationType.HasValue)
            {
                queryBuilder.And(x => x.ApplicationType == applicationType.Value);
            }

            Expression<Func<Authority, bool>> condition = queryBuilder.Build();
            IQueryable<Authority> authorities = base.FindByPage(condition, pageIndex, pageSize, out rowCount, out pageCount);

            return authorities.ToList();
        }
        #endregion

        #region # 获取权限列表 —— ICollection<Authority> Find(string keywords, string infoSystemNo...
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="menuId">菜单Id</param>
        /// <param name="roleId">角色Id</param>
        /// <returns>权限列表</returns>
        public ICollection<Authority> Find(string keywords, string infoSystemNo, ApplicationType? applicationType, Guid? menuId, Guid? roleId)
        {
            QueryBuilder<Authority> queryBuilder = QueryBuilder<Authority>.Affirm();
            if (!string.IsNullOrWhiteSpace(keywords))
            {
                queryBuilder.And(x => x.Keywords.Contains(keywords));
            }
            if (!string.IsNullOrWhiteSpace(infoSystemNo))
            {
                queryBuilder.And(x => x.InfoSystemNo == infoSystemNo);
            }
            if (applicationType.HasValue)
            {
                queryBuilder.And(x => x.ApplicationType == applicationType.Value);
            }
            if (menuId.HasValue)
            {
                queryBuilder.And(x => x.MenuLeaves.Any(y => y.Id == menuId.Value));
            }
            if (roleId.HasValue)
            {
                queryBuilder.And(x => x.Roles.Any(y => y.Id == roleId.Value));
            }

            Expression<Func<Authority, bool>> condition = queryBuilder.Build();
            IQueryable<Authority> authorities = base.Find(condition);

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
            #region # 验证

            Guid[] roleIds_ = roleIds?.Distinct().ToArray() ?? Array.Empty<Guid>();
            if (!roleIds_.Any())
            {
                return new List<Authority>();
            }

            #endregion

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
            #region # 验证

            Guid[] roleIds_ = roleIds?.Distinct().ToArray() ?? Array.Empty<Guid>();
            if (!roleIds_.Any())
            {
                return new List<Guid>();
            }

            #endregion

            Expression<Func<Authority, bool>> condition =
                x =>
                    x.Roles.Any(y => roleIds_.Contains(y.Id));

            IQueryable<Guid> authorityIds = base.FindIds(condition);

            return authorityIds.ToList();
        }
        #endregion

        #region # 是否存在给定权限 —— bool ExistsPath(string authorityPath)
        /// <summary>
        /// 是否存在给定权限
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="authorityPath">权限路径</param>
        /// <returns>是否存在</returns>
        public bool ExistsPath(string infoSystemNo, ApplicationType applicationType, string authorityPath)
        {
            Expression<Func<Authority, bool>> condition =
                x =>
                    x.InfoSystemNo == infoSystemNo &&
                    x.ApplicationType == applicationType &&
                    x.AuthorityPath == authorityPath;

            return base.Exists(condition);
        }
        #endregion
    }
}
