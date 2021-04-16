using SD.Common;
using SD.Infrastructure.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            this.PrivateKey = Guid.NewGuid().ToString();
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

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(nameof(password), @"密码不可为空！");
            }
            if (loginId.Length < 2 || loginId.Length > 20)
            {
                throw new ArgumentOutOfRangeException(nameof(loginId), @"登录名长度不可小于2或大于20！");
            }
            if (password.Length < 6 || password.Length > 20)
            {
                throw new ArgumentOutOfRangeException(nameof(password), @"密码长度不可小于6或大于20！");
            }

            #endregion

            base.Number = loginId;
            base.Name = realName;
            this.Password = password.ToMD5();

            //初始化关键字
            this.InitKeywords();
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

        #region 私钥 —— string PrivateKey
        /// <summary>
        /// 私钥
        /// </summary>
        public string PrivateKey { get; private set; }
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
                throw new ArgumentNullException(nameof(oldPassword), "旧密码不可为空！");
            }
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                throw new ArgumentNullException(nameof(newPassword), "新密码不可为空！");
            }
            if (newPassword.Length < 6 || newPassword.Length > 20)
            {
                throw new ArgumentOutOfRangeException(nameof(newPassword), "密码长度不可小于6或大于20！");
            }
            if (this.Password != oldPassword.ToMD5())
            {
                throw new ArgumentOutOfRangeException(nameof(oldPassword), @"旧密码不正确！");
            }

            #endregion

            this.Password = newPassword.ToMD5();
        }
        #endregion

        #region 设置密码 —— void SetPassword(string password)
        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="password">密码</param>
        public void SetPassword(string password)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(nameof(password), "密码不可为空！");
            }
            if (password.Length < 6 || password.Length > 20)
            {
                throw new ArgumentOutOfRangeException(nameof(password), "密码长度不可小于6或大于20！");
            }
            if (password.ToMD5() == this.Password)
            {
                return;
            }

            #endregion

            this.Password = password.ToMD5();
        }
        #endregion

        #region 设置私钥 —— void SetPrivateKey(string privateKey)
        /// <summary>
        /// 设置私钥
        /// </summary>
        /// <param name="privateKey">私钥</param>
        public void SetPrivateKey(string privateKey)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(privateKey))
            {
                throw new ArgumentNullException(nameof(privateKey), "私钥不可为空！");
            }

            #endregion

            this.PrivateKey = privateKey.Trim();
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

        #region 分配角色 —— void RelateRoles(IEnumerable<Role> roles)
        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="roles">角色集</param>
        public void RelateRoles(IEnumerable<Role> roles)
        {
            roles = roles?.ToArray() ?? new Role[0];

            this.ClearRoleRelations();
            if (roles.Any())
            {
                this.AppendRoles(roles);
            }
        }
        #endregion

        #region 追加角色 —— void AppendRoles(IEnumerable<Role> roles)
        /// <summary>
        /// 追加角色
        /// </summary>
        /// <param name="roles">角色集</param>
        public void AppendRoles(IEnumerable<Role> roles)
        {
            #region # 验证

            roles = roles?.ToArray() ?? new Role[0];
            if (!roles.Any())
            {
                throw new ArgumentNullException(nameof(roles), @"要追加的角色不可为空！");
            }

            #endregion

            foreach (Role role in roles)
            {
                this.Roles.Add(role);
                role.Users.Add(this);
            }
        }
        #endregion

        #region 清空角色关系 —— void ClearRoleRelations()
        /// <summary>
        /// 清空角色关系
        /// </summary>
        public void ClearRoleRelations()
        {
            foreach (Role role in this.Roles.ToArray())
            {
                this.Roles.Remove(role);
                role.Users.Remove(this);
            }
        }
        #endregion

        #region 获取信息系统编号列表 —— ICollection<string> GetInfoSystemNos()
        /// <summary>
        /// 获取信息系统编号列表
        /// </summary>
        /// <returns>信息系统编号列表</returns>
        public ICollection<string> GetInfoSystemNos()
        {
            return this.Roles.Select(x => x.SystemNo).Distinct().ToList();
        }
        #endregion

        #region 初始化关键字 —— void InitKeywords()
        /// <summary>
        /// 初始化关键字
        /// </summary>
        private void InitKeywords()
        {
            StringBuilder keywordsBuilder = new StringBuilder();
            keywordsBuilder.Append(base.Number);
            keywordsBuilder.Append(base.Name);

            base.SetKeywords(keywordsBuilder.ToString());
        }
        #endregion

        #endregion
    }
}
