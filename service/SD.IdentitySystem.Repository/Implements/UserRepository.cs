using SD.CacheManager;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using SD.Toolkits.EntityFramework.Extensions;
using ShSoft.Common.PoweredByLee;
using ShSoft.Infrastructure.Constants;
using ShSoft.Infrastructure.Repository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace SD.IdentitySystem.Repository.Implements
{
    /// <summary>
    /// 用户仓储实现
    /// </summary>
    public class UserRepository : EFRepositoryProvider<User>, IUserRepository
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
        public IEnumerable<User> FindByPage(string systemNo, string keywords, int pageIndex, int pageSize, out int rowCount, out int pageCount)
        {
            Expression<Func<User, bool>> condition =
                x =>
                    (string.IsNullOrEmpty(systemNo) || x.Roles.Any(y => y.SystemNo == systemNo)) &&
                    (string.IsNullOrEmpty(keywords) || x.Keywords.Contains(keywords));

            return base.FindByPage(condition, pageIndex, pageSize, out rowCount, out pageCount).AsEnumerable();
        }
        #endregion


        #region # 从缓存获取用户列表 —— IEnumerable<User> FindAllFromCache()
        /// <summary>
        /// 从缓存获取用户列表
        /// </summary>
        /// <returns>用户列表</returns>
        public IEnumerable<User> FindAllFromCache()
        {
            IList<User> users = CacheMediator.Get<IList<User>>(typeof(IUserRepository).FullName);

            if (users == null || !users.Any())
            {
                IQueryable<User> systems = base.FindAllInner();

                string sql = systems.ParseSql();

                SqlHelper sqlHelper = new SqlHelper(WebConfigSetting.DefaultConnectionString);
                DataTable dateTable = sqlHelper.GetDataTable(sql);

                users = dateTable.ToList<User>();

                CacheMediator.Set(typeof(IUserRepository).FullName, users);
            }

            return users.AsEnumerable();
        }
        #endregion

        #region # 从缓存获取用户 —— User SingleOrDefaultFromCache(string loginId)
        /// <summary>
        /// 从缓存获取用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <returns>用户</returns>
        public User SingleOrDefaultFromCache(string loginId)
        {
            return this.FindAllFromCache().SingleOrDefault(x => x.Number == loginId);
        }
        #endregion

        #region # 从缓存获取用户是否存在 —— bool ExistsFromCache(string loginId)
        /// <summary>
        /// 从缓存获取用户是否存在
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <returns>是否存在</returns>
        public bool ExistsFromCache(string loginId)
        {
            return this.FindAllFromCache().Any(x => x.Number == loginId);
        }
        #endregion
    }
}
