using SD.IdentitySystem.Domain.Entities;
using SD.Infrastructure.Constants;
using SD.Infrastructure.RepositoryBase;
using System;
using System.Collections.Generic;

namespace SD.IdentitySystem.Domain.IRepositories.Interfaces
{
    /// <summary>
    /// 菜单仓储接口
    /// </summary>
    public interface IMenuRepository : IAggRootRepository<Menu>
    {
        #region # 分页获取菜单列表 —— ICollection<Menu> FindByPage(string keywords, string infoSystemNo...
        /// <summary>
        /// 分页获取菜单列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>菜单列表</returns>
        ICollection<Menu> FindByPage(string keywords, string infoSystemNo, ApplicationType? applicationType, int pageIndex, int pageSize, out int rowCount, out int pageCount);
        #endregion

        #region # 获取菜单列表 —— ICollection<Menu> FindBySystem(string infoSystemNo...
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单列表</returns>
        ICollection<Menu> FindBySystem(string infoSystemNo, ApplicationType? applicationType);
        #endregion

        #region # 根据权限获取菜单列表 —— ICollection<Menu> FindByAuthority(IEnumerable<Guid>...
        /// <summary>
        /// 根据权限获取菜单列表
        /// </summary>
        /// <param name="authorityIds">权限Id集</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单列表</returns>
        ICollection<Menu> FindByAuthority(IEnumerable<Guid> authorityIds, ApplicationType? applicationType);
        #endregion

        #region # 是否存在菜单 —— bool Exists(Guid? parentNodeId, ApplicationType applicationType...
        /// <summary>
        /// 是否存在菜单
        /// </summary>
        /// <param name="parentNodeId">上级节点Id</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="menuName">菜单名称</param>
        /// <returns>是否存在</returns>
        bool Exists(Guid? parentNodeId, ApplicationType applicationType, string menuName);
        #endregion
    }
}
