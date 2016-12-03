using System.Collections.Generic;
using System.Data;
using System.Linq;
using SD.CacheManager;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using SD.Toolkits.EntityFramework.Extensions;
using ShSoft.Common.PoweredByLee;
using ShSoft.Infrastructure.Constants;
using ShSoft.Infrastructure.Repository.EntityFramework;

namespace SD.IdentitySystem.Repository.Implements
{
    /// <summary>
    /// 信息系统仓储实现
    /// </summary>
    public class InfoSystemRepository : EFRepositoryProvider<InfoSystem>, IInfoSystemRepository
    {
        #region # 获取信息系统列表 —— override IQueryable<InfoSystem> FindAllInner()
        /// <summary>
        /// 获取信息系统列表
        /// </summary>
        /// <returns>信息系统列表</returns>
        protected override IQueryable<InfoSystem> FindAllInner()
        {
            IList<InfoSystem> infoSystems = CacheMediator.Get<IList<InfoSystem>>(typeof(IInfoSystemRepository).FullName);

            if (infoSystems == null || !infoSystems.Any())
            {
                IQueryable<InfoSystem> systems = base.FindAllInner();

                string sql = systems.ParseSql();

                SqlHelper sqlHelper = new SqlHelper(WebConfigSetting.DefaultConnectionString);
                DataTable dateTable = sqlHelper.GetDataTable(sql);

                infoSystems = dateTable.ToList<InfoSystem>();

                CacheMediator.Set(typeof(IInfoSystemRepository).FullName, infoSystems);
            }

            return infoSystems.AsQueryable();
        }
        #endregion

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
