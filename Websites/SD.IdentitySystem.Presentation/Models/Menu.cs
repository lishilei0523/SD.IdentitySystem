﻿using SD.IdentitySystem.Presentation.EasyUI;
using SD.Infrastructure.PresentationBase;
using System;
using System.Collections.Generic;

namespace SD.IdentitySystem.Presentation.Models
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class Menu : ModelBase, ITreeGrid<Menu>
    {
        #region 构造器
        /// <summary>
        /// 构造器
        /// </summary>
        public Menu()
        {
            this.children = new HashSet<Menu>();
        }
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

        #region 上级菜单Id —— Guid? ParentMenuId
        /// <summary>
        /// 上级菜单Id
        /// </summary>
        public Guid? ParentMenuId { get; set; }
        #endregion


        //Others

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

        #region 应用程序类型名称 —— string ApplicationTypeName
        /// <summary>
        /// 应用程序类型名称
        /// </summary>
        public string ApplicationTypeName { get; set; }
        #endregion

        #region 类型 —— string type
        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; }
        #endregion

        #region 导航属性 - 下级菜单集 —— ICollection<Menu> children
        /// <summary>
        /// 导航属性 - 下级菜单集
        /// </summary>
        public ICollection<Menu> children { get; set; }
        #endregion
    }
}
