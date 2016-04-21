using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShSoft.Framework2016.Infrastructure.IEntity;

namespace SD.UAC.Domain.Entities
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Authority : PlainEntity
    {
        #region # 构造器

        #region 01.无参构造器
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

        #region 02.创建权限构造器
        /// <summary>
        /// 创建权限构造器
        /// </summary>
        /// <param name="authorityName">权限名称</param>
        /// <param name="englishName">英文名称</param>
        /// <param name="description">描述</param>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="namespace">命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        public Authority(string authorityName, string englishName, string description, string assemblyName, string @namespace, string className, string methodName)
            : this()
        {
            //验证参数
            this.CheckBasicInfo(assemblyName, @namespace, className, methodName);

            base.Name = authorityName;
            this.EnglishName = englishName;
            this.Description = description;
            this.AssemblyName = assemblyName;
            this.Namespace = @namespace;
            this.ClassName = className;
            this.MethodName = methodName;

            //初始化权限路径与关键字
            this.InitPath();
            this.InitKeywords();
        }
        #endregion

        #endregion

        #region # 属性

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

        #region 英文名 —— string EnglishName
        /// <summary>
        /// 英文名
        /// </summary>
        public string EnglishName { get; private set; }
        #endregion

        #region 权限描述 —— string Description
        /// <summary>
        /// 权限描述
        /// </summary>
        public string Description { get; private set; }
        #endregion

        #region 权限路径 —— string AuthorityPath
        /// <summary>
        /// 权限路径
        /// </summary>
        public string AuthorityPath { get; private set; }
        #endregion

        #region 导航属性 - 菜单（叶子节点） —— ICollection<Menu> MenuLeaves
        /// <summary>
        /// 导航属性 - 菜单（叶子节点）
        /// </summary>
        public virtual ICollection<Menu> MenuLeaves { get; private set; }
        #endregion

        #region 导航属性 - 信息系统类别 —— InfoSystemKind InfoSystemKind
        /// <summary>
        /// 导航属性 - 信息系统类别
        /// </summary>
        public virtual InfoSystemKind InfoSystemKind { get; private set; }
        #endregion

        #region 导航属性 - 角色集 —— ICollection<Role> Roles
        /// <summary>
        /// 导航属性 - 角色集
        /// </summary>
        public virtual ICollection<Role> Roles { get; private set; }
        #endregion

        #endregion

        #region # 方法

        #region 修改权限信息 —— void UpdateInfo(string authorityName, string englishName...
        /// <summary>
        /// 修改权限信息
        /// </summary>
        /// <param name="authorityName">权限名称</param>
        /// <param name="englishName">英文名称</param>
        /// <param name="description">描述</param>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="namespace">命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        public void UpdateInfo(string authorityName, string englishName, string description, string assemblyName, string @namespace, string className, string methodName)
        {
            //验证参数
            this.CheckBasicInfo(assemblyName, @namespace, className, methodName);

            base.Name = authorityName;
            this.EnglishName = englishName;
            this.Description = description;
            this.AssemblyName = assemblyName;
            this.Namespace = @namespace;
            this.ClassName = className;
            this.MethodName = methodName;

            //初始化权限路径与关键字
            this.InitPath();
            this.InitKeywords();
        }
        #endregion

        #region 设置菜单 —— void AppendMenu(Menu menuLeaf)
        /// <summary>
        /// 设置菜单
        /// </summary>
        /// <param name="menuLeaf">菜单（叶子节点）</param>
        public void AppendMenu(Menu menuLeaf)
        {
            #region # 验证参数

            if (menuLeaf == null)
            {
                throw new ArgumentNullException("menuLeaf", @"菜单不可为空！");
            }
            if (!menuLeaf.IsLeaf)
            {
                throw new ArgumentOutOfRangeException("menuLeaf", @"菜单必须是叶子节点！");
            }

            #endregion

            this.MenuLeaves.Add(menuLeaf);
        }
        #endregion

        #region 清空角色集 —— void RemoveRoles()
        /// <summary>
        /// 清空角色集
        /// </summary>
        public void RemoveRoles()
        {
            foreach (Role role in this.Roles.ToArray())
            {
                this.Roles.Remove(role);
            }
        }
        #endregion

        #region 初始化权限路径 —— void InitPath()
        /// <summary>
        /// 初始化权限路径
        /// </summary>
        private void InitPath()
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

        #region 验证基本信息 —— void CheckBasicInfo(string assemblyName, string @namespace...
        /// <summary>
        /// 验证基本信息
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="namespace">命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        private void CheckBasicInfo(string assemblyName, string @namespace, string className, string methodName)
        {
            if (string.IsNullOrWhiteSpace(assemblyName))
            {
                throw new ArgumentNullException("assemblyName", @"程序集名称不可为空！");
            }
            if (string.IsNullOrWhiteSpace(@namespace))
            {
                throw new ArgumentNullException("namespace", @"命名空间不可为空！");
            }
            if (string.IsNullOrWhiteSpace(className))
            {
                throw new ArgumentNullException("className", @"类名不可为空！");
            }
            if (string.IsNullOrWhiteSpace(methodName))
            {
                throw new ArgumentNullException("methodName", @"方法名不可为空！");
            }
        }
        #endregion

        #endregion
    }
}
