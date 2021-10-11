using SD.Common;
using SD.IdentitySystem.AppService.Maps;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories;
using SD.IdentitySystem.Domain.Mediators;
using SD.IdentitySystem.IAppService.DTOs.Inputs;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.Global.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
#if NET461_OR_GREATER
using System.ServiceModel;
#endif
#if NETSTANDARD2_0_OR_GREATER
using CoreWCF;
#endif

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
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="systemName">信息系统名称</param>
        /// <param name="adminLoginId">系统管理员账号</param>
        /// <param name="applicationType">应用程序类型</param>
        public void CreateInfoSystem(string systemNo, string systemName, string adminLoginId, ApplicationType applicationType)
        {
            #region # 验证

            if (this._repMediator.InfoSystemRep.ExistsNo(systemNo))
            {
                throw new ArgumentOutOfRangeException(nameof(systemNo), $"信息系统编号\"{systemNo}\"已存在！");
            }
            if (this._repMediator.UserRep.ExistsNo(adminLoginId))
            {
                throw new ArgumentOutOfRangeException(nameof(adminLoginId), $"用户名：\"{adminLoginId}\"已存在！");
            }

            #endregion

            InfoSystem infoSystem = new InfoSystem(systemNo, systemName, adminLoginId, applicationType);

            this._unitOfWork.RegisterAdd(infoSystem);
            this._unitOfWork.UnitedCommit();
        }
        #endregion

        #region # 修改信息系统 —— void UpdateInfoSystem(Guid infoSystemId, string systemNo...
        /// <summary>
        /// 修改信息系统
        /// </summary>
        /// <param name="infoSystemId">信息系统Id</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="systemName">信息系统名称</param>
        public void UpdateInfoSystem(Guid infoSystemId, string systemNo, string systemName)
        {
            #region # 验证

            if (this._repMediator.InfoSystemRep.ExistsNo(infoSystemId, systemNo))
            {
                throw new ArgumentOutOfRangeException(nameof(systemNo), $"信息系统编号\"{systemNo}\"已存在！");
            }

            #endregion

            InfoSystem infoSystem = this._unitOfWork.Resolve<InfoSystem>(infoSystemId);
            infoSystem.UpdateInfo(systemNo, systemName);

            this._unitOfWork.RegisterSave(infoSystem);
            this._unitOfWork.Commit();
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
            InfoSystem infoSystem = this._unitOfWork.Resolve<InfoSystem>(systemNo);
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
            initParams = initParams?.ToArray() ?? Array.Empty<InfoSystemParam>();
            IDictionary<string, InfoSystemParam> paramDictionary = initParams.ToDictionary(x => x.SystemNo, x => x);

            ICollection<InfoSystem> infoSystems = this._unitOfWork.ResolveRange<InfoSystem>(paramDictionary.Keys);
            foreach (InfoSystem infoSystem in infoSystems)
            {
                InfoSystemParam param = paramDictionary[infoSystem.Number];
                infoSystem.Init(param.Host, param.Port, param.Index);
            }

            this._unitOfWork.RegisterSaveRange(infoSystems);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 创建权限 —— void CreateAuthority(string systemNo...
        /// <summary>
        /// 创建权限
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="authorityName">权限名称</param>
        /// <param name="authorityPath">权限路径</param>
        /// <param name="englishName">英文名称</param>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="namespace">命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="description">描述</param>
        public void CreateAuthority(string systemNo, ApplicationType applicationType, string authorityName, string authorityPath, string englishName, string assemblyName, string @namespace, string className, string methodName, string description)
        {
            //验证
            Assert.IsTrue(this._repMediator.InfoSystemRep.ExistsNo(systemNo), $"编号为\"{systemNo}\"的信息系统不存在！");
            Assert.IsFalse(this._repMediator.AuthorityRep.ExistsPath(systemNo, applicationType, authorityPath), "已存在该权限！");

            Authority authority = new Authority(systemNo, applicationType, authorityName, authorityPath, englishName, assemblyName, @namespace, className, methodName, description);

            this._unitOfWork.RegisterAdd(authority);
            this._unitOfWork.UnitedCommit();
        }
        #endregion

        #region # 批量创建权限 —— void CreateAuthorities(string systemNo...
        /// <summary>
        /// 批量创建权限
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="authorityParams">权限参数模型集</param>
        public void CreateAuthorities(string systemNo, ApplicationType applicationType, IEnumerable<AuthorityParam> authorityParams)
        {
            //验证
            Assert.IsTrue(this._repMediator.InfoSystemRep.ExistsNo(systemNo), $"编号为\"{systemNo}\"的信息系统不存在！");

            IList<Authority> authorities = new List<Authority>();
            foreach (AuthorityParam param in authorityParams)
            {
                Authority authority = new Authority(systemNo, applicationType, param.AuthorityName, param.AuthorityPath, param.EnglishName, param.AssemblyName, param.Namespace, param.ClassName, param.MethodName, param.Description);

                //验证
                Assert.IsFalse(this._repMediator.AuthorityRep.ExistsPath(systemNo, applicationType, authority.AuthorityPath), "已存在该权限！");

                authorities.Add(authority);
            }

            this._unitOfWork.RegisterAddRange(authorities);
            this._unitOfWork.UnitedCommit();
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

            if (authority.AuthorityPath != authorityPath)
            {
                Assert.IsFalse(this._repMediator.AuthorityRep.ExistsPath(authority.SystemNo, authority.ApplicationType, authorityPath), "已存在该权限！");
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

        #region # 创建菜单 —— Guid CreateMenu(string systemNo, string menuName...
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="menuName">菜单名称</param>
        /// <param name="sort">排序</param>
        /// <param name="url">链接地址</param>
        /// <param name="path">路径</param>
        /// <param name="icon">图标</param>
        /// <param name="parentNodeId">上级节点Id</param>
        /// <returns>菜单Id</returns>
        public Guid CreateMenu(string systemNo, ApplicationType applicationType, string menuName, int sort, string url, string path, string icon, Guid? parentNodeId)
        {
            //验证参数
            Assert.IsTrue(this._repMediator.InfoSystemRep.ExistsNo(systemNo), $"编号为\"{systemNo}\"的信息系统不存在！");
            Assert.IsFalse(this._repMediator.MenuRep.Exists(parentNodeId, applicationType, menuName), "给定菜单级别下，相同应用程序类型的菜单名称已存在！");

            Menu parentNode = parentNodeId.HasValue
                ? this._unitOfWork.Resolve<Menu>(parentNodeId.Value)
                : null;
            Menu menu = new Menu(systemNo, applicationType, menuName, sort, url, path, icon, parentNode);

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
                Guid? parentId = menu.ParentNode?.Id;
                Assert.IsFalse(this._repMediator.MenuRep.Exists(parentId, menu.ApplicationType, menuName), "给定菜单级别下，相同应用程序类型的菜单名称已存在！");
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

        #region # 创建角色 —— void CreateRole(string systemNo, string roleName...
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="description">描述</param>
        /// <param name="authorityIds">权限Id集</param>
        public void CreateRole(string systemNo, string roleName, string description, IEnumerable<Guid> authorityIds)
        {
            //验证
            authorityIds = authorityIds?.Distinct().ToArray() ?? Array.Empty<Guid>();
            Assert.IsTrue(this._repMediator.InfoSystemRep.ExistsNo(systemNo));

            //创建角色
            Role role = new Role(roleName, systemNo, description);

            //分配权限
            ICollection<Authority> authorities = this._unitOfWork.ResolveRange<Authority>(authorityIds);
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
            Role role = this._unitOfWork.Resolve<Role>(roleId);
            role.UpdateInfo(roleName, description);

            //分配权限
            ICollection<Authority> authorities = this._unitOfWork.ResolveRange<Authority>(authorityIds);
            role.RelateAuthorities(authorities);

            this._unitOfWork.RegisterSave(role);
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

            Role role = this._unitOfWork.Resolve<Role>(roleId);
            ICollection<Authority> authorities = this._unitOfWork.ResolveRange<Authority>(authorityIds);

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
            authorityIds = authorityIds?.Distinct().ToArray() ?? Array.Empty<Guid>();
            if (!authorityIds.Any()) return;

            Role role = this._unitOfWork.Resolve<Role>(roleId);
            ICollection<Authority> authorities = this._unitOfWork.ResolveRange<Authority>(authorityIds);

            role.AppendAuthorities(authorities);

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


        //查询部分

        #region # 获取信息系统 —— InfoSystemInfo GetInfoSystem(string systemNo)
        /// <summary>
        /// 获取信息系统
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>信息系统</returns>
        public InfoSystemInfo GetInfoSystem(string systemNo)
        {
            InfoSystem infoSystem = this._repMediator.InfoSystemRep.Single(systemNo);

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
            ICollection<InfoSystem> systems = this._repMediator.InfoSystemRep.FindAll();
            IEnumerable<InfoSystemInfo> systemInfos = systems.Select(x => x.ToDTO());

            return systemInfos;
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
            ICollection<InfoSystem> specSystems = this._repMediator.InfoSystemRep.FindByPage(keywords, pageIndex, pageSize, out int rowCount, out int pageCount);
            IEnumerable<InfoSystemInfo> specSystemInfos = specSystems.Select(x => x.ToDTO());

            return new PageModel<InfoSystemInfo>(specSystemInfos, pageIndex, pageSize, pageCount, rowCount);
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

            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> systemInfos = systems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            AuthorityInfo authorityInfo = authority.ToDTO(systemInfos);

            return authorityInfo;
        }
        #endregion

        #region # 获取权限列表 —— IEnumerable<AuthorityInfo> GetAuthorities(string keywords...
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="menuId">菜单Id</param>
        /// <param name="roleId">角色Id</param>
        /// <returns>权限列表</returns>
        public IEnumerable<AuthorityInfo> GetAuthorities(string keywords, string systemNo, ApplicationType? applicationType, Guid? menuId, Guid? roleId)
        {
            ICollection<Authority> authorities = this._repMediator.AuthorityRep.Find(keywords, systemNo, applicationType, menuId, roleId);

            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> systemInfos = systems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            return authorities.Select(x => x.ToDTO(systemInfos));
        }
        #endregion

        #region # 分页获取权限列表 —— PageModel<AuthorityInfo> GetAuthoritiesByPage(string keywords...
        /// <summary>
        /// 分页获取权限列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>权限列表</returns>
        public PageModel<AuthorityInfo> GetAuthoritiesByPage(string keywords, string systemNo, ApplicationType? applicationType, int pageIndex, int pageSize)
        {
            ICollection<Authority> authorities = this._repMediator.AuthorityRep.FindByPage(keywords, systemNo, applicationType, pageIndex, pageSize, out int rowCount, out int pageCount);

            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> systemInfos = systems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            IEnumerable<AuthorityInfo> authorityInfos = authorities.Select(x => x.ToDTO(systemInfos));

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

            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> systemInfos = systems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            return menu.ToDTO(systemInfos);
        }
        #endregion

        #region # 获取菜单列表 —— IEnumerable<MenuInfo> GetMenus(string systemNo...
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单列表</returns>
        public IEnumerable<MenuInfo> GetMenus(string systemNo, ApplicationType? applicationType)
        {
            ICollection<Menu> menus = this._repMediator.MenuRep.FindBySystem(systemNo, applicationType);

            IEnumerable<string> systemNos = menus.Select(x => x.SystemNo);
            IDictionary<string, InfoSystemInfo> systemInfos = this._repMediator.InfoSystemRep.Find(systemNos).ToDictionary(x => x.Key, x => x.Value.ToDTO());

            return menus.Select(x => x.ToDTO(systemInfos));
        }
        #endregion

        #region # 分页获取菜单列表 —— PageModel<MenuInfo> GetMenusByPage(string keywords...
        /// <summary>
        /// 分页获取菜单列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>菜单列表</returns>
        public PageModel<MenuInfo> GetMenusByPage(string keywords, string systemNo, ApplicationType? applicationType, int pageIndex, int pageSize)
        {
            ICollection<Menu> menus = this._repMediator.MenuRep.FindByPage(keywords, systemNo, applicationType, pageIndex, pageSize, out int rowCount, out int pageCount);

            IEnumerable<string> systemNos = menus.Select(x => x.SystemNo);
            IDictionary<string, InfoSystemInfo> systemInfos = this._repMediator.InfoSystemRep.Find(systemNos).ToDictionary(x => x.Key, x => x.Value.ToDTO());

            IEnumerable<MenuInfo> specMenuInfos = menus.Select(x => x.ToDTO(systemInfos));

            return new PageModel<MenuInfo>(specMenuInfos, pageIndex, pageSize, pageCount, rowCount);
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

            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> systemInfos = systems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            RoleInfo roleInfo = role.ToDTO(systemInfos);

            return roleInfo;
        }
        #endregion

        #region # 获取角色列表 —— IEnumerable<RoleInfo> GetRoles(string systemNo...
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="loginId">用户名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>角色列表</returns>
        public IEnumerable<RoleInfo> GetRoles(string keywords, string loginId, string systemNo)
        {
            IEnumerable<Role> roles = this._repMediator.RoleRep.Find(keywords, loginId, systemNo);

            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> systemInfos = systems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            return roles.Select(x => x.ToDTO(systemInfos));
        }
        #endregion

        #region # 分页获取角色列表 —— PageModel<RoleInfo> GetRolesByPage(string keywords...
        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>角色列表</returns>
        public PageModel<RoleInfo> GetRolesByPage(string keywords, string systemNo, int pageIndex, int pageSize)
        {
            ICollection<Role> roles = this._repMediator.RoleRep.FindByPage(keywords, systemNo, pageIndex, pageSize, out int rowCount, out int pageCount);

            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> systemInfos = systems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            IEnumerable<RoleInfo> roleInfos = roles.Select(x => x.ToDTO(systemInfos));

            return new PageModel<RoleInfo>(roleInfos, pageIndex, pageSize, pageCount, rowCount);
        }
        #endregion

        #region # 是否存在权限 —— bool ExistsAuthority(string systemNo, ApplicationType...
        /// <summary>
        /// 是否存在权限
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="authorityPath">权限路径</param>
        /// <returns>是否存在</returns>
        public bool ExistsAuthority(string systemNo, ApplicationType applicationType, string authorityPath)
        {
            return this._repMediator.AuthorityRep.ExistsPath(systemNo, applicationType, authorityPath);
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