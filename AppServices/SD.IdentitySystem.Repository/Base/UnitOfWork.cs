using System.Collections.Generic;
using System.Linq;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories;
using SD.Infrastructure.Repository.EntityFramework;

namespace SD.IdentitySystem.Repository.Base
{
    /// <summary>
    /// 单元事务 - 统一身份认证
    /// </summary>
    public sealed class UnitOfWork : EFUnitOfWorkProvider, IUnitOfWorkIdentity
    {
        #region # 获取权限集 —— IEnumerable<Authority> ResolveAuthorities(...
        /// <summary>
        /// 获取权限集
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>权限集</returns>
        public IEnumerable<Authority> ResolveAuthorities(string systemNo)
        {
            return base.ResolveRange<Authority>(x => x.SystemNo == systemNo).AsEnumerable();
        }
        #endregion
    }
}
