using SD.IdentitySystem.AppService.Maps;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories;
using SD.IdentitySystem.Domain.Mediators;
using SD.IdentitySystem.IAppService.DTOs.Inputs;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.DTOBase;
using System;
using System.Collections.Generic;
using System.Linq;
#if NET40_OR_GREATER
using System.ServiceModel;
#endif
#if NETSTANDARD2_0_OR_GREATER
using CoreWCF;
#endif

namespace SD.IdentitySystem.AppService.Implements
{
    /// <summary>
    /// 权限管理服务契约实现
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, IncludeExceptionDetailInFaults = true)]
    public class AuthorizationContract : IAuthorizationContract
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
        public AuthorizationContract(RepositoryMediator repMediator, IUnitOfWorkIdentity unitOfWork)
        {
            this._repMediator = repMediator;
            this._unitOfWork = unitOfWork;
        }

        #endregion


        //命令部分

        #region # 创建信息系统 —— void CreateInfoSystem(string infoSystemNo, string infoSystemName...
        /// <summary>
        /// 创建信息系统
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="infoSystemName">信息系统名称</param>
        /// <param name="adminLoginId">系统管理员账号</param>
        /// <param name="applicationType">应用程序类型</param>
        public void CreateInfoSystem(string infoSystemNo, string infoSystemName, string adminLoginId, ApplicationType applicationType)
        {
            #region # 验证

            if (this._repMediator.InfoSystemRep.ExistsNo(infoSystemNo))
            {
                throw new ArgumentOutOfRangeException(nameof(infoSystemNo), $"信息系统编号\"{infoSystemNo}\"已存在！");
            }
            if (this._repMediator.UserRep.ExistsNo(adminLoginId))
            {
                throw new ArgumentOutOfRangeException(nameof(adminLoginId), $"用户名：\"{adminLoginId}\"已存在！");
            }

            #endregion

            InfoSystem infoSystem = new InfoSystem(infoSystemNo, infoSystemName, adminLoginId, applicationType);
            User superAdmin = this._unitOfWork.Resolve<User>(CommonConstants.AdminLoginId);
            User systemAdmin = new User(infoSystem.AdminLoginId, $"{infoSystem.Name}管理员", CommonConstants.InitialPassword);
            Role systemAdminRole = new Role($"{infoSystem.Name}管理员", infoSystem.Number, null, CommonConstants.ManagerRoleNo);
            superAdmin.AppendRoles(new[] { systemAdminRole });
            systemAdmin.AppendRoles(new[] { systemAdminRole });
            Menu systemMenu = new Menu(infoSystem.Number, infoSystem.ApplicationType, infoSystem.Name, 0, null, null, null, null);

            this._unitOfWork.RegisterAdd(infoSystem);
            this._unitOfWork.RegisterAdd(systemAdmin);
            this._unitOfWork.RegisterAdd(systemAdminRole);
            this._unitOfWork.RegisterAdd(systemMenu);
            this._unitOfWork.RegisterSave(superAdmin);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 修改信息系统 —— void UpdateInfoSystem(string infoSystemNo, string infoSystemName)
        /// <summary>
        /// 修改信息系统
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="infoSystemName">信息系统名称</param>
        public void UpdateInfoSystem(string infoSystemNo, string infoSystemName)
        {
            #region # 验证

            if (this._repMediator.InfoSystemRep.ExistsNo(infoSystemNo))
            {
                throw new ArgumentOutOfRangeException(nameof(infoSystemNo), $"信息系统编号\"{infoSystemNo}\"已存在！");
            }

            #endregion

            InfoSystem infoSystem = this._unitOfWork.Resolve<InfoSystem>(infoSystemNo);
            infoSystem.UpdateInfo(infoSystemName);

            this._unitOfWork.RegisterSave(infoSystem);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 初始化信息系统 —— void InitInfoSystem(string infoSystemNo, string host...
        /// <summary>
        /// 初始化信息系统
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="host">主机名称</param>
        /// <param name="port">端口</param>
        /// <param name="index">首页</param>
        public void InitInfoSystem(string infoSystemNo, string host, int port, string index)
        {
            InfoSystem infoSystem = this._unitOfWork.Resolve<InfoSystem>(infoSystemNo);
            infoSystem.Init(host, port, index);

            this._unitOfWork.RegisterSave(infoSystem);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 批量初始化信息系统 —— void InitInfoSystems(IEnumerable<InfoSystemParam> initParams)
        /// <summary>
        /// 批量初始化信息系统
        /// </summary>
        /// <param name="initParams">初始化信息系统参数模型集</param>
        public void InitInfoSystems(IEnumerable<InfoSystemParam> initParams)
        {
            #region # 验证

            initParams = initParams?.ToArray() ?? Array.Empty<InfoSystemParam>();
            if (!initParams.Any())
            {
                return;
            }

            #endregion

            IDictionary<string, InfoSystemParam> paramDictionary = initParams.ToDictionary(x => x.infoSystemNo, x => x);
            ICollection<InfoSystem> infoSystems = this._unitOfWork.ResolveRange<InfoSystem>(paramDictionary.Keys);
            foreach (InfoSystem infoSystem in infoSystems)
            {
                InfoSystemParam param = paramDictionary[infoSystem.Number];
                infoSystem.Init(param.host, param.port, param.index);
            }

            this._unitOfWork.RegisterSaveRange(infoSystems);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 创建权限 —— void CreateAuthority(string infoSystemNo...
        /// <summary>
        /// 创建权限
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="authorityName">权限名称</param>
        /// <param name="authorityPath">权限路径</param>
        /// <param name="englishName">英文名称</param>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="namespace">命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="description">描述</param>
        public void CreateAuthority(string infoSystemNo, ApplicationType applicationType, string authorityName, string authorityPath, string englishName, string assemblyName, string @namespace, string className, string methodName, string description)
        {
            #region # 验证

            if (this._repMediator.AuthorityRep.ExistsPath(infoSystemNo, applicationType, authorityPath))
            {
                throw new ArgumentOutOfRangeException(nameof(authorityPath), "给定信息系统与应用程序类型已存在该权限路径！");
            }

            #endregion

            Authority authority = new Authority(infoSystemNo, applicationType, authorityName, authorityPath, englishName, assemblyName, @namespace, className, methodName, description);

            //为系统管理员角色追加权限
            Guid adminRoleId = this._repMediator.RoleRep.GetManagerRoleId(infoSystemNo);
            Role adminRole = this._unitOfWork.Resolve<Role>(adminRoleId);
            adminRole.AppendAuthorities(new[] { authority });

            this._unitOfWork.RegisterAdd(authority);
            this._unitOfWork.RegisterSave(adminRole);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 批量创建权限 —— void CreateAuthorities(string infoSystemNo...
        /// <summary>
        /// 批量创建权限
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="authorityParams">权限参数模型集</param>
        public void CreateAuthorities(string infoSystemNo, ApplicationType applicationType, IEnumerable<AuthorityParam> authorityParams)
        {
            IList<Authority> authorities = new List<Authority>();
            foreach (AuthorityParam param in authorityParams)
            {
                Authority authority = new Authority(infoSystemNo, applicationType, param.authorityName, param.authorityPath, param.englishName, param.assemblyName, param.@namespace, param.className, param.methodName, param.description);

                #region # 验证

                if (this._repMediator.AuthorityRep.ExistsPath(infoSystemNo, applicationType, param.authorityPath))
                {
                    throw new ArgumentOutOfRangeException(nameof(authorityParams), "给定信息系统与应用程序类型已存在该权限路径！");
                }

                #endregion

                authorities.Add(authority);
            }

            //为系统管理员角色追加权限
            Guid adminRoleId = this._repMediator.RoleRep.GetManagerRoleId(infoSystemNo);
            Role adminRole = this._unitOfWork.Resolve<Role>(adminRoleId);
            adminRole.AppendAuthorities(authorities);

            this._unitOfWork.RegisterAddRange(authorities);
            this._unitOfWork.RegisterSave(adminRole);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 修改权限 —— void UpdateAuthority(Guid authorityId...
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="authorityId">权限Id</param>
        /// <param name="authorityName">权限名称</param>
        /// <param name="authorityPath">权限路径</param>
        /// <param name="englishName">英文名称</param>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="namespace">命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="description">描述</param>
        public void UpdateAuthority(Guid authorityId, string authorityName, string authorityPath, string englishName, string assemblyName, string @namespace, string className, string methodName, string description)
        {
            Authority authority = this._unitOfWork.Resolve<Authority>(authorityId);

            #region # 验证

            if (authority.AuthorityPath != authorityPath &&
                this._repMediator.AuthorityRep.ExistsPath(authority.InfoSystemNo, authority.ApplicationType, authorityPath))
            {
                throw new ArgumentOutOfRangeException(nameof(authorityPath), "给定信息系统与应用程序类型已存在该权限路径！");
            }

            #endregion

            authority.UpdateInfo(authorityName, authorityPath, englishName, assemblyName, @namespace, className, methodName, description);

            this._unitOfWork.RegisterSave(authority);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 删除权限 —— void RemoveAuthority(Guid authorityId)
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="authorityId">权限Id</param>
        public void RemoveAuthority(Guid authorityId)
        {
            Authority authority = this._unitOfWork.Resolve<Authority>(authorityId);

            //清空关系
            authority.ClearRoleRelations();
            authority.ClearMenuRelations();

            this._unitOfWork.RegisterRemove(authority);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 创建菜单 —— Guid CreateMenu(string infoSystemNo, string menuName...
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="menuName">菜单名称</param>
        /// <param name="sort">排序</param>
        /// <param name="url">链接地址</param>
        /// <param name="path">路径</param>
        /// <param name="icon">图标</param>
        /// <param name="parentNodeId">上级节点Id</param>
        /// <returns>菜单Id</returns>
        public Guid CreateMenu(string infoSystemNo, ApplicationType applicationType, string menuName, int sort, string url, string path, string icon, Guid? parentNodeId)
        {
            #region # 验证

            if (this._repMediator.MenuRep.Exists(parentNodeId, applicationType, menuName))
            {
                throw new ArgumentOutOfRangeException(nameof(menuName), "给定菜单级别下，相同应用程序类型的菜单名称已存在！");
            }

            #endregion

            Menu parentNode = parentNodeId.HasValue
                ? this._unitOfWork.Resolve<Menu>(parentNodeId.Value)
                : null;
            Menu menu = new Menu(infoSystemNo, applicationType, menuName, sort, url, path, icon, parentNode);

            this._unitOfWork.RegisterAdd(menu);
            this._unitOfWork.Commit();

            return menu.Id;
        }
        #endregion

        #region # 修改菜单 —— void UpdateMenu(Guid menuId, string menuName...
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <param name="menuName">菜单名称</param>
        /// <param name="sort">排序</param>
        /// <param name="url">链接地址</param>
        /// <param name="path">路径</param>
        /// <param name="icon">图标</param>
        public void UpdateMenu(Guid menuId, string menuName, int sort, string url, string path, string icon)
        {
            Menu menu = this._unitOfWork.Resolve<Menu>(menuId);

            #region # 验证

            if (menuName != menu.Name)
            {
                Guid? parentNodeId = menu.ParentNode?.Id;
                if (this._repMediator.MenuRep.Exists(parentNodeId, menu.ApplicationType, menuName))
                {
                    throw new ArgumentOutOfRangeException(nameof(menuName), "给定菜单级别下，相同应用程序类型的菜单名称已存在！");
                }
            }

            #endregion

            menu.UpdateInfo(menuName, sort, url, path, icon);

            this._unitOfWork.RegisterSave(menu);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 删除菜单 —— void RemoveMenu(Guid menuId)
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        public void RemoveMenu(Guid menuId)
        {
            //递归删除
            Menu menu = this._unitOfWork.Resolve<Menu>(menuId);
            foreach (Menu subMenu in menu.SubNodes.ToArray())
            {
                this.RemoveMenu(subMenu.Id);
            }

            //清空权限关系
            menu.ClearAuthorityRelations();

            this._unitOfWork.RegisterPhysicsRemove(menu);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 关联权限到菜单 —— void RelateAuthoritiesToMenu(Guid menuId...
        /// <summary>
        /// 关联权限到菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <param name="authorityIds">权限Id集</param>
        public void RelateAuthoritiesToMenu(Guid menuId, IEnumerable<Guid> authorityIds)
        {
            authorityIds = authorityIds?.Distinct().ToArray() ?? Array.Empty<Guid>();

            Menu menu = this._unitOfWork.Resolve<Menu>(menuId);

            #region # 验证

            if (!menu.IsLeaf)
            {
                throw new InvalidOperationException("非叶子级菜单不可关联权限！");
            }

            #endregion

            ICollection<Authority> authorities = this._unitOfWork.ResolveRange<Authority>(authorityIds);
            menu.RelateAuthorities(authorities);

            this._unitOfWork.RegisterSave(menu);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 创建角色 —— void CreateRole(string infoSystemNo, string roleName...
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="description">描述</param>
        /// <param name="authorityIds">权限Id集</param>
        public void CreateRole(string infoSystemNo, string roleName, string description, IEnumerable<Guid> authorityIds)
        {
            authorityIds = authorityIds?.Distinct().ToArray() ?? Array.Empty<Guid>();

            ICollection<Authority> authorities = this._unitOfWork.ResolveRange<Authority>(authorityIds);

            Role role = new Role(roleName, infoSystemNo, description);
            role.RelateAuthorities(authorities);

            this._unitOfWork.RegisterAdd(role);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 修改角色 —— void UpdateRole(Guid roleId, string roleName...
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="description">描述</param>
        /// <param name="authorityIds">权限Id集</param>
        public void UpdateRole(Guid roleId, string roleName, string description, IEnumerable<Guid> authorityIds)
        {
            authorityIds = authorityIds?.Distinct().ToArray() ?? Array.Empty<Guid>();

            ICollection<Authority> authorities = this._unitOfWork.ResolveRange<Authority>(authorityIds);

            Role role = this._unitOfWork.Resolve<Role>(roleId);
            role.UpdateInfo(roleName, description);
            role.RelateAuthorities(authorities);

            this._unitOfWork.RegisterSave(role);
            this._unitOfWork.Commit();
        }

        #endregion

        #region # 删除角色 —— void RemoveRole(Guid roleId)
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        public void RemoveRole(Guid roleId)
        {
            Role role = this._unitOfWork.Resolve<Role>(roleId);

            #region # 验证

            if (role.Users.Any())
            {
                throw new InvalidOperationException($"角色\"{role.Name}\"已被用户使用，不可删除！");
            }

            #endregion

            //清空角色/权限关系
            role.ClearAuthorityRelations();

            this._unitOfWork.RegisterPhysicsRemove(role);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 关联权限到角色 —— void RelateAuthoritiesToRole(Guid roleId, IEnumerable<Guid> authorityIds)
        /// <summary>
        /// 关联权限到角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="authorityIds">权限Id集</param>
        public void RelateAuthoritiesToRole(Guid roleId, IEnumerable<Guid> authorityIds)
        {
            authorityIds = authorityIds?.Distinct().ToArray() ?? Array.Empty<Guid>();

            ICollection<Authority> authorities = this._unitOfWork.ResolveRange<Authority>(authorityIds);

            Role role = this._unitOfWork.Resolve<Role>(roleId);
            role.RelateAuthorities(authorities);

            this._unitOfWork.RegisterSave(role);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 追加权限到角色 —— void AppendAuthoritiesToRole(Guid roleId, IEnumerable<Guid> authorityIds)
        /// <summary>
        /// 追加权限到角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="authorityIds">权限Id集</param>
        public void AppendAuthoritiesToRole(Guid roleId, IEnumerable<Guid> authorityIds)
        {
            #region # 验证

            authorityIds = authorityIds?.Distinct().ToArray() ?? Array.Empty<Guid>();
            if (!authorityIds.Any())
            {
                return;
            }

            #endregion

            ICollection<Authority> authorities = this._unitOfWork.ResolveRange<Authority>(authorityIds);

            Role role = this._unitOfWork.Resolve<Role>(roleId);
            role.AppendAuthorities(authorities);

            this._unitOfWork.RegisterSave(role);
            this._unitOfWork.Commit();
        }
        #endregion


        //查询部分

        #region # 获取信息系统 —— InfoSystemInfo GetInfoSystem(string infoSystemNo)
        /// <summary>
        /// 获取信息系统
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <returns>信息系统</returns>
        public InfoSystemInfo GetInfoSystem(string infoSystemNo)
        {
            InfoSystem infoSystem = this._repMediator.InfoSystemRep.Single(infoSystemNo);

            return infoSystem.ToDTO();
        }
        #endregion

        #region # 获取信息系统列表 —— IEnumerable<InfoSystemInfo> GetInfoSystems()
        /// <summary>
        /// 获取信息系统列表
        /// </summary>
        /// <returns>信息系统列表</returns>
        public IEnumerable<InfoSystemInfo> GetInfoSystems()
        {
            ICollection<InfoSystem> infoSystems = this._repMediator.InfoSystemRep.FindAll();
            IEnumerable<InfoSystemInfo> infoSystemInfos = infoSystems.Select(x => x.ToDTO());

            return infoSystemInfos;
        }
        #endregion

        #region # 分页获取信息系统列表 —— PageModel<InfoSystemInfo> GetInfoSystemsByPage(string keywords...
        /// <summary>
        /// 分页获取信息系统列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>信息系统列表</returns>
        public PageModel<InfoSystemInfo> GetInfoSystemsByPage(string keywords, int pageIndex, int pageSize)
        {
            ICollection<InfoSystem> infoSystems = this._repMediator.InfoSystemRep.FindByPage(keywords, pageIndex, pageSize, out int rowCount, out int pageCount);
            IEnumerable<InfoSystemInfo> infoSystemInfos = infoSystems.Select(x => x.ToDTO());

            return new PageModel<InfoSystemInfo>(infoSystemInfos, pageIndex, pageSize, pageCount, rowCount);
        }
        #endregion

        #region # 获取权限 —— AuthorityInfo GetAuthority(Guid authorityId)
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="authorityId">权限Id</param>
        /// <returns>权限视图模型</returns>
        public AuthorityInfo GetAuthority(Guid authorityId)
        {
            Authority authority = this._repMediator.AuthorityRep.Single(authorityId);

            IDictionary<string, InfoSystem> infoSystems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> infoSystemInfos = infoSystems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            AuthorityInfo authorityInfo = authority.ToDTO(infoSystemInfos);

            return authorityInfo;
        }
        #endregion

        #region # 获取权限列表 —— IEnumerable<AuthorityInfo> GetAuthorities(string keywords...
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="menuId">菜单Id</param>
        /// <param name="roleId">角色Id</param>
        /// <returns>权限列表</returns>
        public IEnumerable<AuthorityInfo> GetAuthorities(string keywords, string infoSystemNo, ApplicationType? applicationType, Guid? menuId, Guid? roleId)
        {
            ICollection<Authority> authorities = this._repMediator.AuthorityRep.Find(keywords, infoSystemNo, applicationType, menuId, roleId);

            IDictionary<string, InfoSystem> infoSystems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> infoSystemInfos = infoSystems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            return authorities.Select(x => x.ToDTO(infoSystemInfos));
        }
        #endregion

        #region # 分页获取权限列表 —— PageModel<AuthorityInfo> GetAuthoritiesByPage(string keywords...
        /// <summary>
        /// 分页获取权限列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>权限列表</returns>
        public PageModel<AuthorityInfo> GetAuthoritiesByPage(string keywords, string infoSystemNo, ApplicationType? applicationType, int pageIndex, int pageSize)
        {
            ICollection<Authority> authorities = this._repMediator.AuthorityRep.FindByPage(keywords, infoSystemNo, applicationType, pageIndex, pageSize, out int rowCount, out int pageCount);

            IDictionary<string, InfoSystem> infoSystems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> infoSystemInfos = infoSystems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            IEnumerable<AuthorityInfo> authorityInfos = authorities.Select(x => x.ToDTO(infoSystemInfos));

            return new PageModel<AuthorityInfo>(authorityInfos, pageIndex, pageSize, pageCount, rowCount);
        }
        #endregion

        #region # 获取菜单 —— MenuInfo GetMenu(Guid menuId)
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns>菜单</returns>
        public MenuInfo GetMenu(Guid menuId)
        {
            Menu menu = this._repMediator.MenuRep.Single(menuId);

            IDictionary<string, InfoSystem> infoSystems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> infoSystemInfos = infoSystems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            return menu.ToDTO(infoSystemInfos);
        }
        #endregion

        #region # 获取菜单列表 —— IEnumerable<MenuInfo> GetMenus(string infoSystemNo...
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单列表</returns>
        public IEnumerable<MenuInfo> GetMenus(string infoSystemNo, ApplicationType? applicationType)
        {
            ICollection<Menu> menus = this._repMediator.MenuRep.FindBySystem(infoSystemNo, applicationType);

            IEnumerable<string> infoSystemNos = menus.Select(x => x.InfoSystemNo);
            IDictionary<string, InfoSystemInfo> infoSystemInfos = this._repMediator.InfoSystemRep.Find(infoSystemNos).ToDictionary(x => x.Key, x => x.Value.ToDTO());

            return menus.Select(x => x.ToDTO(infoSystemInfos));
        }
        #endregion

        #region # 分页获取菜单列表 —— PageModel<MenuInfo> GetMenusByPage(string keywords...
        /// <summary>
        /// 分页获取菜单列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>菜单列表</returns>
        public PageModel<MenuInfo> GetMenusByPage(string keywords, string infoSystemNo, ApplicationType? applicationType, int pageIndex, int pageSize)
        {
            ICollection<Menu> menus = this._repMediator.MenuRep.FindByPage(keywords, infoSystemNo, applicationType, pageIndex, pageSize, out int rowCount, out int pageCount);

            IEnumerable<string> infoSystemNos = menus.Select(x => x.InfoSystemNo);
            IDictionary<string, InfoSystemInfo> infoSystemInfos = this._repMediator.InfoSystemRep.Find(infoSystemNos).ToDictionary(x => x.Key, x => x.Value.ToDTO());

            IEnumerable<MenuInfo> menuInfos = menus.Select(x => x.ToDTO(infoSystemInfos));

            return new PageModel<MenuInfo>(menuInfos, pageIndex, pageSize, pageCount, rowCount);
        }
        #endregion

        #region # 获取角色 —— RoleInfo GetRole(Guid roleId)
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>角色</returns>
        public RoleInfo GetRole(Guid roleId)
        {
            Role role = this._repMediator.RoleRep.Single(roleId);

            IDictionary<string, InfoSystem> infoSystems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> infoSystemInfos = infoSystems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            RoleInfo roleInfo = role.ToDTO(infoSystemInfos);

            return roleInfo;
        }
        #endregion

        #region # 获取角色列表 —— IEnumerable<RoleInfo> GetRoles(string infoSystemNo...
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="loginId">用户名</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <returns>角色列表</returns>
        public IEnumerable<RoleInfo> GetRoles(string keywords, string loginId, string infoSystemNo)
        {
            IEnumerable<Role> roles = this._repMediator.RoleRep.Find(keywords, loginId, infoSystemNo);

            IDictionary<string, InfoSystem> infoSystems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> infoSystemInfos = infoSystems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            return roles.Select(x => x.ToDTO(infoSystemInfos));
        }
        #endregion

        #region # 分页获取角色列表 —— PageModel<RoleInfo> GetRolesByPage(string keywords...
        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>角色列表</returns>
        public PageModel<RoleInfo> GetRolesByPage(string keywords, string infoSystemNo, int pageIndex, int pageSize)
        {
            ICollection<Role> roles = this._repMediator.RoleRep.FindByPage(keywords, infoSystemNo, pageIndex, pageSize, out int rowCount, out int pageCount);

            IDictionary<string, InfoSystem> infoSystems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> infoSystemInfos = infoSystems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            IEnumerable<RoleInfo> roleInfos = roles.Select(x => x.ToDTO(infoSystemInfos));

            return new PageModel<RoleInfo>(roleInfos, pageIndex, pageSize, pageCount, rowCount);
        }
        #endregion

        #region # 是否存在权限 —— bool ExistsAuthority(string infoSystemNo, ApplicationType...
        /// <summary>
        /// 是否存在权限
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="authorityPath">权限路径</param>
        /// <returns>是否存在</returns>
        public bool ExistsAuthority(string infoSystemNo, ApplicationType applicationType, string authorityPath)
        {
            return this._repMediator.AuthorityRep.ExistsPath(infoSystemNo, applicationType, authorityPath);
        }
        #endregion

        #region # 是否存在角色 —— bool ExistsRole(string infoSystemNo, Guid? roleId, string roleName)
        /// <summary>
        /// 是否存在角色
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="roleName">角色名称</param>
        /// <returns>是否存在</returns>
        public bool ExistsRole(string infoSystemNo, Guid? roleId, string roleName)
        {
            return this._repMediator.RoleRep.Exists(infoSystemNo, roleId, roleName);
        }
        #endregion
    }
}
