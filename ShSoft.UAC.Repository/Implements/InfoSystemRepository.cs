using System.Collections.Generic;
using System.Linq;
using ShSoft.Framework2016.Infrastructure.Repository.EntityFrameworkProvider;
using ShSoft.UAC.Domain.Entities;
using ShSoft.UAC.Domain.IRepositories.Interfaces;

namespace ShSoft.UAC.Repository.Implements
{
    /// <summary>
    /// 信息系统仓储实现
    /// </summary>
    public class InfoSystemRepository : EFRepositoryProvider<InfoSystem>, IInfoSystemRepository
    {
        #region # 根据信息系统类别获取信息系统编号集 —— IEnumerable<string> GetInfoSystemNos(...
        /// <summary>
        /// 根据信息系统类别获取信息系统编号集
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>信息系统集</returns>
        public IEnumerable<string> GetInfoSystemNos(string systemKindNo)
        {
            return base.FindNos(x => x.InfoSystemKindNo == systemKindNo).AsEnumerable();
        }
        #endregion
    }
}
