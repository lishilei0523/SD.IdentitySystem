using SD.UAC.Domain.Entities;
using SD.UAC.Domain.IRepositories.Interfaces;
using ShSoft.Framework2016.Infrastructure.Repository.EntityFrameworkProvider;

namespace SD.UAC.Repository.Implements
{
    /// <summary>
    /// 用户仓储实现
    /// </summary>
    public class UserRepository : EFRepositoryProvider<User>, IUserRepository
    {

    }
}
