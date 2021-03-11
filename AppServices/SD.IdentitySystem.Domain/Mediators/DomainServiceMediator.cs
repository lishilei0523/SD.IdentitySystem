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
        public DomainServiceMediator(IRoleService roleSvc)
        {
            this.RoleSvc = roleSvc;
        }

        /// <summary>
        /// 角色领域服务接口
        /// </summary>
        public IRoleService RoleSvc { get; private set; }
    }
}
