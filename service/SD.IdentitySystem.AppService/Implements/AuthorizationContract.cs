using SD.CacheManager;
using SD.IdentitySystem.AppService.Maps;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using SD.IdentitySystem.Domain.Mediators;
using SD.IdentitySystem.IAppService.DTOs.Inputs;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using ShSoft.Common.PoweredByLee;
using ShSoft.Infrastructure.DTOBase;
using ShSoft.Infrastructure.Global.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using ValueObjects = ShSoft.ValueObjects.Enums;

namespace SD.IdentitySystem.AppService.Implements
{
    /// <summary>
    /// 权限服务契约实现
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class AuthorizationContract : IAuthorizationContract
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
        public AuthorizationContract(DomainServiceMediator svcMediator, RepositoryMediator repMediator, IUnitOfWorkIdentity unitOfWork)
        {
            this._svcMediator = svcMediator;
            this._repMediator = repMediator;
            this._unitOfWork = unitOfWork;
        }

        #endregion


        //命令部分

        #region # 创建信息系统 —— void CreateInfoSystem(string systemNo, string systemName...

        /// <summary>
        /// 创建信息系统
        /// </summary>
        /// <param name="systemNo">组织编号</param>
        /// <param name="systemName">信息系统名称</param>
        /// <param name="adminLoginId">系统管理员登录名</param>
        /// <param name="applicationType">应用程序类型</param>
        public void CreateInfoSystem(string systemNo, string systemName, string adminLoginId, ValueObjects.ApplicationType applicationType)
        {
            //验证
            Assert.IsFalse(this._repMediator.UserRep.Exists(adminLoginId), string.Format("登录名：\"{0}\"已存在，请重试！", adminLoginId));

            InfoSystem infoSystem = new InfoSystem(systemNo, systemName, adminLoginId, applicationType);

            this._unitOfWork.RegisterAdd(infoSystem);
            this._unitOfWork.UnitedCommit();

            //清除缓存
            CacheMediator.Remove(typeof(IInfoSystemRepository).FullName);
        }
        #endregion

        #region # 初始化信息系统 —— void InitInfoSystem(string systemNo, string host...
        /// <summary>
        /// 初始化信息系统
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="host">主机名称</param>
        /// <param name="port">端口</param>
        /// <param name="index">首页</param>
        public void InitInfoSystem(string systemNo, string host, int port, string index)
        {
            InfoSystem currentSystem = this._unitOfWork.Resolve<InfoSystem>(systemNo);
            currentSystem.Init(host, port, index);

            this._unitOfWork.RegisterSave(currentSystem);
            this._unitOfWork.Commit();

            //清除缓存
            CacheMediator.Remove(typeof(IInfoSystemRepository).FullName);
        }
        #endregion

        #region # 批量初始化信息系统 —— void InitInfoSystems(IEnumerable<InfoSystemParam> initParams)
        /// <summary>
        /// 批量初始化信息系统
        /// </summary>
        /// <param name="initParams">初始化信息系统参数模型集</param>
        public void InitInfoSystems(IEnumerable<InfoSystemParam> initParams)
        {
            foreach (InfoSystemParam param in initParams)
            {
                InfoSystem currentSystem = this._unitOfWork.Resolve<InfoSystem>(param.SystemNo);
                currentSystem.Init(param.Host, param.Port, param.Index);

                this._unitOfWork.RegisterSave(currentSystem);
            }

            this._unitOfWork.Commit();
        }
        #endregion


        #region # 批量创建权限 —— IEnumerable<Guid> CreateAuthorities(string systemNo...
        /// <summary>
        /// 批量创建权限
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="authorityParams">权限参数模型集</param>
        /// <returns>权限Id集</returns>
        public IEnumerable<Guid> CreateAuthorities(string systemNo, IEnumerable<AuthorityParam> authorityParams)
        {
            //验证
            Assert.IsTrue(this._repMediator.InfoSystemRep.Exists(systemNo), string.Format("编号为\"{0}\"的信息系统不存在！", systemNo));

            IList<Guid> authorityIds = new List<Guid>();

            foreach (AuthorityParam param in authorityParams)
            {
                Authority authority = new Authority(systemNo, param.AuthorityName, param.EnglishName, param.Description, param.AssemblyName, param.Namespace, param.ClassName, param.MethodName);

                //验证
                Assert.IsFalse(this._repMediator.AuthorityRep.ExistsPath(authority.AuthorityPath), "已存在该权限！");

                this._unitOfWork.RegisterAdd(authority);
                authorityIds.Add(authority.Id);
            }

            this._unitOfWork.UnitedCommit();

            return authorityIds;
        }
        #endregion

