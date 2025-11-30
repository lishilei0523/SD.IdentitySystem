using SD.IdentitySystem.Domain.IRepositories.Interfaces;

namespace SD.IdentitySystem.Domain.Mediators
{
    /// <summary>
    /// 仓储中介者
    /// </summary>
    public sealed class RepositoryMediator
    {
        #region # 依赖注入构造器

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public RepositoryMediator(IInfoSystemRepository infoSystemRep, IUserRepository userRep, IRoleRepository roleRep, IMenuRepository menuRep, IAuthorityRepository authorityRep, ILoginRecordRepository loginRecordRep)
        {
            this.InfoSystemRep = infoSystemRep;
            this.UserRep = userRep;
            this.RoleRep = roleRep;
            this.MenuRep = menuRep;
            this.AuthorityRep = authorityRep;
            this.LoginRecordRep = loginRecordRep;
        }

        #endregion

        #region # 属性

        /// <summary>
        /// 信息系统仓储接口
        /// </summary>
        public IInfoSystemRepository InfoSystemRep { get; private set; }

        /// <summary>
        /// 用户仓储接口
        /// </summary>
        public IUserRepository UserRep { get; private set; }

        /// <summary>
        /// 角色仓储接口
        /// </summary>
        public IRoleRepository RoleRep { get; private set; }

        /// <summary>
        /// 菜单仓储接口
        /// </summary>
        public IMenuRepository MenuRep { get; private set; }

        /// <summary>
        /// 权限仓储接口
        /// </summary>
        public IAuthorityRepository AuthorityRep { get; private set; }

        /// <summary>
        /// 登录记录仓储接口
        /// </summary>
        public ILoginRecordRepository LoginRecordRep { get; private set; }

        #endregion
    }
}
