using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using ShSoft.Infrastructure.Repository.EntityFramework;
using ShSoft.ValueObjects;

namespace SD.IdentitySystem.Repository.Implements
{
    /// <summary>
    /// 角色仓储实现
    /// </summary>
    public class RoleRepository : EFRepositoryProvider<Role>, IRoleRepository
    {
        #region # 获取角色集 —— IEnumerable<Role> FindBySystemKind(string systemKindNo)
        /// <summary>
        /// 获取角色集
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>角色集</returns>
        public IEnumerable<Role> FindBySystemKind(string systemKindNo)
        {
            return base.Find(x => x.SystemNo == systemKindNo).AsEnumerable();
        }
        #endregion

        #region # 分页获取角色集 —— IEnumerable<Role> FindByPage(string systemKindNo, string keywords...
        /// <summary>
        /// 分页获取角色集
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录条数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>角色集</returns>
        public IEnumerable<Role> FindByPage(string systemKindNo, string keywords, int pageIndex, int pageSize, out int rowCount, out int pageCount)
        {
            Expression<Func<Role, bool>> condition =
                x =>
                    (string.IsNullOrEmpty(systemKindNo) || x.SystemNo == systemKindNo) &&
                    (string.IsNullOrEmpty(keywords) || x.Keywords.Contains(keywords));

            return base.FindByPage(condition, pageIndex, pageSize, out rowCount, out pageCount).AsEnumerable();
        }
        #endregion

        #region # 获取系统管理员角色 —— Role GetManagerRole(string systemKindNo)
        /// <summary>
        /// 获取系统管理员角色
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>系统管理员角色</returns>
        public Role GetManagerRole(string systemKindNo)
        {
            IQueryable<Role> specRoles = base.Find(x => x.SystemNo == systemKindNo);

            #region # 验证业务

            if (specRoles.All(x => x.Number != Constants.ManagerRoleNo))
            {
                throw new ApplicationException(string.Format("未为编号为\"{0}\"的信息系统类别初始化系统管理员角色！", systemKindNo));
            }

            #endregion

            return specRoles.Single(x => x.Number == Constants.ManagerRoleNo);
        }
        #endregion

        #region # 获取系统管理员角色Id —— Guid GetManagerRoleId(string systemKindNo)
        /// <summary>
        /// 获取系统管理员角色Id
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>系统管理员角色Id</returns>
        public Guid GetManagerRoleId(string systemKindNo)
        {
            IQueryable<Role> specRoles = base.Find(x => x.SystemNo == systemKindNo);

            #region # 验证业务

            if (specRoles.All(x => x.Number != Constants.ManagerRoleNo))
            {
                throw new ApplicationException(string.Format("未为编号为\"{0}\"的信息系统类别初始化系统管理员角色！", systemKindNo));
            }

            #endregion

            return specRoles.Where(x => x.Number == Constants.ManagerRoleNo).Select(x => x.Id).Single();
        }
        #endregion

        #region # 角色是否存在 —— bool Exists(string systemKindNo, string roleName)
        /// <summary>
        /// 角色是否存在
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="roleName">角色名称</param>
        /// <returns>是否存在</returns>
        public bool Exists(string systemKindNo, string roleName)
        {
            return base.Exists(x => x.SystemNo == systemKindNo && x.Name == roleName);
        }
        #endregion
    }
}