        #region # 修改权限 —— void UpdateAuthority(Guid authorityId...
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="authorityId">权限Id</param>
        /// <param name="authorityParam">权限参数模型</param>
        public void UpdateAuthority(Guid authorityId, AuthorityParam authorityParam)
        {
            Authority authority = this._unitOfWork.Resolve<Authority>(authorityId);

            authority.UpdateInfo(authorityParam.AuthorityName, authorityParam.EnglishName, authorityParam.Description, authorityParam.AssemblyName, authorityParam.Namespace, authorityParam.ClassName, authorityParam.MethodName);

            this._unitOfWork.RegisterSave(authority);
            this._unitOfWork.UnitedCommit();
        }
        #endregion

        #region # 删除权限 —— void RemoveAuthority(Guid authorityId)
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="authorityId">权限Id</param>
        public void RemoveAuthority(Guid authorityId)
        {
            this._unitOfWork.RegisterRemove<Authority>(authorityId);
            this._unitOfWork.UnitedCommit();
        }
        #endregion


        #region # 创建菜单 —— Guid CreateMenu(string systemNo, string menuName...
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="menuName">菜单名称</param>
        /// <param name="sort">排序（倒序）</param>
        /// <param name="url">链接地址</param>
        /// <param name="icon">图标</param>
        /// <param name="parentId">上级菜单Id</param>
        /// <returns>菜单Id</returns>
        public Guid CreateMenu(string systemNo, string menuName, int sort, string url, string icon, Guid? parentId)
        {
            //验证参数
            Assert.IsTrue(this._repMediator.InfoSystemRep.Exists(systemNo), string.Format("编号为\"{0}\"的信息系统不存在！", systemNo));
            Assert.IsFalse(this._repMediator.MenuRep.Exists(parentId, menuName), "给定菜单级别下菜单名称已存在！");

            Menu parentMenu = parentId == null ? null : this._unitOfWork.Resolve<Menu>(parentId.Value);
            Menu menu = new Menu(systemNo, menuName, sort, url, icon, parentMenu);

            this._unitOfWork.RegisterAdd(menu);
            this._unitOfWork.UnitedCommit();

            return menu.Id;
        }
        #endregion

        #region # 修改菜单 —— void UpdateMenu(Guid menuId, string menuName...
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <param name="menuName">菜单名称</param>
        /// <param name="sort">排序（倒序）</param>
        /// <param name="url">链接地址</param>
        /// <param name="icon">图标</param>
        public void UpdateMenu(Guid menuId, string menuName, int sort, string url, string icon)
        {
            Menu currentMenu = this._unitOfWork.Resolve<Menu>(menuId);

            #region # 验证参数

            if (menuName != currentMenu.Name)
            {
                Guid? parentId = currentMenu.ParentNode == null ? (Guid?)null : currentMenu.ParentNode.Id;
                Assert.IsFalse(this._repMediator.MenuRep.Exists(parentId, menuName), "给定菜单级别下菜单名称已存在！");
            }

            #endregion

            currentMenu.UpdateInfo(menuName, sort, url, icon);

            this._unitOfWork.RegisterSave(currentMenu);
            this._unitOfWork.UnitedCommit();
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
            Menu currentMenu = this._unitOfWork.Resolve<Menu>(menuId);

            foreach (Menu subMenu in currentMenu.SubNodes.ToArray())
            {
                this.RemoveMenu(subMenu.Id);
            }

            //清空关联
            currentMenu.ClearRelations();

            this._unitOfWork.RegisterPhysicsRemove<Menu>(menuId);
            this._unitOfWork.UnitedCommit();
        }
        #endregion

        #region # 关联权限 —— void RelateAuthorities(Guid menuId...
        /// <summary>
        /// 关联权限
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <param name="authorityIds">权限Id集</param>
        public void RelateAuthorities(Guid menuId, IEnumerable<Guid> authorityIds)
        {
            Menu currentMenu = this._unitOfWork.Resolve<Menu>(menuId);

            #region # 验证

            if (!currentMenu.IsLeaf)
            {
                throw new InvalidOperationException("非叶子级菜单不可关联权限！");
            }

            #endregion

            IList<Authority> authorities = new List<Authority>();

            foreach (Guid authorityId in authorityIds)
            {
                Authority currentAuthority = this._unitOfWork.Resolve<Authority>(authorityId);

                authorities.Add(currentAuthority);
            }

            currentMenu.RelateAuthorities(authorities);

            this._unitOfWork.RegisterSave(currentMenu);
            this._unitOfWork.UnitedCommit();
        }
        #endregion


