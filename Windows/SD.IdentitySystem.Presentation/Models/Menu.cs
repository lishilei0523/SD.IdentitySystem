using SD.Infrastructure.Constants;
using SD.Infrastructure.PresentationBase;
using SD.Toolkits.Recursion.Tree;
using System;
using System.Collections.Generic;

namespace SD.IdentitySystem.Presentation.Models
{
    /// <summary>
    /// 菜单模型
    /// </summary>
    public class Menu : Model, ITree<Menu>
    {
        #region 构造器
        /// <summary>
        /// 构造器
        /// </summary>
        public Menu()
        {
            //初始化导航属性
            this.SubNodes = new HashSet<Menu>();

            //默认值
            this.IsChecked = false;
        }
        #endregion

        #region 信息系统编号 —— string InfoSystemNo
        /// <summary>
        /// 信息系统编号
        /// </summary>
        public string InfoSystemNo { get; set; }
        #endregion

        #region 信息系统名称 —— string InfoSystemName
        /// <summary>
        /// 信息系统名称
        /// </summary>
        public string InfoSystemName { get; set; }
        #endregion

        #region 应用程序类型 —— ApplicationType ApplicationType
        /// <summary>
        /// 应用程序类型
        /// </summary>
        public ApplicationType ApplicationType { get; set; }
        #endregion

        #region 链接地址 —— string Url
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { get; set; }
        #endregion

        #region 路径 —— string Path
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        #endregion

        #region 图标 —— string Icon
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        #endregion

        #region 排序 —— int Sort
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        #endregion

        #region 是否是根级节点 —— bool IsRoot
        /// <summary>
        /// 是否是根级节点
        /// </summary>
        public bool IsRoot { get; set; }
        #endregion

        #region 是否是叶子级节点 —— bool IsLeaf
        /// <summary>
        /// 是否是叶子级节点
        /// </summary>

        public bool IsLeaf { get; set; }
        #endregion

        #region 上级菜单Id —— Guid? ParentMenuId
        /// <summary>
        /// 上级菜单Id
        /// </summary>
        public Guid? ParentMenuId { get; set; }
        #endregion


        //导航属性

        #region 导航属性 - 上级节点 —— Menu ParentNode
        /// <summary>
        /// 导航属性 - 上级节点
        /// </summary>
        public Menu ParentNode { get; set; }
        #endregion

        #region 导航属性 - 下级节点集 —— ICollection<Menu> SubNodes
        /// <summary>
        /// 导航属性 - 下级节点集
        /// </summary>
        public ICollection<Menu> SubNodes { get; set; }
        #endregion
    }
}
