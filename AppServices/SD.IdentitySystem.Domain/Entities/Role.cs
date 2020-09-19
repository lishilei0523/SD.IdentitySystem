using SD.Common;
using SD.Infrastructure.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Domain.Entities
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : AggregateRootEntity
    {
        #region # 构造器

        #region 01.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected Role()
        {
            //初始化导航属性
            this.Users = new HashSet<User>();
            this.Authorities = new HashSet<Authority>();
        }
        #endregion

        #region 02.创建角色构造器
        /// <summary>
        /// 创建角色构造器
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="description">角色描述</param>
        /// <param name="roleNo">角色编号</param>
        public Role(string roleName, string systemNo, string description, string roleNo = null)
            : this()
        {
            //验证参数
            Assert.IsFalse(string.IsNullOrWhiteSpace(roleName), "角色名称不可为空！");

            base.Number = roleNo;
            base.Name = roleName;
            this.SystemNo = systemNo;
            this.Description = description;

            //初始化关键字
            this.InitKeywords();
        }
        #endregion

        #endregion

        #region # 属性

        #region 信息系统编号 —— string SystemNo
        /// <summary>
        /// 信息系统编号
        /// </summary>
        public string SystemNo { get; private set; }
        #endregion

        #region 角色描述 —— string Description
        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; private set; }
        #endregion

        #region 导航属性 - 用户集 —— ICollection<User> Users
        /// <summary>
        /// 导航属性 - 用户集
        /// </summary>
        public virtual ICollection<User> Users { get; private set; }
        #endregion

        #region 导航属性 - 权限集 —— ICollection<Authority> Authorities
        /// <summary>
        /// 导航属性 - 权限集
        /// </summary>
        public virtual ICollection<Authority> Authorities { get; set; }
        #endregion

        #endregion

        #region # 方法

        //Public

        #region 修改角色信息 —— void UpdateInfo(string roleName, string description)
        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="description">角色描述</param>
        public void UpdateInfo(string roleName, string description)
        {
            base.Name = roleName;
            this.Description = description;

            //初始化关键字
            this.InitKeywords();
        }
        #endregion

        #region 分配权限 —— void SetAuthorities(IEnumerable<Authority> authorities)
        /// <summary>
        /// 分配权限
        /// </summary>
        /// <param name="authorities">权限集</param>
        public void SetAuthorities(IEnumerable<Authority> authorities)
        {
            this.ClearAuthorities();
            this.AppendAuthorities(authorities);
        }
        #endregion

        #region 追加权限 —— void AppendAuthorities(IEnumerable<Authority> authorities)
        /// <summary>
        /// 追加权限
        /// </summary>
        /// <param name="authorities">权限集</param>
        public void AppendAuthorities(IEnumerable<Authority> authorities)
        {
            authorities = authorities == null ? new Authority[0] : authorities.ToArray();

            #region # 验证参数

            foreach (Authority authority in authorities)
            {
                if (this.SystemNo != authority.SystemNo)
                {
                    throw new ArgumentOutOfRangeException("authorities", string.Format("角色与Id为\"{0}\"的权限的信息系统不匹配！角色所在信息系统：\"{1}\"，权限所在信息系统：\"{2}\"", authority.Id, this.SystemNo, authority.SystemNo));
                }
                authority.Roles.Add(this);
            }

            #endregion

            this.Authorities.AddRange(authorities);
        }
        #endregion

        #region 清空权限集 —— void ClearAuthorities()
        /// <summary>
        /// 清空权限集
        /// </summary>
        public void ClearAuthorities()
        {
            foreach (Authority authority in this.Authorities.ToArray())
            {
                this.Authorities.Remove(authority);
                authority.Roles.Remove(this);
            }
        }
        #endregion


        //Private

        #region 初始化关键字 —— void InitKeywords()
        /// <summary>
        /// 初始化关键字
        /// </summary>
        private void InitKeywords()
        {
            base.SetKeywords(this.Name);
        }
        #endregion

        #endregion
    }
}