using Microsoft.EntityFrameworkCore;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Repository.EntityFrameworkCore.Base;

namespace SD.IdentitySystem.Repository.Base
{
    /// <summary>
    /// EF Core上下文
    /// </summary>
    internal class DbSession : DbSessionBase
    {
        /// <summary>
        /// 配置
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GlobalSetting.WriteConnectionString, options =>
            {
                options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                options.UseCompatibilityLevel(110);//兼容级别：SQL Server 2012
            });
            base.OnConfiguring(optionsBuilder);
        }
    }
}
