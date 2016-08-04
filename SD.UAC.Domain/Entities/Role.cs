using System;
using System.Collections.Generic;
using System.Linq;
using ShSoft.Common.PoweredByLee;
using ShSoft.Infrastructure.EntityBase;

namespace SD.UAC.Domain.Entities
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
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="description">角色描述</param>
        public Role(string roleName, string systemKindNo, string systemNo, string description)
            : this()
        {
            //验证参数
            Assert.IsFalse(string.IsNullOrWhiteSpace(roleName), "角色名称不可为空！");

            base.Name = roleName;
            this.SystemKindNo = systemKindNo;
            this.SystemNo = systemNo;
            this.Description = description;

            //初始化关键字
            this.InitKeywords();
        }
        #endregion

        #endregion

        #region # 属性

        #region 信息系统类别编号 —— string SystemKindNo
        /// <summary>
        /// 信息系统类别编号
        /// </summary>
        public string SystemKindNo { get; private set; }
        #endregion

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

        #region 只读属性 - 用户数 —— int UserCount
        /// <summary>
        /// 只读属性 - 用户数
        /// </summary>
        public int UserCount
        {
            get { return this.Users.Count(x => !x.Deleted); }
        }
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

        #region 修改角色信息 —— void UpdateInfo(string roleName, string description)
        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="description">角色描述</param>
        public void UpdateInfo(string roleName, string description)
        {
            //验证参数
            base.CheckName(roleName);

            base.Name = roleName;
            this.Description = description;

            //初始化关键字
            this.InitKeywords();
        }
        #endregion

        #region 获取权限集 —— IEnumerable<Authority> GetAuthorities()
        /// <summary>
        /// 获取权限集
        /// </summary>
        /// <returns>权限集</returns>
        public IEnumerable<Authority> GetAuthorities()
        {
            return this.Authorities.ToArray();
        }
        #endregion

        #region 分配权限 —— void SetAuthorities(IEnumerable<Authority> authorities)
        /// <summary>
        /// 分配权限
        /// </summary>
        /// <param name="authorities">权限集</param>
        public void SetAuthorities(IEnumerable<Authority> authorities)
        {
            this.Authorities.Clear();
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

            if (!authorities.Any())
            {
                throw new ArgumentNullException("authorities", @"权限集不可为空！");
            }

            foreach (Authority authority in authorities)
            {
                if (this.SystemKindNo != authority.SystemKindNo)
                {
                    throw new ArgumentOutOfRangeException("authorities", string.Format("角色与Id为\"{0}\"的权限的信息系统类别不匹配！角色所在信息系统类别：\"{1}\"，权限所在信息系统类别：\"{2}\"", authority.Id, this.SystemKindNo, authority.SystemKindNo));
                }
            }

            #endregion

            this.Authorities.AddRange(authorities);
        }
        #endregion

        #region 清空权限集 —— void RemoveAuthorities()
        /// <summary>
        /// 清空权限集
        /// </summary>
        public void RemoveAuthorities()
        {
            foreach (Authority authority in this.Authorities.ToArray())
            {
                this.Authorities.Remove(authority);
            }
        }
        #endregion

        #region 清空用户集 —— void RemoveUsers()
        /// <summary>
        /// 清空用户集
        /// </summary>
        public void RemoveUsers()
        {
            foreach (User user in this.Users.ToArray())
            {
                this.Users.Remove(user);
            }
        }
        #endregion

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