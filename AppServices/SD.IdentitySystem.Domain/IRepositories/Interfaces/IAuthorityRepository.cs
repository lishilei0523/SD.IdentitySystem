using SD.IdentitySystem.Domain.Entities;
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
        #region # 分页获取权限列表 —— ICollection<Authority> FindByPage(string keywords...
        /// <summary>
        /// 分页获取权限列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录条数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>权限列表</returns>
        ICollection<Authority> FindByPage(string keywords, string systemNo, ApplicationType? applicationType, int pageIndex, int pageSize, out int rowCount, out int pageCount);
        #endregion

        #region # 获取权限列表 —— ICollection<Authority> Find(string keywords, string systemNo...
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="menuId">菜单Id</param>
        /// <param name="roleId">角色Id</param>
        /// <returns>权限列表</returns>
        ICollection<Authority> Find(string keywords, string systemNo, ApplicationType? applicationType, Guid? menuId, Guid? roleId);
        #endregion

        #region # 根据角色获取权限列表 —— ICollection<Authority> FindByRole(IEnumerable<Guid> roleIds)
        /// <summary>
        /// 根据角色获取权限列表
        /// </summary>
        /// <param name="roleIds">角色Id集</param>
        /// <returns>权限列表</returns>
        ICollection<Authority> FindByRole(IEnumerable<Guid> roleIds);
        #endregion

        #region # 根据角色获取权限Id列表 —— ICollection<Guid> FindIdsByRole(IEnumerable<Guid> roleIds)
        /// <summary>
        /// 根据角色获取权限Id列表
        /// </summary>
        /// <param name="roleIds">角色Id集</param>
        /// <returns>权限Id列表</returns>
        ICollection<Guid> FindIdsByRole(IEnumerable<Guid> roleIds);
        #endregion

        #region # 是否存在给定权限 —— bool ExistsPath(string systemNo...
        /// <summary>
        /// 是否存在给定权限
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="authorityPath">权限路径</param>
        /// <returns>是否存在</returns>
        bool ExistsPath(string systemNo, ApplicationType applicationType, string authorityPath);
        #endregion
    }
}
