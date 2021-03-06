﻿using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using SD.Infrastructure.Repository.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Repository.Implements
{
    /// <summary>
    /// 信息系统仓储实现
    /// </summary>
    public class InfoSystemRepository : EFAggRootRepositoryProvider<InfoSystem>, IInfoSystemRepository
    {
        #region # 获取全部信息系统列表 —— new ICollection<InfoSystem> FindAll()
        /// <summary>
        /// 获取全部信息系统列表
        /// </summary>
        /// <returns>信息系统列表</returns>
        public new ICollection<InfoSystem> FindAll()
        {
            IQueryable<InfoSystem> infoSystems = base.FindAllInner().OrderBy(x => x.Number);

            return infoSystems.ToList();
        }
        #endregion

        #region # 获取信息系统字典 —— IDictionary<string, InfoSystem> FindDictionary()
        /// <summary>
        /// 获取信息系统字典
        /// </summary>
        /// <returns>信息系统字典</returns>
        public IDictionary<string, InfoSystem> FindDictionary()
        {
            IDictionary<string, InfoSystem> dictionary = this.FindAll().ToDictionary(x => x.Number, x => x);

            return dictionary;
        }
        #endregion
    }
}
