using SD.IdentitySystem.DataImporter.Models;
using SD.IdentitySystem.IAppService.DTOs.Inputs;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using SD.Infrastructure.WPF.Caliburn.Base;
using SD.Toolkits.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace SD.IdentitySystem.DataImporter.ViewModels
{
    /// <summary>
    /// 首页视图模型
    /// </summary>
    public class HomeViewModel : ScreenBase
    {
        #region # 字段及构造器

        /// <summary>
        /// Excel文件路径
        /// </summary>
        private const string ExcelPath = @"Content\Datas\IdentitySystem.xls";

        /// <summary>
        /// 用户服务契约接口代理
        /// </summary>
        private readonly ServiceProxy<IUserContract> _userContract;

        /// <summary>
        /// 权限服务契约接口代理
        /// </summary>
        private readonly ServiceProxy<IAuthorizationContract> _authorizationContract;

        /// <summary>
        /// 身份认证服务契约接口代理
        /// </summary>
        private readonly ServiceProxy<IAuthenticationContract> _authenticationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public HomeViewModel(ServiceProxy<IUserContract> userContract, ServiceProxy<IAuthorizationContract> authorizationContract, ServiceProxy<IAuthenticationContract> authenticationContract)
        {
            this._userContract = userContract;
            this._authorizationContract = authorizationContract;
            this._authenticationContract = authenticationContract;
        }

        #endregion

        #region # 方法

        //Initializations

        #region 初始化 —— override async void OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        /// <summary>
        /// 初始化
        /// </summary>
        protected override async Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            string loginId = CommonConstants.AdminLoginId;
            string password = CommonConstants.InitialPassword;

            LoginInfo loginInfo = await this.Login(loginId, password);
            AppDomain.CurrentDomain.SetData(GlobalSetting.ApplicationId, loginInfo);
        }
        #endregion


        //Actions

        #region 导入信息系统 —— async void ImportInfoSystems()
        /// <summary>
        /// 导入信息系统
        /// </summary>
        public async void ImportInfoSystems()
        {
            this.Busy();

            await Task.Run(() =>
            {
                IAuthorizationContract authorizationContract = this._authorizationContract.Channel;
                InfoSystem[] infoSystems = ExcelReader.ReadFile<InfoSystem>(ExcelPath, "信息系统");

                foreach (InfoSystem infoSystem in infoSystems)
                {
                    ApplicationType applicationType = (ApplicationType)Enum.Parse(typeof(ApplicationType), infoSystem.应用程序类型);
                    authorizationContract.CreateInfoSystem(infoSystem.信息系统编号, infoSystem.信息系统名称, infoSystem.管理员登录名, applicationType);
                }
            });

            this.Idle();
            this.ToastSuccess("导入信息系统成功！");
        }
        #endregion

        #region 导入权限 —— async void ImportAuthorities()
        /// <summary>
        /// 导入权限
        /// </summary>
        public async void ImportAuthorities()
        {
            this.Busy();

            await Task.Run(() =>
            {
                IAuthorizationContract authorizationContract = this._authorizationContract.Channel;
                Authority[] authorities = ExcelReader.ReadFile<Authority>(ExcelPath, "权限");
                var authorityGroups = authorities.GroupBy(x => new
                {
                    x.信息系统编号,
                    x.应用程序类型
                });

                foreach (var authorityGroup in authorityGroups)
                {
                    IList<AuthorityParam> authorityParams = new List<AuthorityParam>();
                    foreach (Authority authority in authorityGroup)
                    {
                        AuthorityParam authorityParam = new AuthorityParam()
                        {
                            authorityName = authority.权限名称,
                            authorityPath = authority.权限路径
                        };
                        authorityParams.Add(authorityParam);
                    }

                    ApplicationType applicationType = (ApplicationType)Enum.Parse(typeof(ApplicationType), authorityGroup.Key.应用程序类型);
                    authorizationContract.CreateAuthorities(authorityGroup.Key.信息系统编号, applicationType, authorityParams);
                }
            });

            this.Idle();
            this.ToastSuccess("导入权限成功！");
        }
        #endregion

        #region 导入菜单 —— async void ImportMenus()
        /// <summary>
        /// 导入菜单
        /// </summary>
        public async void ImportMenus()
        {
            this.Busy();

            await Task.Run(() =>
            {
                IAuthorizationContract authorizationContract = this._authorizationContract.Channel;
                Menu[] menus = ExcelReader.ReadFile<Menu>(ExcelPath, "菜单");

                IEnumerable<IGrouping<string, Menu>> menuGroups = menus.GroupBy(x => x.信息系统编号);
                foreach (IGrouping<string, Menu> menuGroup in menuGroups)
                {
                    //获取信息系统根级菜单
                    Guid infoSystemMenuId = authorizationContract.GetMenus(null, menuGroup.Key, null).First().Id;

                    IList<Menu> rootMenus = menuGroup.Where(x => string.IsNullOrWhiteSpace(x.上级Id)).ToList();
                    this.CreateMenus(authorizationContract, menuGroup.Key, menus, rootMenus, infoSystemMenuId);
                }
            });

            this.Idle();
            this.ToastSuccess("导入菜单成功！");
        }
        #endregion

        #region 关联权限到菜单 —— async void RelateAuthoritiesToMenus()
        /// <summary>
        /// 关联权限到菜单
        /// </summary>
        public async void RelateAuthoritiesToMenus()
        {
            this.Busy();

            await Task.Run(() =>
            {
                IAuthorizationContract authorizationContract = this._authorizationContract.Channel;
                MenuRelatedAuthority[] relatedAuthorities = ExcelReader.ReadFile<MenuRelatedAuthority>(ExcelPath, "菜单相关权限");

                MenuInfo[] menuInfos = authorizationContract.GetMenus(null, null, null).ToArray();
                AuthorityInfo[] authorityInfos = authorizationContract.GetAuthorities(null, null, null, null, null).ToArray();

                var relatedAuthorityGroups = relatedAuthorities.GroupBy(x => new
                {
                    x.信息系统编号,
                    x.应用程序类型,
                    x.菜单名称
                });
                foreach (var relatedAuthorityGroup in relatedAuthorityGroups)
                {
                    ApplicationType applicationType = (ApplicationType)Enum.Parse(typeof(ApplicationType), relatedAuthorityGroup.Key.应用程序类型);
                    MenuInfo menuInfo = menuInfos.Single(x => x.InfoSystemNo == relatedAuthorityGroup.Key.信息系统编号 && x.ApplicationType == applicationType && x.Name == relatedAuthorityGroup.Key.菜单名称);

                    IList<AuthorityInfo> relatedAuthorityInfos = new List<AuthorityInfo>();
                    foreach (MenuRelatedAuthority relatedAuthority in relatedAuthorityGroup)
                    {
                        AuthorityInfo authorityInfo = authorityInfos.Single(x => x.InfoSystemInfo.Number == relatedAuthority.信息系统编号 && x.ApplicationType == applicationType && x.Name == relatedAuthority.权限名称);
                        relatedAuthorityInfos.Add(authorityInfo);
                    }

                    IEnumerable<Guid> authorityIds = relatedAuthorityInfos.Select(x => x.Id);
                    authorizationContract.RelateAuthoritiesToMenu(menuInfo.Id, authorityIds);
                }
            });

            this.Idle();
            this.ToastSuccess("关联权限到菜单成功！");
        }
        #endregion

        #region 导入角色 —— async void ImportRoles()
        /// <summary>
        /// 导入角色
        /// </summary>
        public async void ImportRoles()
        {
            this.Busy();

            await Task.Run(() =>
            {
                IAuthorizationContract authorizationContract = this._authorizationContract.Channel;
                Role[] roles = ExcelReader.ReadFile<Role>(ExcelPath, "角色");
                foreach (Role role in roles)
                {
                    authorizationContract.CreateRole(role.信息系统编号, role.角色名称, role.描述, null);
                }
            });

            this.Idle();
            this.ToastSuccess("导入角色成功！");
        }
        #endregion

        #region 关联权限到角色 —— async void RelateAuthoritiesToRoles()
        /// <summary>
        /// 关联权限到角色
        /// </summary>
        public async void RelateAuthoritiesToRoles()
        {
            this.Busy();

            await Task.Run(() =>
            {
                IAuthorizationContract authorizationContract = this._authorizationContract.Channel;
                RoleRelatedAuthority[] relatedAuthorities = ExcelReader.ReadFile<RoleRelatedAuthority>(ExcelPath, "角色相关权限");

                RoleInfo[] roleInfos = authorizationContract.GetRoles(null, null, null).ToArray();
                AuthorityInfo[] authorityInfos = authorizationContract.GetAuthorities(null, null, null, null, null).ToArray();

                var relatedAuthorityGroups = relatedAuthorities.GroupBy(x => new
                {
                    x.信息系统编号,
                    x.角色名称
                });
                foreach (var relatedAuthorityGroup in relatedAuthorityGroups)
                {
                    RoleInfo roleInfo = roleInfos.Single(x => x.InfoSystemNo == relatedAuthorityGroup.Key.信息系统编号 && x.Name == relatedAuthorityGroup.Key.角色名称);

                    IList<AuthorityInfo> relatedAuthorityInfos = new List<AuthorityInfo>();
                    foreach (RoleRelatedAuthority relatedAuthority in relatedAuthorityGroup)
                    {
                        ApplicationType applicationType = (ApplicationType)Enum.Parse(typeof(ApplicationType), relatedAuthority.应用程序类型);
                        AuthorityInfo authorityInfo = authorityInfos.Single(x => x.InfoSystemInfo.Number == relatedAuthority.信息系统编号 && x.ApplicationType == applicationType && x.Name == relatedAuthority.权限名称);
                        relatedAuthorityInfos.Add(authorityInfo);
                    }

                    IEnumerable<Guid> authorityIds = relatedAuthorityInfos.Select(x => x.Id);
                    authorizationContract.RelateAuthoritiesToRole(roleInfo.Id, authorityIds);
                }
            });

            this.Idle();
            this.ToastSuccess("关联权限到角色成功！");
        }
        #endregion

        #region 导入用户 —— async void ImportUsers()
        /// <summary>
        /// 导入用户
        /// </summary>
        public async void ImportUsers()
        {
            this.Busy();

            await Task.Run(() =>
            {
                IUserContract userContract = this._userContract.Channel;
                User[] users = ExcelReader.ReadFile<User>(ExcelPath, "用户");
                foreach (User user in users)
                {
                    userContract.CreateUser(user.用户名, user.真实姓名, user.密码);
                }
            });

            this.Idle();
            this.ToastSuccess("导入用户成功！");
        }
        #endregion

        #region 关联角色到用户 —— async void RelateRolesToUsers()
        /// <summary>
        /// 关联角色到用户
        /// </summary>
        public async void RelateRolesToUsers()
        {
            this.Busy();

            await Task.Run(() =>
            {
                IUserContract userContract = this._userContract.Channel;
                IAuthorizationContract authorizationContract = this._authorizationContract.Channel;
                UserRelatedRole[] relatedRoles = ExcelReader.ReadFile<UserRelatedRole>(ExcelPath, "用户相关角色");

                RoleInfo[] roleInfos = authorizationContract.GetRoles(null, null, null).ToArray();
                IEnumerable<IGrouping<string, UserRelatedRole>> relatedRoleGroups = relatedRoles.GroupBy(x => x.用户名);
                foreach (IGrouping<string, UserRelatedRole> relatedRoleGroup in relatedRoleGroups)
                {
                    IList<RoleInfo> relatedRoleInfos = new List<RoleInfo>();
                    foreach (UserRelatedRole userRelatedRole in relatedRoleGroup)
                    {
                        RoleInfo roleInfo = roleInfos.Single(x => x.InfoSystemNo == userRelatedRole.信息系统编号 && x.Name == userRelatedRole.角色名称);
                        relatedRoleInfos.Add(roleInfo);
                    }

                    IEnumerable<Guid> roleIds = relatedRoleInfos.Select(x => x.Id);
                    userContract.RelateRolesToUser(relatedRoleGroup.Key, roleIds);
                }
            });

            this.Idle();
            this.ToastSuccess("关联角色到用户成功！");
        }
        #endregion


        //Private

        #region 登录 —— async Task<LoginInfo> Login(string loginId, string password)
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="password">密码</param>
        private async Task<LoginInfo> Login(string loginId, string password)
        {
            this.Busy();

            LoginInfo loginInfo = await Task.Run(() => this._authenticationContract.Channel.Login(loginId, password));

            this.Idle();

            return loginInfo;
        }
        #endregion

        #region 创建菜单 —— void CreateMenus(IAuthorizationContract...
        /// <summary>
        /// 创建菜单
        /// </summary>
        private void CreateMenus(IAuthorizationContract authorizationContract, string infoSystemNo, ICollection<Menu> allMenus, ICollection<Menu> menus, Guid parentId)
        {
            foreach (Menu menu in menus)
            {
                ApplicationType applicationType = (ApplicationType)Enum.Parse(typeof(ApplicationType), menu.应用程序类型);

                //创建菜单
                Guid menuId = authorizationContract.CreateMenu(infoSystemNo, applicationType, menu.菜单名称, menu.排序, menu.链接地址, menu.路径, menu.图标, parentId);

                //创建下级菜单
                IList<Menu> subMenus = allMenus.Where(x => x.上级Id == menu.Id).ToList();
                this.CreateMenus(authorizationContract, infoSystemNo, allMenus, subMenus, menuId);
            }
        }
        #endregion

        #endregion
    }
}
