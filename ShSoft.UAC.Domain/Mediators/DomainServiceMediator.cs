using ShSoft.UAC.Domain.IDomainServices;

namespace ShSoft.UAC.Domain.Mediators
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
        /// <param name="infoSystemKindSvc">信息系统类别领域服务接口</param>
        /// <param name="infoSystemSvc">信息系统领域服务接口</param>
        /// <param name="numberSvc">编号领域服务</param>
        public DomainServiceMediator(IUserService userSvc, IInfoSystemKindService infoSystemKindSvc, IInfoSystemService infoSystemSvc, INumberService numberSvc)
        {
            this.UserSvc = userSvc;
            this.InfoSystemKindSvc = infoSystemKindSvc;
            this.InfoSystemSvc = infoSystemSvc;
            this.NumberSvc = numberSvc;
        }

        /// <summary>
        /// 信息系统类别领域服务接口
        /// </summary>
        public IInfoSystemKindService InfoSystemKindSvc { get; private set; }

        /// <summary>
        /// 信息系统领域服务接口
        /// </summary>
        public IInfoSystemService InfoSystemSvc { get; private set; }

        /// <summary>
        /// 用户领域服务接口
        /// </summary>
        public IUserService UserSvc { get; private set; }

        /// <summary>
        /// 编号领域服务接口
        /// </summary>
        public INumberService NumberSvc { get; private set; }
    }
}
