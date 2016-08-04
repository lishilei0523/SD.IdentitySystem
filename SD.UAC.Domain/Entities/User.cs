﻿using System;
using System.Collections.Generic;
using System.Linq;
using SD.Toolkits.Recursion.Tree;
using SD.UAC.Common;
using ShSoft.Common.PoweredByLee;
using ShSoft.Infrastructure.EntityBase;

namespace SD.UAC.Domain.Entities
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
        public User(string loginId, string password)
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
            this.Password = password.ToMD5();
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

        #region 上次登录时间 —— DateTime? LastLoginTime
        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; private set; }
        #endregion

        #region 是否启用 —— bool Enabled
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; private set; }
        #endregion

        #region 只读属性 - 信息系统编号集 —— IEnumerable<string> SystemNos
        /// <summary>
        /// 只读属性 - 信息系统编号集
        /// </summary>
        public IEnumerable<string> SystemNos
        {
            get { return this.Roles.Select(x => x.SystemNo).Distinct(); }
        }
        #endregion

        #region 导航属性 - 角色集 —— ICollection<Role> Roles
        /// <summary>
        /// 导航属性 - 角色集
        /// </summary>
        public virtual ICollection<Role> Roles { get; private set; }
        #endregion

        #endregion

        #region # 方法

        #region 登录 —— bool Login(string password)
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="password">密码</param>
        public void Login(string password)
        {
            if (this.Password != password.ToMD5())
            {
                throw new InvalidOperationException("密码不正确！");
            }

            this.LastLoginTime = DateTime.Now;
        }
        #endregion

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

        #region 启用用户 —— void Enable(string password)
        /// <summary>
        /// 启用用户
        /// </summary>
        public void Enable(string password)
        {
            #region # 验证业务

            if (this.Enabled)
            {
                throw new InvalidOperationException("用户已被启用！");
            }
            if (this.Password.ToMD5() != password)
            {
                throw new ArgumentOutOfRangeException("password", @"启用失败，密码不正确！");
            }

            #endregion

            this.Enabled = true;
        }
        #endregion

        #region 停用用户 —— void Disable(string password)
        /// <summary>
        /// 停用用户
        /// </summary>
        public void Disable(string password)
        {
            #region # 验证业务

            if (!this.Enabled)
            {
                throw new InvalidOperationException("用户已被停用！");
            }
            if (this.Password.ToMD5() != password)
            {
                throw new ArgumentOutOfRangeException("password", @"停用失败，密码不正确！");
            }

            #endregion

            this.Enabled = false;
        }
        #endregion

        #region 分配角色 —— void SetRoles(IEnumerable<Role> roles)
        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="roles">角色集</param>
        public void SetRoles(IEnumerable<Role> roles)
        {
            #region # 验证参数

            if (roles == null)
            {
                throw new ArgumentNullException("roles", @"角色集不可为null！");
            }

            #endregion

            this.Roles.Clear();
            this.AppendRoles(roles);
        }
        #endregion

        #region 追加角色 —— void AppendRoles(IEnumerable<Role> roles)
        /// <summary>
        /// 追加角色
        /// </summary>
        /// <param name="roles">角色集</param>
        public void AppendRoles(IEnumerable<Role> roles)
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
            }
        }
        #endregion

        #region 获取角色集 —— IEnumerable<Role> GetRoles(string systemNo)
        /// <summary>
        /// 获取角色集
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>角色集</returns>
        public IEnumerable<Role> GetRoles(string systemNo)
        {
            return this.Roles.Where(x => x.SystemNo == systemNo);
        }
        #endregion

        #region 获取菜单集 —— IEnumerable<Menu> GetMenus(string systemNo)
        /// <summary>
        /// 获取菜单集
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>菜单集</returns>
        public IEnumerable<Menu> GetMenus(string systemNo)
        {
            //获取所有权限
            IEnumerable<Authority> authorities = this.GetAuthorities(systemNo);

            //获取所有权限的菜单叶子节点
            IEnumerable<Menu> menuLeaves = authorities.SelectMany(x => x.MenuLeaves).Where(x => x != null).Distinct();

            //尾递归
            return menuLeaves.TailRecurseParentNodes();
        }
        #endregion

        #region 获取权限集 —— IEnumerable<Authority> GetAuthorities(string systemNo)
        /// <summary>
        /// 获取权限集
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>权限集</returns>
        public IEnumerable<Authority> GetAuthorities(string systemNo)
        {
            //获取给定系统内用户所有角色
            IEnumerable<Role> roles = this.Roles.Where(x => x.SystemNo == systemNo);

            //获取所有权限
            return roles.SelectMany(x => x.Authorities).Distinct();
        }
        #endregion

        #endregion
    }
}
