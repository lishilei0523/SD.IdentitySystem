using System;
using System.Collections.Generic;
using SD.IdentitySystem.Domain.Entities;
using ShSoft.Infrastructure.RepositoryBase;

namespace SD.IdentitySystem.Domain.IRepositories.Interfaces
{
    /// <summary>
    /// 角色仓储接口
    /// </summary>
    public interface IRoleRepository : IRepository<Role>
    {
        #region # 获取角色集 —— IEnumerable<Role> FindBySystemKind(string systemKindNo)
        /// <summary>
        /// 获取角色集
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>角色集</returns>
        IEnumerable<Role> FindBySystemKind(string systemKindNo);
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
        IEnumerable<Role> FindByPage(string systemKindNo, string keywords, int pageIndex, int pageSize, out int rowCount, out int pageCount);
        #endregion

        #region # 获取系统管理员角色 —— Role GetManagerRole(string systemKindNo)
        /// <summary>
        /// 获取系统管理员角色
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>系统管理员角色</returns>
        Role GetManagerRole(string systemKindNo);
        #endregion

        #region # 获取系统管理员角色Id —— Guid GetManagerRoleId(string systemKindNo)
        /// <summary>
        /// 获取系统管理员角色Id
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>系统管理员角色Id</returns>
        Guid GetManagerRoleId(string systemKindNo);
        #endregion

        #region # 角色是否存在 —— bool Exists(string systemKindNo, string roleName)
        /// <summary>
        /// 角色是否存在
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="roleName">角色名称</param>
        /// <returns>是否存在</returns>
        bool Exists(string systemKindNo, string roleName);
        #endregion
    }
}
