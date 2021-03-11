using SD.IdentitySystem.Domain.Entities;
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
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录条数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>权限列表</returns>
        ICollection<Authority> FindByPage(string keywords, string systemNo, int pageIndex, int pageSize, out int rowCount, out int pageCount);
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
        ICollection<Authority> Find(string keywords, string systemNo, Guid? menuId, Guid? roleId);
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

        #region # 是否存在给定权限 —— bool ExistsPath(string authorityPath)
        /// <summary>
        /// 是否存在给定权限
        /// </summary>
        /// <param name="authorityPath">权限路径</param>
        /// <returns>是否存在</returns>
        bool ExistsPath(string authorityPath);
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
        bool ExistsPath(string assemblyName, string @namespace, string className, string methodName);
        #endregion
    }
}
