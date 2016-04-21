using SD.UAC.Domain.IRepositories.Interfaces;

namespace SD.UAC.Domain.Mediators
{
    /// <summary>
    /// 仓储中介者
    /// </summary>
    public sealed class RepositoryMediator
    {
        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="departmentRep">部门仓储接口</param>
        /// <param name="employeeRep">员工仓储接口</param>
        /// <param name="infoSystemKindRep">信息系统类别仓储接口</param>
        /// <param name="infoSystemRep">信息系统仓储接口</param>
        /// <param name="organizationRep">组织仓储接口</param>
        /// <param name="userRep">用户仓储接口</param>
        /// <param name="positionRep">岗位仓储接口</param>
        /// <param name="authorityRep">权限仓储接口</param>
        /// <param name="menuRep">菜单仓储接口</param>
        /// <param name="roleRep">角色仓储接口</param>
        public RepositoryMediator(IInfoSystemKindRepository infoSystemKindRep, IInfoSystemRepository infoSystemRep, IUserRepository userRep, IAuthorityRepository authorityRep, IMenuRepository menuRep, IRoleRepository roleRep)
        {
            this.InfoSystemKindRep = infoSystemKindRep;
            this.InfoSystemRep = infoSystemRep;
            this.UserRep = userRep;
            this.AuthorityRep = authorityRep;
            this.MenuRep = menuRep;
            this.RoleRep = roleRep;
        }

        /// <summary>
        /// 信息系统类别仓储接口
        /// </summary>
        public IInfoSystemKindRepository InfoSystemKindRep { get; private set; }

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
    }
}
