using System.Data.Entity.Migrations;
using ShSoft.Framework2016.Infrastructure.IRepository;
using ShSoft.UAC.Repository.Base;

namespace ShSoft.UAC.Repository.Migrations
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
            //初始化数据
            IDataInitializer initializer = new DataInitializer(context);
            initializer.Initialize();
        }
    }
}
