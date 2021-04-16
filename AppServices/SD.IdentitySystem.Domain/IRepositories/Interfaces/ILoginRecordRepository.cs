using SD.IdentitySystem.Domain.Entities;
using SD.Infrastructure.RepositoryBase;
using System;
using System.Collections.Generic;

namespace SD.IdentitySystem.Domain.IRepositories.Interfaces
{
    /// <summary>
    /// 登录记录仓储接口
    /// </summary>
    public interface ILoginRecordRepository : IAggRootRepository<LoginRecord>
    {
        #region # 分页获取用户登录记录列表 —— ICollection<LoginRecord> FindByPage(string keywords...
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
        ICollection<LoginRecord> FindByPage(string keywords, DateTime? startTime, DateTime? endTime, int pageIndex, int pageSize, out int rowCount, out int pageCount);
        #endregion
    }
}
