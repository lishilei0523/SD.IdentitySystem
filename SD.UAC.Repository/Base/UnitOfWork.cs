using System.Collections.Generic;
using System.Linq;
using SD.UAC.Domain.Entities;
using SD.UAC.Domain.IRepositories;
using ShSoft.Infrastructure.Repository.EntityFramework;

namespace SD.UAC.Repository.Base
{
    /// <summary>
    /// 单元事务 - 人资
    /// </summary>
    public sealed class UnitOfWork : EFUnitOfWorkProvider, IUnitOfWorkUAC
    {
        #region # 获取权限集 —— IEnumerable<Authority> ResolveAuthorities(...
        /// <summary>
        /// 获取权限集
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>权限集</returns>
        public IEnumerable<Authority> ResolveAuthorities(string systemKindNo)
        {
            return base.ResolveRange<Authority>(x => x.SystemKindNo == systemKindNo).AsEnumerable();
        }
        #endregion
    }
}
