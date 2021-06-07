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
        /// 权限列表
        /// </summary>
        private readonly IList<Authority> _authorities;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public DataInitializer(RepositoryMediator repMediator, IUnitOfWorkIdentity unitOfWork)
        {
            this._repMediator = repMediator;
            this._unitOfWork = unitOfWork;
            this._systems = new List<InfoSystem>();
            this._users = new List<User>();
            this._roles = new List<Role>();
            this._menus = new List<Menu>();
            this._authorities = new List<Authority>();
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
            this.InitRoleAuthorities();

            if (this._systems.Any())
            {
                this._unitOfWork.RegisterAddRange(this._systems);
                this._unitOfWork.RegisterAddRange(this._users);
                this._unitOfWork.RegisterAddRange(this._roles);
                this._unitOfWork.RegisterAddRange(this._menus);
                this._unitOfWork.RegisterAddRange(this._authorities);

                this._unitOfWork.Commit();
            }

            //注册获取用户信息事件
            EFUnitOfWorkProvider.GetLoginInfo += MembershipMediator.GetLoginInfo;
        }
        #endregion


        //Private

        #region # 初始化超级管理员 —— void InitAdmin()
        /// <summary>
        /// 初始化超级管理员
        /// </summary>
        private void InitAdmin()
        {
            if (!this._repMediator.UserRep.ExistsNo(CommonConstants.AdminLoginId))
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
                this._systems.Add(new InfoSystem("00", "身份认证", "identity", ApplicationType.Complex));
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
        /// 初始化菜单
        /// </summary>
        private void InitMenus()
        {
            if (this._repMediator.MenuRep.Count() == 0)
            {
                const string systemNo = "00";
                this.InitMvcMenus(systemNo);
                this.InitWpfMenus(systemNo);
                this.InitAngularMenus(systemNo);
            }
        }
        #endregion

        #region # 初始化MVC菜单 —— void InitMvcMenus(string systemNo)
        /// <summary>
        /// 初始化MVC菜单
        /// </summary>
        private void InitMvcMenus(string systemNo)
        {
            //创建菜单
            Menu root = new Menu(systemNo, ApplicationType.Web, "身份认证系统", 1, null, null, null, null);
            Menu systemManagement = new Menu(systemNo, ApplicationType.Web, "信息系统管理", 2, "/InfoSystem/Index", null, null, root);
            Menu userManagement = new Menu(systemNo, ApplicationType.Web, "用户管理", 3, "/User/Index", null, null, root);
            Menu roleManagement = new Menu(systemNo, ApplicationType.Web, "角色管理", 4, "/Role/Index", null, null, root);
            Menu menuManagement = new Menu(systemNo, ApplicationType.Web, "菜单管理", 5, "/Menu/Index", null, null, root);
            Menu authorityManagement = new Menu(systemNo, ApplicationType.Web, "权限管理", 6, "/Authority/Index", null, null, root);
            Menu loginRecordManagement = new Menu(systemNo, ApplicationType.Web, "登录记录", 7, "/LoginRecord/Index", null, null, root);

            //创建权限
            Authority systemManagementIndex = new Authority(systemNo, ApplicationType.Web, "信息系统管理首页", "/InfoSystem/Index", null, null, null, null, null, null);
            Authority userManagementIndex = new Authority(systemNo, ApplicationType.Web, "用户管理首页", "/User/Index", null, null, null, null, null, null);
            Authority roleManagementIndex = new Authority(systemNo, ApplicationType.Web, "角色管理首页", "/Role/Index", null, null, null, null, null, null);
            Authority menuManagementIndex = new Authority(systemNo, ApplicationType.Web, "菜单管理首页", "/Menu/Index", null, null, null, null, null, null);
            Authority authorityManagementIndex = new Authority(systemNo, ApplicationType.Web, "权限管理首页", "/Authority/Index", null, null, null, null, null, null);
            Authority loginRecordManagementIndex = new Authority(systemNo, ApplicationType.Web, "登录记录首页", "/LoginRecord/Index", null, null, null, null, null, null);

            //菜单关联权限
            systemManagement.RelateAuthorities(new[] { systemManagementIndex });
            userManagement.RelateAuthorities(new[] { userManagementIndex });
            roleManagement.RelateAuthorities(new[] { roleManagementIndex });
            menuManagement.RelateAuthorities(new[] { menuManagementIndex });
            authorityManagement.RelateAuthorities(new[] { authorityManagementIndex });
            loginRecordManagement.RelateAuthorities(new[] { loginRecordManagementIndex });

            this._menus.Add(root);
            this._menus.Add(systemManagement);
            this._menus.Add(loginRecordManagement);
            this._menus.Add(userManagement);
            this._menus.Add(roleManagement);
            this._menus.Add(menuManagement);
            this._menus.Add(authorityManagement);

            this._authorities.Add(systemManagementIndex);
            this._authorities.Add(userManagementIndex);
            this._authorities.Add(roleManagementIndex);
            this._authorities.Add(menuManagementIndex);
            this._authorities.Add(authorityManagementIndex);
            this._authorities.Add(loginRecordManagementIndex);
        }
        #endregion

        #region # 初始化WPF菜单 —— void InitWpfMenus(string systemNo)
        /// <summary>
        /// 初始化WPF菜单
        /// </summary>
        private void InitWpfMenus(string systemNo)
        {
            //创建菜单
            Menu root = new Menu(systemNo, ApplicationType.Windows, "身份认证系统", 1, null, null, "Home", null);
            Menu systemManagement = new Menu(systemNo, ApplicationType.Windows, "信息系统管理", 2, "SD.IdentitySystem.Client.ViewModels.InfoSystem.IndexViewModel", null, "LabelOutline", root);
            Menu userManagement = new Menu(systemNo, ApplicationType.Windows, "用户管理", 3, "SD.IdentitySystem.Client.ViewModels.User.IndexViewModel", null, "LabelOutline", root);
            Menu roleManagement = new Menu(systemNo, ApplicationType.Windows, "角色管理", 4, "SD.IdentitySystem.Client.ViewModels.Role.IndexViewModel", null, "LabelOutline", root);
            Menu menuManagement = new Menu(systemNo, ApplicationType.Windows, "菜单管理", 5, "SD.IdentitySystem.Client.ViewModels.Menu.IndexViewModel", null, "LabelOutline", root);
            Menu authorityManagement = new Menu(systemNo, ApplicationType.Windows, "权限管理", 6, "SD.IdentitySystem.Client.ViewModels.Authority.IndexViewModel", null, "LabelOutline", root);
            Menu loginRecordManagement = new Menu(systemNo, ApplicationType.Windows, "登录记录", 7, "SD.IdentitySystem.Client.ViewModels.LoginRecord.IndexViewModel", null, "LabelOutline", root);

            //创建权限
            Authority systemManagementIndex = new Authority(systemNo, ApplicationType.Windows, "信息系统管理首页", "/InfoSystem/Index", null, null, null, null, null, null);
            Authority userManagementIndex = new Authority(systemNo, ApplicationType.Windows, "用户管理首页", "/User/Index", null, null, null, null, null, null);
            Authority roleManagementIndex = new Authority(systemNo, ApplicationType.Windows, "角色管理首页", "/Role/Index", null, null, null, null, null, null);
            Authority menuManagementIndex = new Authority(systemNo, ApplicationType.Windows, "菜单管理首页", "/Menu/Index", null, null, null, null, null, null);
            Authority authorityManagementIndex = new Authority(systemNo, ApplicationType.Windows, "权限管理首页", "/Authority/Index", null, null, null, null, null, null);
            Authority loginRecordManagementIndex = new Authority(systemNo, ApplicationType.Windows, "登录记录首页", "/LoginRecord/Index", null, null, null, null, null, null);

            //菜单关联权限
            systemManagement.RelateAuthorities(new[] { systemManagementIndex });
            userManagement.RelateAuthorities(new[] { userManagementIndex });
            roleManagement.RelateAuthorities(new[] { roleManagementIndex });
            menuManagement.RelateAuthorities(new[] { menuManagementIndex });
            authorityManagement.RelateAuthorities(new[] { authorityManagementIndex });
            loginRecordManagement.RelateAuthorities(new[] { loginRecordManagementIndex });

            this._menus.Add(root);
            this._menus.Add(systemManagement);
            this._menus.Add(loginRecordManagement);
            this._menus.Add(userManagement);
            this._menus.Add(roleManagement);
            this._menus.Add(menuManagement);
            this._menus.Add(authorityManagement);

            this._authorities.Add(systemManagementIndex);
            this._authorities.Add(userManagementIndex);
            this._authorities.Add(roleManagementIndex);
            this._authorities.Add(menuManagementIndex);
            this._authorities.Add(authorityManagementIndex);
            this._authorities.Add(loginRecordManagementIndex);
        }
        #endregion

        #region # 初始化Angular菜单 —— void InitAngularMenus(string systemNo)
        /// <summary>
        /// 初始化Angular菜单
        /// </summary>
        private void InitAngularMenus(string systemNo)
        {
            //创建菜单
            Menu root = new Menu(systemNo, ApplicationType.IOS, "身份认证系统", 1, null, null, "Bank", null);
            Menu systemManagement = new Menu(systemNo, ApplicationType.IOS, "信息系统管理", 2, "/Home/InfoSystem", null, null, root);
            Menu userManagement = new Menu(systemNo, ApplicationType.IOS, "用户管理", 3, "/Home/User", null, null, root);
            Menu roleManagement = new Menu(systemNo, ApplicationType.IOS, "角色管理", 4, "/Home/Role", null, null, root);
            Menu menuManagement = new Menu(systemNo, ApplicationType.IOS, "菜单管理", 5, "/Home/Menu", null, null, root);
            Menu authorityManagement = new Menu(systemNo, ApplicationType.IOS, "权限管理", 6, "/Home/Authority", null, null, root);
            Menu loginRecordManagement = new Menu(systemNo, ApplicationType.IOS, "登录记录", 7, "/Home/LoginRecord", null, null, root);

            //创建权限
            Authority systemManagementIndex = new Authority(systemNo, ApplicationType.IOS, "信息系统管理首页", "/InfoSystem/Index", null, null, null, null, null, null);
            Authority userManagementIndex = new Authority(systemNo, ApplicationType.IOS, "用户管理首页", "/User/Index", null, null, null, null, null, null);
            Authority roleManagementIndex = new Authority(systemNo, ApplicationType.IOS, "角色管理首页", "/Role/Index", null, null, null, null, null, null);
            Authority menuManagementIndex = new Authority(systemNo, ApplicationType.IOS, "菜单管理首页", "/Menu/Index", null, null, null, null, null, null);
            Authority authorityManagementIndex = new Authority(systemNo, ApplicationType.IOS, "权限管理首页", "/Authority/Index", null, null, null, null, null, null);
            Authority loginRecordManagementIndex = new Authority(systemNo, ApplicationType.IOS, "登录记录首页", "/LoginRecord/Index", null, null, null, null, null, null);

            //菜单关联权限
            systemManagement.RelateAuthorities(new[] { systemManagementIndex });
            userManagement.RelateAuthorities(new[] { userManagementIndex });
            roleManagement.RelateAuthorities(new[] { roleManagementIndex });
            menuManagement.RelateAuthorities(new[] { menuManagementIndex });
            authorityManagement.RelateAuthorities(new[] { authorityManagementIndex });
            loginRecordManagement.RelateAuthorities(new[] { loginRecordManagementIndex });

            this._menus.Add(root);
            this._menus.Add(systemManagement);
            this._menus.Add(loginRecordManagement);
            this._menus.Add(userManagement);
            this._menus.Add(roleManagement);
            this._menus.Add(menuManagement);
            this._menus.Add(authorityManagement);

            this._authorities.Add(systemManagementIndex);
            this._authorities.Add(userManagementIndex);
            this._authorities.Add(roleManagementIndex);
            this._authorities.Add(menuManagementIndex);
            this._authorities.Add(authorityManagementIndex);
            this._authorities.Add(loginRecordManagementIndex);
        }
        #endregion

        #region # 初始化角色权限 —— void InitRoleAuthorities()
        /// <summary>
        /// 初始化角色权限
        /// </summary>
        private void InitRoleAuthorities()
        {
            foreach (InfoSystem infoSystem in this._systems)
            {
                foreach (Role role in this._roles.Where(x => x.SystemNo == infoSystem.Number))
                {
                    IEnumerable<Authority> authorities = this._authorities.Where(x => x.SystemNo == infoSystem.Number);
                    role.RelateAuthorities(authorities);
                }
            }
        }
        #endregion
    }
}
