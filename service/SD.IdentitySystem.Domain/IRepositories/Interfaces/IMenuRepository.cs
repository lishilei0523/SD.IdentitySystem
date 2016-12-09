using SD.IdentitySystem.Domain.Entities;
using ShSoft.Infrastructure.RepositoryBase;
using System;
using System.Collections.Generic;

namespace SD.IdentitySystem.Domain.IRepositories.Interfaces
{
    /// <summary>
    /// 菜单仓储接口
    /// </summary>
    public interface IMenuRepository : IRepository<Menu>
    {
        #region # 分页获取菜单列表 —— IEnumerable<Menu> FindByPage(string keywords...
        /// <summary>
        /// 分页获取菜单列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录条数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>菜单列表</returns>
        IEnumerable<Menu> FindByPage(string keywords, string systemNo, int pageIndex, int pageSize, out int rowCount, out int pageCount);
        #endregion

        #region # 获取菜单列表 —— IEnumerable<Menu> FindBySystem(string systemNo)
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>菜单列表</returns>
        IEnumerable<Menu> FindBySystem(string systemNo);
        #endregion

        #region # 根据上级菜单Id判断菜单是否存在 —— bool Exists(Guid? parentId, string menuName)
        /// <summary>
        /// 根据上级菜单Id判断菜单是否存在
        /// </summary>
        /// <param name="parentId">上级菜单Id</param>
        /// <param name="menuName">菜单名称</param>
        /// <returns>菜单名称是否存在</returns>
        bool Exists(Guid? parentId, string menuName);
        #endregion

        #region # 根据权限获取菜单列表 —— IEnumerable<Menu> FindByAuthority(IEnumerable<Guid>...
        /// <summary>
        /// 根据权限获取菜单列表
        /// </summary>
        /// <param name="authorityIds">权限Id集</param>
        /// <returns>菜单列表</returns>
        IEnumerable<Menu> FindByAuthority(IEnumerable<Guid> authorityIds);
        #endregion
    }
}
