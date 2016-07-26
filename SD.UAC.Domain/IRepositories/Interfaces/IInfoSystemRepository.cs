using System.Collections.Generic;
using SD.UAC.Domain.Entities;
using ShSoft.Infrastructure.RepositoryBase;

namespace SD.UAC.Domain.IRepositories.Interfaces
{
    /// <summary>
    /// 信息系统仓储接口
    /// </summary>
    public interface IInfoSystemRepository : IRepository<InfoSystem>
    {
        #region # 根据信息系统类别获取信息系统编号集 —— IEnumerable<string> GetInfoSystemNos(...
        /// <summary>
        /// 根据信息系统类别获取信息系统编号集
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>信息系统集</returns>
        IEnumerable<string> GetInfoSystemNos(string systemKindNo);
        #endregion
    }
}
