using SD.AOP.Core.Aspects.ForMethod;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories;
using SD.IdentitySystem.Domain.Mediators;
using ShSoft.Infrastructure.Global;
using ShSoft.Infrastructure.RepositoryBase;
using ShSoft.ValueObjects;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Transactions;
using SD.CacheManager;

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
        /// 信息系统列表
        /// </summary>
        private readonly IList<InfoSystem> _systems;

        /// <summary>
        /// 用户列表
        /// </summary>
        private readonly IList<User> _users;

        /// <summary>
        /// 角色列表
        /// </summary>
        private readonly IList<Role> _roles;

        /// <summary>
        /// 菜单列表
        /// </summary>
        private readonly IList<Menu> _menus;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="repMediator">仓储中介者</param>
        /// <param name="unitOfWork">单元事务</param>
        public DataInitializer(RepositoryMediator repMediator, IUnitOfWorkIdentity unitOfWork)
        {
            this._repMediator = repMediator;
            this._unitOfWork = unitOfWork;
            this._systems = new List<InfoSystem>();
            this._users = new List<User>();
            this._roles = new List<Role>();
            this._menus = new List<Menu>();
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
            CacheMediator.Clear();

            this.InitAdmin();
            this.InitInfoSystems();
            this.InitSystemAdmins();
            this.InitRoles();
            this.InitUserRoles();
            this.InitMenus();

            if (this._systems.Any())
            {
                this._unitOfWork.RegisterAddRange(this._systems);
                this._unitOfWork.RegisterAddRange(this._users);
                this._unitOfWork.RegisterAddRange(this._roles);
                this._unitOfWork.RegisterAddRange(this._menus);

                this._unitOfWork.Commit();
            }
        }
        #endregion


        //Private

        #region # 初始化超级管理员 —— void InitAdmin()
        /// <summary>
        /// 初始化超级管理员
        /// </summary>
        private void InitAdmin()
        {
            if (!this._repMediator.UserRep.Exists(Constants.AdminLoginId))
            {
                User admin = new User(Constants.AdminLoginId, "超级管理员", Constants.InitialPassword);

                this._users.Add(admin);
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
                this._systems.Add(new InfoSystem("00", "身份认证", "identity"));
            }
        }
        #endregion

        #region # 初始化信息系统管理员 —— void InitSystemAdmins()
        /// <summary>
        /// 初始化信息系统管理员
        /// </summary>
        private void InitSystemAdmins()
        {
            foreach (InfoSystem system in this._systems)
            {
                this._users.Add(new User(system.AdminLoginId, string.Format("{0}系统管理员", system.Name), Constants.InitialPassword));
            }
        }
        #endregion

        #region # 初始化角色 —— void InitRoles()
        /// <summary>
        /// 初始化角色
        /// </summary>
        private void InitRoles()
        {
            foreach (InfoSystem system in this._systems)
            {
                Role adminRole = new Role("系统管理员", system.Number, "系统管理员", Constants.ManagerRoleNo);

                #region # 给角色授权

                IEnumerable<Authority> specAuthorities = this._unitOfWork.ResolveAuthorities(system.Number).ToArray();

                if (specAuthorities.Any())
                {
                    adminRole.SetAuthorities(specAuthorities);
                }

                #endregion

                this._roles.Add(adminRole);
            }
        }
        #endregion

        #region # 初始化用户角色 —— void InitUserRoles()
        /// <summary>
        /// 初始化用户角色
        /// </summary>
        private void InitUserRoles()
        {
            if (this._systems.Any())
            {
                //获取超级管理员
                User superAdmin = this._users.Single(x => x.Number == Constants.AdminLoginId);

                foreach (Role role in this._roles)
                {
                    //获取信息系统
                    InfoSystem currentSystem = this._systems.Single(x => x.Number == role.SystemNo);

                    //追加系统管理员权限
                    User systemAdmin = this._users.Single(x => x.Number == currentSystem.AdminLoginId);
                    systemAdmin.AppendRoles(currentSystem.Number, new[] { role });

                    //追加超级管理员权限
                    superAdmin.AppendRoles(currentSystem.Number, new[] { role });
                }
            }
        }
        #endregion

        #region # 初始化菜单 —— void InitMenus()
        /// <summary>
        /// 初始化菜单
        /// </summary>
        private void InitMenus()
        {
            if (this._repMediator.MenuRep.Count() == 0)
            {
                Menu root = new Menu("00", "身份认证系统", 1, null, null, null);
                Menu userManagement = new Menu("00", "用户管理", 2, "/User/Index", null, root);
                Menu roleManagement = new Menu("00", "角色管理", 3, "/Role/Index", null, root);
                Menu menuManagement = new Menu("00", "菜单管理", 4, "/Menu/Index", null, root);
                Menu authorityManagement = new Menu("00", "权限管理", 5, "/Authority/Index", null, root);

                this._menus.Add(root);
                this._menus.Add(userManagement);
                this._menus.Add(roleManagement);
                this._menus.Add(menuManagement);
                this._menus.Add(authorityManagement);
            }
        }
        #endregion
    }
}
