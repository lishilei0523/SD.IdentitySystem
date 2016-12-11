using SD.IdentitySystem.Domain.Entities;
using ShSoft.Infrastructure.RepositoryBase;
using System;
using System.Collections.Generic;

namespace SD.IdentitySystem.Domain.IRepositories.Interfaces
{
    /// <summary>
    /// 权限仓储接口
    /// </summary>
    public interface IAuthorityRepository : IRepository<Authority>
    {
        #region # 根据信息系统获取权限列表 —— IEnumerable<Authority> FindBySystem(...
        /// <summary>
        /// 根据信息系统获取权限列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>权限列表</returns>
        IEnumerable<Authority> FindBySystem(string systemNo);
        #endregion

        #region # 根据信息系统获取权限Id集 —— IEnumerable<Guid> FindAuthorityIds(string systemNo)
        /// <summary>
        /// 根据信息系统获取权限Id集
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>权限Id集</returns>
        IEnumerable<Guid> FindAuthorityIds(string systemNo);
        #endregion

        #region # 根据菜单获取权限列表 —— IEnumerable<Authority> FindByMenu(Guid menuId)
        /// <summary>
        /// 根据菜单获取权限列表
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns>权限列表</returns>
        IEnumerable<Authority> FindByMenu(Guid menuId);
        #endregion

        #region # 根据角色获取权限列表 —— IEnumerable<Authority> FindByRole(...
        /// <summary>
        /// 根据角色获取权限列表
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>权限列表</returns>
        IEnumerable<Authority> FindByRole(Guid roleId);
        #endregion

        #region # 根据角色获取权限列表 —— IEnumerable<Authority> FindByRole(IEnumerable<Guid> roleIds)
        /// <summary>
        /// 根据角色获取权限列表
        /// </summary>
        /// <param name="roleIds">角色Id集</param>
        /// <returns>权限列表</returns>
        IEnumerable<Authority> FindByRole(IEnumerable<Guid> roleIds);
        #endregion

        #region # 根据角色获取权限Id列表 —— IEnumerable<Guid> FindIdsByRole(IEnumerable<Guid> roleIds)
        /// <summary>
        /// 根据角色获取权限Id列表
        /// </summary>
        /// <param name="roleIds">角色Id集</param>
        /// <returns>权限Id列表</returns>
        IEnumerable<Guid> FindIdsByRole(IEnumerable<Guid> roleIds);
        #endregion

        #region # 根据角色获取权限Id列表 —— IEnumerable<Authority> FindIdsByRole(...
        /// <summary>
        /// 根据角色获取权限Id列表
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>权限Id列表</returns>
        IEnumerable<Guid> FindIdsByRole(Guid roleId);
        #endregion

        #region # 分页获取权限集 —— IEnumerable<Authority> FindByPage(string systemNo...
        /// <summary>
        /// 分页获取权限集
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录条数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>权限集</returns>
        IEnumerable<Authority> FindByPage(string systemNo, string keywords, int pageIndex, int pageSize, out int rowCount, out int pageCount);
        #endregion

        #region # 是否存在给定权限 —— bool ExistsPath(string authorityPath)
        /// <summary>
        /// 是否存在给定权限
        /// </summary>
        /// <param name="authorityPath">权限路径</param>
        /// <returns>是否存在</returns>
        bool ExistsPath(string authorityPath);
        #endregion

        #region # 是否存在给定权限 ——  bool ExistsPath(string assemblyName, string @namespace
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
