using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.EventSources.AuthorizationContext;
using SD.IdentitySystem.Domain.IRepositories;
using SD.Infrastructure.Constants;
using SD.Infrastructure.EventBase;

namespace SD.IdentitySystem.DomainEventHandler.AuthorizationContext
{
    /// <summary>
    /// 信息系统已创建事件处理者
    /// </summary>
    public class InfoSystemCreatedEventHandler : IEventHandler<InfoSystemCreatedEvent>
    {
        #region # 字段及依赖注入构造器
        /// <summary>
        /// 单元事务
        /// </summary>
        private readonly IUnitOfWorkIdentity _unitOfWork;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="unitOfWork">单元事务</param>
        public InfoSystemCreatedEventHandler(IUnitOfWorkIdentity unitOfWork)
        {
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
            //获取超级管理员用户
            User admin = this._unitOfWork.Resolve<User>(CommonConstants.AdminLoginId);

            //创建系统管理员用户
            string adminName = $"{eventSource.SystemName}管理员";
            User systemAdmin = new User(eventSource.AdminLoginId, adminName, CommonConstants.InitialPassword);

            //创建系统管理员角色
            Role adminRole = new Role("系统管理员", eventSource.SystemNo, "系统管理员", CommonConstants.ManagerRoleNo);

            //为超级管理员与系统管理员追加角色
            admin.AppendRoles(new[] { adminRole });
            systemAdmin.AppendRoles(new[] { adminRole });

            //创建系统根级菜单
            Menu systemMenu = new Menu(eventSource.SystemNo, eventSource.ApplicationType, eventSource.SystemName, 0, null, null, null, null);

            this._unitOfWork.RegisterAdd(systemAdmin);
            this._unitOfWork.RegisterAdd(adminRole);
            this._unitOfWork.RegisterAdd(systemMenu);
            this._unitOfWork.RegisterSave(admin);
            this._unitOfWork.Commit();
        }
        #endregion
    }
}