        #region # 创建角色 —— Guid CreateRole(string systemNo, string roleName...
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="description">角色描述</param>
        /// <param name="authorityIds">权限Id集</param>
        /// <returns>角色Id</returns>
        public Guid CreateRole(string systemNo, string roleName, string description, IEnumerable<Guid> authorityIds)
        {
            //验证
            Assert.IsTrue(this._repMediator.InfoSystemRep.Exists(systemNo));

            //创建角色
            Role role = new Role(roleName, systemNo, description);

            //分配权限
            IEnumerable<Authority> authorities = authorityIds.Select(authorityId => this._unitOfWork.Resolve<Authority>(authorityId));
            role.SetAuthorities(authorities);

            this._unitOfWork.RegisterAdd(role);
            this._unitOfWork.UnitedCommit();

            return role.Id;
        }
        #endregion

        #region # 为角色分配权限 —— void SetAuthorities(Guid roleId, IEnumerable<Guid> authorityIds)
        /// <summary>
        /// 为角色分配权限
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="authorityIds">权限Id集</param>
        public void SetAuthorities(Guid roleId, IEnumerable<Guid> authorityIds)
        {
            Role role = this._unitOfWork.Resolve<Role>(roleId);

            IEnumerable<Authority> authorities = authorityIds.Select(authorityId => this._unitOfWork.Resolve<Authority>(authorityId));

            role.SetAuthorities(authorities);
            this._unitOfWork.RegisterSave(role);
            this._unitOfWork.UnitedCommit();
        }
        #endregion

        #region # 为角色追加权限 —— void AppendAuthorities(Guid roleId, IEnumerable<Guid> authorityIds)
        /// <summary>
        /// 为角色追加权限
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="authorityIds">权限Id集</param>
        public void AppendAuthorities(Guid roleId, IEnumerable<Guid> authorityIds)
        {
            Role role = this._unitOfWork.Resolve<Role>(roleId);

            IEnumerable<Authority> authorities = authorityIds.Select(authorityId => this._unitOfWork.Resolve<Authority>(authorityId));

            role.AppendAuthorities(authorities);
            this._unitOfWork.RegisterSave(role);
            this._unitOfWork.UnitedCommit();
        }
        #endregion

        #region # 修改角色 —— void UpdateRole(Guid roleId, string roleName...
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="description">角色描述</param>
        /// <param name="authorityIds">权限Id集</param>
        public void UpdateRole(Guid roleId, string roleName, string description, IEnumerable<Guid> authorityIds)
        {
            Role role = this._unitOfWork.Resolve<Role>(roleId);

            role.UpdateInfo(roleName, roleName);

            IEnumerable<Authority> authorities = authorityIds.Distinct().Select(authorityId => this._unitOfWork.Resolve<Authority>(authorityId));
            role.SetAuthorities(authorities);

            this._unitOfWork.RegisterSave(role);
            this._unitOfWork.UnitedCommit();
        }

        #endregion

        #region # 删除角色 —— void RemoveRole(Guid roleId)
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        public void RemoveRole(Guid roleId)
        {
            #region # 验证

            Role currentRole = this._unitOfWork.Resolve<Role>(roleId);

            if (currentRole.Users.Any())
            {
                throw new InvalidOperationException(string.Format("角色\"{0}\"已被用户使用，不可删除！", currentRole.Name));
            }

            #endregion

            this._unitOfWork.RegisterPhysicsRemove<Role>(roleId);
            this._unitOfWork.UnitedCommit();
        }
        #endregion


        //查询部分

        #region # 获取信息系统 —— InfoSystemInfo GetInfoSystem(string systemNo)
        /// <summary>
        /// 获取信息系统
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>信息系统</returns>
        public InfoSystemInfo GetInfoSystem(string systemNo)
        {
            InfoSystem currentSystem = this._repMediator.InfoSystemRep.Single(systemNo);

            return currentSystem.ToDTO();
        }
        #endregion

