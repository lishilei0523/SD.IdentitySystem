using SD.IdentitySystem.Domain.Entities;
using SD.Infrastructure.RepositoryBase;
using System.Collections.Generic;

namespace SD.IdentitySystem.Domain.IRepositories.Interfaces
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public interface IUserRepository : IRepository<User>
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


        #region # 从缓存获取用户列表 —— IEnumerable<User> FindAllFromCache()
        /// <summary>
        /// 从缓存获取用户列表
        /// </summary>
        /// <returns>用户列表</returns>
        IEnumerable<User> FindAllFromCache();
        #endregion

        #region # 从缓存获取用户 —— User SingleOrDefaultFromCache(string loginId)
        /// <summary>
        /// 从缓存获取用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <returns>用户</returns>
        User SingleOrDefaultFromCache(string loginId);
        #endregion

        #region # 从缓存获取用户是否存在 —— bool ExistsFromCache(string loginId)
        /// <summary>
        /// 从缓存获取用户是否存在
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <returns>是否存在</returns>
        bool ExistsFromCache(string loginId);
        #endregion
    }
}
