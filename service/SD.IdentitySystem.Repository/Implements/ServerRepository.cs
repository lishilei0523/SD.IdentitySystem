using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using SD.Infrastructure.Repository.EntityFramework;

namespace SD.IdentitySystem.Repository.Implements
{
    /// <summary>
    /// 服务器仓储实现
    /// </summary>
    public class ServerRepository : EFAggRootRepositoryProvider<Server>, IServerRepository
    {

    }
}
