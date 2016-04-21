using SD.UAC.Domain.IRepositories;
using ShSoft.Framework2016.Infrastructure.Repository.EntityFrameworkProvider;

namespace SD.UAC.Repository.Base
{
    /// <summary>
    /// 单元事务 - 人资
    /// </summary>
    public sealed class UnitOfWork : EFUnitOfWorkProvider, IUnitOfWorkUAC
    {

    }
}
