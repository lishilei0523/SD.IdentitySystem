using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.Infrastructure;
using SD.Infrastructure.DTOBase;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace SD.IdentitySystem.IAppService.Interfaces
{
    /// <summary>
    /// 用户服务契约接口
    /// </summary>
    [ServiceContract(Namespace = "http://SD.IdentitySystem.IAppService.Interfaces")]
    public interface IUserContract : IApplicationService
    {
        //命令部分

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
        /// <param name="roleIds">角色Id集</param>
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SetRoles(string loginId, IEnumerable<Guid> roleIds);
        #endregion

        #region # 为用户追加角色 —— void AppendRoles(string loginId...
        /// <summary>
        /// 为用户追加角色
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="roleIds">角色Id集</param>
        [OperationContract]
        void AppendRoles(string loginId, IEnumerable<Guid> roleIds);
        #endregion


        //查询部分

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

        #region # 根据角色获取用户列表 —— PageModel<UserInfo> GetUsersByRole(string keywords...
        /// <summary>
        /// 根据角色获取用户列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>用户列表</returns>
        [OperationContract]
        PageModel<UserInfo> GetUsersByRole(string keywords, Guid roleId, int pageIndex, int pageSize);
        #endregion

        #region # 根据账号集获取用户字典 —— IDictionary<string, UserInfo> GetUsersByLoginIds(...
        /// <summary>
        /// 根据账号集获取用户字典
        /// </summary>
        /// <param name="loginIds">账号集</param>
        /// <returns>用户字典</returns>
        [OperationContract]
        IDictionary<string, UserInfo> GetUsersByLoginIds(IEnumerable<string> loginIds);
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


        #region # 获取用户登录记录列表 —— PageModel<LoginRecordInfo> GetLoginRecords(string keywords...
        /// <summary>
        /// 获取用户登录记录列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>用户登录记录列表</returns>
        [OperationContract]
        PageModel<LoginRecordInfo> GetLoginRecords(string keywords, DateTime? startTime, DateTime? endTime, int pageIndex, int pageSize);
        #endregion
    }
}
