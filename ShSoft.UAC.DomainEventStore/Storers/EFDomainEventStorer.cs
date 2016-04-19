using System.Data.Entity;
using ShSoft.Framework2016.Infrastructure.DomainEvent.EFStorer.Provider;
using ShSoft.UAC.DomainEventStore.Migrations;

namespace ShSoft.UAC.DomainEventStore.Storers
{
    /// <summary>
    /// 领域事件存储者 - EF实现
    /// </summary>
    public class EFDomainEventStorer : EntityFrameworkStorerProvider
    {
        /// <summary>
        /// 静态构造器
        /// </summary>
        static EFDomainEventStorer()
        {
            //数据迁移
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EFDomainEventStorer, Configuration>());
        }
    }
}
