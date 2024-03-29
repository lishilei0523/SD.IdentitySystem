﻿using Microsoft.EntityFrameworkCore;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Repository.EntityFrameworkCore;
using SD.Toolkits.EntityFrameworkCore.Extensions;
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
        #region # 获取实体对象列表 —— override IQueryable<Menu> FindAllInner()
        /// <summary>
        /// 获取实体对象列表
        /// </summary>
        /// <returns>实体对象列表</returns>
        protected override IQueryable<Menu> FindAllInner()
        {
            return base._dbContext.Set<Menu>();
        }
        #endregion

        #region # 获取菜单列表 —— ICollection<Menu> FindBySystem(string keywords...
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单列表</returns>
        public ICollection<Menu> FindBySystem(string keywords, string infoSystemNo, ApplicationType? applicationType)
        {
            QueryBuilder<Menu> queryBuilder = QueryBuilder<Menu>.Affirm();
            if (!string.IsNullOrWhiteSpace(keywords))
            {
                queryBuilder.And(x => x.Keywords.Contains(keywords));
            }
            if (!string.IsNullOrWhiteSpace(infoSystemNo))
            {
                queryBuilder.And(x => x.InfoSystemNo == infoSystemNo);
            }
            if (applicationType != null)
            {
                queryBuilder.And(x => x.ApplicationType == applicationType.Value || x.ApplicationType == ApplicationType.Complex);
            }

            Expression<Func<Menu, bool>> condition = queryBuilder.Build();
            IQueryable<Menu> menus = base.Find(condition).OrderBy(x => x.Sort);

            return menus.ToList();
        }
        #endregion

        #region # 分页获取菜单列表 —— ICollection<Menu> FindByPage(string keywords...
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
        public ICollection<Menu> FindByPage(string keywords, string infoSystemNo, ApplicationType? applicationType, int pageIndex, int pageSize, out int rowCount, out int pageCount)
        {
            QueryBuilder<Menu> queryBuilder = QueryBuilder<Menu>.Affirm();
            if (!string.IsNullOrWhiteSpace(keywords))
            {
                queryBuilder.And(x => x.Keywords.Contains(keywords));
            }
            if (!string.IsNullOrWhiteSpace(infoSystemNo))
            {
                queryBuilder.And(x => x.InfoSystemNo == infoSystemNo);
            }
            if (applicationType != null)
            {
                queryBuilder.And(x => x.ApplicationType == applicationType.Value || x.ApplicationType == ApplicationType.Complex);
            }

            Expression<Func<Menu, bool>> condition = queryBuilder.Build();
            IQueryable<Menu> menus = base.FindByPage(condition, pageIndex, pageSize, out rowCount, out pageCount);

            return menus.ToList();
        }
        #endregion

        #region # 根据权限获取菜单列表 —— ICollection<Menu> FindByAuthorities(IEnumerable<Guid>...
        /// <summary>
        /// 根据权限获取菜单列表
        /// </summary>
        /// <param name="authorityIds">权限Id集</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单列表</returns>
        public ICollection<Menu> FindByAuthorities(IEnumerable<Guid> authorityIds, ApplicationType? applicationType)
        {
            #region # 验证

            authorityIds = authorityIds?.Distinct().ToArray() ?? Array.Empty<Guid>();
            if (!authorityIds.Any())
            {
                return new List<Menu>();
            }

            #endregion

            QueryBuilder<Menu> queryBuilder = QueryBuilder<Menu>.Affirm();
            if (authorityIds.Any())
            {
                queryBuilder.And(x => x.Authorities.Any(y => authorityIds.Contains(y.Id)));
            }
            if (applicationType.HasValue)
            {
                queryBuilder.And(x => x.ApplicationType == applicationType.Value);
            }

            Expression<Func<Menu, bool>> condition = queryBuilder.Build();
            IQueryable<Menu> menus = base.Find(condition).Include(x => x.ParentNode).OrderBy(x => x.Sort);

            return menus.ToList();
        }
        #endregion

        #region # 是否存在菜单 —— bool Exists(Guid? parentNodeId, ApplicationType applicationType...
        /// <summary>
        /// 是否存在菜单
        /// </summary>
        /// <param name="parentNodeId">上级节点Id</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="menuName">菜单名称</param>
        /// <returns>是否存在</returns>
        public bool Exists(Guid? parentNodeId, ApplicationType applicationType, string menuName)
        {
            Expression<Func<Menu, bool>> condition;
            if (parentNodeId.HasValue)
            {
                condition =
                    x =>
                        x.ParentNode.Id == parentNodeId.Value &&
                        x.ApplicationType == applicationType &&
                        x.Name == menuName;
            }
            else
            {
                condition =
                    x =>
                        x.IsRoot &&
                        x.ApplicationType == applicationType &&
                        x.Name == menuName;
            }

            return base.Exists(condition);
        }
        #endregion
    }
}
