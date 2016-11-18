using System.Collections.Generic;
using SD.IdentitySystem.Domain.Entities;
using ShSoft.Infrastructure.RepositoryBase;

namespace SD.IdentitySystem.Domain.IRepositories.Interfaces
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
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
