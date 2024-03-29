﻿using SD.IdentitySystem.Domain.Entities;
using SD.Infrastructure.Constants;
using SD.Infrastructure.RepositoryBase;
using System;
using System.Collections.Generic;

namespace SD.IdentitySystem.Domain.IRepositories.Interfaces
{
    /// <summary>
    /// 权限仓储接口
    /// </summary>
    public interface IAuthorityRepository : IAggRootRepository<Authority>
    {
        #region # 获取权限列表 —— ICollection<Authority> Find(string keywords...
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="menuId">菜单Id</param>
        /// <param name="roleId">角色Id</param>
        /// <returns>权限列表</returns>
        ICollection<Authority> Find(string keywords, string infoSystemNo, ApplicationType? applicationType, Guid? menuId, Guid? roleId);
        #endregion

        #region # 分页获取权限列表 —— ICollection<Authority> FindByPage(string keywords...
        /// <summary>
        /// 分页获取权限列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="menuId">菜单Id</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>权限列表</returns>
        ICollection<Authority> FindByPage(string keywords, string infoSystemNo, ApplicationType? applicationType, Guid? menuId, Guid? roleId, int pageIndex, int pageSize, out int rowCount, out int pageCount);
        #endregion

        #region # 根据角色获取权限列表 —— ICollection<Authority> FindByRoles(IEnumerable<Guid> roleIds...
        /// <summary>
        /// 根据角色获取权限列表
        /// </summary>
        /// <param name="roleIds">角色Id集</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>权限列表</returns>
        ICollection<Authority> FindByRoles(IEnumerable<Guid> roleIds, ApplicationType? applicationType);
        #endregion

        #region # 根据角色获取权限Id列表 —— ICollection<Guid> FindIdsByRoles(IEnumerable<Guid> roleIds)
        /// <summary>
        /// 根据角色获取权限Id列表
        /// </summary>
        /// <param name="roleIds">角色Id集</param>
        /// <returns>权限Id列表</returns>
        ICollection<Guid> FindIdsByRoles(IEnumerable<Guid> roleIds);
        #endregion

        #region # 是否存在权限路径 —— bool ExistsPath(string infoSystemNo...
        /// <summary>
        /// 是否存在权限路径
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="authorityPath">权限路径</param>
        /// <returns>是否存在</returns>
        bool ExistsPath(string infoSystemNo, ApplicationType applicationType, string authorityPath);
        #endregion
    }
}
