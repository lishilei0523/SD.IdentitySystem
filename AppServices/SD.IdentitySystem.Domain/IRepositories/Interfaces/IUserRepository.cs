using SD.IdentitySystem.Domain.Entities;
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

        #region # 根据角色获取用户列表 —— IEnumerable<User> FindByPage(string keywords, Guid? roleId...
        /// <summary>
        /// 根据角色获取用户列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录条数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>用户列表</returns>
        IEnumerable<User> FindByPage(string keywords, Guid? roleId, int pageIndex, int pageSize, out int rowCount, out int pageCount);
        #endregion
    }
}
