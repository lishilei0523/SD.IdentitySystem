using System;
using ShSoft.Framework2016.Infrastructure.Repository.EntityFrameworkProvider;
using ShSoft.UAC.Domain.Entities;
using ShSoft.UAC.Domain.IRepositories.Interfaces;

namespace ShSoft.UAC.Repository.Implements
{
    /// <summary>
    /// 用户仓储实现
    /// </summary>
    public class UserRepository : EFRepositoryProvider<User>, IUserRepository
    {

    }
}
