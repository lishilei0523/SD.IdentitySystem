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

            if (infoSystems == null)
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

        #region # 根据信息系统类别获取信息系统编号集 —— IEnumerable<string> GetInfoSystemNos(...
        /// <summary>
        /// 根据信息系统类别获取信息系统编号集
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>信息系统集</returns>
        public IEnumerable<string> GetInfoSystemNos(string systemKindNo)
        {
            return this.FindNos(x => x.SystemKindNo == systemKindNo).AsEnumerable();
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
