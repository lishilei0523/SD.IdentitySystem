using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Repository.EntityFramework;
using SD.Infrastructure.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SD.IdentitySystem.Repository.Implements
{
    /// <summary>
    /// 菜单仓储实现
    /// </summary>
    public class MenuRepository : EFAggRootRepositoryProvider<Menu>, IMenuRepository
    {
        #region # 分页获取菜单列表 —— ICollection<Menu> FindByPage(string keywords, string systemNo...
        /// <summary>
        /// 分页获取菜单列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount"></param>
        /// <param name="pageCount"></param>
        /// <returns>菜单列表</returns>
        public ICollection<Menu> FindByPage(string keywords, string systemNo, ApplicationType? applicationType, int pageIndex, int pageSize, out int rowCount, out int pageCount)
        {
            IQueryable<Menu> menus = base.FindAllInner();

            if (!string.IsNullOrWhiteSpace(keywords))
            {
                menus = menus.Where(x => x.Keywords.Contains(keywords));
            }
            if (!string.IsNullOrWhiteSpace(systemNo))
            {
                menus = menus.Where(x => x.SystemNo == systemNo);
            }
            if (applicationType != null)
            {
                menus = menus.Where(x => x.ApplicationType == applicationType || x.ApplicationType == ApplicationType.Complex);
            }

            IOrderedQueryable<Menu> orderedMenus = menus.OrderByDescending(x => x.AddedTime);
            menus = orderedMenus.ToPage(pageIndex, pageSize, out rowCount, out pageCount);

            return menus.ToList();
        }
        #endregion

        #region # 获取菜单列表 —— ICollection<Menu> FindBySystem(string systemNo...
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单列表</returns>
        public ICollection<Menu> FindBySystem(string systemNo, ApplicationType? applicationType)
        {
            IQueryable<Menu> menus = base.FindAllInner();

            if (!string.IsNullOrEmpty(systemNo))
            {
                menus = menus.Where(x => x.SystemNo == systemNo);
            }
            if (applicationType != null)
            {
                menus = menus.Where(x => x.ApplicationType == applicationType || x.ApplicationType == ApplicationType.Complex);
            }

            return menus.ToList();
        }
        #endregion

        #region # 根据权限获取菜单列表 —— ICollection<Menu> FindByAuthority(IEnumerable<Guid>...
        /// <summary>
        /// 根据权限获取菜单列表
        /// </summary>
        /// <param name="authorityIds">权限Id集</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单列表</returns>
        public ICollection<Menu> FindByAuthority(IEnumerable<Guid> authorityIds, ApplicationType? applicationType)
        {
            Expression<Func<Menu, bool>> condition =
                x =>
                    (x.Authorities.Any(y => authorityIds.Contains(y.Id))) &&
                    (applicationType == null || x.ApplicationType == applicationType);

            return base.Find(condition).ToList();
        }
        #endregion

        #region # 是否存在菜单 —— bool Exists(Guid? parentId, ApplicationType applicationType...
        /// <summary>
        /// 是否存在菜单
        /// </summary>
        /// <param name="parentId">上级菜单Id</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="menuName">菜单名称</param>
        /// <returns>是否存在</returns>
        public bool Exists(Guid? parentId, ApplicationType applicationType, string menuName)
        {
            Expression<Func<Menu, bool>> condition;

            if (parentId == null)
            {
                condition =
                    x =>
                        x.IsRoot &&
                        x.ApplicationType == applicationType &&
                        x.Name == menuName;
            }
            else
            {
                condition =
                    x =>
                        x.ParentNode.Id == parentId &&
                        x.ApplicationType == applicationType &&
                        x.Name == menuName;
            }

            return base.Exists(condition);
        }
        #endregion
    }
}
