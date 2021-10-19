using SD.IdentitySystem.Domain.IRepositories;
using SD.Infrastructure.Repository.EntityFrameworkCore;

namespace SD.IdentitySystem.Repository.Base
{
    /// <summary>
    /// 单元事务 - 统一身份认证
    /// </summary>
    public sealed class UnitOfWork : EFUnitOfWorkProvider, IUnitOfWorkIdentity
    {

    }
}
