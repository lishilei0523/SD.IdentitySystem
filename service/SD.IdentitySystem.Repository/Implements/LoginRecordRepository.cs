using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using SD.Infrastructure.Repository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SD.IdentitySystem.Repository.Implements
{
    /// <summary>
    /// 登录记录仓储实现
    /// </summary>
    public class LoginRecordRepository : EFRepositoryProvider<LoginRecord>, ILoginRecordRepository
    {
        #region # 分页获取用户登录记录列表 —— IEnumerable<LoginRecord> FindByPage(string keywords...
        /// <summary>
        /// 分页获取用户登录记录列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录条数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>用户登录记录列表</returns>
        public IEnumerable<LoginRecord> FindByPage(string keywords, DateTime? startTime, DateTime? endTime, int pageIndex, int pageSize, out int rowCount, out int pageCount)
        {
            Expression<Func<LoginRecord, bool>> condition =
                x =>
                    (string.IsNullOrEmpty(keywords) || x.Keywords.Contains(keywords)) &&
                    (startTime == null || x.AddedTime >= startTime) &&
                    (endTime == null || x.AddedTime <= endTime);

            return base.FindByPage(condition, pageIndex, pageSize, out rowCount, out pageCount).AsEnumerable();
        }
        #endregion
    }
}
