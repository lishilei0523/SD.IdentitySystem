using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using SD.CacheManager;
using SD.IdentitySystem.AppService.Maps;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using SD.IdentitySystem.Domain.Mediators;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using ShSoft.Common.PoweredByLee;
using ShSoft.Infrastructure.DTOBase;
using ShSoft.Infrastructure.Global.Transaction;
using ShSoft.ValueObjects;

namespace SD.IdentitySystem.AppService.Implements
{
    /// <summary>
    /// 用户服务契约实现
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class UserContract : IUserContract
    {
        #region # 字段及依赖注入构造器

        /// <summary>
        /// 领域服务中介者
        /// </summary>
        private readonly DomainServiceMediator _svcMediator;

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
        /// <param name="svcMediator">领域服务中介者</param>
        /// <param name="repMediator">仓储中介者</param>
        /// <param name="unitOfWork">单元事务</param>
        public UserContract(DomainServiceMediator svcMediator, RepositoryMediator repMediator, IUnitOfWorkIdentity unitOfWork)
        {
            this._svcMediator = svcMediator;
            this._repMediator = repMediator;
            this._unitOfWork = unitOfWork;
        }

        #endregion

        ////////////////////////////////命令部分////////////////////////////////

        #region # 创建用户 —— void CreateUser(string loginId, string realName...
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="password">密码</param>
        [OperationBehavior(TransactionScopeRequired = true)]
        public void CreateUser(string loginId, string realName, string password)
        {
            //验证参数
            Assert.IsFalse(this.ExistsUser(loginId), "登录名\"{0}\"已存在，请重试！");

            User user = new User(loginId, realName, password);

            this._unitOfWork.RegisterAdd(user);
            this._unitOfWork.UnitedCommit();

            //清除缓存
            CacheMediator.Remove(typeof(IUserRepository).FullName);
        }
        #endregion

        #region # 修改密码 —— void UpdatePassword(string loginId, string oldPassword...
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        public void UpdatePassword(string loginId, string oldPassword, string newPassword)
        {
            User currentUser = this._unitOfWork.Resolve<User>(loginId);
            currentUser.UpdatePassword(oldPassword, newPassword);

            this._unitOfWork.RegisterSave(currentUser);
            this._unitOfWork.UnitedCommit();

            //清除缓存
            CacheMediator.Remove(typeof(IUserRepository).FullName);
        }
        #endregion

        #region # 重置密码 —— void ResetPassword(string loginId, string newPassword)
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="newPassword">新密码</param>
        public void ResetPassword(string loginId, string newPassword)
        {
            User currentUser = this._unitOfWork.Resolve<User>(loginId);
            currentUser.UpdatePassword(newPassword);

            this._unitOfWork.RegisterSave(currentUser);
            this._unitOfWork.UnitedCommit();

            //清除缓存
            CacheMediator.Remove(typeof(IUserRepository).FullName);
        }
        #endregion

        #region # 启用用户 —— void EnableUser(string loginId)
        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        public void EnableUser(string loginId)
        {
            User currentUser = this._unitOfWork.Resolve<User>(loginId);
            currentUser.Enable();

            this._unitOfWork.RegisterSave(currentUser);
            this._unitOfWork.UnitedCommit();

            //清除缓存
            CacheMediator.Remove(typeof(IUserRepository).FullName);
        }
        #endregion

        #region # 停用用户 —— void DisableUser(string loginId)
        /// <summary>
        /// 停用用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        public void DisableUser(string loginId)
        {
            User currentUser = this._unitOfWork.Resolve<User>(loginId);
            currentUser.Disable();

            this._unitOfWork.RegisterSave(currentUser);
            this._unitOfWork.UnitedCommit();

            //清除缓存
            CacheMediator.Remove(typeof(IUserRepository).FullName);
        }
        #endregion

        #region # 删除用户 —— void RemoveUser(string loginId)
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        public void RemoveUser(string loginId)
        {
            #region # 验证

            if (loginId == Constants.AdminLoginId)
            {
                throw new InvalidOperationException("超级管理员不可删除！");
            }

            #endregion

            this._unitOfWork.RegisterRemove<User>(loginId);
            this._unitOfWork.UnitedCommit();

            //清除缓存
            CacheMediator.Remove(typeof(IUserRepository).FullName);
        }
        #endregion

        #region # 为用户分配角色 —— void SetRoles(string loginId...
        /// <summary>
        /// 为用户分配角色
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemRoles">信息系统、角色Id字典</param>
        /// <remarks>IDictionary[string, IEnumerable[Guid]]，[信息系统编号，角色Id集]</remarks>
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SetRoles(string loginId, IDictionary<string, IEnumerable<Guid>> systemRoles)
        {
            User currentUser = this._unitOfWork.Resolve<User>(loginId);

            foreach (KeyValuePair<string, IEnumerable<Guid>> systemRole in systemRoles)
            {
                InfoSystem currentSystem = this._repMediator.InfoSystemRep.Single(systemRole.Key);

                IList<Role> roles = new List<Role>();

                foreach (Guid roleId in systemRole.Value)
                {
                    Role currentRole = this._unitOfWork.Resolve<Role>(roleId);

                    if (currentRole.SystemNo != currentSystem.Number)
                    {
                        throw new InvalidOperationException(string.Format("角色\"{0}\"与信息系统\"{1}\"不匹配！", roleId, systemRole.Key));
                    }

                    roles.Add(currentRole);
                }

                currentUser.SetRoles(currentSystem.Number, roles);
            }


            this._unitOfWork.RegisterSave(currentUser);
            this._unitOfWork.UnitedCommit();
        }
        #endregion

        #region # 为用户追加角色 —— void AppendRoles(string loginId, IEnumerable<Guid> roleIds)
        /// <summary>
        /// 为用户追加角色
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemRoles">信息系统、角色Id字典</param>
        /// <remarks>IDictionary[string, IEnumerable[Guid]]，[信息系统编号，角色Id集]</remarks>
        public void AppendRoles(string loginId, IDictionary<string, IEnumerable<Guid>> systemRoles)
        {
            User currentUser = this._unitOfWork.Resolve<User>(loginId);

            foreach (var systemRole in systemRoles)
            {
                InfoSystem currentSystem = this._repMediator.InfoSystemRep.Single(systemRole.Key);

                IList<Role> roles = new List<Role>();

                foreach (Guid roleId in systemRole.Value)
                {
                    Role currentRole = this._unitOfWork.Resolve<Role>(roleId);

                    if (currentRole.SystemNo != currentSystem.Number)
                    {
                        throw new InvalidOperationException(string.Format("角色\"{0}\"与信息系统\"{1}\"不匹配！", roleId, systemRole.Key));
                    }

                    roles.Add(currentRole);
                }

                currentUser.AppendRoles(currentSystem.Number, roles);
            }

            this._unitOfWork.RegisterSave(currentUser);
            this._unitOfWork.UnitedCommit();
        }
        #endregion


        ////////////////////////////////查询部分////////////////////////////////

        #region # 获取用户 —— UserInfo GetUser(string loginId)
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <returns>用户</returns>
        public UserInfo GetUser(string loginId)
        {
            User currentUser = this._repMediator.UserRep.SingleOrDefault(loginId);

            return currentUser.ToDTO();
        }
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
        public PageModel<UserInfo> GetUsers(string systemNo, string keywords, int pageIndex, int pageSize)
        {
            int rowCount, pageCount;

            IEnumerable<User> specUsers = this._repMediator.UserRep.FindByPage(systemNo, keywords, pageIndex, pageSize, out rowCount, out pageCount);

            IEnumerable<UserInfo> specUserInfos = specUsers.Select(x => x.ToDTO());

            return new PageModel<UserInfo>(specUserInfos, pageIndex, pageSize, pageCount, rowCount);
        }
        #endregion

        #region # 是否存在用户 —— bool ExistsUser(string loginId)
        /// <summary>
        /// 是否存在用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <returns>是否存在</returns>
        public bool ExistsUser(string loginId)
        {
            return this._repMediator.UserRep.ExistsFromCache(loginId);
        }
        #endregion


        #region # 获取用户菜单树 —— IEnumerable<MenuInfo> GetMenus(string loginId, string systemNo)
        /// <summary>
        /// 获取用户菜单树
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>用户菜单树</returns>
        public IEnumerable<MenuInfo> GetMenus(string loginId, string systemNo)
        {
            IEnumerable<Menu> menus = this._repMediator.UserRoleRep.GetMenus(loginId, systemNo);

            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> systemInfos = systems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            return menus.Select(x => x.ToDTO(systemInfos));
        }
        #endregion

        #region # 获取用户角色列表 —— IEnumerable<RoleInfo> GetRoles(string loginId, string systemNo)
        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>角色列表</returns>
        public IEnumerable<RoleInfo> GetRoles(string loginId, string systemNo)
        {
            IEnumerable<Role> roles = this._repMediator.UserRoleRep.GetRoles(loginId, systemNo);

            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> systemInfos = systems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            return roles.Select(x => x.ToDTO(systemInfos));
        }
        #endregion

        #region # 获取用户权限列表 —— IEnumerable<AuthorityInfo> GetAuthorities(string loginId...
        /// <summary>
        /// 获取用户权限列表
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>权限列表</returns>
        public IEnumerable<AuthorityInfo> GetAuthorities(string loginId, string systemNo)
        {
            IEnumerable<Authority> authorities = this._repMediator.UserRoleRep.GetAuthorities(loginId, systemNo);

            IDictionary<string, InfoSystem> systems = this._repMediator.InfoSystemRep.FindDictionary();
            IDictionary<string, InfoSystemInfo> systemInfos = systems.ToDictionary(x => x.Key, x => x.Value.ToDTO());

            return authorities.Select(x => x.ToDTO(systemInfos));
        }
        #endregion
    }
}