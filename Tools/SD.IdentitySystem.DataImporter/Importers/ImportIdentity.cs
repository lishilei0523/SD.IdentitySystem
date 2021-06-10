using Realgoal.MOM.DataDrive.Bussiness.Models.MOM.IdentityContext;
using SD.IdentitySystem.DataImporter;
using SD.IdentitySystem.IAppService.DTOs.Inputs;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IOC.Core.Mediators;
using SD.Toolkits.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationType = SD.Infrastructure.Constants.ApplicationType;

namespace Realgoal.MOM.DataDrive.Bussiness.ImportMethods.ImportBase
{
    public class ImportIdentity
    {
        #region # 信息系统数据处理 —— void ImportSystem(List<ManagementSystem> managementSystem)
        /// <summary>
        /// 导入信息系统
        /// </summary>
        public void ImportSystem()
        {
            var authorizationContract = Proxy<IAuthorizationContract>.Instance;
            InfoSystemModel[] infoSystemModels = ExcelReader.ReadFile<InfoSystemModel>(Constants.ExcelPath, "信息系统");

            foreach (InfoSystemModel infoSystemModel in infoSystemModels)
            {
                ApplicationType applicationType = (ApplicationType)Enum.Parse(typeof(ApplicationType), infoSystemModel.应用程序类型);

                authorizationContract.CreateInfoSystem(infoSystemModel.信息系统编号, infoSystemModel.信息系统名称, infoSystemModel.管理员登录名, applicationType);
            }
        }

        #endregion

        #region # 权限处理 —— void ImportAuthoritie(List<Authoritie> authorities)
        /// <summary>
        /// 权限处理
        /// </summary>

        public void ImportAuthoritie()
        {
            var authorizationContract = Proxy<IAuthorizationContract>.Instance;
            AuthorityModel[] authorityModels = ExcelReader.ReadFile<AuthorityModel>(Constants.ExcelPath, "系统菜单");
            IEnumerable<IGrouping<string, AuthorityModel>> authorityModelGroups = authorityModels.GroupBy(x => x.所属系统);

            foreach (IGrouping<string, AuthorityModel> authorityModelGroup in authorityModelGroups)
            {
                IOrderedEnumerable<AuthorityModel> list = authorityModelGroup.Where(x => !string.IsNullOrWhiteSpace(x.角色)).OrderBy(x => x.排序);
                List<AuthorityParam> 权限集 = new List<AuthorityParam>();
                foreach (AuthorityModel item in list)
                {
                    AuthorityParam 权限 = new AuthorityParam()
                    {
                        AssemblyName = item.应用程序类型,
                        AuthorityName = item.菜单名称,
                        Namespace = item.应用程序类型 + "." + item.菜单名称,
                        ClassName = item.Id,
                        MethodName = item.排序 + item.Id
                    };
                    权限集.Add(权限);
                }


                authorizationContract.CreateAuthorities(authorityModelGroup.Key, ApplicationType.Web, 权限集);
            }
        }

        #endregion

        #region # 菜单处理 -- void ImportMenu(List<Authoritie> authorities)
        /// <summary>
        /// 加载菜单
        /// </summary>

        public void ImportMenu()
        {
            var authorizationContract = Proxy<IAuthorizationContract>.Instance;
            List<AuthorityModel> authorityModels = ExcelReader.ReadFile<AuthorityModel>(Constants.ExcelPath, "系统菜单").ToList();
            IEnumerable<IGrouping<string, AuthorityModel>> authorityModelGroups = authorityModels.GroupBy(x => x.所属系统);

            //获取权限列表
            List<AuthorityInfo> authorities = authorizationContract.GetAuthorities(null, null, null, null, null).ToList();
            foreach (IGrouping<string, AuthorityModel> authorityModelGroup in authorityModelGroups)
            {
                //获取根级目录
                Guid parentId = authorizationContract.GetMenus(authorityModelGroup.Key, null).First().Id;
                IOrderedEnumerable<AuthorityModel> parentMenus = authorityModels.Where(x => x.所属系统 == authorityModelGroup.Key && string.IsNullOrWhiteSpace(x.父级Id)).OrderBy(x => x.排序);

                Parallel.ForEach(parentMenus, (Action<AuthorityModel>)(menu =>
                {
                    CreateMenu(authorityModels, authorityModelGroup.Key, menu, parentId, authorities);
                }));
            }
        }

