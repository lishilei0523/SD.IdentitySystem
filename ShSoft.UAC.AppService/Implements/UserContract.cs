using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using ShSoft.Framework2015.Infrastructure.ValueObjects.Enums;
using ShSoft.Framework2016.Common.PoweredByLee;
using ShSoft.Framework2016.Infrastructure.Global.Transaction;
using ShSoft.Framework2016.Infrastructure.IDTO;
using ShSoft.Framework2016.Infrastructure.WCF.IOC;
using ShSoft.UAC.AppService.Maps;
using ShSoft.UAC.Domain.Entities;
using ShSoft.UAC.Domain.IRepositories;
using ShSoft.UAC.Domain.Mediators;
using ShSoft.UAC.IAppService.DTOs.Outputs;
using ShSoft.UAC.IAppService.Interfaces;

namespace ShSoft.UAC.AppService.Implements
{
    /// <summary>
    /// 用户服务契约实现
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [IocServiceBehavior]
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
        private readonly IUnitOfWorkUAC _unitOfWork;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="svcMediator">领域服务中介者</param>
        /// <param name="repMediator">仓储中介者</param>
        /// <param name="unitOfWork">单元事务</param>
        public UserContract(DomainServiceMediator svcMediator, RepositoryMediator repMediator, IUnitOfWorkUAC unitOfWork)
        {
            this._svcMediator = svcMediator;
            this._repMediator = repMediator;
            this._unitOfWork = unitOfWork;
        }

        #endregion

        ////////////////////////////////命令部分////////////////////////////////

        #region # 创建信息系统 —— void CreateInfoSystem(string systemNo, string systemName...
        /// <summary>
        /// 创建信息系统
        /// </summary>
        /// <param name="systemNo">组织编号</param>
        /// <param name="systemName">信息系统名称</param>
        /// <param name="systemKindNo">信息系统类别编号</param>
        public void CreateInfoSystem(string systemNo, string systemName, string systemKindNo)
        {
            //验证
            Assert.IsTrue(this._repMediator.InfoSystemKindRep.Exists(systemKindNo), string.Format("编号为\"{0}\"的信息系统类别不存在！", systemKindNo));

            InfoSystem infoSystem = new InfoSystem(systemNo, systemName, systemKindNo);

            this._unitOfWork.RegisterAdd(infoSystem);
            this._unitOfWork.UnitedCommit();
        }
        #endregion

        #region # 设置系统管理员 —— void SetSystemAdmin(string systemNo, string loginId)
        /// <summary>
        /// 设置系统管理员
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="loginId">登录名</param>
        public void SetSystemAdmin(string systemNo, string loginId)
        {
            InfoSystem currentSystem = this._unitOfWork.Resolve<InfoSystem>(systemNo);
            currentSystem.SetAdmin(loginId);

            this._unitOfWork.RegisterSave(currentSystem);
            this._unitOfWork.UnitedCommit();
        }
        #endregion


        #region # 创建用户 —— void CreateUser(string loginId, string password)
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        public void CreateUser(string loginId, string password)
        {
            //验证参数
            this._svcMediator.UserSvc.AssertLoginIdNotExists(loginId);

            User user = new User(loginId, password);

            this._unitOfWork.RegisterAdd(user);
            this._unitOfWork.UnitedCommit();
        }
        #endregion

        #region # 创建用户（带角色） —— void CreateUserWithRoles(string loginId, string password...
        /// <summary>
        /// 创建用户（带角色）
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleIds">角色Id集</param>
        public void CreateUserWithRoles(string loginId, string password, string systemNo, IEnumerable<Guid> roleIds)
        {
            //验证参数
            this._svcMediator.UserSvc.AssertLoginIdNotExists(loginId);

            User user = new User(loginId, password);

            //分配角色
            InfoSystem currentSystem = this._unitOfWork.Resolve<InfoSystem>(systemNo);
            IEnumerable<Role> roles = roleIds.Select(roleId => currentSystem.GetRole(roleId));
            user.SetRoles(roles);

            this._unitOfWork.RegisterAdd(user);
            this._unitOfWork.UnitedCommit();
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
        }
        #endregion

        #region # 修改密码 —— void UpdatePassword(string loginId, string newPassword)
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="newPassword">新密码</param>
        public void UpdatePassword(string loginId, string newPassword)
        {
            User currentUser = this._unitOfWork.Resolve<User>(loginId);
            currentUser.UpdatePassword(newPassword);

            this._unitOfWork.RegisterSave(currentUser);
            this._unitOfWork.UnitedCommit();
        }

        #endregion

        #region # 启用用户 —— void EnableUser(string loginId, string password)
        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        public void EnableUser(string loginId, string password)
        {
            User currentUser = this._unitOfWork.Resolve<User>(loginId);
            currentUser.Enable(password);

            this._unitOfWork.RegisterSave(currentUser);
            this._unitOfWork.UnitedCommit();
        }
        #endregion

