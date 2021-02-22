using SD.IdentitySystem.Domain.IDomainServices;

namespace SD.IdentitySystem.Domain.Mediators
{
    /// <summary>
    /// 领域服务中介者
    /// </summary>
    public class DomainServiceMediator
    {
        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="userSvc">用户领域服务接口</param>
        /// <param name="systemSvc">信息系统领域服务接口</param>
        /// <param name="roleSvc">角色领域服务接口</param>
        public DomainServiceMediator(IUserService userSvc, IInfoSystemService systemSvc, IRoleService roleSvc)
        {
            this.UserSvc = userSvc;
            this.InfoSystemSvc = systemSvc;
            this.RoleSvc = roleSvc;
        }

        /// <summary>
        /// 信息系统领域服务接口
        /// </summary>
        public IInfoSystemService InfoSystemSvc { get; private set; }

        /// <summary>
        /// 用户领域服务接口
        /// </summary>
        public IUserService UserSvc { get; private set; }

        /// <summary>
        /// 角色领域服务接口
        /// </summary>
        public IRoleService RoleSvc { get; private set; }
    }
}
