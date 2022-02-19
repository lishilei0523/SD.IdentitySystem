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
    /// 用户管理服务契约接口
    /// </summary>
    [ServiceContract(Namespace = "http://SD.IdentitySystem.IAppService.Interfaces")]
    public interface IUserContract : IApplicationService
    {
        //命令部分

        #region # 创建用户 —— void CreateUser(string loginId, string realName...
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="password">密码</param>
        [OperationContract]
        void CreateUser(string loginId, string realName, string password);
        #endregion

        #region # 修改密码 —— void UpdatePassword(string loginId, string oldPassword...
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        [OperationContract]
        void UpdatePassword(string loginId, string oldPassword, string newPassword);
        #endregion

        #region # 重置密码 —— void ResetPassword(string loginId, string password)
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="password">密码</param>
        [OperationContract]
        void ResetPassword(string loginId, string password);
        #endregion

        #region # 设置私钥 —— void SetPrivateKey(string loginId, string privateKey)
        /// <summary>
        /// 设置私钥
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="privateKey">私钥</param>
        [OperationContract]
        void SetPrivateKey(string loginId, string privateKey);
        #endregion

        #region # 启用用户 —— void EnableUser(string loginId)
        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        [OperationContract]
        void EnableUser(string loginId);
        #endregion

        #region # 停用用户 —— void DisableUser(string loginId)
        /// <summary>
        /// 停用用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        [OperationContract]
        void DisableUser(string loginId);
        #endregion

        #region # 删除用户 —— void RemoveUser(string loginId)
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        [OperationContract]
        void RemoveUser(string loginId);
        #endregion

        #region # 关联角色到用户 —— void RelateRolesToUser(string loginId...
        /// <summary>
        /// 关联角色到用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="roleIds">角色Id集</param>
        [OperationContract]
        void RelateRolesToUser(string loginId, IEnumerable<Guid> roleIds);
        #endregion

        #region # 追加角色到用户 —— void AppendRolesToUser(string loginId...
        /// <summary>
        /// 追加角色到用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="roleIds">角色Id集</param>
        [OperationContract]
        void AppendRolesToUser(string loginId, IEnumerable<Guid> roleIds);
        #endregion


        //查询部分

        #region # 获取用户 —— UserInfo GetUser(string loginId)
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <returns>用户</returns>
        [OperationContract]
        UserInfo GetUser(string loginId);
        #endregion

        #region # 获取用户字典 —— IDictionary<string, UserInfo> GetUsersByNo(...
        /// <summary>
        /// 获取用户字典
        /// </summary>
        /// <param name="loginIds">用户名集</param>
        /// <returns>用户字典</returns>
        [OperationContract]
        IDictionary<string, UserInfo> GetUsersByNo(IEnumerable<string> loginIds);
        #endregion

        #region # 获取用户列表 —— IEnumerable<UserInfo> GetUsers(string keywords...
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <returns>用户列表</returns>
        [OperationContract]
        IEnumerable<UserInfo> GetUsers(string keywords, string infoSystemNo, Guid? roleId);
        #endregion

        #region # 分页获取用户列表 —— PageModel<UserInfo> GetUsersByPage(string keywords...
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>用户列表</returns>
        [OperationContract]
        PageModel<UserInfo> GetUsersByPage(string keywords, string infoSystemNo, Guid? roleId, int pageIndex, int pageSize);
        #endregion

        #region # 获取用户信息系统列表 —— IEnumerable<InfoSystemInfo> GetUserInfoSystems(string loginId)
        /// <summary>
        /// 获取用户信息系统列表
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <returns>信息系统列表</returns>
        [OperationContract]
        IEnumerable<InfoSystemInfo> GetUserInfoSystems(string loginId);
        #endregion

        #region # 获取用户菜单列表 —— IEnumerable<MenuInfo> GetUserMenus(string loginId, string infoSystemNo...
        /// <summary>
        /// 获取用户菜单列表
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>用户菜单列表</returns>
        [OperationContract]
        IEnumerable<MenuInfo> GetUserMenus(string loginId, string infoSystemNo, ApplicationType? applicationType);
        #endregion

        #region # 获取用户角色列表 —— IEnumerable<RoleInfo> GetUserRoles(string loginId, string infoSystemNo)
        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <returns>角色列表</returns>
        [OperationContract]
        IEnumerable<RoleInfo> GetUserRoles(string loginId, string infoSystemNo);
        #endregion

        #region # 获取用户权限列表 —— IEnumerable<AuthorityInfo> GetUserAuthorities(string loginId...
        /// <summary>
        /// 获取用户权限列表
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>权限列表</returns>
        [OperationContract]
        IEnumerable<AuthorityInfo> GetUserAuthorities(string loginId, string infoSystemNo, ApplicationType? applicationType);
        #endregion

        #region # 分页获取登录记录列表 —— PageModel<LoginRecordInfo> GetLoginRecordsByPage(string keywords...
        /// <summary>
        /// 分页获取登录记录列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>登录记录列表</returns>
        [OperationContract]
        PageModel<LoginRecordInfo> GetLoginRecordsByPage(string keywords, DateTime? startTime, DateTime? endTime, int pageIndex, int pageSize);
        #endregion

        #region # 是否存在用户 —— bool ExistsUser(string loginId)
        /// <summary>
        /// 是否存在用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <returns>是否存在</returns>
        [OperationContract]
        bool ExistsUser(string loginId);
        #endregion

        #region # 是否存在私钥 —— bool ExistsPrivateKey(string loginId, string privateKey)
        /// <summary>
        /// 是否存在私钥
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>是否存在</returns>
        [OperationContract]
        bool ExistsPrivateKey(string loginId, string privateKey);
        #endregion
    }
}
