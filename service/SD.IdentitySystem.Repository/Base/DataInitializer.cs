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
using System.Collections.Generic;
using System.Transactions;

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

            this.InitInfoSystems();
            this.InitAdmin();
            this.InitPredefineRoles();
            this.InitIdentitySystemMenus();
        }
        #endregion


        //Private

        #region # 初始化信息系统 —— void InitInfoSystems()
        /// <summary>
        /// 初始化信息系统
        /// </summary>
        private void InitInfoSystems()
        {
            if (this._repMediator.InfoSystemRep.Count() == 0)
            {
                IList<InfoSystem> systems = new List<InfoSystem>();

                systems.Add(new InfoSystem("00", "身份认证", "identity"));
                systems.Add(new InfoSystem("01", "市场端", "market"));
                systems.Add(new InfoSystem("02", "销售端", "sales"));
                systems.Add(new InfoSystem("03", "工程端", "project"));
                systems.Add(new InfoSystem("04", "人资端", "hrm"));
                systems.Add(new InfoSystem("05", "财务端", "finance"));

                this._unitOfWork.RegisterAddRange(systems);
                this._unitOfWork.UnitedCommit();

                //清除缓存
                CacheMediator.Remove(typeof(IInfoSystemRepository).FullName);
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
                IEnumerable<InfoSystem> systems = this._repMediator.InfoSystemRep.FindAll();

                IList<Role> predefineRoles = new List<Role>();

                foreach (InfoSystem system in systems)
                {
                    Role adminRole = new Role("系统管理员", system.Number, "系统管理员", Constants.ManagerRoleNo);

                    IEnumerable<Authority> specAuthorities = this._unitOfWork.ResolveAuthorities(system.Number);

                    adminRole.SetAuthorities(specAuthorities);

                    predefineRoles.Add(adminRole);
                }

                this._unitOfWork.RegisterAddRange(predefineRoles);
                this._unitOfWork.Commit();
            }
        }
        #endregion

        #region # 初始化身份认证系统菜单 —— void InitIdentitySystemMenus()
        /// <summary>
        /// 初始化身份认证系统菜单
        /// </summary>
        private void InitIdentitySystemMenus()
        {
            Menu userManagement = new Menu("00", "用户管理", 1, "", null, null);
            Menu roleManagement = new Menu("00", "角色管理", 2, "", null, null);
            Menu menuManagement = new Menu("00", "菜单管理", 3, "", null, null);
            Menu authorityManagement = new Menu("00", "权限管理", 4, "", null, null);

            this._unitOfWork.RegisterAdd(userManagement);
            this._unitOfWork.RegisterAdd(roleManagement);
            this._unitOfWork.RegisterAdd(menuManagement);
            this._unitOfWork.RegisterAdd(authorityManagement);

            this._unitOfWork.Commit();
        }
        #endregion
    }
}
