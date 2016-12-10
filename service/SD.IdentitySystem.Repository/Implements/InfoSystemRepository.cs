using System.Collections.Generic;
using System.Linq;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using ShSoft.Infrastructure.Repository.EntityFramework;

namespace SD.IdentitySystem.Repository.Implements
{
    /// <summary>
    /// 信息系统仓储实现
    /// </summary>
    public class InfoSystemRepository : EFRepositoryProvider<InfoSystem>, IInfoSystemRepository
    {
        #region # 获取信息系统编号列表 —— IEnumerable<string> FindAllNos()
        /// <summary>
        /// 获取信息系统编号列表
        /// </summary>
        /// <returns>信息系统编号列表</returns>
        public IEnumerable<string> FindAllNos()
        {
            return this.FindNos(x => true).AsEnumerable();
        }
        #endregion

        #region # 获取信息系统字典 —— IDictionary<string, InfoSystem> FindDictionary()
        /// <summary>
        /// 获取信息系统字典
        /// </summary>
        /// <returns>信息系统字典</returns>
        public IDictionary<string, InfoSystem> FindDictionary()
        {
            IDictionary<string, InfoSystem> dictionary = new Dictionary<string, InfoSystem>();

            foreach (InfoSystem system in this.FindAll())
            {
                dictionary.Add(system.Number, system);
            }

            return dictionary;
        }
        #endregion
    }
}
