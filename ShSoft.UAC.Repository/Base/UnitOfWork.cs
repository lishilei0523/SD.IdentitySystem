using ShSoft.Framework2016.Infrastructure.Repository.EntityFrameworkProvider;
using ShSoft.UAC.Domain.IRepositories;

namespace ShSoft.UAC.Repository.Base
{
    /// <summary>
    /// 单元事务 - 人资
    /// </summary>
    public sealed class UnitOfWork : EFUnitOfWorkProvider, IUnitOfWorkUAC
    {

    }
}
