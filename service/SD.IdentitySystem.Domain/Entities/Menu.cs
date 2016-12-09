using System;
using System.Collections.Generic;
using System.Linq;
using SD.Toolkits.Recursion.Tree;
using ShSoft.Infrastructure.EntityBase;

namespace SD.IdentitySystem.Domain.Entities
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class Menu : AggregateRootEntity, ITree<Menu>
    {
        #region # 构造器

        #region 01.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected Menu()
        {
            //初始化导航属性
            this.SubNodes = new HashSet<Menu>();
            this.Authorities = new HashSet<Authority>();
        }
        #endregion

        #region 02.创建菜单构造器
        /// <summary>
        /// 创建菜单构造器
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="menuName">菜单名称</param>
        /// <param name="sort">菜单排序</param>
        /// <param name="url">链接地址</param>
        /// <param name="icon">图标</param>
        /// <param name="parentNode">上级菜单</param>
        public Menu(string systemNo, string menuName, int sort, string url, string icon, Menu parentNode)
            : this()
        {
            #region # 验证参数

            if (parentNode != null && parentNode.SystemNo != systemNo)
            {
                throw new InvalidOperationException("子级菜单的信息系统必须与父级菜单一致！");
            }

            #endregion

            base.Name = menuName;
            base.Sort = sort;
            this.SystemNo = systemNo;
            this.Url = url;
            this.Icon = icon;
            this.ParentNode = parentNode;
            this.IsRoot = parentNode == null;
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

        #region 链接地址 —— string Url
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { get; private set; }
        #endregion

        #region 图标 —— string Icon
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; private set; }
        #endregion

        #region 是否是根级节点 —— bool IsRoot
        /// <summary>
        /// 是否是根级节点
        /// </summary>
        public bool IsRoot { get; private set; }
        #endregion

        #region 只读属性 - 是否是叶子级节点 —— bool IsLeaf
        /// <summary>
        /// 只读属性 - 是否是叶子级节点
        /// </summary>
        public bool IsLeaf
        {
            get { return !this.SubNodes.Any(); }
        }
        #endregion

        #region 导航属性 - 父级菜单 —— Menu ParentNode
        /// <summary>
        /// 导航属性 - 父级菜单
        /// </summary>
        public virtual Menu ParentNode { get; private set; }
        #endregion

        #region 导航属性 - 子级菜单集 ——ICollection<Menu> SubNodes
        /// <summary>
        /// 导航属性 - 子级菜单集
        /// </summary>
        public virtual ICollection<Menu> SubNodes { get; private set; }
        #endregion

        #region 导航属性 - 权限集 —— ICollection<Authority> Authorities
        /// <summary>
        /// 导航属性 - 权限集
        /// </summary>
        public virtual ICollection<Authority> Authorities { get; private set; }
        #endregion

        #endregion

        #region # 方法

        #region 修改菜单信息 —— void UpdateInfo(string menuName, int sort, string url, string icon)
        /// <summary>
        /// 修改菜单信息
        /// </summary>
        /// <param name="menuName">菜单名称</param>
        /// <param name="sort">菜单排序</param>
        /// <param name="url">链接地址</param>
        /// <param name="icon">图标</param>
        public void UpdateInfo(string menuName, int sort, string url, string icon)
        {
            //验证参数
            base.CheckName(menuName);

            base.Name = menuName;
            base.Sort = sort;
            this.Url = url;
            this.Icon = icon;
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

        #endregion
    }
}