using System.Configuration;
using System.Data.Entity;
using ShSoft.Framework2016.Infrastructure.Constants;
using ShSoft.Framework2016.Infrastructure.Repository.EntityFrameworkProvider.Base;
using Configuration = SD.UAC.Repository.Migrations.Configuration;

namespace SD.UAC.Repository.Base
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