        #region # 停用用户 —— void DisableUser(string loginId, string password)
        /// <summary>
        /// 停用用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        public void DisableUser(string loginId, string password)
        {
            User currentUser = this._unitOfWork.Resolve<User>(loginId);
            currentUser.Disable(password);

            this._unitOfWork.RegisterSave(currentUser);
            this._unitOfWork.UnitedCommit();
        }
        #endregion

        #region # 删除用户 —— void RemoveUser(string loginId)
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        public void RemoveUser(string loginId)
        {
            this._unitOfWork.RegisterRemove<User>(loginId);
            this._unitOfWork.UnitedCommit();
        }
        #endregion

        #region # 为用户分配角色 —— void SetRoles(string loginId, string systemNo...
        /// <summary>
        /// 为用户分配角色
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleIds">角色Id集</param>
        public void SetRoles(string loginId, string systemNo, IEnumerable<Guid> roleIds)
        {
            User currentUser = this._unitOfWork.Resolve<User>(loginId);
            InfoSystem currentSystem = this._unitOfWork.Resolve<InfoSystem>(systemNo);

            IEnumerable<Role> roles = roleIds.Select(roleId => currentSystem.GetRole(roleId));
            currentUser.SetRoles(roles);

            this._unitOfWork.RegisterSave(currentUser);
            this._unitOfWork.UnitedCommit();
        }
        #endregion

        #region # 为用户追加角色 —— void AppendRoles(string loginId, string systemNo...
        /// <summary>
        /// 为用户追加角色
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleIds">角色Id集</param>
        public void AppendRoles(string loginId, string systemNo, IEnumerable<Guid> roleIds)
        {
            User currentUser = this._unitOfWork.Resolve<User>(loginId);
            InfoSystem currentSystem = this._unitOfWork.Resolve<InfoSystem>(systemNo);

            IEnumerable<Role> roles = roleIds.Select(roleId => currentSystem.GetRole(roleId));
            currentUser.AppendRoles(roles);

            this._unitOfWork.RegisterSave(currentUser);
            this._unitOfWork.UnitedCommit();
        }
        #endregion

        #region # 登录 —— Guid Login(string loginId, string password)
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        /// <returns>公钥</returns>
        public Guid Login(string loginId, string password)
        {
            //验证
            Assert.IsTrue(this._repMediator.UserRep.Exists(loginId), string.Format("用户名\"{0}\"不存在！", loginId));

            User currentUser = this._unitOfWork.Resolve<User>(loginId);
            currentUser.Login(password);

            //TODO 生成公钥
            Guid publicKey = Guid.NewGuid();

            return publicKey;
        }
        #endregion


        #region # 创建角色 —— Guid CreateRole(string systemNo, string roleName...
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="authorityIds">权限Id集</param>
        /// <returns>角色Id</returns>
        public Guid CreateRole(string systemNo, string roleName, IEnumerable<Guid> authorityIds)
        {
            InfoSystem currentSystem = this._unitOfWork.Resolve<InfoSystem>(systemNo);

            Role role = new Role(roleName, roleName);
            currentSystem.CreateRole(role);

            InfoSystemKind currentKind = this._unitOfWork.Resolve<InfoSystemKind>(currentSystem.InfoSystemKindNo);

            IEnumerable<Authority> authorities = authorityIds.Select(authorityId => currentKind.GetAuthority(authorityId));

            role.SetAuthorities(authorities);

            this._unitOfWork.RegisterSave(currentSystem);
            this._unitOfWork.UnitedCommit();

            return role.Id;
        }
        #endregion

        #region # 为角色授权 —— void SetAuthorities(string systemNo, Guid roleId...
        /// <summary>
        /// 为角色授权
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="authorityIds">权限Id集</param>
        public void SetAuthorities(string systemNo, Guid roleId, IEnumerable<Guid> authorityIds)
        {
            InfoSystem currentSystem = this._unitOfWork.Resolve<InfoSystem>(systemNo);
            Role role = currentSystem.GetRole(roleId);

            InfoSystemKind currentKind = this._unitOfWork.Resolve<InfoSystemKind>(currentSystem.InfoSystemKindNo);

            IEnumerable<Authority> authorities = authorityIds.Select(authorityId => currentKind.GetAuthority(authorityId));

            role.SetAuthorities(authorities);
            this._unitOfWork.RegisterSave(currentSystem);
            this._unitOfWork.UnitedCommit();
        }
        #endregion

        #region # 修改角色 —— void UpdateRole(string systemNo, Guid roleId, string roleName...
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="authorityIds">权限Id集</param>
        public void UpdateRole(string systemNo, Guid roleId, string roleName, IEnumerable<Guid> authorityIds)
        {
            InfoSystem currentSystem = this._unitOfWork.Resolve<InfoSystem>(systemNo);

            Role role = currentSystem.GetRole(roleId);

            role.UpdateInfo(roleName, roleName);

            InfoSystemKind currentKind = this._unitOfWork.Resolve<InfoSystemKind>(currentSystem.InfoSystemKindNo);

            IEnumerable<Authority> authorities =
                authorityIds.Select(authorityId => currentKind.GetAuthority(authorityId));
            role.SetAuthorities(authorities);

            this._unitOfWork.RegisterSave(currentSystem);
            this._unitOfWork.UnitedCommit();
        }

