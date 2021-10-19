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
    /// 角色仓储实现
    /// </summary>
    public class RoleRepository : EFAggRootRepositoryProvider<Role>, IRoleRepository
    {
        #region # 分页获取角色列表 —— ICollection<Role> FindByPage(string keywords...
        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录条数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>角色列表</returns>
        public ICollection<Role> FindByPage(string keywords, string systemNo, int pageIndex, int pageSize, out int rowCount, out int pageCount)
        {
            QueryBuilder<Role> queryBuilder = QueryBuilder<Role>.Affirm();
            if (!string.IsNullOrWhiteSpace(keywords))
            {
                queryBuilder.And(x => x.Keywords.Contains(keywords));
            }
            if (!string.IsNullOrWhiteSpace(systemNo))
            {
                queryBuilder.And(x => x.SystemNo == systemNo);
            }

            Expression<Func<Role, bool>> condition = queryBuilder.Build();
            IQueryable<Role> roles = base.FindByPage(condition, pageIndex, pageSize, out rowCount, out pageCount);

            return roles.ToList();
        }
        #endregion

        #region # 获取角色列表 —— ICollection<Role> Find(string keywords, string loginId...
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="loginId">用户名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>角色列表</returns>
        public ICollection<Role> Find(string keywords, string loginId, string systemNo)
        {
            QueryBuilder<Role> queryBuilder = QueryBuilder<Role>.Affirm();
            if (!string.IsNullOrWhiteSpace(keywords))
            {
                queryBuilder.And(x => x.Keywords.Contains(keywords));
            }
            if (!string.IsNullOrWhiteSpace(loginId))
            {
                queryBuilder.And(x => x.Users.Any(y => y.Number == loginId));
            }
            if (!string.IsNullOrWhiteSpace(systemNo))
            {
                queryBuilder.And(x => x.SystemNo == systemNo);
            }

            Expression<Func<Role, bool>> condition = queryBuilder.Build();
            IQueryable<Role> roles = this.Find(condition);

            return roles.ToList();
        }
        #endregion

        #region # 获取系统管理员角色 —— Role GetManagerRole(string systemNo)
        /// <summary>
        /// 获取系统管理员角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>系统管理员角色</returns>
        public Role GetManagerRole(string systemNo)
        {
            Expression<Func<Role, bool>> condition =
                x =>
                    x.SystemNo == systemNo &&
                    x.Number == CommonConstants.ManagerRoleNo;

            Role managerRole = base.SingleOrDefault(condition);

            #region # 验证

            if (managerRole == null)
            {
                throw new ApplicationException($"未为编号为\"{systemNo}\"的信息系统初始化系统管理员角色！");
            }

            #endregion

            return managerRole;
        }
        #endregion

        #region # 获取角色Id列表 —— ICollection<Guid> FindIds(string loginId, string systemNo)
        /// <summary>
        /// 获取角色Id列表
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>角色Id列表</returns>
        public ICollection<Guid> FindIds(string loginId, string systemNo)
        {
            QueryBuilder<Role> queryBuilder = QueryBuilder<Role>.Affirm();
            if (!string.IsNullOrWhiteSpace(loginId))
            {
                queryBuilder.And(x => x.Users.Any(y => y.Number == loginId));
            }
            if (!string.IsNullOrWhiteSpace(systemNo))
            {
                queryBuilder.And(x => x.SystemNo == systemNo);
            }

            Expression<Func<Role, bool>> condition = queryBuilder.Build();
            IQueryable<Guid> roleIds = this.FindIds(condition);

            return roleIds.ToArray();
        }
        #endregion

        #region # 获取系统管理员角色Id —— Guid GetManagerRoleId(string systemNo)
        /// <summary>
        /// 获取系统管理员角色Id
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>系统管理员角色Id</returns>
        public Guid GetManagerRoleId(string systemNo)
        {
            IQueryable<Role> roles = base.Find(x => x.SystemNo == systemNo);

            #region # 验证

            if (roles.All(x => x.Number != CommonConstants.ManagerRoleNo))
            {
                throw new ApplicationException($"未为编号为\"{systemNo}\"的信息系统初始化系统管理员角色！");
            }

            #endregion

            roles = roles.Where(x => x.Number == CommonConstants.ManagerRoleNo);
            IQueryable<Guid> roleIds = roles.Select(x => x.Id);
            Guid systemAdminRoleId = roleIds.Single();

            return systemAdminRoleId;
        }
        #endregion

        #region # 是否存在角色 —— bool Exists(string systemNo, string roleName)
        /// <summary>
        /// 是否存在角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleName">角色名称</param>
        /// <returns>是否存在</returns>
        public bool Exists(string systemNo, string roleName)
        {
            Expression<Func<Role, bool>> condition =
                x =>
                    x.SystemNo == systemNo &&
                    x.Name == roleName;

            return base.Exists(condition);
        }
        #endregion
    }
}
