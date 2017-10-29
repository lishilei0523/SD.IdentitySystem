using SD.IdentitySystem.IPresentation.ViewModels.Formats.EasyUI;
using SD.Infrastructure.MVC;
using System;
using System.Collections.Generic;

namespace SD.IdentitySystem.IPresentation.ViewModels.Outputs
{
    /// <summary>
    /// 菜单视图模型
    /// </summary>
    public class MenuView : ViewModel, ITreeGrid<MenuView>
    {
        #region 构造器
        /// <summary>
        /// 构造器
        /// </summary>
        public MenuView()
        {
            this.children = new HashSet<MenuView>();
        }
        #endregion

        #region 链接地址 —— string Url
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { get; set; }
        #endregion

        #region 图标 —— string Icon
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
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

        #region 排序 —— int Sort
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        #endregion

        #region 父级菜单Id —— Guid? ParentMenuId
        /// <summary>
        /// 父级菜单Id
        /// </summary>
        public Guid? ParentMenuId { get; set; }
        #endregion


        //Others

        #region 信息系统编号 —— string SystemNo
        /// <summary>
        /// 信息系统编号
        /// </summary>
        public string SystemNo { get; set; }
        #endregion

        #region 信息系统名称 —— string SystemName
        /// <summary>
        /// 信息系统名称
        /// </summary>
        public string SystemName { get; set; }
        #endregion

        #region 类型 —— string type
        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; }
        #endregion

        #region 导航属性 - 子级菜单集 —— ICollection<MenuView> children
        /// <summary>
        /// 导航属性 - 子级菜单集
        /// </summary>
        public ICollection<MenuView> children { get; set; }
        #endregion
    }
}
