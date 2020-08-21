using System.Data.Entity.Migrations;
using SD.IdentitySystem.Repository.Base;

namespace SD.IdentitySystem.Repository.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DbSession>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DbSession context)
        {

        }
    }
}