        #endregion

        #region # 删除角色 —— void RemoveRole(string systemNo, Guid roleId)
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        public void RemoveRole(string systemNo, Guid roleId)
        {
            this._unitOfWork.RegisterRemove<Role>(roleId);
            this._unitOfWork.UnitedCommit();
        }
        #endregion


        ////////////////////////////////查询部分////////////////////////////////

        #region # 获取信息系统 —— InfoSystemInfo GetInfoSystem(string systemNo)
        /// <summary>
        /// 获取信息系统
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>信息系统</returns>
        public InfoSystemInfo GetInfoSystem(string systemNo)
        {
            InfoSystem currentSystem = this._repMediator.InfoSystemRep.Single(systemNo);
            return currentSystem.ToDTO(this._repMediator);
        }
        #endregion

        #region # 获取信息系统集 —— IEnumerable<InfoSystemInfo> GetInfoSystems(string loginId...
        /// <summary>
        /// 获取信息系统集
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>信息系统集</returns>
        public IEnumerable<InfoSystemInfo> GetInfoSystems(string loginId, string systemKindNo)
        {
            User currentUser = this._repMediator.UserRep.Single(loginId);

            IEnumerable<InfoSystem> systems = currentUser.GetInfoSystems(systemKindNo);

            return systems.Select(x => x.ToDTO(this._repMediator));
        }
        #endregion


        #region # 获取用户 —— UserInfo GetUser(string loginId)
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <returns>用户</returns>
        public UserInfo GetUser(string loginId)
        {
            User currentUser = this._repMediator.UserRep.SingleOrDefault(loginId);
            if (currentUser == null)
            {
                return null;
            }
            return currentUser.ToDTO();
        }
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
        public PageModel<UserInfo> GetUsersByPage(string systemNo, string keywords, int pageIndex, int pageSize)
        {
            return null;
        }
        #endregion

        #region # 是否存在用户 —— bool ExistUser(string loginId)
        /// <summary>
        /// 是否存在用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <returns>是否存在</returns>
        public bool ExistUser(string loginId)
        {
            return this._repMediator.UserRep.Exists(loginId);
        }
        #endregion


        #region # 获取角色 —— RoleInfo GetRole(string systemNo, Guid roleId)
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <returns>角色</returns>
        public RoleInfo GetRole(string systemNo, Guid roleId)
        {
            InfoSystem currentSystem = this._repMediator.InfoSystemRep.Single(systemNo);
            Role currentRole = currentSystem.GetRole(roleId);

            return currentRole.ToDTOWithAuthority();
        }
        #endregion

        #region # 获取角色列表 —— IEnumerable<RoleInfo> GetRoles(string systemNo)
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>角色列表</returns>
        public IEnumerable<RoleInfo> GetRoles(string systemNo)
        {
            IEnumerable<Role> roles = this._repMediator.RoleRep.FindBySystem(systemNo);

            return roles.Select(x => x.ToDTO());
        }
        #endregion

        #region # 分页获取角色列表 —— PageModel<RoleInfo> GetRolesByPage(string systemNo...
        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>角色列表</returns>
        public PageModel<RoleInfo> GetRolesByPage(string systemNo, string keywords, int pageIndex, int pageSize)
        {
            int rowCount, pageCount;

            IEnumerable<Role> roles = this._repMediator.RoleRep.FindByPage(systemNo, keywords, pageIndex, pageSize, out rowCount, out pageCount);

            IEnumerable<RoleInfo> roleInfos = roles.Select(x => x.ToDTO());

            return new PageModel<RoleInfo>(roleInfos, pageIndex, pageSize, pageCount, rowCount);
        }
        #endregion

        #region # 是否存在角色 —— bool ExistsRole(string systemNo, Guid? roleId, string roleName)
        /// <summary>
        /// 是否存在角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="roleName">角色名称</param>
        /// <returns>是否存在</returns>
        public bool ExistsRole(string systemNo, Guid? roleId, string roleName)
        {
            return this._svcMediator.InfoSystemSvc.ExistsRole(systemNo, roleId, roleName);
        }
        #endregion


        #region # 获取菜单树 —— IEnumerable<MenuInfo> GetMenus(string loginId, string systemNo)
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>菜单树</returns>
        public IEnumerable<MenuInfo> GetMenus(string loginId, string systemNo)
        {
            User currentUser = this._unitOfWork.Resolve<User>(loginId);
            IEnumerable<Menu> menus = currentUser.GetMenus(systemNo);

            return menus.Select(x => x.ToDTO());
        }
        #endregion

        #region # 获取用户权限集 —— IEnumerable<AuthorityInfo> GetAuthorities(string loginId...
        /// <summary>
        /// 获取用户权限集
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>权限集</returns>
        public IEnumerable<AuthorityInfo> GetAuthorities(string loginId, string systemNo)
        {
            User currentUser = this._repMediator.UserRep.Single(loginId);
            IEnumerable<Authority> authorities = currentUser.GetAuthorities(systemNo);
            return authorities.Select(x => x.ToDTO());
        }
        #endregion
    }
}