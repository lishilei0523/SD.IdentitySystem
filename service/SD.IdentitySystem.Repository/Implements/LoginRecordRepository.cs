using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using ShSoft.Infrastructure.Repository.EntityFramework;

namespace SD.IdentitySystem.Repository.Implements
{
    /// <summary>
    /// 登录记录仓储实现
    /// </summary>
    public class LoginRecordRepository : EFRepositoryProvider<LoginRecord>, ILoginRecordRepository
    {

    }
}
