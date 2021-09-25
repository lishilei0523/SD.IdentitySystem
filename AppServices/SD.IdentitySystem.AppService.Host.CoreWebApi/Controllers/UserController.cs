using Microsoft.AspNetCore.Mvc;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.DTOBase;
using SD.Toolkits.AspNetCore.Attributes;
using System;
using System.Collections.Generic;

namespace SD.IdentitySystem.AppService.Host.Controllers
{
    /// <summary>
    /// 用户WebApi接口
    /// </summary>
    [ApiController]
    [Route("Api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        #region # 字段及依赖注入构造器

        /// <summary>
        /// 用户服务契约接口
        /// </summary>
        private readonly IUserContract _userContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public UserController(IUserContract userContract)
        {
            this._userContract = userContract;
        }

        #endregion


        //命令部分

        #region # 创建用户 —— void CreateUser(string loginId, string realName...
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="password">密码</param>
        [HttpPost]
        [WrapPostParameters]
        public void CreateUser(string loginId, string realName, string password)
        {
            this._userContract.CreateUser(loginId, realName, password);
        }
        #endregion

        #region # 修改密码 —— void UpdatePassword(string loginId, string oldPassword...
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        [HttpPost]
        [WrapPostParameters]
        public void UpdatePassword(string loginId, string oldPassword, string newPassword)
        {
            this._userContract.UpdatePassword(loginId, oldPassword, newPassword);
        }
        #endregion

        #region # 重置密码 —— void ResetPassword(string loginId, string password)
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="password">密码</param>
        [HttpPost]
        [WrapPostParameters]
        public void ResetPassword(string loginId, string password)
        {
            this._userContract.ResetPassword(loginId, password);
        }
        #endregion

        #region # 设置私钥 —— void SetPrivateKey(string loginId, string privateKey)
        /// <summary>
        /// 设置私钥
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="privateKey">私钥</param>
        [HttpPost]
        [WrapPostParameters]
        public void SetPrivateKey(string loginId, string privateKey)
        {
            this._userContract.SetPrivateKey(loginId, privateKey);
        }
        #endregion

        #region # 启用用户 —— void EnableUser(string loginId)
        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        [HttpPost]
        [WrapPostParameters]
        public void EnableUser(string loginId)
        {
            this._userContract.EnableUser(loginId);
        }
        #endregion

        #region # 停用用户 —— void DisableUser(string loginId)
        /// <summary>
        /// 停用用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        [HttpPost]
        [WrapPostParameters]
        public void DisableUser(string loginId)
        {
            this._userContract.DisableUser(loginId);
        }
        #endregion

        #region # 删除用户 —— void RemoveUser(string loginId)
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        [HttpPost]
        [WrapPostParameters]
        public void RemoveUser(string loginId)
        {
            this._userContract.RemoveUser(loginId);
        }
        #endregion

        #region # 关联角色到用户 —— void RelateRolesToUser(string loginId...
        /// <summary>
        /// 关联角色到用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="roleIds">角色Id集</param>
        [HttpPost]
        [WrapPostParameters]
        public void RelateRolesToUser(string loginId, IEnumerable<Guid> roleIds)
        {
            this._userContract.RelateRolesToUser(loginId, roleIds);
        }
        #endregion

        #region # 追加角色到用户 —— void AppendRolesToUser(string loginId...
        /// <summary>
        /// 追加角色到用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="roleIds">角色Id集</param>
        [HttpPost]
        [WrapPostParameters]
        public void AppendRolesToUser(string loginId, IEnumerable<Guid> roleIds)
        {
            this._userContract.AppendRolesToUser(loginId, roleIds);
        }
        #endregion


        //查询部分

        #region # 获取用户 —— UserInfo GetUser(string loginId)
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <returns>用户</returns>
        [HttpGet]
        public UserInfo GetUser(string loginId)
        {
            return this._userContract.GetUser(loginId);
        }
        #endregion

        #region # 获取用户列表 —— IEnumerable<UserInfo> GetUsers(string keywords)
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <returns>用户列表</returns>
        [HttpGet]
        public IEnumerable<UserInfo> GetUsers(string keywords)
        {
            return this._userContract.GetUsers(keywords);
        }
        #endregion

        #region # 获取用户字典 —— IDictionary<string, UserInfo> GetUsersByLoginIds(...
        /// <summary>
        /// 获取用户字典
        /// </summary>
        /// <param name="loginIds">用户名集</param>
        /// <returns>用户字典</returns>
        [HttpGet]
        public IDictionary<string, UserInfo> GetUsersByLoginIds([FromJson] IEnumerable<string> loginIds)
        {
            return this._userContract.GetUsersByLoginIds(loginIds);
        }
        #endregion

        #region # 分页获取用户列表 —— PageModel<UserInfo> GetUsersByPage(string keywords...
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>用户列表</returns>
        [HttpGet]
        public PageModel<UserInfo> GetUsersByPage(string keywords, string systemNo, Guid? roleId, int pageIndex, int pageSize)
        {
            return this._userContract.GetUsersByPage(keywords, systemNo, roleId, pageIndex, pageSize);
        }
        #endregion

        #region # 获取用户信息系统列表 —— IEnumerable<InfoSystemInfo> GetUserInfoSystems(string loginId)
        /// <summary>
        /// 获取用户信息系统列表
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <returns>信息系统列表</returns>
        [HttpGet]
        public IEnumerable<InfoSystemInfo> GetUserInfoSystems(string loginId)
        {
            return this._userContract.GetUserInfoSystems(loginId);
        }
        #endregion

        #region # 获取用户菜单树 —— IEnumerable<MenuInfo> GetUserMenus(string loginId, string systemNo...
        /// <summary>
        /// 获取用户菜单树
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>用户菜单树</returns>
        [HttpGet]
        public IEnumerable<MenuInfo> GetUserMenus(string loginId, string systemNo, ApplicationType? applicationType)
        {
            return this._userContract.GetUserMenus(loginId, systemNo, applicationType);
        }
        #endregion

        #region # 获取用户角色列表 —— IEnumerable<RoleInfo> GetUserRoles(string loginId, string systemNo)
        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>角色列表</returns>
        [HttpGet]
        public IEnumerable<RoleInfo> GetUserRoles(string loginId, string systemNo)
        {
            return this._userContract.GetUserRoles(loginId, systemNo);
        }
        #endregion

        #region # 获取用户权限列表 —— IEnumerable<AuthorityInfo> GetUserAuthorities(string loginId...
        /// <summary>
        /// 获取用户权限列表
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>权限列表</returns>
        [HttpGet]
        public IEnumerable<AuthorityInfo> GetUserAuthorities(string loginId, string systemNo)
        {
            return this._userContract.GetUserAuthorities(loginId, systemNo);
        }
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
        [HttpGet]
        public PageModel<LoginRecordInfo> GetLoginRecordsByPage(string keywords, DateTime? startTime, DateTime? endTime, int pageIndex, int pageSize)
        {
            return this._userContract.GetLoginRecordsByPage(keywords, startTime, endTime, pageIndex, pageSize);
        }
        #endregion

        #region # 是否存在用户 —— bool ExistsUser(string loginId)
        /// <summary>
        /// 是否存在用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <returns>是否存在</returns>
        [HttpGet]
        public bool ExistsUser(string loginId)
        {
            return this._userContract.ExistsUser(loginId);
        }
        #endregion

        #region # 是否存在私钥 —— bool ExistsPrivateKey(string loginId, string privateKey)
        /// <summary>
        /// 是否存在私钥
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>是否存在</returns>
        [HttpGet]
        public bool ExistsPrivateKey(string loginId, string privateKey)
        {
            return this._userContract.ExistsPrivateKey(loginId, privateKey);
        }
        #endregion
    }
}
