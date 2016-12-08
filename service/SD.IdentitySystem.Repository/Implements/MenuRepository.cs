using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public IEnumerable<Menu> FindByPage(string keywords, string systemNo, int pageIndex, int pageSize, out int rowCount, out int pageCount)
        {
            Expression<Func<Menu, bool>> condition =
                x =>
                    (string.IsNullOrEmpty(keywords) || x.Keywords.Contains(keywords)) &&
                    (string.IsNullOrEmpty(systemNo) || x.SystemNo == systemNo);

            return base.FindByPage(condition, pageIndex, pageSize, out rowCount, out pageCount).AsEnumerable();
        }
        #endregion

        #region # 获取菜单列表 —— IEnumerable<Menu> FindBySystem(string systemNo)
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>菜单列表</returns>
        public IEnumerable<Menu> FindBySystem(string systemNo)
        {
            return base.Find(x => string.IsNullOrEmpty(systemNo) || x.SystemNo == systemNo).AsEnumerable();
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
