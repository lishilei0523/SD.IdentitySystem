using SD.IdentitySystem.AppService.Maps;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories;
using SD.IdentitySystem.Domain.Mediators;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.DTOBase;
using SD.Toolkits.Recursion.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
#if NET40_OR_GREATER
using System.ServiceModel;
#endif
#if NETSTANDARD2_0_OR_GREATER
using CoreWCF;
#endif

namespace SD.IdentitySystem.AppService.Implements
{
    /// <summary>
    /// 用户管理服务契约实现
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, IncludeExceptionDetailInFaults = true)]
    public class UserContract : IUserContract
    {
        #region # 字段及依赖注入构造器

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
        public UserContract(RepositoryMediator repMediator, IUnitOfWorkIdentity unitOfWork)
        {
            this._repMediator = repMediator;
            this._unitOfWork = unitOfWork;
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
        public void CreateUser(string loginId, string realName, string password)
        {
            #region # 验证

            if (this._repMediator.UserRep.ExistsNo(loginId))
            {
                throw new ArgumentOutOfRangeException(nameof(loginId), $"用户名\"{loginId}\"已存在，请重试！");
            }

            #endregion

            User user = new User(loginId, realName, password);

            this._unitOfWork.RegisterAdd(user);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 修改密码 —— void UpdatePassword(string loginId, string oldPassword...
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        public void UpdatePassword(string loginId, string oldPassword, string newPassword)
        {
            User user = this._unitOfWork.Resolve<User>(loginId);
            user.UpdatePassword(oldPassword, newPassword);

            this._unitOfWork.RegisterSave(user);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 重置密码 —— void ResetPassword(string loginId, string password)
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="password">新密码</param>
        public void ResetPassword(string loginId, string password)
        {
            User user = this._unitOfWork.Resolve<User>(loginId);
            user.SetPassword(password);

            this._unitOfWork.RegisterSave(user);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 设置私钥 —— void SetPrivateKey(string loginId, string privateKey)
        /// <summary>
        /// 设置私钥
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="privateKey">私钥</param>
        public void SetPrivateKey(string loginId, string privateKey)
        {
            User user = this._unitOfWork.Resolve<User>(loginId);

            #region # 验证

            if (!string.IsNullOrWhiteSpace(privateKey) && user.PrivateKey != privateKey)
            {
                if (this._repMediator.UserRep.ExistsPrivateKey(null, privateKey))
                {
                    throw new ArgumentOutOfRangeException(nameof(privateKey), "私钥已存在！");
                }
            }

            #endregion

            user.SetPrivateKey(privateKey);

            this._unitOfWork.RegisterSave(user);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 启用用户 —— void EnableUser(string loginId)
        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        public void EnableUser(string loginId)
        {
            User user = this._unitOfWork.Resolve<User>(loginId);
            user.Enable();

            this._unitOfWork.RegisterSave(user);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 停用用户 —— void DisableUser(string loginId)
        /// <summary>
        /// 停用用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        public void DisableUser(string loginId)
        {
            User user = this._unitOfWork.Resolve<User>(loginId);
            user.Disable();

            this._unitOfWork.RegisterSave(user);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 删除用户 —— void RemoveUser(string loginId)
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        public void RemoveUser(string loginId)
        {
            User user = this._unitOfWork.Resolve<User>(loginId);

            #region # 验证

            if (loginId == CommonConstants.AdminLoginId)
            {
                throw new InvalidOperationException("超级管理员不可删除！");
            }

            #endregion

            //清空用户/角色关系
            user.ClearRoleRelations();

            this._unitOfWork.RegisterPhysicsRemove(user);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 关联角色到用户 —— void RelateRolesToUser(string loginId...
        /// <summary>
        /// 关联角色到用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="roleIds">角色Id集</param>
        public void RelateRolesToUser(string loginId, IEnumerable<Guid> roleIds)
        {
            roleIds = roleIds?.Distinct().ToArray() ?? Array.Empty<Guid>();

            ICollection<Role> roles = this._unitOfWork.ResolveRange<Role>(roleIds);

            User user = this._unitOfWork.Resolve<User>(loginId);
            user.RelateRoles(roles);

            this._unitOfWork.RegisterSave(user);
            this._unitOfWork.Commit();
        }
        #endregion

        #region # 追加角色到用户 —— void AppendRolesToUser(string loginId...
        /// <summary>
        /// 追加角色到用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="roleIds">角色Id集</param>
        public void AppendRolesToUser(string loginId, IEnumerable<Guid> roleIds)
        {
            #region # 验证

            roleIds = roleIds?.Distinct().ToArray() ?? Array.Empty<Guid>();
            if (roleIds.Any())
            {
                return;
            }

            #endregion

            ICollection<Role> roles = this._unitOfWork.ResolveRange<Role>(roleIds);

            User user = this._unitOfWork.Resolve<User>(loginId);
            user.AppendRoles(roles);

            this._unitOfWork.RegisterSave(user);
            this._unitOfWork.Commit();
        }
        #endregion


        //查询部分

        #region # 获取用户 —— UserInfo GetUser(string loginId)
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <returns>用户</returns>
        public UserInfo GetUser(string loginId)
        {
            User user = this._repMediator.UserRep.SingleOrDefault(loginId);
            UserInfo userInfo = user?.ToDTO();

            return userInfo;
        }
        #endregion

        #region # 获取用户字典 —— IDictionary<string, UserInfo> GetUsersByNo(...
        /// <summary>
        /// 获取用户字典
        /// </summary>
        /// <param name="loginIds">用户名集</param>
        /// <returns>用户字典</returns>
        public IDictionary<string, UserInfo> GetUsersByNo(IEnumerable<string> loginIds)
        {
            IDictionary<string, User> users = this._repMediator.UserRep.Find(loginIds);
            IDictionary<string, UserInfo> userInfos = users.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            return userInfos;
        }
        #endregion

        #region # 获取用户列表 —— IEnumerable<UserInfo> GetUsers(string keywords...
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <returns>用户列表</returns>
        public IEnumerable<UserInfo> GetUsers(string keywords, string infoSystemNo, Guid? roleId)
        {
            ICollection<User> users = this._repMediator.UserRep.Find(keywords, infoSystemNo, roleId);
            IEnumerable<UserInfo> userInfos = users.Select(x => x.ToDTO());

            return userInfos;
        }
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
        public PageModel<UserInfo> GetUsersByPage(string keywords, string infoSystemNo, Guid? roleId, int pageIndex, int pageSize)
        {
            ICollection<User> users = this._repMediator.UserRep.FindByPage(keywords, infoSystemNo, roleId, pageIndex, pageSize, out int rowCount, out int pageCount);
            IEnumerable<UserInfo> userInfos = users.Select(x => x.ToDTO());

            return new PageModel<UserInfo>(userInfos, pageIndex, pageSize, pageCount, rowCount);
        }
        #endregion

        #region # 获取用户信息系统列表 —— IEnumerable<InfoSystemInfo> GetUserInfoSystems(string loginId)
        /// <summary>
        /// 获取用户信息系统列表
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <returns>信息系统列表</returns>
        public IEnumerable<InfoSystemInfo> GetUserInfoSystems(string loginId)
        {
            User user = this._repMediator.UserRep.Single(loginId);

            ICollection<string> infoSystemNos = user.GetRelatedInfoSystemNos();
            IDictionary<string, InfoSystem> infoSystems = this._repMediator.InfoSystemRep.Find(infoSystemNos);
            IEnumerable<InfoSystemInfo> infoSystemInfos = infoSystems.Values.Select(x => x.ToDTO());

            return infoSystemInfos;
        }
        #endregion

        #region # 获取用户菜单列表 —— IEnumerable<MenuInfo> GetMenus(string loginId, string infoSystemNo...
        /// <summary>
        /// 获取用户菜单列表
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>用户菜单列表</returns>
        public IEnumerable<MenuInfo> GetUserMenus(string loginId, string infoSystemNo, ApplicationType? applicationType)
        {
            ICollection<Guid> roleIds = this._repMediator.RoleRep.FindIds(loginId, infoSystemNo);
            ICollection<Guid> authorityIds = this._repMediator.AuthorityRep.FindIdsByRoles(roleIds);

            ICollection<Menu> menus = this._repMediator.MenuRep.FindByAuthorities(authorityIds, applicationType);
            menus = menus.TailRecurseParentNodes();

            IEnumerable<string> infoSystemNos = menus.Select(x => x.InfoSystemNo);
            IDictionary<string, InfoSystemInfo> infoSystemInfos = this._repMediator.InfoSystemRep.Find(infoSystemNos).ToDictionary(x => x.Key, x => x.Value.ToDTO());

            IEnumerable<MenuInfo> menuInfos = menus.Select(x => x.ToDTO(infoSystemInfos));

            return menuInfos;
        }
        #endregion

        #region # 获取用户角色列表 —— IEnumerable<RoleInfo> GetUserRoles(string loginId, string infoSystemNo)
        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <returns>角色列表</returns>
        public IEnumerable<RoleInfo> GetUserRoles(string loginId, string infoSystemNo)
        {
            ICollection<Role> roles = this._repMediator.RoleRep.Find(null, loginId, infoSystemNo);

            IEnumerable<string> infoSystemNos = roles.Select(x => x.InfoSystemNo);
            IDictionary<string, InfoSystemInfo> infoSystemInfos = this._repMediator.InfoSystemRep.Find(infoSystemNos).ToDictionary(x => x.Key, x => x.Value.ToDTO());

            IEnumerable<RoleInfo> roleInfos = roles.Select(x => x.ToDTO(infoSystemInfos));

            return roleInfos;
        }
        #endregion

        #region # 获取用户权限列表 —— IEnumerable<AuthorityInfo> GetUserAuthorities(string loginId...
        /// <summary>
        /// 获取用户权限列表
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>权限列表</returns>
        public IEnumerable<AuthorityInfo> GetUserAuthorities(string loginId, string infoSystemNo, ApplicationType? applicationType)
        {
            ICollection<Guid> roleIds = this._repMediator.RoleRep.FindIds(loginId, infoSystemNo);
            ICollection<Authority> authorities = this._repMediator.AuthorityRep.FindByRoles(roleIds, applicationType);

            IEnumerable<string> infoSystemNos = authorities.Select(x => x.InfoSystemNo);
            IDictionary<string, InfoSystemInfo> infoSystemInfos = this._repMediator.InfoSystemRep.Find(infoSystemNos).ToDictionary(x => x.Key, x => x.Value.ToDTO());

            IEnumerable<AuthorityInfo> authorityInfos = authorities.Select(x => x.ToDTO(infoSystemInfos));

            return authorityInfos;
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
        public PageModel<LoginRecordInfo> GetLoginRecordsByPage(string keywords, DateTime? startTime, DateTime? endTime, int pageIndex, int pageSize)
        {
            ICollection<LoginRecord> records = this._repMediator.LoginRecordRep.FindByPage(keywords, startTime, endTime, pageIndex, pageSize, out int rowCount, out int pageCount);
            IEnumerable<LoginRecordInfo> recordInfos = records.Select(x => x.ToDTO());

            return new PageModel<LoginRecordInfo>(recordInfos, pageIndex, pageSize, pageCount, rowCount);
        }
        #endregion

        #region # 是否存在用户 —— bool ExistsUser(string loginId)
        /// <summary>
        /// 是否存在用户
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <returns>是否存在</returns>
        public bool ExistsUser(string loginId)
        {
            return this._repMediator.UserRep.ExistsNo(loginId);
        }
        #endregion

        #region # 是否存在私钥 —— bool ExistsPrivateKey(string loginId, string privateKey)
        /// <summary>
        /// 是否存在私钥
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>是否存在</returns>
        public bool ExistsPrivateKey(string loginId, string privateKey)
        {
            return this._repMediator.UserRep.ExistsPrivateKey(loginId, privateKey);
        }
        #endregion
    }
}
