using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories;
using SD.IdentitySystem.Domain.Mediators;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Repository.EntityFramework;
using SD.Infrastructure.RepositoryBase;
using System.Collections.Generic;
using System.Linq;

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
        public void Initialize()
        {
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

            //注册获取用户信息事件
            EFUnitOfWorkProvider.GetLoginInfo += () => Membership.LoginInfo;
        }
        #endregion


        //Private

        #region # 初始化超级管理员 —— void InitAdmin()
        /// <summary>
        /// 初始化超级管理员
        /// </summary>
        private void InitAdmin()
        {
            if (!this._repMediator.UserRep.Exists(CommonConstants.AdminLoginId))
            {
                User admin = new User(CommonConstants.AdminLoginId, "超级管理员", CommonConstants.InitialPassword);

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
                this._systems.Add(new InfoSystem("00", "身份认证", "identity", ApplicationType.Web));
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
                this._users.Add(new User(system.AdminLoginId, $"{system.Name}系统管理员", CommonConstants.InitialPassword));
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
                Role adminRole = new Role("系统管理员", system.Number, "系统管理员", CommonConstants.ManagerRoleNo);

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
                User superAdmin = this._users.Single(x => x.Number == CommonConstants.AdminLoginId);

                foreach (Role role in this._roles)
                {
                    //获取信息系统
                    InfoSystem currentSystem = this._systems.Single(x => x.Number == role.SystemNo);

                    //追加系统管理员权限
                    User systemAdmin = this._users.Single(x => x.Number == currentSystem.AdminLoginId);
                    systemAdmin.AppendRoles(new[] { role });

                    //追加超级管理员权限
                    superAdmin.AppendRoles(new[] { role });
                }
            }
        }
        #endregion

        #region # 初始化菜单 —— void InitMenus()
        /// <summary>
        /// 初始化菜单/// </summary>
        private void InitMenus()
        {
            if (this._repMediator.MenuRep.Count() == 0)
            {
                Menu root = new Menu("00", ApplicationType.Web, "身份认证系统", 1, null, null, null, null);
                Menu systemManagement = new Menu("00", ApplicationType.Web, "信息系统管理", 2, "/InfoSystem/Index", null, null, root);
                Menu loginRecordManagement = new Menu("00", ApplicationType.Web, "登录记录", 3, "/LoginRecord/Index", null, null, root);
                Menu userManagement = new Menu("00", ApplicationType.Web, "用户管理", 4, "/User/Index", null, null, root);
                Menu roleManagement = new Menu("00", ApplicationType.Web, "角色管理", 5, "/Role/Index", null, null, root);
                Menu menuManagement = new Menu("00", ApplicationType.Web, "菜单管理", 6, "/Menu/Index", null, null, root);
                Menu authorityManagement = new Menu("00", ApplicationType.Web, "权限管理", 7, "/Authority/Index", null, null, root);

                this._menus.Add(root);
                this._menus.Add(systemManagement);
                this._menus.Add(loginRecordManagement);
                this._menus.Add(userManagement);
                this._menus.Add(roleManagement);
                this._menus.Add(menuManagement);
                this._menus.Add(authorityManagement);
            }
        }
        #endregion
    }
}
