using System;
using System.Collections.Generic;
using System.Linq;
using SD.UAC.Common;
using ShSoft.Infrastructure.EntityBase;

namespace SD.UAC.Domain.Entities
{
    /// <summary>
    /// 信息系统
    /// </summary>
    public class InfoSystem : AggregateRootEntity
    {
        #region # 构造器

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

        #region 获取系统管理员角色 —— Role GetManagerRole()
        /// <summary>
        /// 获取系统管理员角色
        /// </summary>
        /// <returns>系统管理员角色</returns>
        public Role GetManagerRole()
        {
            #region # 验证业务

            if (this.Roles.All(x => x.Name != Constants.ManagerRoleName))
            {
                throw new ApplicationException("发生严重的应用程序错误，未为信息系统初始化系统管理员角色！");
            }

            #endregion

            return this.Roles.Single(x => x.Name == Constants.ManagerRoleName);
        }
        #endregion

        #region 删除角色 —— void RemoveRole(Role role)
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="role">角色</param>
        public void RemoveRole(Role role)
        {
            foreach (Authority authority in role.Authorities)
            {
                authority.Roles.Remove(role);
            }
            foreach (User user in role.Users)
            {
                user.Roles.Remove(role);
            }

            this.Roles.Remove(role);
        }
        #endregion

        #endregion
    }
}
