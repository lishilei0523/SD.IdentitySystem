﻿using SD.IdentitySystem.Domain.Entities;
using SD.Infrastructure.RepositoryBase;
using System;
using System.Collections.Generic;

namespace SD.IdentitySystem.Domain.IRepositories.Interfaces
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public interface IUserRepository : IAggRootRepository<User>
    {
        #region # 分页获取用户列表 —— ICollection<User> FindByPage(string keywords...
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount"></param>
        /// <param name="pageCount"></param>
        /// <returns>用户列表</returns>
        ICollection<User> FindByPage(string keywords, string systemNo, Guid? roleId, int pageIndex, int pageSize, out int rowCount, out int pageCount);
        #endregion

        #region # 根据私钥获取唯一用户 —— User SingleByPrivateKey(string privateKey)
        /// <summary>
        /// 根据私钥获取唯一用户
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <returns>用户</returns>
        User SingleByPrivateKey(string privateKey);
        #endregion

        #region # 是否存在私钥 —— bool ExistsPrivateKey(string loginId, string privateKey)
        /// <summary>
        /// 是否存在私钥
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>是否存在</returns>
        bool ExistsPrivateKey(string loginId, string privateKey);
        #endregion
    }
}
