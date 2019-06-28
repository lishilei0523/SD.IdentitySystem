using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Repository.EntityFramework;
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
            Expression<Func<Menu, bool>> condition =
                x =>
                    (string.IsNullOrEmpty(keywords) || x.Keywords.Contains(keywords)) &&
                    (string.IsNullOrEmpty(systemNo) || x.SystemNo == systemNo) &&
                    (applicationType == null || x.ApplicationType == applicationType);

            return base.FindByPage(condition, pageIndex, pageSize, out rowCount, out pageCount).ToList();
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
            Expression<Func<Menu, bool>> condition =
                x =>
                    (string.IsNullOrEmpty(systemNo) || x.SystemNo == systemNo) &&
                    (applicationType == null || x.ApplicationType == applicationType);


            return base.Find(condition).ToList();
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
    }
}
