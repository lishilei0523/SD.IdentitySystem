using ShSoft.Common.PoweredByLee;
using ShSoft.Infrastructure.EntityBase;
using ShSoft.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Domain.Entities
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : AggregateRootEntity
    {
        #region # 构造器

        #region 01.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected User()
        {
            //初始化导航属性
            this.Roles = new HashSet<Role>();

            //默认值
            this.Enabled = true;
        }
        #endregion

        #region 02.创建用户构造器
        /// <summary>
        /// 创建用户构造器
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="password">密码</param>
        public User(string loginId, string realName, string password)
            : this()
        {
            #region # 验证参数

            base.CheckName(loginId);

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("password", @"密码不可为空！");
            }
            if (loginId.Length < 2 || loginId.Length > 20)
            {
                throw new ArgumentOutOfRangeException("loginId", @"登录名长度不可小于2或大于20！");
            }
            if (password.Length < 6 || password.Length > 16)
            {
                throw new ArgumentOutOfRangeException("password", @"密码长度不可小于6或大于16！");
            }

            #endregion

            base.Number = loginId;
            base.Name = realName;
            this.Password = password.ToMD5();
            base.SetKeywords(loginId + realName);
        }
        #endregion

        #endregion

        #region # 属性

        #region 密码 —— string Password
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; private set; }
        #endregion

        #region 是否启用 —— bool Enabled
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; private set; }
        #endregion

        #region 导航属性 - 角色集 —— ICollection<Role> Roles
        /// <summary>
        /// 导航属性 - 角色集
        /// </summary>
        public virtual ICollection<Role> Roles { get; private set; }
        #endregion

        #endregion

        #region # 方法

        //Public

        #region 修改密码 —— void UpdatePassword(string oldPassword, string newPassword)
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        public void UpdatePassword(string oldPassword, string newPassword)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(oldPassword))
            {
                throw new ArgumentNullException("oldPassword", "旧密码不可为空！");
            }
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                throw new ArgumentNullException("newPassword", "新密码不可为空！");
            }
            if (newPassword.Length < 6 || newPassword.Length > 32)
            {
                throw new ArgumentOutOfRangeException("newPassword", "密码长度不可少于6个字符或超过32个字符！");
            }
            if (this.Password != oldPassword.ToMD5())
            {
                throw new ArgumentOutOfRangeException("oldPassword", @"旧密码不正确！");
            }

            #endregion

            this.Password = newPassword.ToMD5();
        }
        #endregion

        #region 修改密码 —— void UpdatePassword(string newPassword)
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="newPassword">新密码</param>
        public void UpdatePassword(string newPassword)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                throw new ArgumentNullException("newPassword", "新密码不可为空！");
            }
            if (newPassword.Length < 6 || newPassword.Length > 32)
            {
                throw new ArgumentOutOfRangeException("newPassword", "密码长度不可少于6个字符或超过32个字符！");
            }
            if (newPassword == this.Password)
            {
                return;
            }

            #endregion

            this.Password = newPassword.ToMD5();
        }
        #endregion

        #region 重置密码 —— void ResetPassword()
        /// <summary>
        /// 重置密码
        /// </summary>
        public void ResetPassword()
        {
            this.Password = Constants.InitialPassword.ToMD5();
        }
        #endregion

        #region 启用用户 —— void Enable()
        /// <summary>
        /// 启用用户
        /// </summary>
        public void Enable()
        {
            this.Enabled = true;
        }
        #endregion

        #region 停用用户 —— void Disable()
        /// <summary>
        /// 停用用户
        /// </summary>
        public void Disable()
        {
            this.Enabled = false;
        }
        #endregion

        #region 分配角色 —— void SetRoles(string systemNo, IEnumerable<Role> roles)
        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roles">角色集</param>
        public void SetRoles(string systemNo, IEnumerable<Role> roles)
        {
            #region # 验证参数

            if (roles == null)
            {
                throw new ArgumentNullException("roles", @"角色集不可为null！");
            }

            #endregion

            this.ClearRelation();
            this.AppendRoles(systemNo, roles);
        }
        #endregion

        #region 追加角色 —— void AppendRoles(string systemNo, IEnumerable<Role> roles)
        /// <summary>
        /// 追加角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roles">角色集</param>
        public void AppendRoles(string systemNo, IEnumerable<Role> roles)
        {
            #region # 验证参数

            if (roles == null)
            {
                throw new ArgumentNullException("roles", @"角色集不可为null！");
            }

            #endregion

            foreach (Role role in roles)
            {
                this.Roles.Add(role);
                role.Users.Add(this);
            }
        }
        #endregion

        #region 清空用户关系 —— void ClearRelation()
        /// <summary>
        /// 清空用户关系
        /// </summary>
        public void ClearRelation()
        {
            foreach (Role role in this.Roles.ToArray())
            {
                this.Roles.Remove(role);
                role.Users.Remove(this);
            }
        }
        #endregion

        #region 获取信息系统编号列表 —— IEnumerable<string> GetInfoSystemNos()
        /// <summary>
        /// 获取信息系统编号列表
        /// </summary>
        /// <returns>信息系统编号列表</returns>
        public IEnumerable<string> GetInfoSystemNos()
        {
            return this.Roles.Select(x => x.SystemNo).Distinct();
        }
        #endregion

        #endregion
    }
}