        #region # 获取信息系统列表 —— IEnumerable<InfoSystemInfo> GetInfoSystems()
        /// <summary>
        /// 获取信息系统列表
        /// </summary>
        /// <returns>信息系统列表</returns>
        public IEnumerable<InfoSystemInfo> GetInfoSystems()
        {
            IEnumerable<InfoSystem> systems = this._repMediator.InfoSystemRep.FindAll();

            IEnumerable<InfoSystemInfo> systemInfos = systems.Select(x => x.ToDTO());

            return systemInfos;
        }
        #endregion

        #region # 获取信息系统列表 —— IEnumerable<InfoSystemInfo> GetInfoSystems(string loginId)
        /// <summary>
        /// 获取信息系统列表
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <returns>信息系统列表</returns>
        public IEnumerable<InfoSystemInfo> GetInfoSystemsByUser(string loginId)
        {
            User currentUser = this._repMediator.UserRep.Single(loginId);

            IEnumerable<string> systemNos = currentUser.GetInfoSystemNos();
            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.Find(systemNos);

            return systems.Values.Select(x => x.ToDTO());
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
            int rowCount, pageCount;

            IEnumerable<InfoSystem> specSystems = this._repMediator.InfoSystemRep.FindByPage(keywords, pageIndex, pageSize, out rowCount, out pageCount);

            IEnumerable<InfoSystemInfo> specSystemInfos = specSystems.Select(x => x.ToDTO());

            return new PageModel<InfoSystemInfo>(specSystemInfos, pageIndex, pageSize, pageCount, rowCount);
        }
        #endregion


        #region # 分页获取权限列表 —— PageModel<AuthorityInfo> GetAuthoritiesByPage(string systemNo...
        /// <summary>
        /// 分页获取权限列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>权限列表</returns>
        public PageModel<AuthorityInfo> GetAuthoritiesByPage(string systemNo, string keywords, int pageIndex, int pageSize)
        {
            int rowCount, pageCount;

            IEnumerable<Authority> authorities = this._repMediator.AuthorityRep.FindByPage(systemNo, keywords, pageIndex, pageSize, out rowCount, out pageCount);

            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> systemInfos = systems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            IEnumerable<AuthorityInfo> authorityInfos = authorities.Select(x => x.ToDTO(systemInfos));

            return new PageModel<AuthorityInfo>(authorityInfos, pageIndex, pageSize, pageCount, rowCount);
        }
        #endregion

        #region # 获取权限列表 —— IEnumerable<AuthorityInfo> GetAuthorities(string systemNo)
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>权限列表</returns>
        public IEnumerable<AuthorityInfo> GetAuthorities(string systemNo)
        {
            IEnumerable<Authority> authorities = this._repMediator.AuthorityRep.FindBySystem(systemNo);

            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> systemInfos = systems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            return authorities.Select(x => x.ToDTO(systemInfos));
        }
        #endregion

        #region # 根据菜单获取权限列表 —— IEnumerable<AuthorityInfo> GetAuthoritiesByMenu(...
        /// <summary>
        /// 根据菜单获取权限列表
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns>权限列表</returns>
        public IEnumerable<AuthorityInfo> GetAuthoritiesByMenu(Guid menuId)
        {
            Menu currentMenu = this._repMediator.MenuRep.Single(menuId);

            //验证叶子节点
            Assert.IsTrue(currentMenu.IsLeaf, string.Format("Id为\"{0}\"的菜单不是叶子节点！", menuId));

            IEnumerable<Authority> authorities = this._repMediator.AuthorityRep.FindByMenu(menuId);

            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> systemInfos = systems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            return authorities.Select(x => x.ToDTO(systemInfos));
        }
        #endregion

        #region # 根据角色获取权限列表 —— IEnumerable<AuthorityInfo> GetAuthoritiesByRole(...
        /// <summary>
        /// 根据角色获取权限列表
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>权限列表</returns>
        public IEnumerable<AuthorityInfo> GetAuthoritiesByRole(Guid roleId)
        {
            IEnumerable<Authority> authorities = this._repMediator.AuthorityRep.FindByRole(roleId);

            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> systemInfos = systems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            IEnumerable<AuthorityInfo> authorityInfos = authorities.Select(x => x.ToDTO(systemInfos));

            return authorityInfos;
        }
        #endregion

