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
    /// 用户仓储实现
    /// </summary>
    public class UserRepository : EFAggRootRepositoryProvider<User>, IUserRepository
    {
        #region # 分页获取用户列表 —— ICollection<User> FindByPage(string keywords...
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount"></param>
        /// <param name="pageCount"></param>
        /// <returns>用户列表</returns>
        public ICollection<User> FindByPage(string keywords, string infoSystemNo, Guid? roleId, int pageIndex, int pageSize, out int rowCount, out int pageCount)
        {
            QueryBuilder<User> queryBuilder = new QueryBuilder<User>(x => x.Number != CommonConstants.AdminLoginId);
            if (!string.IsNullOrWhiteSpace(keywords))
            {
                queryBuilder.And(x => x.Keywords.Contains(keywords));
            }
            if (!string.IsNullOrWhiteSpace(infoSystemNo))
            {
                queryBuilder.And(x => x.Roles.Any(y => y.InfoSystemNo == infoSystemNo));
            }
            if (roleId.HasValue)
            {
                Guid roleId_ = roleId.Value;
                queryBuilder.And(x => x.Roles.Any(y => y.Id == roleId_));
            }

            Expression<Func<User, bool>> condition = queryBuilder.Build();
            IQueryable<User> users = base.FindByPage(condition, pageIndex, pageSize, out rowCount, out pageCount);

            return users.ToList();
        }
        #endregion

        #region # 根据私钥获取唯一用户 —— User SingleByPrivateKey(string privateKey)
        /// <summary>
        /// 根据私钥获取唯一用户
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <returns>用户</returns>
        public User SingleByPrivateKey(string privateKey)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(privateKey))
            {
                throw new ArgumentNullException(nameof(privateKey), "私钥不可为空！");
            }

            #endregion

            User user = base.SingleOrDefault(x => x.PrivateKey == privateKey);

            return user;
        }
        #endregion

        #region # 是否存在私钥 —— bool ExistsPrivateKey(string loginId, string privateKey)
        /// <summary>
        /// 是否存在私钥
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>是否存在</returns>
        public bool ExistsPrivateKey(string loginId, string privateKey)
        {
            if (!string.IsNullOrWhiteSpace(loginId))
            {
                User user = base.SingleOrDefault(loginId);
                if (user != null && user.PrivateKey == privateKey)
                {
                    return false;
                }

                return base.Exists(x => x.PrivateKey == privateKey);
            }

            return base.Exists(x => x.PrivateKey == privateKey);
        }
        #endregion
    }
}
