﻿using System;
using System.Collections.Generic;
using System.ServiceModel;
using SD.IdentitySystem.IAppService.DTOs.Inputs;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using ShSoft.Infrastructure;
using ShSoft.Infrastructure.DTOBase;

namespace SD.IdentitySystem.IAppService.Interfaces
{
    /// <summary>
    /// 权限服务契约接口
    /// </summary>
    [ServiceContract(Namespace = "http://SD.IdentitySystem.IAppService.Interfaces")]
    public interface IAuthorizationContract : IApplicationService
    {
        ////////////////////////////////命令部分////////////////////////////////

        #region # 初始化信息系统类别 —— void InitInfoSystemKinds(IEnumerable<InfoSystemKindParam> initParams)
        /// <summary>
        /// 初始化信息系统类别
        /// </summary>
        /// <param name="initParams">初始化信息系统类别参数模型集</param>
        [OperationContract]
        void InitInfoSystemKinds(IEnumerable<InfoSystemKindParam> initParams);
        #endregion


        #region # 批量创建权限 —— IEnumerable<Guid> CreateAuthorities(string systemKindNo...
        /// <summary>
        /// 批量创建权限
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="authorityParams">权限参数模型集</param>
        /// <returns>权限Id集</returns>
        [OperationContract]
        IEnumerable<Guid> CreateAuthorities(string systemKindNo, IEnumerable<AuthorityParam> authorityParams);
        #endregion

        #region # 修改权限 —— void UpdateAuthority(Guid authorityId...
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="authorityId">权限Id</param>
        /// <param name="authorityParam">权限参数模型</param>
        [OperationContract]
        void UpdateAuthority(Guid authorityId, AuthorityParam authorityParam);
        #endregion

        #region # 为权限设置菜单 —— void AppendMenu(Guid menuId...
        /// <summary>
        /// 为权限设置菜单
        /// </summary>
        /// <param name="menuId">菜单Id（叶子节点）</param>
        /// <param name="authorityIds">权限Id集</param>
        [OperationContract]
        void AppendMenu(Guid menuId, IEnumerable<Guid> authorityIds);
        #endregion

        #region # 删除权限 —— void RemoveAuthority(Guid authorityId)
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="authorityId">权限Id</param>
        [OperationContract]
        void RemoveAuthority(Guid authorityId);
        #endregion


        #region # 创建菜单 —— Guid CreateMenu(string systemKindNo, string menuName...
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="menuName">菜单名称</param>
        /// <param name="sort">排序（倒序）</param>
        /// <param name="url">链接地址</param>
        /// <param name="icon">图标</param>
        /// <param name="parentId">上级菜单Id</param>
        /// <returns>菜单Id</returns>
        [OperationContract]
        Guid CreateMenu(string systemKindNo, string menuName, int sort, string url, string icon, Guid? parentId);
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
        [OperationContract]
        void UpdateMenu(Guid menuId, string menuName, int sort, string url, string icon);
        #endregion

        #region # 删除菜单 —— void RemoveMenu(Guid menuId)
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        [OperationContract]
        void RemoveMenu(Guid menuId);
        #endregion


        #region # 创建角色 —— Guid CreateRole(string systemKindNo, string roleName...
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="authorityIds">权限Id集</param>
        /// <returns>角色Id</returns>
        [OperationContract]
        Guid CreateRole(string systemKindNo, string roleName, IEnumerable<Guid> authorityIds);
        #endregion

        #region # 为角色分配权限 —— void SetAuthorities(Guid roleId, IEnumerable<Guid> authorityIds)
        /// <summary>
        /// 为角色分配权限
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="authorityIds">权限Id集</param>
        [OperationContract]
        void SetAuthorities(Guid roleId, IEnumerable<Guid> authorityIds);
        #endregion

        #region # 为角色追加权限 —— void AppendAuthorities(Guid roleId, IEnumerable<Guid> authorityIds)
        /// <summary>
        /// 为角色追加权限
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="authorityIds">权限Id集</param>
        [OperationContract]
        void AppendAuthorities(Guid roleId, IEnumerable<Guid> authorityIds);
        #endregion

