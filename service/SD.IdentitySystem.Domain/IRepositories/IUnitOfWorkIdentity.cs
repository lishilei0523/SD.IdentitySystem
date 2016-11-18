using System.Collections.Generic;
using SD.IdentitySystem.Domain.Entities;
using ShSoft.Infrastructure.RepositoryBase;

namespace SD.IdentitySystem.Domain.IRepositories
{
    /// <summary>
    /// 单元事务 - 统一身份认证
    /// </summary>
    public interface IUnitOfWorkIdentity : IUnitOfWork
    {
        #region # 获取权限集 —— IEnumerable<Authority> ResolveAuthorities(...
        /// <summary>
        /// 获取权限集
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>权限集</returns>
        IEnumerable<Authority> ResolveAuthorities(string systemKindNo);
        #endregion
    }
}
