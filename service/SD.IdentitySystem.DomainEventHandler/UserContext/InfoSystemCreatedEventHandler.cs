using System;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.EventSources.UserContext;
using SD.IdentitySystem.Domain.IRepositories;
using SD.IdentitySystem.Domain.Mediators;
using ShSoft.Infrastructure.EventBase;
using ShSoft.ValueObjects;

namespace SD.IdentitySystem.DomainEventHandler.UserContext
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
        private readonly IUnitOfWorkIdentity _unitOfWork;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="svcMediator">领域服务中介者</param>
        /// <param name="repMediator">仓储中介者</param>
        /// <param name="unitOfWork">单元事务</param>
        public InfoSystemCreatedEventHandler(DomainServiceMediator svcMediator, RepositoryMediator repMediator, IUnitOfWorkIdentity unitOfWork)
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
            //获取系统管理员角色
            Guid adminRoleId = this._repMediator.RoleRep.GetManagerRoleId(eventSource.SystemNo);
            Role adminRole = this._unitOfWork.Resolve<Role>(adminRoleId);

            //创建系统管理员用户，并为用户分配角色
            string adminName = string.Format("{0}管理员", eventSource.SystemName);
            User systemAdmin = new User(eventSource.AdminLoginId, adminName, Constants.InitialPassword);
            systemAdmin.SetRoles(eventSource.SystemNo, new[] { adminRole });

            //获取超级管理员，并为超级管理员追加角色
            User admin = this._unitOfWork.Resolve<User>(Constants.AdminLoginId);
            admin.AppendRoles(eventSource.SystemNo, new[] { adminRole });

            this._unitOfWork.RegisterAdd(systemAdmin);
            this._unitOfWork.RegisterSave(admin);
            this._unitOfWork.Commit();
        }
        #endregion
    }
}