        #region # 根据角色获取权限Id列表 —— IEnumerable<Guid> GetAuthorityIdsByRole(...
        /// <summary>
        /// 根据角色获取权限Id列表
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>权限Id列表</returns>
        public IEnumerable<Guid> GetAuthorityIdsByRole(Guid roleId)
        {
            IEnumerable<Guid> authorityIds = this._repMediator.AuthorityRep.FindIdsByRole(roleId);

            return authorityIds;
        }
        #endregion

        #region # 获取权限Id列表 —— IEnumerable<Guid> GetAuthorityIds(string systemNo)
        /// <summary>
        /// 获取权限Id列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>权限Id列表</returns>
        public IEnumerable<Guid> GetAuthorityIds(string systemNo)
        {
            return this._repMediator.AuthorityRep.FindAuthorityIds(systemNo);
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
            Authority currentAuthority = this._repMediator.AuthorityRep.Single(authorityId);

            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> systemInfos = systems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            return currentAuthority.ToDTO(systemInfos);
        }
        #endregion

        #region # 是否存在权限 —— bool ExistsAuthority(string assemblyName, string @namespace...
        /// <summary>
        /// 是否存在权限
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="namespace">命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        /// <returns>是否存在</returns>
        public bool ExistsAuthority(string assemblyName, string @namespace, string className, string methodName)
        {
            return this._repMediator.AuthorityRep.ExistsPath(assemblyName, @namespace, className, methodName);
        }
        #endregion


        #region # 分页获取菜单列表 —— PageModel<MenuInfo> GetMenusByPage(string keywords...
        /// <summary>
        /// 分页获取菜单列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>菜单列表</returns>
        public PageModel<MenuInfo> GetMenusByPage(string keywords, string systemNo, int pageIndex, int pageSize)
        {
            int rowCount, pageCount;

            IEnumerable<Menu> specMenus = this._repMediator.MenuRep.FindByPage(keywords, pageIndex, pageSize, out rowCount, out pageCount);

            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> systemInfos = systems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            IEnumerable<MenuInfo> specMenuInfos = specMenus.Select(x => x.ToDTO(systemInfos));

            return new PageModel<MenuInfo>(specMenuInfos, pageIndex, pageSize, pageCount, rowCount);
        }
        #endregion

        #region # 获取菜单列表 —— IEnumerable<MenuInfo> GetMenus(string systemNo)
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>菜单列表</returns>
        public IEnumerable<MenuInfo> GetMenus(string systemNo)
        {
            IEnumerable<Menu> menus = this._repMediator.MenuRep.FindBySystem(systemNo);

            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> systemInfos = systems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            return menus.Select(x => x.ToDTO(systemInfos));
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
            Menu currentMenu = this._repMediator.MenuRep.Single(menuId);

            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> systemInfos = systems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            return currentMenu.ToDTO(systemInfos);
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
            Role currentRole = this._repMediator.RoleRep.Single(roleId);

            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> systemInfos = systems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            return currentRole.ToDTO(systemInfos);
        }
        #endregion

        #region # 获取角色列表 —— IEnumerable<RoleInfo> GetRoles(string systemNo)
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>角色列表</returns>
        public IEnumerable<RoleInfo> GetRoles(string systemNo)
        {
            IEnumerable<Role> roles = this._repMediator.RoleRep.FindBySystem(systemNo);

            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> systemInfos = systems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            return roles.Select(x => x.ToDTO(systemInfos));
        }
        #endregion

        #region # 分页获取角色列表 —— PageModel<RoleInfo> GetRolesByPage(string systemNo...
        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>角色列表</returns>
        public PageModel<RoleInfo> GetRolesByPage(string systemNo, string keywords, int pageIndex, int pageSize)
        {
            int rowCount, pageCount;

            IEnumerable<Role> roles = this._repMediator.RoleRep.FindByPage(systemNo, keywords, pageIndex, pageSize, out rowCount, out pageCount);

            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> systemInfos = systems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            IEnumerable<RoleInfo> roleInfos = roles.Select(x => x.ToDTO(systemInfos));

            return new PageModel<RoleInfo>(roleInfos, pageIndex, pageSize, pageCount, rowCount);
        }
        #endregion

        #region # 是否存在角色 —— bool ExistsRole(string systemNo, Guid? roleId, string roleName)
        /// <summary>
        /// 是否存在角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="roleName">角色名称</param>
        /// <returns>是否存在</returns>
        public bool ExistsRole(string systemNo, Guid? roleId, string roleName)
        {
            return this._svcMediator.RoleSvc.ExistsRole(systemNo, roleId, roleName);
        }
        #endregion
    }
}