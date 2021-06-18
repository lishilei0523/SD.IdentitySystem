using SD.IdentitySystem.IAppService.DTOs.Inputs;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.DTOBase;
using SD.Toolkits.WebApi.Attributes;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SD.IdentitySystem.AppService.Host.Controllers
{
    /// <summary>
    /// 权限WebApi接口
    /// </summary>
    public class AuthorizationController : ApiController
    {
        #region # 字段及依赖注入构造器

        /// <summary>
        /// 用户服务契约接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public AuthorizationController(IAuthorizationContract authorizationContract)
        {
            this._authorizationContract = authorizationContract;
        }

        #endregion


        //命令部分

        #region # 创建信息系统 —— void CreateInfoSystem(string systemNo, string systemName...
        /// <summary>
        /// 创建信息系统
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="systemName">信息系统名称</param>
        /// <param name="adminLoginId">系统管理员登录名</param>
        /// <param name="applicationType">应用程序类型</param>
        [HttpPost]
        [WrapPostParameters]
        public void CreateInfoSystem(string systemNo, string systemName, string adminLoginId, ApplicationType applicationType)
        {
            this._authorizationContract.CreateInfoSystem(systemNo, systemName, adminLoginId, applicationType);
        }
        #endregion

        #region # 修改信息系统 —— void UpdateInfoSystem(Guid infoSystemId, string systemNo...
        /// <summary>
        /// 修改信息系统
        /// </summary>
        /// <param name="infoSystemId">信息系统Id</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="systemName">信息系统名称</param>
        [HttpPost]
        [WrapPostParameters]
        public void UpdateInfoSystem(Guid infoSystemId, string systemNo, string systemName)
        {
            this._authorizationContract.UpdateInfoSystem(infoSystemId, systemNo, systemName);
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
        [HttpPost]
        [WrapPostParameters]
        public void InitInfoSystem(string systemNo, string host, int port, string index)
        {
            this._authorizationContract.InitInfoSystem(systemNo, host, port, index);
        }
        #endregion

        #region # 批量初始化信息系统 —— void InitInfoSystems(IEnumerable<InfoSystemParam> initParams)
        /// <summary>
        /// 批量初始化信息系统
        /// </summary>
        /// <param name="initParams">初始化信息系统参数模型集</param>
        [HttpPost]
        [WrapPostParameters]
        public void InitInfoSystems(IEnumerable<InfoSystemParam> initParams)
        {
            this._authorizationContract.InitInfoSystems(initParams);
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
        [HttpPost]
        [WrapPostParameters]
        public void CreateAuthority(string systemNo, ApplicationType applicationType, string authorityName, string authorityPath, string englishName, string assemblyName, string @namespace, string className, string methodName, string description)
        {
            this._authorizationContract.CreateAuthority(systemNo, applicationType, authorityName, authorityPath, englishName, assemblyName, @namespace, className, methodName, description);
        }
        #endregion

        #region # 批量创建权限 —— void CreateAuthorities(string systemNo...
        /// <summary>
        /// 批量创建权限
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="authorityParams">权限参数模型集</param>
        [HttpPost]
        [WrapPostParameters]
        public void CreateAuthorities(string systemNo, ApplicationType applicationType, IEnumerable<AuthorityParam> authorityParams)
        {
            this._authorizationContract.CreateAuthorities(systemNo, applicationType, authorityParams);
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
        [HttpPost]
        [WrapPostParameters]
        public void UpdateAuthority(Guid authorityId, string authorityName, string authorityPath, string englishName, string assemblyName, string @namespace, string className, string methodName, string description)
        {
            this._authorizationContract.UpdateAuthority(authorityId, authorityName, authorityPath, englishName, assemblyName, @namespace, className, methodName, description);
        }
        #endregion

        #region # 删除权限 —— void RemoveAuthority(Guid authorityId)
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="authorityId">权限Id</param>
        [HttpPost]
        [WrapPostParameters]
        public void RemoveAuthority(Guid authorityId)
        {
            this._authorizationContract.RemoveAuthority(authorityId);
        }
        #endregion

        #region # 创建菜单 —— Guid CreateMenu(string systemNo, ApplicationType applicationType...
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="menuName">菜单名称</param>
        /// <param name="sort">排序（倒序）</param>
        /// <param name="url">链接地址</param>
        /// <param name="path">路径</param>
        /// <param name="icon">图标</param>
        /// <param name="parentNodeId">上级节点Id</param>
        /// <returns>菜单Id</returns>
        [HttpPost]
        [WrapPostParameters]
        public Guid CreateMenu(string systemNo, ApplicationType applicationType, string menuName, int sort, string url, string path, string icon, Guid? parentNodeId)
        {
            return this._authorizationContract.CreateMenu(systemNo, applicationType, menuName, sort, url, path, icon, parentNodeId);
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
        /// <param name="path">路径</param>
        /// <param name="icon">图标</param>
        [HttpPost]
        [WrapPostParameters]
        public void UpdateMenu(Guid menuId, string menuName, int sort, string url, string path, string icon)
        {
            this._authorizationContract.UpdateMenu(menuId, menuName, sort, url, path, icon);
        }
        #endregion

        #region # 删除菜单 —— void RemoveMenu(Guid menuId)
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        [HttpPost]
        [WrapPostParameters]
        public void RemoveMenu(Guid menuId)
        {
            this._authorizationContract.RemoveMenu(menuId);
        }
        #endregion

        #region # 关联权限到菜单 —— void RelateAuthoritiesToMenu(Guid menuId...
        /// <summary>
        /// 关联权限到菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <param name="authorityIds">权限Id集</param>
        [HttpPost]
        [WrapPostParameters]
        public void RelateAuthoritiesToMenu(Guid menuId, IEnumerable<Guid> authorityIds)
        {
            this._authorizationContract.RelateAuthoritiesToMenu(menuId, authorityIds);
        }
        #endregion

        #region # 创建角色 —— void CreateRole(string systemNo, string roleName...
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="description">角色描述</param>
        /// <param name="authorityIds">权限Id集</param>
        [HttpPost]
        [WrapPostParameters]
        public void CreateRole(string systemNo, string roleName, string description, IEnumerable<Guid> authorityIds)
        {
            this._authorizationContract.CreateRole(systemNo, roleName, description, authorityIds);
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
        [HttpPost]
        [WrapPostParameters]
        public void UpdateRole(Guid roleId, string roleName, string description, IEnumerable<Guid> authorityIds)
        {
            this._authorizationContract.UpdateRole(roleId, roleName, description, authorityIds);
        }
        #endregion

        #region # 关联权限到角色 —— void RelateAuthoritiesToRole(Guid roleId, IEnumerable<Guid> authorityIds)
        /// <summary>
        /// 关联权限到角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="authorityIds">权限Id集</param>
        [HttpPost]
        [WrapPostParameters]
        public void RelateAuthoritiesToRole(Guid roleId, IEnumerable<Guid> authorityIds)
        {
            this._authorizationContract.RelateAuthoritiesToRole(roleId, authorityIds);
        }
        #endregion

        #region # 追加权限到角色 —— void AppendAuthoritiesToRole(Guid roleId, IEnumerable<Guid> authorityIds)
        /// <summary>
        /// 追加权限到角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="authorityIds">权限Id集</param>
        [HttpPost]
        [WrapPostParameters]
        public void AppendAuthoritiesToRole(Guid roleId, IEnumerable<Guid> authorityIds)
        {
            this._authorizationContract.AppendAuthoritiesToRole(roleId, authorityIds);
        }
        #endregion

        #region # 删除角色 —— void RemoveRole(Guid roleId)
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        [HttpPost]
        [WrapPostParameters]
        public void RemoveRole(Guid roleId)
        {
            this._authorizationContract.RemoveRole(roleId);
        }
        #endregion


        //查询部分

        #region # 获取信息系统 —— InfoSystemInfo GetInfoSystem(string systemNo)
        /// <summary>
        /// 获取信息系统
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>信息系统</returns>
        [HttpGet]
        public InfoSystemInfo GetInfoSystem(string systemNo)
        {
            return this._authorizationContract.GetInfoSystem(systemNo);
        }
        #endregion

        #region # 获取信息系统列表 —— IEnumerable<InfoSystemInfo> GetInfoSystems()
        /// <summary>
        /// 获取信息系统列表
        /// </summary>
        /// <returns>信息系统列表</returns>
        [HttpGet]
        public IEnumerable<InfoSystemInfo> GetInfoSystems()
        {
            return this._authorizationContract.GetInfoSystems();
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
        [HttpGet]
        public PageModel<InfoSystemInfo> GetInfoSystemsByPage(string keywords, int pageIndex, int pageSize)
        {
            return this._authorizationContract.GetInfoSystemsByPage(keywords, pageIndex, pageSize);
        }
        #endregion

        #region # 获取权限 —— AuthorityInfo GetAuthority(Guid authorityId)
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="authorityId">权限Id</param>
        /// <returns>权限视图模型</returns>
        [HttpGet]
        public AuthorityInfo GetAuthority(Guid authorityId)
        {
            return this._authorizationContract.GetAuthority(authorityId);
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
        [HttpGet]
        public IEnumerable<AuthorityInfo> GetAuthorities(string keywords, string systemNo, ApplicationType? applicationType, Guid? menuId, Guid? roleId)
        {
            return this._authorizationContract.GetAuthorities(keywords, systemNo, applicationType, menuId, roleId);
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
        [HttpGet]
        public PageModel<AuthorityInfo> GetAuthoritiesByPage(string keywords, string systemNo, ApplicationType? applicationType, int pageIndex, int pageSize)
        {
            return this._authorizationContract.GetAuthoritiesByPage(keywords, systemNo, applicationType, pageIndex, pageSize);
        }
        #endregion

        #region # 获取菜单 —— MenuInfo GetMenu(Guid menuId)
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns>菜单</returns>
        [HttpGet]
        public MenuInfo GetMenu(Guid menuId)
        {
            return this._authorizationContract.GetMenu(menuId);
        }
        #endregion

        #region # 获取菜单列表 —— IEnumerable<MenuInfo> GetMenus(string systemNo...
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单列表</returns>
        [HttpGet]
        public IEnumerable<MenuInfo> GetMenus(string systemNo, ApplicationType? applicationType)
        {
            return this._authorizationContract.GetMenus(systemNo, applicationType);
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
        [HttpGet]
        public PageModel<MenuInfo> GetMenusByPage(string keywords, string systemNo, ApplicationType? applicationType, int pageIndex, int pageSize)
        {
            return this._authorizationContract.GetMenusByPage(keywords, systemNo, applicationType, pageIndex, pageSize);
        }
        #endregion

        #region # 获取角色 —— RoleInfo GetRole(Guid roleId)
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>角色</returns>
        [HttpGet]
        public RoleInfo GetRole(Guid roleId)
        {
            return this._authorizationContract.GetRole(roleId);
        }
        #endregion

        #region # 获取角色列表 —— IEnumerable<RoleInfo> GetRoles(string keywords...
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>角色列表</returns>
        [HttpGet]
        public IEnumerable<RoleInfo> GetRoles(string keywords, string loginId, string systemNo)
        {
            return this._authorizationContract.GetRoles(keywords, loginId, systemNo);
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
        [HttpGet]
        public PageModel<RoleInfo> GetRolesByPage(string keywords, string systemNo, int pageIndex, int pageSize)
        {
            return this._authorizationContract.GetRolesByPage(keywords, systemNo, pageIndex, pageSize);
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
        [HttpGet]
        public bool ExistsAuthority(string systemNo, ApplicationType applicationType, string authorityPath)
        {
            return this._authorizationContract.ExistsAuthority(systemNo, applicationType, authorityPath);
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
        [HttpGet]
        public bool ExistsRole(string systemNo, Guid? roleId, string roleName)
        {
            return this._authorizationContract.ExistsRole(systemNo, roleId, roleName);
        }
        #endregion
    }
}
