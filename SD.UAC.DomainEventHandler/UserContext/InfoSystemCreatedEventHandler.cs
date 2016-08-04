using System.Collections.Generic;
using SD.UAC.Common;
using SD.UAC.Domain.Entities;
using SD.UAC.Domain.EventSources.UserContext;
using SD.UAC.Domain.IRepositories;
using SD.UAC.Domain.Mediators;
using ShSoft.Infrastructure.EventBase;

namespace SD.UAC.DomainEventHandler.UserContext
{
    /// <summary>
    /// 信息系统已创建事件处理者
    /// </summary>
    public class InfoSystemCreatedEventHandler : IEventHandler<InfoSystemCreatedEvent>
    {
        #region # 字段及依赖注入构造器

        /// <summary>
        /// 领域服务中介者
        /// </summary>
        private readonly DomainServiceMediator _svcMediator;

        /// <summary>
        /// 仓储中介者
        /// </summary>
        private readonly RepositoryMediator _repMediator;

        /// <summary>
        /// 单元事务
        /// </summary>
        private readonly IUnitOfWorkUAC _unitOfWork;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="svcMediator">领域服务中介者</param>
        /// <param name="repMediator">仓储中介者</param>
        /// <param name="unitOfWork">单元事务</param>
        public InfoSystemCreatedEventHandler(DomainServiceMediator svcMediator, RepositoryMediator repMediator, IUnitOfWorkUAC unitOfWork)
        {
            this._svcMediator = svcMediator;
            this._repMediator = repMediator;
            this._unitOfWork = unitOfWork;
            this.Sort = uint.MaxValue;
        }

        #endregion

        #region # 执行顺序，倒序排列 —— uint Sort
        /// <summary>
        /// 执行顺序，倒序排列
        /// </summary>
        public uint Sort { get; private set; }
        #endregion

        #region # 事件处理方法 —— void Handle(InfoSystemCreatedEvent eventSource)
        /// <summary>
        /// 事件处理方法
        /// </summary>
        /// <param name="eventSource">事件源</param>
        public void Handle(InfoSystemCreatedEvent eventSource)
        {
            //创建系统管理员角色
            Role adminRole = new Role(Constants.ManagerRoleName, eventSource.SystemKindNo, eventSource.SystemNo, Constants.ManagerRoleName);

            //为角色分配权限
            IEnumerable<Authority> authorities = this._unitOfWork.ResolveAuthorities(eventSource.SystemKindNo);
            adminRole.SetAuthorities(authorities);

            //为用户分配角色
            User admin = this._unitOfWork.Resolve<User>(eventSource.AdminLoginId);
            admin.SetRoles(new[] { adminRole });

            this._unitOfWork.RegisterAdd(adminRole);
            this._unitOfWork.RegisterSave(admin);
            this._unitOfWork.Commit();
        }
        #endregion
    }
}
