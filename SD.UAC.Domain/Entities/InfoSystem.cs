using System;
using System.Collections.Generic;
using System.Linq;
using ShSoft.Framework2015.Infrastructure.ValueObjects;
using ShSoft.Framework2016.Infrastructure.IEntity;

namespace SD.UAC.Domain.Entities
{
    /// <summary>
    /// 信息系统
    /// </summary>
    public class InfoSystem : AggregateRootEntity
    {
        #region # 常量及构造器

        #region 00.常量

        /// <summary>
        /// 管理中心信息系统名称
        /// </summary>
        public const string ManageCenterSysName = "管理中心";

        /// <summary>
        /// 超级管理员（管理中心）角色名称
        /// </summary>
        public const string AdminRoleName = "超级管理员";

        /// <summary>
        /// 系统管理员（供应商）角色名称
        /// </summary>
        public const string ManagerRoleName = "系统管理员";

        /// <summary>
        /// 供应商代理人（供应商）角色名称
        /// </summary>
        public const string AgentRoleName = "供应商代理人";

        #endregion

        #region 01.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected InfoSystem()
        {
            //初始化导航属性
            this.Roles = new HashSet<Role>();
        }
        #endregion

        #region 02.创建信息系统
        /// <summary>
        /// 创建信息系统
        /// </summary>
        /// <param name="systemName">系统名称</param>
        /// <param name="systemNo">系统编号</param>
        /// <param name="systemKindNo">系统类别编号</param>
        public InfoSystem(string systemNo, string systemName, string systemKindNo)
            : this()
        {
            #region # 验证参数

            base.CheckName(systemName, 2, 50);

            if (string.IsNullOrWhiteSpace(systemNo))
            {
                throw new ArgumentNullException("systemNo", @"系统编号不可为空！");
            }
            if (string.IsNullOrWhiteSpace(systemKindNo))
            {
                throw new ArgumentNullException("systemKindNo", @"系统类别编号不可为空！");
            }

            #endregion

            base.Number = systemNo;
            base.Name = systemName;
            this.InfoSystemKindNo = systemKindNo;
        }
        #endregion

        #endregion

        #region # 属性

        #region 管理员登录名 —— string AdminLoginId
        /// <summary>
        /// 管理员登录名
        /// </summary>
        public string AdminLoginId { get; private set; }
        #endregion

        #region 信息系统类别编号 —— string InfoSystemKindNo
        /// <summary>
        /// 信息系统类别编号
        /// </summary>
        public string InfoSystemKindNo { get; private set; }
        #endregion

        #region 导航属性 - 角色集 —— ICollection<Role> Roles
        /// <summary>
        /// 导航属性 - 角色集
        /// </summary>
        public virtual ICollection<Role> Roles { get; private set; }
        #endregion

        #endregion

        #region # 方法

        #region 设置管理员 —— void SetAdmin(string loginId)
        /// <summary>
        /// 设置管理员
        /// </summary>
        /// <param name="loginId">登录名</param>
        public void SetAdmin(string loginId)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(loginId))
            {
                throw new ArgumentNullException("loginId", @"管理员登录名不可为空！");
            }

            #endregion

            this.AdminLoginId = loginId;
        }
        #endregion

        #region 创建角色 —— void CreateRole(Role role)
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="role"></param>
        public void CreateRole(Role role)
        {
            #region # 验证参数

            if (role == null)
            {
                throw new ArgumentNullException("role", @"角色不可为空！");
            }
            //if (role.RoleAuthorities.IsNullOrEmpty())
            //{
            //    throw new ArgumentNullException("role", @"权限集不可为空！");
            //}

            #endregion

            this.Roles.Add(role);
            role.InfoSystem = this;
        }
        #endregion

        #region 获取角色 —— Role GetRole(Guid roleId)
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>角色</returns>
        public Role GetRole(Guid roleId)
        {
            #region # 验证参数

            if (roleId == Guid.Empty)
            {
                throw new ArgumentNullException("roleId", @"角色Id不可为空！");
            }
            if (this.Roles.All(x => x.Id != roleId))
            {
                throw new ArgumentOutOfRangeException("roleId", string.Format("Id为\"{0}\"的角色不存在！", roleId));
            }

            #endregion

            return this.Roles.Single(x => x.Id == roleId);
        }
        #endregion

        #region 获取超级管理员角色 —— Role GetAdminRole()
        /// <summary>
        /// 获取超级管理员角色
        /// </summary>
        /// <returns>超级管理员角色</returns>
        public Role GetAdminRole()
        {
            #region # 验证业务

            if (this.InfoSystemKindNo != Constants.MCSystemKindNo)
            {
                throw new InvalidOperationException("只有管理中心系统类别有超级管理员角色");
            }
            if (this.Roles.All(x => x.Name != AdminRoleName))
            {
                throw new ApplicationException("发生严重的应用程序错误，未为管理中心信息系统初始化超级管理员角色！");
            }

            #endregion

            return this.Roles.Single(x => x.Name == AdminRoleName);
        }
        #endregion

        #region 获取系统管理员角色 —— Role GetManagerRole()
        /// <summary>
        /// 获取系统管理员角色
        /// </summary>
        /// <returns>系统管理员角色</returns>
        public Role GetManagerRole()
        {
            #region # 验证业务

            if (this.InfoSystemKindNo != Constants.SupplierSystemKindNo)
            {
                throw new InvalidOperationException("只有非管理中心系统类别有系统管理员角色");
            }
            if (this.Roles.All(x => x.Name != ManagerRoleName))
            {
                throw new ApplicationException("发生严重的应用程序错误，未为信息系统初始化系统管理员角色！");
            }

            #endregion

            return this.Roles.Single(x => x.Name == ManagerRoleName);
        }
        #endregion

        #region 获取代理人角色 —— Role GetAgentRole()
        /// <summary>
        /// 获取代理人角色
        /// </summary>
        /// <returns>代理人角色</returns>
        public Role GetAgentRole()
        {
            #region # 验证业务

            if (this.InfoSystemKindNo != Constants.SupplierSystemKindNo)
            {
                throw new InvalidOperationException("只有供应商系统类别有代理人角色");
            }

            #endregion

            return this.Roles.SingleOrDefault(x => x.Name == AgentRoleName);
        }
        #endregion

        #endregion
    }
}
