using SD.IdentitySystem.Repository.Base;
using SD.Infrastructure;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;

namespace SD.IdentitySystem.Repository.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DbSession>
    {
        public Configuration()
        {
            this.TargetDatabase = new DbConnectionInfo(FrameworkSection.Setting.DatabaseWriteConnectionName.Value);
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DbSession context)
        {

        }
    }
}
