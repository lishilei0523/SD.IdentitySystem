using System;
using System.Collections.Generic;
using SD.IdentitySystem.Domain.Entities;
using ShSoft.Infrastructure.RepositoryBase;

namespace SD.IdentitySystem.Domain.IRepositories.Interfaces
{
    /// <summary>
    /// 菜单仓储接口
    /// </summary>
    public interface IMenuRepository : IRepository<Menu>
    {
        #region # 获取菜单列表 —— IEnumerable<Menu> FindBySystemKind(string systemKindNo)
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>菜单列表</returns>
        IEnumerable<Menu> FindBySystemKind(string systemKindNo);
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
    }
}
