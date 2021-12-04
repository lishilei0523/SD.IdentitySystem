using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.EventSources.AuthorizationContext;
using SD.IdentitySystem.Domain.IRepositories;
using SD.IdentitySystem.Domain.Mediators;
using SD.Infrastructure.EventBase;
using System;

namespace SD.IdentitySystem.DomainEventHandler.AuthorizationContext
{
    /// <summary>
    /// 权限创建事件处理者
    /// </summary>
    /// <remarks>
    /// 处理流程：
    /// *1、查询出给定信息系统
    /// *2、为系统管理员角色追加最新权限
    /// </remarks>
    public class AuthorityCreatedEventHandler : IEventHandler<AuthorityCreatedEvent>
    {
        #region # 字段及构造器

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
        public AuthorityCreatedEventHandler(RepositoryMediator repMediator, IUnitOfWorkIdentity unitOfWork)
        {
            this._repMediator = repMediator;
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region # 事件处理方法 —— void Handle(AuthorityCreatedEvent eventSource)
        /// <summary>
        /// 事件处理方法
        /// </summary>
        /// <param name="eventSource">事件源</param>
        public void Handle(AuthorityCreatedEvent eventSource)
        {
            this.AppendAuthorities(eventSource.InfoSystemNo, eventSource.AuthorityId);
        }
        #endregion

        #region # 追加权限 —— void AppendAuthorities(string infoSystemNo, Guid authorityId)
        /// <summary>
        /// 追加权限
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="authorityId">权限Id</param>
        private void AppendAuthorities(string infoSystemNo, Guid authorityId)
        {
            Authority authority = this._unitOfWork.Resolve<Authority>(authorityId);

            //为系统管理员角色追加权限
            Guid adminRoleId = this._repMediator.RoleRep.GetManagerRoleId(infoSystemNo);
            Role adminRole = this._unitOfWork.Resolve<Role>(adminRoleId);
            adminRole.AppendAuthorities(new[] { authority });

            this._unitOfWork.RegisterSave(adminRole);
            this._unitOfWork.Commit();
        }
        #endregion
    }
}
