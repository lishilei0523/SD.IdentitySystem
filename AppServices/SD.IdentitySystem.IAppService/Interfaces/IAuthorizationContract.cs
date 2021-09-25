using SD.IdentitySystem.IAppService.DTOs.Inputs;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.Infrastructure.AppServiceBase;
using SD.Infrastructure.Constants;
using SD.Infrastructure.DTOBase;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace SD.IdentitySystem.IAppService.Interfaces
{
    /// <summary>
    /// 权限服务契约接口
    /// </summary>
    [ServiceContract(Namespace = "http://SD.IdentitySystem.IAppService.Interfaces")]
    public interface IAuthorizationContract : IApplicationService
    {
        //命令部分

        #region # 创建信息系统 —— void CreateInfoSystem(string systemNo, string systemName...
        /// <summary>
        /// 创建信息系统
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="systemName">信息系统名称</param>
        /// <param name="adminLoginId">系统管理员账号</param>
        /// <param name="applicationType">应用程序类型</param>
        [OperationContract]
        void CreateInfoSystem(string systemNo, string systemName, string adminLoginId, ApplicationType applicationType);
        #endregion

        #region # 修改信息系统 —— void UpdateInfoSystem(Guid infoSystemId, string systemNo...
        /// <summary>
        /// 修改信息系统
        /// </summary>
        /// <param name="infoSystemId">信息系统Id</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="systemName">信息系统名称</param>
        [OperationContract]
        void UpdateInfoSystem(Guid infoSystemId, string systemNo, string systemName);
        #endregion

        #region # 初始化信息系统 —— void InitInfoSystem(string systemNo, string host...
        /// <summary>
        /// 初始化信息系统
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="host">主机名称</param>
        /// <param name="port">端口</param>
        /// <param name="index">首页</param>
        [OperationContract]
        void InitInfoSystem(string systemNo, string host, int port, string index);
        #endregion

        #region # 批量初始化信息系统 —— void InitInfoSystems(IEnumerable<InfoSystemParam> initParams)
        /// <summary>
        /// 批量初始化信息系统
        /// </summary>
        /// <param name="initParams">初始化信息系统参数模型集</param>
        [OperationContract]
        void InitInfoSystems(IEnumerable<InfoSystemParam> initParams);
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
        [OperationContract]
        void CreateAuthority(string systemNo, ApplicationType applicationType, string authorityName, string authorityPath, string englishName, string assemblyName, string @namespace, string className, string methodName, string description);
        #endregion

        #region # 批量创建权限 —— void CreateAuthorities(string systemNo...
        /// <summary>
        /// 批量创建权限
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="authorityParams">权限参数模型集</param>
        [OperationContract]
        void CreateAuthorities(string systemNo, ApplicationType applicationType, IEnumerable<AuthorityParam> authorityParams);
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
        [OperationContract]
        void UpdateAuthority(Guid authorityId, string authorityName, string authorityPath, string englishName, string assemblyName, string @namespace, string className, string methodName, string description);
        #endregion

        #region # 删除权限 —— void RemoveAuthority(Guid authorityId)
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="authorityId">权限Id</param>
        [OperationContract]
        void RemoveAuthority(Guid authorityId);
        #endregion

        #region # 创建菜单 —— Guid CreateMenu(string systemNo, ApplicationType applicationType...
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
        [OperationContract]
        Guid CreateMenu(string systemNo, ApplicationType applicationType, string menuName, int sort, string url, string path, string icon, Guid? parentNodeId);
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
        [OperationContract]
        void UpdateMenu(Guid menuId, string menuName, int sort, string url, string path, string icon);
        #endregion

        #region # 删除菜单 —— void RemoveMenu(Guid menuId)
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        [OperationContract]
        void RemoveMenu(Guid menuId);
        #endregion

        #region # 关联权限到菜单 —— void RelateAuthoritiesToMenu(Guid menuId...
        /// <summary>
        /// 关联权限到菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <param name="authorityIds">权限Id集</param>
        [OperationContract]
        void RelateAuthoritiesToMenu(Guid menuId, IEnumerable<Guid> authorityIds);
        #endregion

        #region # 创建角色 —— void CreateRole(string systemNo, string roleName...
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="description">描述</param>
        /// <param name="authorityIds">权限Id集</param>
        [OperationContract]
        void CreateRole(string systemNo, string roleName, string description, IEnumerable<Guid> authorityIds);
        #endregion

        #region # 修改角色 —— void UpdateRole(Guid roleId, string roleName...
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="description">描述</param>
        /// <param name="authorityIds">权限Id集</param>
        [OperationContract]
        void UpdateRole(Guid roleId, string roleName, string description, IEnumerable<Guid> authorityIds);
        #endregion

        #region # 关联权限到角色 —— void RelateAuthoritiesToRole(Guid roleId, IEnumerable<Guid> authorityIds)
        /// <summary>
        /// 关联权限到角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="authorityIds">权限Id集</param>
        [OperationContract]
        void RelateAuthoritiesToRole(Guid roleId, IEnumerable<Guid> authorityIds);
        #endregion

        #region # 追加权限到角色 —— void AppendAuthoritiesToRole(Guid roleId, IEnumerable<Guid> authorityIds)
        /// <summary>
        /// 追加权限到角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="authorityIds">权限Id集</param>
        [OperationContract]
        void AppendAuthoritiesToRole(Guid roleId, IEnumerable<Guid> authorityIds);
        #endregion

        #region # 删除角色 —— void RemoveRole(Guid roleId)
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        [OperationContract]
        void RemoveRole(Guid roleId);
        #endregion


        //查询部分

        #region # 获取信息系统 —— InfoSystemInfo GetInfoSystem(string systemNo)
        /// <summary>
        /// 获取信息系统
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>信息系统</returns>
        [OperationContract]
        InfoSystemInfo GetInfoSystem(string systemNo);
        #endregion

        #region # 获取信息系统列表 —— IEnumerable<InfoSystemInfo> GetInfoSystems()
        /// <summary>
        /// 获取信息系统列表
        /// </summary>
        /// <returns>信息系统列表</returns>
        [OperationContract]
        IEnumerable<InfoSystemInfo> GetInfoSystems();
        #endregion

        #region # 分页获取信息系统列表 —— PageModel<InfoSystemInfo> GetInfoSystemsByPage(string keywords...
        /// <summary>
        /// 分页获取信息系统列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>信息系统列表</returns>
        [OperationContract]
        PageModel<InfoSystemInfo> GetInfoSystemsByPage(string keywords, int pageIndex, int pageSize);
        #endregion

        #region # 获取权限 —— AuthorityInfo GetAuthority(Guid authorityId)
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="authorityId">权限Id</param>
        /// <returns>权限视图模型</returns>
        [OperationContract]
        AuthorityInfo GetAuthority(Guid authorityId);
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
        [OperationContract]
        IEnumerable<AuthorityInfo> GetAuthorities(string keywords, string systemNo, ApplicationType? applicationType, Guid? menuId, Guid? roleId);
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
        [OperationContract]
        PageModel<AuthorityInfo> GetAuthoritiesByPage(string keywords, string systemNo, ApplicationType? applicationType, int pageIndex, int pageSize);
        #endregion

        #region # 获取菜单 —— MenuInfo GetMenu(Guid menuId)
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns>菜单</returns>
        [OperationContract]
        MenuInfo GetMenu(Guid menuId);
        #endregion

        #region # 获取菜单列表 —— IEnumerable<MenuInfo> GetMenus(string systemNo...
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单列表</returns>
        [OperationContract]
        IEnumerable<MenuInfo> GetMenus(string systemNo, ApplicationType? applicationType);
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
        [OperationContract]
        PageModel<MenuInfo> GetMenusByPage(string keywords, string systemNo, ApplicationType? applicationType, int pageIndex, int pageSize);
        #endregion

        #region # 获取角色 —— RoleInfo GetRole(Guid roleId)
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>角色</returns>
        [OperationContract]
        RoleInfo GetRole(Guid roleId);
        #endregion

        #region # 获取角色列表 —— IEnumerable<RoleInfo> GetRoles(string keywords...
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="loginId">用户名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>角色列表</returns>
        [OperationContract]
        IEnumerable<RoleInfo> GetRoles(string keywords, string loginId, string systemNo);
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
        [OperationContract]
        PageModel<RoleInfo> GetRolesByPage(string keywords, string systemNo, int pageIndex, int pageSize);
        #endregion

        #region # 是否存在权限 —— bool ExistsAuthority(string systemNo, ApplicationType...
        /// <summary>
        /// 是否存在权限
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="authorityPath">权限路径</param>
        /// <returns>是否存在</returns>
        [OperationContract]
        bool ExistsAuthority(string systemNo, ApplicationType applicationType, string authorityPath);
        #endregion

        #region # 是否存在角色 —— bool ExistsRole(string systemNo, Guid? roleId, string roleName)
        /// <summary>
        /// 是否存在角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="roleName">角色名称</param>
        /// <returns>是否存在</returns>
        [OperationContract]
        bool ExistsRole(string systemNo, Guid? roleId, string roleName);
        #endregion
    }
}
