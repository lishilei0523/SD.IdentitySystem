using SD.IdentitySystem.Repository.Base;
using System.Data.Entity.Migrations;

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
