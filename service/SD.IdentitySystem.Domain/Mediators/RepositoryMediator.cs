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
        /// <param name="systemRep">信息系统仓储接口</param>
        /// <param name="userRep">用户仓储接口</param>
        /// <param name="authorityRep">权限仓储接口</param>
        /// <param name="menuRep">菜单仓储接口</param>
        /// <param name="roleRep">角色仓储接口</param>
        /// <param name="loginRecordRep">登录记录仓储接口</param>
        /// <param name="userRoleRep">用户角色仓储接口</param>
        public RepositoryMediator(IInfoSystemRepository systemRep, IUserRepository userRep, IAuthorityRepository authorityRep, IMenuRepository menuRep, IRoleRepository roleRep, ILoginRecordRepository loginRecordRep, IUserRoleRepository userRoleRep)
        {
            this.InfoSystemRep = systemRep;
            this.UserRep = userRep;
            this.AuthorityRep = authorityRep;
            this.MenuRep = menuRep;
            this.RoleRep = roleRep;
            this.LoginRecordRep = loginRecordRep;
            this.UserRoleRep = userRoleRep;
        }

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

        /// <summary>
        /// 用户角色仓储接口
        /// </summary>
        public IUserRoleRepository UserRoleRep { get; private set; }
    }
}
