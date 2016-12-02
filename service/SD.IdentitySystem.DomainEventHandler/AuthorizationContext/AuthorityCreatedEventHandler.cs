using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.EventSources.AuthorizationContext;
using SD.IdentitySystem.Domain.IRepositories;
using SD.IdentitySystem.Domain.Mediators;
using ShSoft.Infrastructure.EventBase;
using System;
using System.Collections.Generic;

namespace SD.IdentitySystem.DomainEventHandler.AuthorizationContext
{
    /// <summary>
    /// 权限创建事件处理者
    /// </summary>
    /// <remarks>
    /// 处理流程：
    /// *1、查询出给定信息系统类别的所有信息系统
    /// *2、为系统管理员角色追加最新权限
    /// </remarks>
    public class AuthorityCreatedEventHandler : IEventHandler<AuthorityCreatedEvent>
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
        public AuthorityCreatedEventHandler(DomainServiceMediator svcMediator, RepositoryMediator repMediator, IUnitOfWorkIdentity unitOfWork)
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

        #region # 事件处理方法 —— void Handle(AuthorityCreatedEvent eventSource)
        /// <summary>
        /// 事件处理方法
        /// </summary>
        /// <param name="eventSource">事件源</param>
        public void Handle(AuthorityCreatedEvent eventSource)
        {
            this.AppendAuthorities(eventSource.SystemNo, eventSource.AuthorityId);
        }
        #endregion

        #region # 追加权限 —— void AppendAuthorities(string systemKindNo, Guid authorityId)
        /// <summary>
        /// 追加权限
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="authorityId">权限Id</param>
        private void AppendAuthorities(Guid authorityId)
        {
            Authority currentAuthority = this._unitOfWork.Resolve<Authority>(authorityId);

            //根据类别获取所有信息系统
            IEnumerable<string> systemNos = this._repMediator.InfoSystemRep.FindAllNos();

            foreach (string systemNo in systemNos)
            {
                //为系统管理员角色追加权限
                Guid adminRoleId = this._repMediator.RoleRep.GetManagerRoleId(systemNo);
                Role adminRole = this._unitOfWork.Resolve<Role>(adminRoleId);
                adminRole.AppendAuthorities(new[] { currentAuthority });

                //注册保存
                this._unitOfWork.RegisterSave(adminRole);
            }

            //提交
            this._unitOfWork.Commit();
        }
        #endregion
    }
}
