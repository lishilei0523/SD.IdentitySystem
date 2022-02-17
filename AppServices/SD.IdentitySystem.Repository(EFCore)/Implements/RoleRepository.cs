using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Repository.EntityFrameworkCore;
using SD.Toolkits.EntityFrameworkCore.Extensions;
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
        #region # 获取实体对象列表 —— override IQueryable<Role> FindAllInner()
        /// <summary>
        /// 获取实体对象列表
        /// </summary>
        /// <returns>实体对象列表</returns>
        protected override IQueryable<Role> FindAllInner()
        {
            return base._dbContext.Set<Role>();
        }
        #endregion

        #region # 获取角色列表 —— ICollection<Role> Find(string keywords...
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="loginId">用户名</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <returns>角色列表</returns>
        public ICollection<Role> Find(string keywords, string loginId, string infoSystemNo)
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
            if (!string.IsNullOrWhiteSpace(infoSystemNo))
            {
                queryBuilder.And(x => x.InfoSystemNo == infoSystemNo);
            }

            Expression<Func<Role, bool>> condition = queryBuilder.Build();
            IQueryable<Role> roles = this.Find(condition);

            return roles.ToList();
        }
        #endregion

        #region # 分页获取角色列表 —— ICollection<Role> FindByPage(string keywords...
        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="loginId">用户名</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>角色列表</returns>
        public ICollection<Role> FindByPage(string keywords, string loginId, string infoSystemNo, int pageIndex, int pageSize, out int rowCount, out int pageCount)
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
            if (!string.IsNullOrWhiteSpace(infoSystemNo))
            {
                queryBuilder.And(x => x.InfoSystemNo == infoSystemNo);
            }

            Expression<Func<Role, bool>> condition = queryBuilder.Build();
            IQueryable<Role> roles = base.FindByPage(condition, pageIndex, pageSize, out rowCount, out pageCount);

            return roles.ToList();
        }
        #endregion

        #region # 获取角色Id列表 —— ICollection<Guid> FindIds(string loginId, string infoSystemNo)
        /// <summary>
        /// 获取角色Id列表
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <returns>角色Id列表</returns>
        public ICollection<Guid> FindIds(string loginId, string infoSystemNo)
        {
            QueryBuilder<Role> queryBuilder = QueryBuilder<Role>.Affirm();
            if (!string.IsNullOrWhiteSpace(loginId))
            {
                queryBuilder.And(x => x.Users.Any(y => y.Number == loginId));
            }
            if (!string.IsNullOrWhiteSpace(infoSystemNo))
            {
                queryBuilder.And(x => x.InfoSystemNo == infoSystemNo);
            }

            Expression<Func<Role, bool>> condition = queryBuilder.Build();
            IQueryable<Guid> roleIds = this.FindIds(condition);

            return roleIds.ToArray();
        }
        #endregion

        #region # 获取系统管理员角色Id —— Guid GetManagerRoleId(string infoSystemNo)
        /// <summary>
        /// 获取系统管理员角色Id
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <returns>系统管理员角色Id</returns>
        public Guid GetManagerRoleId(string infoSystemNo)
        {
            IQueryable<Role> roles = base.Find(x => x.InfoSystemNo == infoSystemNo);

            #region # 验证

            if (roles.All(x => x.Number != CommonConstants.ManagerRoleNo))
            {
                throw new ApplicationException($"未为编号为\"{infoSystemNo}\"的信息系统初始化系统管理员角色！");
            }

            #endregion

            roles = roles.Where(x => x.Number == CommonConstants.ManagerRoleNo);
            IQueryable<Guid> roleIds = roles.Select(x => x.Id);
            Guid infoSystemAdminRoleId = roleIds.Single();

            return infoSystemAdminRoleId;
        }
        #endregion

        #region # 是否存在角色 —— bool Exists(string infoSystemNo, string roleName)
        /// <summary>
        /// 是否存在角色
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="roleName">角色名称</param>
        /// <returns>是否存在</returns>
        public bool Exists(string infoSystemNo, string roleName)
        {
            Expression<Func<Role, bool>> condition =
                x =>
                    x.InfoSystemNo == infoSystemNo &&
                    x.Name == roleName;

            return base.Exists(condition);
        }
        #endregion

        #region # 是否存在角色 —— bool Exists(string infoSystemNo, Guid? roleId, string roleName)
        /// <summary>
        /// 是否存在角色
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="roleName">角色名称</param>
        /// <returns>是否存在</returns>
        public bool Exists(string infoSystemNo, Guid? roleId, string roleName)
        {
            if (roleId.HasValue)
            {
                string originalRoleName = this.Find(x => x.Id == roleId.Value).Select(x => x.Name).Single();
                if (originalRoleName == roleName)
                {
                    return false;
                }

                return this.Exists(infoSystemNo, roleName);
            }

            return this.Exists(infoSystemNo, roleName);
        }
        #endregion
    }
}
