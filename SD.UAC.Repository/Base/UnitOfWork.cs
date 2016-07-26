using SD.UAC.Domain.IRepositories;
using ShSoft.Infrastructure.Repository.EntityFramework;

namespace SD.UAC.Repository.Base
{
    /// <summary>
    /// 单元事务 - 人资
    /// </summary>
    public sealed class UnitOfWork : EFUnitOfWorkProvider, IUnitOfWorkUAC
    {

    }
}