        /// <summary>
        /// 递归添加菜单
        /// </summary>
        /// <param name="excexData">Excel数据</param>
        /// <param name="systemNum">信息系统编号</param>
        /// <param name="menu">当前菜单</param>
        /// <param name="parentId">父级ID</param>
        /// <param name="authorities">权限列表</param>
        private void CreateMenu(List<AuthorityModel> excexData, string systemNum, AuthorityModel menu, Guid parentId, List<AuthorityInfo> authorities)
        {
            IAuthorizationContract authorizationContract = Proxy<IAuthorizationContract>.Instance;
            ApplicationType applicationType = (ApplicationType)Enum.Parse(typeof(ApplicationType), menu.应用程序类型);


            //找到父级Id
            authorizationContract.CreateMenu(systemNum, applicationType, menu.菜单名称, menu.排序, menu.连接地址, menu.路径, menu.图标, parentId);

            IEnumerable<MenuInfo> menus = authorizationContract.GetMenus(systemNum, applicationType);
            Func<MenuInfo, bool> condition =
                x =>
                    x.Name == menu.菜单名称 &&
                    x.Sort == menu.排序 &&
                    x.Url == menu.连接地址 &&
                    x.Path == menu.路径 &&
                    x.Icon == menu.图标;
            MenuInfo menuInfo = menus.Single(condition);

            if (!string.IsNullOrWhiteSpace(menu.角色))
            {
                //获取excel当前菜单权限
                AuthorityInfo authority = authorities.Single(x => x.Name == menu.菜单名称 && x.AssemblyName == menu.应用程序类型 && x.ClassName == menu.Id);
                //关联权限
                authorizationContract.RelateAuthoritiesToMenu(menuInfo.Id, new List<Guid>() { authority.Id });
            }


            //获取子目录集
            IOrderedEnumerable<AuthorityModel> childrenMenus = excexData.Where(x => x.应用程序类型 == menu.应用程序类型 && x.父级Id == menu.Id).OrderBy(x => x.排序);
            Parallel.ForEach(childrenMenus, childrenMenu =>
            {
                CreateMenu(excexData, systemNum, childrenMenu, menuInfo.Id, authorities);
            });
        }

        #endregion

        #region # 角色处理 -- void ImportRole(List<Authoritie> excexData)
        /// <summary>
        /// 加载角色
        /// </summary>

