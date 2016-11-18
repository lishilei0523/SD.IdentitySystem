using System.Collections.Generic;
using System.Transactions;
using SD.AOP.Core.Aspects.ForMethod;
using SD.CacheManager;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using SD.IdentitySystem.Domain.Mediators;
using ShSoft.Infrastructure.Global;
using ShSoft.Infrastructure.Global.Transaction;
using ShSoft.Infrastructure.RepositoryBase;
using ShSoft.ValueObjects;
using ShSoft.ValueObjects.Enums;

namespace SD.IdentitySystem.Repository.Base
{
    /// <summary>
    /// 数据初始化器实现
    /// </summary>
    public class DataInitializer : IDataInitializer
    {
        #region # 字段及依赖注入构造器

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
        /// <param name="repMediator">仓储中介者</param>
        /// <param name="unitOfWork">单元事务</param>
        public DataInitializer(RepositoryMediator repMediator, IUnitOfWorkIdentity unitOfWork)
        {
            this._repMediator = repMediator;
            this._unitOfWork = unitOfWork;
        }

        #endregion


        //Implements

        #region # 初始化基础数据 —— void Initialize()
        /// <summary>
        /// 初始化基础数据
        /// </summary>
        [TransactionAspect(TransactionScopeOption.RequiresNew)]
        public void Initialize()
        {
            Initializer.InitSessionId();

            this.InitInfoSystemKinds();
            this.InitAdmin();
            this.InitPredefineRoles();
            this.InitInfoSystems();
        }
        #endregion


        //Private

        #region # 初始化信息系统类别 —— void InitInfoSystemKinds()
        /// <summary>
        /// 初始化信息系统类别
        /// </summary>
        private void InitInfoSystemKinds()
        {
            if (this._repMediator.InfoSystemKindRep.Count() == 0)
            {
                IList<InfoSystemKind> systemKinds = new List<InfoSystemKind>();
                systemKinds.Add(new InfoSystemKind("01", "市场端", ApplicationType.Web));
                systemKinds.Add(new InfoSystemKind("03", "设计端", ApplicationType.Web));
                systemKinds.Add(new InfoSystemKind("05", "销售端", ApplicationType.Web));
                systemKinds.Add(new InfoSystemKind("06", "工程端", ApplicationType.Web));
                systemKinds.Add(new InfoSystemKind("10", "配送中心", ApplicationType.Web));
                systemKinds.Add(new InfoSystemKind("11", "供应商", ApplicationType.Web));
                systemKinds.Add(new InfoSystemKind("17", "管理中心", ApplicationType.Web));
                systemKinds.Add(new InfoSystemKind("20", "人资端", ApplicationType.Web));
                systemKinds.Add(new InfoSystemKind("21", "财务端", ApplicationType.Windows));
                systemKinds.Add(new InfoSystemKind("23", "档案室", ApplicationType.Windows));
                systemKinds.Add(new InfoSystemKind("33", "消息中心", ApplicationType.Web));
                systemKinds.Add(new InfoSystemKind("80", "身份认证", ApplicationType.Web));

                this._repMediator.InfoSystemKindRep.AddRange(systemKinds);
            }
        }
        #endregion

        #region # 初始化超级管理员 —— void InitAdmin()
        /// <summary>
        /// 初始化超级管理员
        /// </summary>
        private void InitAdmin()
        {
            if (!this._repMediator.UserRep.Exists(Constants.AdminLoginId))
            {
                User admin = new User(Constants.AdminLoginId, "超级管理员", Constants.InitialPassword);

                this._unitOfWork.RegisterAdd(admin);
                this._unitOfWork.Commit();
            }
        }
        #endregion

        #region # 初始化预定义角色 —— void InitPredefineRoles()
        /// <summary>
        /// 初始化预定义角色
        /// </summary>
        private void InitPredefineRoles()
        {
            if (this._repMediator.RoleRep.Count() == 0)
            {
                IEnumerable<InfoSystemKind> systemKinds = this._repMediator.InfoSystemKindRep.FindAll();

                IList<Role> predefineRoles = new List<Role>();

                foreach (InfoSystemKind systemKind in systemKinds)
                {
                    Role adminRole = new Role("系统管理员", systemKind.Number, "系统管理员", Constants.ManagerRoleNo);

                    IEnumerable<Authority> specAuthorities = this._unitOfWork.ResolveAuthorities(systemKind.Number);

                    adminRole.SetAuthorities(specAuthorities);

                    predefineRoles.Add(adminRole);
                }

                this._unitOfWork.RegisterAddRange(predefineRoles);
                this._unitOfWork.Commit();
            }
        }
        #endregion

        #region # 初始化信息系统 —— void InitInfoSystems()
        /// <summary>
        /// 初始化信息系统
        /// </summary>
        private void InitInfoSystems()
        {
            if (this._repMediator.InfoSystemRep.Count() == 0)
            {
                IList<InfoSystem> systems = new List<InfoSystem>();
                systems.Add(new InfoSystem("01", "市场端", "01", "market"));
                systems.Add(new InfoSystem("03", "设计端", "03", "design"));
                systems.Add(new InfoSystem("05", "销售端", "05", "sales"));
                systems.Add(new InfoSystem("06", "工程端", "06", "project"));
                systems.Add(new InfoSystem("10", "配送中心", "10", "warehouse"));
                systems.Add(new InfoSystem("17", "管理中心", "17", "manager"));
                systems.Add(new InfoSystem("20", "人资端", "20", "hrm"));
                systems.Add(new InfoSystem("21", "财务端", "21", "finance"));
                systems.Add(new InfoSystem("23", "档案室", "23", "archive"));
                systems.Add(new InfoSystem("33", "消息中心", "33", "message"));
                systems.Add(new InfoSystem("80", "身份认证", "80", "identity"));

                this._unitOfWork.RegisterAddRange(systems);
                this._unitOfWork.UnitedCommit();

                //清除缓存
                CacheMediator.Remove(typeof(IInfoSystemRepository).FullName);
            }
        }
        #endregion
    }
}
