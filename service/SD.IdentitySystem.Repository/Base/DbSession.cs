using SD.Infrastructure.Constants;
using SD.Infrastructure.Repository.EntityFramework.Base;
using System.Configuration;
using System.Data.Entity;
using Configuration = SD.IdentitySystem.Repository.Migrations.Configuration;

namespace SD.IdentitySystem.Repository.Base
{
    /// <summary>
    /// EF上下文
    /// </summary>
    internal class DbSession : BaseDbSession
    {
        /// <summary>
        /// 静态构造器
        /// </summary>
        static DbSession()
        {
            //读取配置文件，是否开启自动数据迁移
            bool enableMiagration = bool.Parse(ConfigurationManager.AppSettings[CommonConstants.AutoMigrationAppSettingKey]);

            if (enableMiagration)
            {
                //数据迁移
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<DbSession, Configuration>());
            }
        }
    }
}