        public void ImportRole()
        {
            var authorizationContract = Proxy<IAuthorizationContract>.Instance;
            List<AuthorityModel> authorityModels = ExcelReader.ReadFile<AuthorityModel>(Constants.ExcelPath, "系统菜单").ToList();

            #region 加载系统角色列表
            //系统角色列表
            Dictionary<InfoSystemInfo, List<string>> systemRoles = new Dictionary<InfoSystemInfo, List<string>>();
            //获取信息系统
            IEnumerable<InfoSystemInfo> systemInfoList = authorizationContract.GetInfoSystems().Where(x => x.Number != "00"); ;
            foreach (InfoSystemInfo system in systemInfoList)
            {
                List<string> roleNames = new List<string>();//角色名称集
                //获取excel当前系统角色并去重
                IEnumerable<string> tempList = authorityModels.Where(x => x.所属系统 == system.Number && !string.IsNullOrWhiteSpace(x.角色)).Select(x => x.角色).Distinct();
                foreach (string role in tempList)
                {
                    roleNames.AddRange(role.Split(','));
                }
                systemRoles.Add(system, roleNames.Distinct().ToList());
            }
            #endregion

            #region 导入系统角色
            foreach (KeyValuePair<InfoSystemInfo, List<string>> system in systemRoles)
            {
                List<RoleInfo> roles = authorizationContract.GetRoles(null, null, system.Key.Number).ToList();
                //1.获取系统权限列表
                IEnumerable<AuthorityInfo> authorities = authorizationContract.GetAuthorities(null, system.Key.Number, null, null, null);
                foreach (string roleName in system.Value)
                {

                    //获取源_数据角色所对应权限列表 并去重
                    IEnumerable<AuthorityModel> authoritieNames = authorityModels.Where(x => x.角色 != null && x.所属系统 == system.Key.Number && x.角色.Contains(roleName))
                        .Distinct();
                    List<Guid> authorityIds = new List<Guid>();//权限ID集
                    foreach (AuthorityModel item in authoritieNames)
                    {
                        //查找相应权限
                        AuthorityInfo authoritie = authorities.Single(x => x.Name == item.菜单名称 && x.ClassName == item.Id);
                        authorityIds.Add(authoritie.Id);
                    }

                    RoleInfo roleInfo = roles.FirstOrDefault(x => x.Name == roleName);

                    if (roleInfo != null)
                    {
                        authorizationContract.AppendAuthoritiesToRole(roleInfo.Id, authorityIds);
                    }
                    else
                    {
                        authorizationContract.CreateRole(system.Key.Number, roleName, "", authorityIds);
                    }
                }
            }
            #endregion
        }

        #endregion

        #region 用户处理 -- void ImportUser(List<UserModel> users)
        /// <summary>
        /// 用户处理
        /// </summary>

        public void ImportUser()
        {
            var authorizationContract = Proxy<IAuthorizationContract>.Instance;
            List<UserModel> users = ExcelReader.ReadFile<UserModel>(Constants.ExcelPath, "用户").ToList();
            //系统角色集
            Dictionary<InfoSystemInfo, List<RoleInfo>> systemRoles = new Dictionary<InfoSystemInfo, List<RoleInfo>>();
            //获取信息系统列表
            IEnumerable<InfoSystemInfo> systemInfoList = authorizationContract.GetInfoSystems().Where(m => m.Number != "00");
            foreach (InfoSystemInfo item in systemInfoList)
            {
                if (systemRoles.All(x => x.Key.Number != item.Number))
                {
                    systemRoles.Add(item, authorizationContract.GetRoles(null, null, item.Number).ToList());
                }
            }

            foreach (UserModel item in users)
            {
                //角色ID集
                List<Guid> roleIdList = new List<Guid>();
                //创建用户
                this.CreateUser(item.用户名, item.真实姓名, item.密码);
                foreach (string 角色 in item.所属角色.Split(','))
                {
                    foreach (var sysItem in systemRoles)
                    {
                        var sys角色 = sysItem.Value.SingleOrDefault(m => m.Name == 角色);
                        if (sys角色 != null) { roleIdList.Add(sys角色.Id); }
                    }

                }
                //为用户追加角色
                if (!roleIdList.Any()) continue;
                this.AppendRoles(item.用户名, roleIdList);
            }
        }

        #endregion



        #region # 调用服务
        #region # 创建用户 —— void CreateUser(string loginId, string realName...
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="password">密码</param>
        private void CreateUser(string loginId, string realName, string password)
        {
            Proxy<IUserContract>.Instance.CreateUser(loginId, realName, password);
        }
        #endregion

        #region # 为用户追加角色 —— void AppendRoles(string loginId...
        /// <summary>
        /// 为用户追加角色
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="roleIds">角色Id集</param>
        public void AppendRoles(string loginId, IEnumerable<Guid> roleIds)
        {
            Proxy<IUserContract>.Instance.AppendRolesToUser(loginId, roleIds);
        }
        #endregion
        #endregion
    }
}
