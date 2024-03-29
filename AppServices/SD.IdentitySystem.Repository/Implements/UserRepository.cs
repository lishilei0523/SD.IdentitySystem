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
    /// 用户仓储实现
    /// </summary>
    public class UserRepository : EFAggRootRepositoryProvider<User>, IUserRepository
    {
        #region # 获取实体对象列表 —— override IQueryable<User> FindAllInner()
        /// <summary>
        /// 获取实体对象列表
        /// </summary>
        /// <returns>实体对象列表</returns>
        protected override IQueryable<User> FindAllInner()
        {
            return base._dbContext.Set<User>();
        }
        #endregion

        #region # 完整获取用户 —— User SingleFully(string loginId)
        /// <summary>
        /// 完整获取用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <returns>用户</returns>
        public User SingleFully(string loginId)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(loginId))
            {
                throw new ArgumentNullException(nameof(loginId), "用户名不可为空！");
            }

            #endregion

            IQueryable<User> users = base.Find(x => x.Number == loginId).Include(x => x.Roles);
            User user = users.SingleOrDefault();

            return user;
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

            IQueryable<User> users = base.Find(x => x.PrivateKey == privateKey).Include(x => x.Roles);
            User user = users.SingleOrDefault();

            return user;
        }
        #endregion

        #region # 获取用户列表 —— ICollection<User> Find(string keywords...
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <returns>用户列表</returns>
        public ICollection<User> Find(string keywords, string infoSystemNo, Guid? roleId)
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
                queryBuilder.And(x => x.Roles.Any(y => y.Id == roleId.Value));
            }

            Expression<Func<User, bool>> condition = queryBuilder.Build();
            IQueryable<User> users = base.Find(condition);

            return users.ToList();
        }
        #endregion

        #region # 分页获取用户列表 —— ICollection<User> FindByPage(string keywords...
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录数</param>
        /// <param name="pageCount">总页数</param>
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
                queryBuilder.And(x => x.Roles.Any(y => y.Id == roleId.Value));
            }

            Expression<Func<User, bool>> condition = queryBuilder.Build();
            IQueryable<User> users = base.FindByPage(condition, pageIndex, pageSize, out rowCount, out pageCount);

            return users.ToList();
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
