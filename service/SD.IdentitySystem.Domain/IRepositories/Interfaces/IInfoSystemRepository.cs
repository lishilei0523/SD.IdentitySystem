using SD.IdentitySystem.Domain.Entities;
using ShSoft.Infrastructure.RepositoryBase;
using System.Collections.Generic;

namespace SD.IdentitySystem.Domain.IRepositories.Interfaces
{
    /// <summary>
    /// 信息系统仓储接口
    /// </summary>
    public interface IInfoSystemRepository : IRepository<InfoSystem>
    {
        #region # 获取信息系统字典 —— IDictionary<string, InfoSystem> FindDictionary()
        /// <summary>
        /// 获取信息系统字典
        /// </summary>
        /// <returns>信息系统字典</returns>
        IDictionary<string, InfoSystem> FindDictionary();
        #endregion
    }
}
