using System;
using System.Collections.Generic;
using System.ServiceModel;
using SD.UAC.IAppService.DTOs.Outputs;
using ShSoft.Framework2016.Infrastructure;
using ShSoft.Framework2016.Infrastructure.IDTO;

namespace SD.UAC.IAppService.Interfaces
{
    /// <summary>
    /// 用户服务契约接口
    /// </summary>
    [ServiceContract(Namespace = "http://ShSoft.UAC.IAppService.Interfaces")]
    public interface IUserContract : IApplicationService
    {
        ////////////////////////////////命令部分////////////////////////////////

        #region # 创建信息系统 —— void CreateInfoSystem(string systemNo, string systemName...
        /// <summary>
        /// 创建信息系统
        /// </summary>
        /// <param name="systemNo">组织编号</param>
        /// <param name="systemName">信息系统名称</param>
        /// <param name="systemKindNo">信息系统类别编号</param>
        [OperationContract]
        void CreateInfoSystem(string systemNo, string systemName, string systemKindNo);
        #endregion

        #region # 设置系统管理员 —— void SetSystemAdmin(string systemNo, string loginId)
        /// <summary>
        /// 设置系统管理员
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="loginId">登录名</param>
        [OperationContract]
        void SetSystemAdmin(string systemNo, string loginId);
        #endregion


        #region # 创建用户 —— void CreateUser(string loginId, string password)
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        [OperationContract]
        void CreateUser(string loginId, string password);
        #endregion

        #region # 创建用户（带角色） —— void CreateUserWithRoles(string loginId, string password...
        /// <summary>
        /// 创建用户（带角色）
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleIds">角色Id集</param>
        [OperationContract]
        void CreateUserWithRoles(string loginId, string password, string systemNo, IEnumerable<Guid> roleIds);
        #endregion

        #region # 修改密码 —— void UpdatePassword(string loginId, string newPassword)
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="newPassword">新密码</param>
        [OperationContract]
        void UpdatePassword(string loginId, string newPassword);
        #endregion

        #region # 启用用户 —— void EnableUser(string loginId, string password)
        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        [OperationContract]
        void EnableUser(string loginId, string password);
        #endregion

        #region # 停用用户 —— void DisableUser(string loginId, string password)
        /// <summary>
        /// 停用用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        [OperationContract]
        void DisableUser(string loginId, string password);
        #endregion

        #region # 删除用户 —— void RemoveUser(string loginId)
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        [OperationContract]
        void RemoveUser(string loginId);
        #endregion

        #region # 为用户分配角色 —— void SetRoles(string loginId, string systemNo...
        /// <summary>
        /// 为用户分配角色
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleIds">角色Id集</param>
        [OperationContract]
        void SetRoles(string loginId, string systemNo, IEnumerable<Guid> roleIds);
        #endregion

        #region # 为用户追加角色 —— void AppendRoles(string loginId, string systemNo...
        /// <summary>
        /// 为用户追加角色
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleIds">角色Id集</param>
        [OperationContract]
        void AppendRoles(string loginId, string systemNo, IEnumerable<Guid> roleIds);
        #endregion


        #region # 创建角色 —— Guid CreateRole(string systemNo, string roleName...
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="authorityIds">权限Id集</param>
        /// <returns>角色Id</returns>
        [OperationContract]
        Guid CreateRole(string systemNo, string roleName, IEnumerable<Guid> authorityIds);
        #endregion

        #region # 为角色授权 —— void SetAuthorities(string systemNo, Guid roleId...
        /// <summary>
        /// 为角色授权
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="authorityIds">权限Id集</param>
        [OperationContract]
        void SetAuthorities(string systemNo, Guid roleId, IEnumerable<Guid> authorityIds);
        #endregion

        #region # 修改角色 —— void UpdateRole(string systemNo, Guid roleId, string roleName...
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="authorityIds">权限Id集</param>
        [OperationContract]
        void UpdateRole(string systemNo, Guid roleId, string roleName, IEnumerable<Guid> authorityIds);
        #endregion

        #region # 删除角色 —— void RemoveRole(string systemNo, Guid roleId)
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        [OperationContract]
        void RemoveRole(string systemNo, Guid roleId);
        #endregion


        ////////////////////////////////查询部分////////////////////////////////

        #region # 获取信息系统 —— InfoSystemInfo GetInfoSystem(string systemNo)
        /// <summary>
        /// 获取信息系统
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>信息系统</returns>
        [OperationContract]
        InfoSystemInfo GetInfoSystem(string systemNo);
        #endregion

        #region # 获取信息系统列表 —— IEnumerable<InfoSystemInfo> GetInfoSystems(string loginId...
        /// <summary>
        /// 获取信息系统列表
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>信息系统列表</returns>
        [OperationContract]
        IEnumerable<InfoSystemInfo> GetInfoSystems(string loginId, string systemKindNo);
        #endregion


        #region # 获取用户 —— UserInfo GetUser(string loginId)
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <returns>用户</returns>
        [OperationContract]
        UserInfo GetUser(string loginId);
        #endregion

        #region # 分页获取用户列表 —— PageModel<UserInfo> GetUsersByPage(string systemNo...
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>用户列表</returns>
        [OperationContract]
        PageModel<UserInfo> GetUsersByPage(string systemNo, string keywords, int pageIndex, int pageSize);
        #endregion

        #region # 是否存在用户 —— bool ExistUser(string loginId)
        /// <summary>
        /// 是否存在用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <returns>是否存在</returns>
        [OperationContract]
        bool ExistUser(string loginId);
        #endregion


        #region # 获取角色 —— RoleInfo GetRole(string systemNo, Guid roleId)
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <returns>角色</returns>
        [OperationContract]
        RoleInfo GetRole(string systemNo, Guid roleId);
        #endregion

        #region # 获取角色集 —— IEnumerable<RoleInfo> GetRoles(string systemNo)
        /// <summary>
        /// 获取角色集
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>角色集</returns>
        [OperationContract]
        IEnumerable<RoleInfo> GetRoles(string systemNo);
        #endregion

        #region # 分页获取角色集 —— PageModel<RoleInfo> GetRolesByPage(string systemNo...
        /// <summary>
        /// 分页获取角色集
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>角色集</returns>
        [OperationContract]
        PageModel<RoleInfo> GetRolesByPage(string systemNo, string keywords, int pageIndex, int pageSize);
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


        #region # 获取菜单树 —— IEnumerable<MenuInfo> GetMenus(string loginId, string systemNo)
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        [OperationContract]
        IEnumerable<MenuInfo> GetMenus(string loginId, string systemNo);
        #endregion

        #region # 获取用户权限列表 —— IEnumerable<AuthorityInfo> GetAuthorities(string loginId...
        /// <summary>
        /// 获取用户权限列表
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>权限列表</returns>
        [OperationContract]
        IEnumerable<AuthorityInfo> GetAuthorities(string loginId, string systemNo);
        #endregion
    }
}
