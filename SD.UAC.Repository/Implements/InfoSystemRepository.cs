using System.Collections.Generic;
using System.Linq;
using SD.UAC.Domain.Entities;
using SD.UAC.Domain.IRepositories.Interfaces;
using ShSoft.Infrastructure.Repository.EntityFramework;

namespace SD.UAC.Repository.Implements
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
            return base.FindNos(x => x.SystemKindNo == systemKindNo).AsEnumerable();
        }
        #endregion

        #region # 获取信息系统列表 —— IEnumerable<InfoSystem> GetInfoSystems(string systemKindNo...
        /// <summary>
        /// 获取信息系统列表
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="systemNos">信息系统编号集</param>
        /// <returns>信息系统列表</returns>
        public IEnumerable<InfoSystem> GetInfoSystems(string systemKindNo, IEnumerable<string> systemNos)
        {
            IQueryable<InfoSystem> specSystems = this.Find(x => x.SystemKindNo == systemKindNo);

            specSystems = specSystems.Where(x => systemNos.Contains(x.Number));

            return specSystems.AsEnumerable();
        }
        #endregion
    }
}
