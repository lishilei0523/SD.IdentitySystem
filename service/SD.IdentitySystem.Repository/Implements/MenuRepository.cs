using System;
using System.Collections.Generic;
using System.Linq;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using ShSoft.Infrastructure.Repository.EntityFramework;

namespace SD.IdentitySystem.Repository.Implements
{
    /// <summary>
    /// 菜单仓储实现
    /// </summary>
    public class MenuRepository : EFRepositoryProvider<Menu>, IMenuRepository
    {
        #region # 获取菜单列表 —— IEnumerable<Menu> FindBySystem(string systemNo)
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>菜单列表</returns>
        public IEnumerable<Menu> FindBySystem(string systemNo)
        {
            return base.Find(x => x.SystemNo == systemNo).AsEnumerable();
        }
        #endregion

        #region # 根据上级菜单Id判断菜单是否存在 —— bool Exists(Guid? parentId, string menuName)
        /// <summary>
        /// 根据上级菜单Id判断菜单是否存在
        /// </summary>
        /// <param name="parentId">上级菜单Id</param>
        /// <param name="menuName">菜单名称</param>
        /// <returns>菜单名称是否存在</returns>
        public bool Exists(Guid? parentId, string menuName)
        {
            if (parentId == null)
            {
                return base.Exists(x => x.IsRoot && x.Name == menuName);
            }
            return base.Exists(x => x.ParentNode != null && x.ParentNode.Id == parentId && x.Name == menuName);
        }
        #endregion
    }
}
