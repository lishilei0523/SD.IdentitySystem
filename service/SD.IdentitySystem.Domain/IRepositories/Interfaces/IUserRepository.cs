using SD.IdentitySystem.Domain.Entities;
using SD.Infrastructure.RepositoryBase;
using System.Collections.Generic;

namespace SD.IdentitySystem.Domain.IRepositories.Interfaces
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public interface IUserRepository : IAggRootRepository<User>
    {
        #region # 分页获取用户列表 —— IEnumerable<User> FindByPage(string systemNo...
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount"></param>
        /// <param name="pageCount"></param>
        /// <returns>用户列表</returns>
        IEnumerable<User> FindByPage(string systemNo, string keywords, int pageIndex, int pageSize, out int rowCount, out int pageCount);
        #endregion
    }
}
