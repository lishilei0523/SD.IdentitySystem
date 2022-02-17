using SD.Infrastructure.Constants;
using SD.Infrastructure.EntityBase;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SD.IdentitySystem.Domain.Entities
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Authority : AggregateRootEntity
    {
        #region # 构造器

        #region 00.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected Authority()
        {
            //初始化导航属性
            this.Roles = new HashSet<Role>();
            this.MenuLeaves = new HashSet<Menu>();
        }
        #endregion

        #region 01.创建权限构造器
        /// <summary>
        /// 创建权限构造器
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="authorityName">权限名称</param>
        /// <param name="authorityPath">权限路径</param>
        /// <param name="englishName">英文名称</param>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="namespace">命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="description">描述</param>
        public Authority(string infoSystemNo, ApplicationType applicationType, string authorityName, string authorityPath, string englishName, string assemblyName, string @namespace, string className, string methodName, string description)
            : this()
        {
            base.Name = authorityName;
            this.InfoSystemNo = infoSystemNo;
            this.ApplicationType = applicationType;
            this.AuthorityPath = authorityPath;
            this.EnglishName = englishName;
            this.AssemblyName = assemblyName;
            this.Namespace = @namespace;
            this.ClassName = className;
            this.MethodName = methodName;
            this.Description = description;

            //初始化权限路径与关键字
            this.InitPath();
            this.InitKeywords();
        }
        #endregion

        #endregion

        #region # 属性

        #region 信息系统编号 —— string InfoSystemNo
        /// <summary>
        /// 信息系统编号
        /// </summary>
        public string InfoSystemNo { get; private set; }
        #endregion

        #region 应用程序类型 —— ApplicationType ApplicationType
        /// <summary>
        /// 应用程序类型
        /// </summary>
        public ApplicationType ApplicationType { get; private set; }
        #endregion

        #region 权限路径 —— string AuthorityPath
        /// <summary>
        /// 权限路径
        /// </summary>
        public string AuthorityPath { get; private set; }
        #endregion

        #region 英文名称 —— string EnglishName
        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; private set; }
        #endregion

        #region 程序集名称 —— string AssemblyName
        /// <summary>
        /// 程序集名称
        /// </summary>
        public string AssemblyName { get; private set; }
        #endregion

        #region 命名空间 —— string Namespace
        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace { get; private set; }
        #endregion

        #region 类名 —— string ClassName
        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName { get; private set; }
        #endregion

        #region 方法名 —— string MethodName
        /// <summary>
        /// 方法名
        /// </summary>
        public string MethodName { get; private set; }
        #endregion

        #region 描述 —— string Description
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; private set; }
        #endregion

        #region 导航属性 - 菜单（叶子节点）集 —— ICollection<Menu> MenuLeaves
        /// <summary>
        /// 导航属性 - 菜单（叶子节点）集
        /// </summary>
        public virtual ICollection<Menu> MenuLeaves { get; private set; }
        #endregion

        #region 导航属性 - 角色集 —— ICollection<Role> Roles
        /// <summary>
        /// 导航属性 - 角色集
        /// </summary>
        public virtual ICollection<Role> Roles { get; private set; }
        #endregion

        #endregion

        #region # 方法

        #region 修改权限 —— void UpdateInfo(string authorityName...
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="authorityName">权限名称</param>
        /// <param name="authorityPath">权限路径</param>
        /// <param name="englishName">英文名称</param>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="namespace">命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="description">描述</param>
        public void UpdateInfo(string authorityName, string authorityPath, string englishName, string assemblyName, string @namespace, string className, string methodName, string description)
        {
            base.Name = authorityName;
            this.AuthorityPath = authorityPath;
            this.EnglishName = englishName;
            this.AssemblyName = assemblyName;
            this.Namespace = @namespace;
            this.ClassName = className;
            this.MethodName = methodName;
            this.Description = description;

            //初始化权限路径与关键字
            this.InitPath();
            this.InitKeywords();
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
                role.Authorities.Remove(this);
            }
        }
        #endregion

        #region 清空菜单关系 —— void ClearMenuRelations()
        /// <summary>
        /// 清空菜单关系
        /// </summary>
        public void ClearMenuRelations()
        {
            foreach (Menu menu in this.MenuLeaves.ToArray())
            {
                this.MenuLeaves.Remove(menu);
                menu.Authorities.Remove(this);
            }
        }
        #endregion

        #region 初始化权限路径 —— void InitPath()
        /// <summary>
        /// 初始化权限路径
        /// </summary>
        private void InitPath()
        {
            if (string.IsNullOrWhiteSpace(this.AuthorityPath))
            {
                StringBuilder pathBuilder = new StringBuilder("/");
                pathBuilder.Append(this.AssemblyName);
                pathBuilder.Append("/");
                pathBuilder.Append(this.Namespace);
                pathBuilder.Append("/");
                pathBuilder.Append(this.ClassName);
                pathBuilder.Append("/");
                pathBuilder.Append(this.MethodName);

                this.AuthorityPath = pathBuilder.ToString();
            }
        }
        #endregion

        #region 初始化关键字 —— void InitKeywords()
        /// <summary>
        /// 初始化关键字
        /// </summary>
        private void InitKeywords()
        {
            StringBuilder keywordsBuilder = new StringBuilder();
            keywordsBuilder.Append(this.Name);
            keywordsBuilder.Append(this.EnglishName);
            keywordsBuilder.Append(this.Description);
            keywordsBuilder.Append(this.AuthorityPath);

            base.SetKeywords(keywordsBuilder.ToString());
        }
        #endregion

        #endregion
    }
}
