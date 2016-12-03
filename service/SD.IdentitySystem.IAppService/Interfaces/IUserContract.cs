using System;
using System.Collections.Generic;
using System.ServiceModel;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using ShSoft.Infrastructure;
using ShSoft.Infrastructure.DTOBase;

namespace SD.IdentitySystem.IAppService.Interfaces
{
    /// <summary>
    /// 用户服务契约接口
    /// </summary>
    [ServiceContract(Namespace = "http://SD.IdentitySystem.IAppService.Interfaces")]
    public interface IUserContract : IApplicationService
    {
        ////////////////////////////////命令部分////////////////////////////////

        #region # 创建用户 —— void CreateUser(string loginId, string realName...
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="password">密码</param>
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void CreateUser(string loginId, string realName, string password);
        #endregion

        #region # 修改密码 —— void UpdatePassword(string loginId, string oldPassword...
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        [OperationContract]
        void UpdatePassword(string loginId, string oldPassword, string newPassword);
        #endregion

        #region # 重置密码 —— void ResetPassword(string loginId, string newPassword)
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="newPassword">新密码</param>
        [OperationContract]
        void ResetPassword(string loginId, string newPassword);
        #endregion

        #region # 启用用户 —— void EnableUser(string loginId)
        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        [OperationContract]
        void EnableUser(string loginId);
        #endregion

        #region # 停用用户 —— void DisableUser(string loginId)
        /// <summary>
        /// 停用用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        [OperationContract]
        void DisableUser(string loginId);
        #endregion

        #region # 删除用户 —— void RemoveUser(string loginId)
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        [OperationContract]
        void RemoveUser(string loginId);
        #endregion

        #region # 为用户分配角色 —— void SetRoles(string loginId...
        /// <summary>
        /// 为用户分配角色
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemRoles">信息系统、角色Id字典</param>
        /// <remarks>IDictionary[string, IEnumerable[Guid]]，[信息系统编号，角色Id集]</remarks>
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SetRoles(string loginId, IDictionary<string, IEnumerable<Guid>> systemRoles);
        #endregion

        #region # 为用户追加角色 —— void AppendRoles(string loginId...
        /// <summary>
        /// 为用户追加角色
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemRoles">信息系统、角色Id字典</param>
        /// <remarks>IDictionary[string, IEnumerable[Guid]]，[信息系统编号，角色Id集]</remarks>
        [OperationContract]
        void AppendRoles(string loginId, IDictionary<string, IEnumerable<Guid>> systemRoles);
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

        #region # 获取信息系统列表 —— IEnumerable<InfoSystemInfo> GetInfoSystems()
        /// <summary>
        /// 获取信息系统列表
        /// </summary>
        /// <returns>信息系统列表</returns>
        [OperationContract]
        IEnumerable<InfoSystemInfo> GetInfoSystems();
        #endregion

        #region # 获取信息系统列表 —— IEnumerable<InfoSystemInfo> GetInfoSystemsByUser(string loginId)
        /// <summary>
        /// 获取信息系统列表
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <returns>信息系统列表</returns>
        [OperationContract]
        IEnumerable<InfoSystemInfo> GetInfoSystemsByUser(string loginId);
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

        #region # 分页获取用户列表 —— PageModel<UserInfo> GetUsers(string systemNo...
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>用户列表</returns>
        [OperationContract]
        PageModel<UserInfo> GetUsers(string systemNo, string keywords, int pageIndex, int pageSize);
        #endregion

        #region # 是否存在用户 —— bool ExistsUser(string loginId)
        /// <summary>
        /// 是否存在用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <returns>是否存在</returns>
        [OperationContract]
        bool ExistsUser(string loginId);
        #endregion


        #region # 获取用户菜单树 —— IEnumerable<MenuInfo> GetMenus(string loginId, string systemNo)
        /// <summary>
        /// 获取用户菜单树
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>用户菜单树</returns>
        [OperationContract]
        IEnumerable<MenuInfo> GetMenus(string loginId, string systemNo);
        #endregion

        #region # 获取用户角色列表 —— IEnumerable<RoleInfo> GetRoles(string loginId, string systemNo)
        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>角色列表</returns>
        [OperationContract]
        IEnumerable<RoleInfo> GetRoles(string loginId, string systemNo);
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
