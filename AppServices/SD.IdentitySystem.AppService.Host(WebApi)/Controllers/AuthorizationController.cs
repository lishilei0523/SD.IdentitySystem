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
    /// 权限管理WebApi接口
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

        #region # 创建信息系统 —— void CreateInfoSystem(string infoSystemNo, string infoSystemName...
        /// <summary>
        /// 创建信息系统
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="infoSystemName">信息系统名称</param>
        /// <param name="adminLoginId">系统管理员用户名</param>
        /// <param name="applicationType">应用程序类型</param>
        [HttpPost]
        [WrapPostParameters]
        public void CreateInfoSystem(string infoSystemNo, string infoSystemName, string adminLoginId, ApplicationType applicationType)
        {
            this._authorizationContract.CreateInfoSystem(infoSystemNo, infoSystemName, adminLoginId, applicationType);
        }
        #endregion

        #region # 修改信息系统 —— void UpdateInfoSystem(string infoSystemNo, string infoSystemName)
        /// <summary>
        /// 修改信息系统
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="infoSystemName">信息系统名称</param>
        [HttpPost]
        [WrapPostParameters]
        public void UpdateInfoSystem(string infoSystemNo, string infoSystemName)
        {
            this._authorizationContract.UpdateInfoSystem(infoSystemNo, infoSystemName);
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
        [HttpPost]
        [WrapPostParameters]
        public void InitInfoSystem(string infoSystemNo, string host, int port, string index)
        {
            this._authorizationContract.InitInfoSystem(infoSystemNo, host, port, index);
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

        #region # 创建权限 —— void CreateAuthority(string infoSystemNo...
        /// <summary>
        /// 创建权限
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="authorityName">权限名称</param>
        /// <param name="authorityPath">权限路径</param>
        /// <param name="description">描述</param>
        [HttpPost]
        [WrapPostParameters]
        public void CreateAuthority(string infoSystemNo, ApplicationType applicationType, string authorityName, string authorityPath, string description)
        {
            this._authorizationContract.CreateAuthority(infoSystemNo, applicationType, authorityName, authorityPath, description);
        }
        #endregion

        #region # 批量创建权限 —— void CreateAuthorities(string infoSystemNo...
        /// <summary>
        /// 批量创建权限
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="authorityParams">权限参数模型集</param>
        [HttpPost]
        [WrapPostParameters]
        public void CreateAuthorities(string infoSystemNo, ApplicationType applicationType, IEnumerable<AuthorityParam> authorityParams)
        {
            this._authorizationContract.CreateAuthorities(infoSystemNo, applicationType, authorityParams);
        }
        #endregion

        #region # 修改权限 —— void UpdateAuthority(Guid authorityId...
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="authorityId">权限Id</param>
        /// <param name="authorityName">权限名称</param>
        /// <param name="authorityPath">权限路径</param>
        /// <param name="description">描述</param>
        [HttpPost]
        [WrapPostParameters]
        public void UpdateAuthority(Guid authorityId, string authorityName, string authorityPath, string description)
        {
            this._authorizationContract.UpdateAuthority(authorityId, authorityName, authorityPath, description);
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

        #region # 创建菜单 —— Guid CreateMenu(string infoSystemNo, ApplicationType applicationType...
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
        [HttpPost]
        [WrapPostParameters]
        public Guid CreateMenu(string infoSystemNo, ApplicationType applicationType, string menuName, int sort, string url, string path, string icon, Guid? parentNodeId)
        {
            return this._authorizationContract.CreateMenu(infoSystemNo, applicationType, menuName, sort, url, path, icon, parentNodeId);
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

        #region # 创建角色 —— void CreateRole(string infoSystemNo, string roleName...
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="description">描述</param>
        /// <param name="authorityIds">权限Id集</param>
        [HttpPost]
        [WrapPostParameters]
        public void CreateRole(string infoSystemNo, string roleName, string description, IEnumerable<Guid> authorityIds)
        {
            this._authorizationContract.CreateRole(infoSystemNo, roleName, description, authorityIds);
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
        [HttpPost]
        [WrapPostParameters]
        public void UpdateRole(Guid roleId, string roleName, string description, IEnumerable<Guid> authorityIds)
        {
            this._authorizationContract.UpdateRole(roleId, roleName, description, authorityIds);
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


        //查询部分

        #region # 获取信息系统 —— InfoSystemInfo GetInfoSystem(string infoSystemNo)
        /// <summary>
        /// 获取信息系统
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <returns>信息系统</returns>
        [HttpGet]
        public InfoSystemInfo GetInfoSystem(string infoSystemNo)
        {
            return this._authorizationContract.GetInfoSystem(infoSystemNo);
        }
        #endregion

        #region # 获取信息系统字典 —— IDictionary<string, InfoSystemInfo> GetInfoSystemsByNo(...
        /// <summary>
        /// 获取信息系统字典
        /// </summary>
        /// <param name="infoSystemNos">信息系统编号集</param>
        /// <returns>信息系统字典</returns>
        [HttpGet]
        public IDictionary<string, InfoSystemInfo> GetInfoSystemsByNo(IEnumerable<string> infoSystemNos)
        {
            return this._authorizationContract.GetInfoSystemsByNo(infoSystemNos);
        }
        #endregion

        #region # 获取信息系统列表 —— IEnumerable<InfoSystemInfo> GetInfoSystems(string keywords)
        /// <summary>
        /// 获取信息系统列表
        /// </summary>
        /// <returns>信息系统列表</returns>
        [HttpGet]
        public IEnumerable<InfoSystemInfo> GetInfoSystems(string keywords)
        {
            return this._authorizationContract.GetInfoSystems(keywords);
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

        #region # 获取权限字典 —— IDictionary<Guid, AuthorityInfo> GetAuthoritiesById(...
        /// <summary>
        /// 获取权限字典
        /// </summary>
        /// <param name="authorityIds">权限Id集</param>
        /// <returns>权限字典</returns>
        [HttpGet]
        public IDictionary<Guid, AuthorityInfo> GetAuthoritiesById(IEnumerable<Guid> authorityIds)
        {
            return this._authorizationContract.GetAuthoritiesById(authorityIds);
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
        [HttpGet]
        public IEnumerable<AuthorityInfo> GetAuthorities(string keywords, string infoSystemNo, ApplicationType? applicationType, Guid? menuId, Guid? roleId)
        {
            return this._authorizationContract.GetAuthorities(keywords, infoSystemNo, applicationType, menuId, roleId);
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
        [HttpGet]
        public PageModel<AuthorityInfo> GetAuthoritiesByPage(string keywords, string infoSystemNo, ApplicationType? applicationType, int pageIndex, int pageSize)
        {
            return this._authorizationContract.GetAuthoritiesByPage(keywords, infoSystemNo, applicationType, pageIndex, pageSize);
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

        #region # 获取菜单字典 —— IDictionary<Guid, MenuInfo> GetMenusById(...
        /// <summary>
        /// 获取菜单字典
        /// </summary>
        /// <param name="menuIds">菜单Id集</param>
        /// <returns>菜单字典</returns>
        [HttpGet]
        public IDictionary<Guid, MenuInfo> GetMenusById(IEnumerable<Guid> menuIds)
        {
            return this._authorizationContract.GetMenusById(menuIds);
        }
        #endregion

        #region # 获取菜单列表 —— IEnumerable<MenuInfo> GetMenus(string keywords...
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单列表</returns>
        [HttpGet]
        public IEnumerable<MenuInfo> GetMenus(string keywords, string infoSystemNo, ApplicationType? applicationType)
        {
            return this._authorizationContract.GetMenus(keywords, infoSystemNo, applicationType);
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
        [HttpGet]
        public PageModel<MenuInfo> GetMenusByPage(string keywords, string infoSystemNo, ApplicationType? applicationType, int pageIndex, int pageSize)
        {
            return this._authorizationContract.GetMenusByPage(keywords, infoSystemNo, applicationType, pageIndex, pageSize);
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

        #region # 获取角色字典 —— IDictionary<Guid, RoleInfo> GetRolesById(...
        /// <summary>
        /// 获取角色字典
        /// </summary>
        /// <param name="roleIds">角色Id集</param>
        /// <returns>角色字典</returns>
        [HttpGet]
        public IDictionary<Guid, RoleInfo> GetRolesById(IEnumerable<Guid> roleIds)
        {
            return this._authorizationContract.GetRolesById(roleIds);
        }
        #endregion

        #region # 获取角色列表 —— IEnumerable<RoleInfo> GetRoles(string keywords...
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="loginId">用户名</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <returns>角色列表</returns>
        [HttpGet]
        public IEnumerable<RoleInfo> GetRoles(string keywords, string loginId, string infoSystemNo)
        {
            return this._authorizationContract.GetRoles(keywords, loginId, infoSystemNo);
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
        [HttpGet]
        public PageModel<RoleInfo> GetRolesByPage(string keywords, string infoSystemNo, int pageIndex, int pageSize)
        {
            return this._authorizationContract.GetRolesByPage(keywords, infoSystemNo, pageIndex, pageSize);
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
        [HttpGet]
        public bool ExistsAuthority(string infoSystemNo, ApplicationType applicationType, string authorityPath)
        {
            return this._authorizationContract.ExistsAuthority(infoSystemNo, applicationType, authorityPath);
        }
        #endregion

        #region # 是否存在菜单 —— bool ExistsMenu(Guid? parentNodeId, ApplicationType applicationType...
        /// <summary>
        /// 是否存在菜单
        /// </summary>
        /// <param name="parentNodeId">上级节点Id</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="menuName">菜单名称</param>
        /// <returns>是否存在</returns>
        [HttpGet]
        public bool ExistsMenu(Guid? parentNodeId, ApplicationType applicationType, string menuName)
        {
            return this._authorizationContract.ExistsMenu(parentNodeId, applicationType, menuName);
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
        [HttpGet]
        public bool ExistsRole(string infoSystemNo, Guid? roleId, string roleName)
        {
            return this._authorizationContract.ExistsRole(infoSystemNo, roleId, roleName);
        }
        #endregion
    }
}
