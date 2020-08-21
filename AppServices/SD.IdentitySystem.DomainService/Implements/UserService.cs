using SD.IdentitySystem.Domain.IDomainServices;
using SD.IdentitySystem.Domain.Mediators;

namespace SD.IdentitySystem.DomainService.Implements
{
    /// <summary>
    /// 用户领域服务实现
    /// </summary>
    public class UserService : IUserService
    {
        #region # 字段及依赖注入构造器

        /// <summary>
        /// 仓储中介者
        /// </summary>
        private readonly RepositoryMediator _repMediator;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="repMediator">仓储中介者</param>
        public UserService(RepositoryMediator repMediator)
        {
            this._repMediator = repMediator;
        }

        #endregion
    }
}
