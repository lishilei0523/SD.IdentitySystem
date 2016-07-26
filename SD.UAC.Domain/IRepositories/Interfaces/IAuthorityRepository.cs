using System;
using System.Collections.Generic;
using SD.UAC.Domain.Entities;
using ShSoft.Infrastructure.RepositoryBase;

namespace SD.UAC.Domain.IRepositories.Interfaces
{
    /// <summary>
    /// 权限仓储接口
    /// </summary>
    public interface IAuthorityRepository : IRepository<Authority>
    {
        #region # 根据信息系统类别获取权限集 —— IEnumerable<Authority> FindBySystemKind(...
        /// <summary>
        /// 根据信息系统类别获取权限集
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>权限集</returns>
        IEnumerable<Authority> FindBySystemKind(string systemKindNo);
        #endregion

        #region # 根据信息系统类别获取权限Id集 —— IEnumerable<Guid> FindAuthorityIds(string systemKindNo)
        /// <summary>
        /// 根据信息系统类别获取权限Id集
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>权限Id集</returns>
        IEnumerable<Guid> FindAuthorityIds(string systemKindNo);
        #endregion

        #region # 分页获取权限集 —— IEnumerable<Authority> FindByPage(string systemKindNo...
        /// <summary>
        /// 分页获取权限集
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录条数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>权限集</returns>
        IEnumerable<Authority> FindByPage(string systemKindNo, string keywords, int pageIndex, int pageSize, out int rowCount, out int pageCount);
        #endregion

        #region # 是否存在给定权限 —— bool Exists(string systemKindNo, Guid authorityId)
        /// <summary>
        /// 是否存在给定权限
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="authorityId">权限Id</param>
        /// <returns>是否存在</returns>
        bool Exists(string systemKindNo, Guid authorityId);
        #endregion

        #region # 是否存在给定权限 —— bool Exists(string systemKindNo, string authorityPath)
        /// <summary>
        /// 是否存在给定权限
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="authorityPath">权限路径</param>
        /// <returns>是否存在</returns>
        bool Exists(string systemKindNo, string authorityPath);
        #endregion
    }
}
