﻿using SD.IdentitySystem.Domain.Entities;
using SD.Infrastructure.RepositoryBase;
using System;
using System.Collections.Generic;

namespace SD.IdentitySystem.Domain.IRepositories.Interfaces
{
    /// <summary>
    /// 角色仓储接口
    /// </summary>
    public interface IRoleRepository : IAggRootRepository<Role>
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
        ICollection<Role> FindByPage(string keywords, string systemNo, int pageIndex, int pageSize, out int rowCount, out int pageCount);
        #endregion

        #region # 获取角色列表 —— ICollection<Role> Find(string keywords, string loginId...
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>角色列表</returns>
        ICollection<Role> Find(string keywords, string loginId, string systemNo);
        #endregion

        #region # 获取系统管理员角色 —— Role GetManagerRole(string systemNo)
        /// <summary>
        /// 获取系统管理员角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>系统管理员角色</returns>
        Role GetManagerRole(string systemNo);
        #endregion

        #region # 获取角色Id列表 —— ICollection<Guid> FindIds(string loginId, string systemNo)
        /// <summary>
        /// 获取角色Id列表
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>角色Id列表</returns>
        ICollection<Guid> FindIds(string loginId, string systemNo);
        #endregion

        #region # 获取系统管理员角色Id —— Guid GetManagerRoleId(string systemNo)
        /// <summary>
        /// 获取系统管理员角色Id
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>系统管理员角色Id</returns>
        Guid GetManagerRoleId(string systemNo);
        #endregion

        #region # 是否存在角色 —— bool Exists(string systemNo, string roleName)
        /// <summary>
        /// 是否存在角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleName">角色名称</param>
        /// <returns>是否存在</returns>
        bool Exists(string systemNo, string roleName);
        #endregion
    }
}
