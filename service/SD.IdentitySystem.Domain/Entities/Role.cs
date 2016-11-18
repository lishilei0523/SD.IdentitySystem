using System;
using System.Collections.Generic;
using System.Linq;
using ShSoft.Common.PoweredByLee;
using ShSoft.Infrastructure.EntityBase;

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
            this.UserRoles = new HashSet<UserRole>();
            this.Authorities = new HashSet<Authority>();
        }
        #endregion

        #region 02.创建角色构造器
        /// <summary>
        /// 创建角色构造器
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="description">角色描述</param>
        /// <param name="roleNo">角色编号</param>
        public Role(string roleName, string systemKindNo, string description, string roleNo = null)
            : this()
        {
            //验证参数
            Assert.IsFalse(string.IsNullOrWhiteSpace(roleName), "角色名称不可为空！");

            base.Number = roleNo;
            base.Name = roleName;
            this.SystemKindNo = systemKindNo;
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

        #region 角色描述 —— string Description
        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; private set; }
        #endregion

        #region 导航属性 - 用户角色集 —— ICollection<UserRole> UserRoles
        /// <summary>
        /// 导航属性 - 用户角色集
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; private set; }
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
            //验证参数
            base.CheckName(roleName);

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
                if (this.SystemKindNo != authority.SystemKindNo)
                {
                    throw new ArgumentOutOfRangeException("authorities", string.Format("角色与Id为\"{0}\"的权限的信息系统类别不匹配！角色所在信息系统类别：\"{1}\"，权限所在信息系统类别：\"{2}\"", authority.Id, this.SystemKindNo, authority.SystemKindNo));
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

        #region 获取权限集 —— IEnumerable<Authority> GetAuthorities()
        /// <summary>
        /// 获取权限集
        /// </summary>
        /// <returns>权限集</returns>
        public IEnumerable<Authority> GetAuthorities()
        {
            return this.Authorities.Where(x => !x.Deleted);
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