        #region # 修改角色 —— void UpdateRole(Guid roleId, string roleName...
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="authorityIds">权限Id集</param>
        [OperationContract]
        void UpdateRole(Guid roleId, string roleName, IEnumerable<Guid> authorityIds);
        #endregion

        #region # 删除角色 —— void RemoveRole(Guid roleId)
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        [OperationContract]
        void RemoveRole(Guid roleId);
        #endregion


        ////////////////////////////////查询部分////////////////////////////////

        #region # 获取信息系统类别列表 —— IEnumerable<InfoSystemKindInfo> GetSystemKinds()
        /// <summary>
        /// 获取信息系统类别列表
        /// </summary>
        /// <returns>信息系统类别列表</returns>
        [OperationContract]
        IEnumerable<InfoSystemKindInfo> GetSystemKinds();
        #endregion


        #region # 分页获取权限列表 —— PageModel<AuthorityInfo> GetAuthoritiesByPage(string systemKindNo...
        /// <summary>
        /// 分页获取权限列表
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>权限列表</returns>
        [OperationContract]
        PageModel<AuthorityInfo> GetAuthoritiesByPage(string systemKindNo, string keywords, int pageIndex, int pageSize);
        #endregion

        #region # 获取权限列表 —— IEnumerable<AuthorityInfo> GetAuthorities(string systemKindNo)
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>权限列表</returns>
        [OperationContract]
        IEnumerable<AuthorityInfo> GetAuthorities(string systemKindNo);
        #endregion

        #region # 根据菜单获取权限列表 —— IEnumerable<AuthorityInfo> GetAuthoritiesByMenu(...
        /// <summary>
        /// 根据菜单获取权限列表
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns>权限列表</returns>
        [OperationContract]
        IEnumerable<AuthorityInfo> GetAuthoritiesByMenu(Guid menuId);
        #endregion

        #region # 根据角色获取权限列表 —— IEnumerable<AuthorityInfo> GetAuthoritiesByRole(...
        /// <summary>
        /// 根据角色获取权限列表
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>权限列表</returns>
        [OperationContract]
        IEnumerable<AuthorityInfo> GetAuthoritiesByRole(Guid roleId);
        #endregion

        #region # 根据角色获取权限Id列表 —— IEnumerable<Guid> GetAuthorityIdsByRole(...
        /// <summary>
        /// 根据角色获取权限Id列表
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>权限Id列表</returns>
        [OperationContract]
        IEnumerable<Guid> GetAuthorityIdsByRole(Guid roleId);
        #endregion

        #region # 获取权限Id集 —— IEnumerable<Guid> GetAuthorityIds(string systemKindNo)
        /// <summary>
        /// 获取权限Id集
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>权限Id集</returns>
        [OperationContract]
        IEnumerable<Guid> GetAuthorityIds(string systemKindNo);
        #endregion

        #region # 判断权限是否存在 —— bool ExistsAuthority(string authorityPath)
        /// <summary>
        /// 判断权限是否存在
        /// </summary>
        /// <param name="authorityPath">权限路径</param>
        /// <returns>是否存在</returns>
        [OperationContract]
        bool ExistsAuthority(string authorityPath);
        #endregion


        #region # 获取菜单列表 —— IEnumerable<MenuInfo> GetMenus(string systemKindNo)
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>菜单列表</returns>
        [OperationContract]
        IEnumerable<MenuInfo> GetMenus(string systemKindNo);
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

        #region # 获取角色列表 —— IEnumerable<RoleInfo> GetRoles(string systemKindNo)
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>角色列表</returns>
        [OperationContract]
        IEnumerable<RoleInfo> GetRoles(string systemKindNo);
        #endregion

        #region # 分页获取角色列表 —— PageModel<RoleInfo> GetRolesByPage(string systemKindNo...
        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>角色列表</returns>
        [OperationContract]
        PageModel<RoleInfo> GetRolesByPage(string systemKindNo, string keywords, int pageIndex, int pageSize);
        #endregion

        #region # 是否存在角色 —— bool ExistsRole(string systemKindNo, Guid? roleId, string roleName)
        /// <summary>
        /// 是否存在角色
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="roleName">角色名称</param>
        /// <returns>是否存在</returns>
        [OperationContract]
        bool ExistsRole(string systemKindNo, Guid? roleId, string roleName);
        #endregion
    }
}
