﻿using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using SD.Infrastructure.Repository.EntityFrameworkCore;
using SD.Toolkits.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SD.IdentitySystem.Repository.Implements
{
    /// <summary>
    /// 登录记录仓储实现
    /// </summary>
    public class LoginRecordRepository : EFAggRootRepositoryProvider<LoginRecord>, ILoginRecordRepository
    {
        #region # 获取实体对象列表 —— override IQueryable<LoginRecord> FindAllInner()
        /// <summary>
        /// 获取实体对象列表
        /// </summary>
        /// <returns>实体对象列表</returns>
        protected override IQueryable<LoginRecord> FindAllInner()
        {
            return base._dbContext.Set<LoginRecord>();
        }
        #endregion

        #region # 分页获取登录记录列表 —— ICollection<LoginRecord> FindByPage(string keywords...
        /// <summary>
        /// 分页获取登录记录列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>登录记录列表</returns>
        public ICollection<LoginRecord> FindByPage(string keywords, DateTime? startTime, DateTime? endTime, int pageIndex, int pageSize, out int rowCount, out int pageCount)
        {
            QueryBuilder<LoginRecord> queryBuilder = QueryBuilder<LoginRecord>.Affirm();
            if (!string.IsNullOrWhiteSpace(keywords))
            {
                queryBuilder.And(x => x.Keywords.Contains(keywords));
            }
            if (startTime.HasValue)
            {
                queryBuilder.And(x => x.AddedTime >= startTime.Value);
            }
            if (endTime.HasValue)
            {
                queryBuilder.And(x => x.AddedTime <= endTime.Value);
            }

            Expression<Func<LoginRecord, bool>> condition = queryBuilder.Build();
            IQueryable<LoginRecord> loginRecords = base.FindByPage(condition, pageIndex, pageSize, out rowCount, out pageCount);

            return loginRecords.ToList();
        }
        #endregion
    }
}
