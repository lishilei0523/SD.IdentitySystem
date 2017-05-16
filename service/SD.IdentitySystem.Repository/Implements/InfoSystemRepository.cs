using System.Collections.Generic;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using SD.Infrastructure.Repository.EntityFramework;

namespace SD.IdentitySystem.Repository.Implements
{
    /// <summary>
    /// 信息系统仓储实现
    /// </summary>
    public class InfoSystemRepository : EFAggRootRepositoryProvider<InfoSystem>, IInfoSystemRepository
    {
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
