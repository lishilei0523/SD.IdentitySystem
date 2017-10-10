using SD.IdentitySystem.Domain.IRepositories.Interfaces;

namespace SD.IdentitySystem.Domain.Mediators
{
    /// <summary>
    /// 仓储中介者
    /// </summary>
    public sealed class RepositoryMediator
    {
        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public RepositoryMediator(IServerRepository serverRep, IInfoSystemRepository systemRep, IUserRepository userRep, IAuthorityRepository authorityRep, IMenuRepository menuRep, IRoleRepository roleRep, ILoginRecordRepository loginRecordRep)
        {
            this.ServerRep = serverRep;
            this.InfoSystemRep = systemRep;
            this.UserRep = userRep;
            this.AuthorityRep = authorityRep;
            this.MenuRep = menuRep;
            this.RoleRep = roleRep;
            this.LoginRecordRep = loginRecordRep;
        }

        /// <summary>
        /// 服务器仓储接口
        /// </summary>
        public IServerRepository ServerRep { get; private set; }

        /// <summary>
        /// 信息系统仓储接口
        /// </summary>
        public IInfoSystemRepository InfoSystemRep { get; private set; }

        /// <summary>
        /// 用户仓储接口
        /// </summary>
        public IUserRepository UserRep { get; private set; }

        /// <summary>
        /// 权限仓储接口
        /// </summary>
        public IAuthorityRepository AuthorityRep { get; private set; }

        /// <summary>
        /// 菜单仓储接口
        /// </summary>
        public IMenuRepository MenuRep { get; private set; }

        /// <summary>
        /// 角色仓储接口
        /// </summary>
        public IRoleRepository RoleRep { get; private set; }

        /// <summary>
        /// 登录记录仓储接口
        /// </summary>
        public ILoginRecordRepository LoginRecordRep { get; private set; }
    }
}
