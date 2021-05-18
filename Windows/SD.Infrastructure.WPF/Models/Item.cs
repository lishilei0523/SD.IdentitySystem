using Caliburn.Micro;
using SD.Infrastructure.WPF.Aspects;
using System;

namespace SD.Infrastructure.WPF.Models
{
    /// <summary>
    /// 数据项
    /// </summary>
    public class Item : PropertyChangedBase
    {
        #region # 字段及构造器

        #region 00.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        public Item()
        {
            //默认值
            this.IsSelected = false;
            this.IsChecked = false;
        }
        #endregion

        #region 01.创建数据项构造器
        /// <summary>
        /// 创建数据项构造器
        /// </summary>
        /// <param name="id">标识Id</param>
        /// <param name="name">名称</param>
        /// <param name="isSelected">是否选中</param>
        /// <param name="isChecked">是否勾选</param>
        /// <param name="groupKey">分组键</param>
        public Item(Guid id, string name, bool? isSelected, bool? isChecked, string groupKey = null)
            : this()
        {
            this.Id = id;
            this.Name = name;
            this.IsSelected = isSelected;
            this.IsChecked = isChecked;
            this.GroupKey = groupKey;
        }
        #endregion

        #endregion

        #region # 属性

        #region 标识Id —— Guid Id
        /// <summary>
        /// 标识Id
        /// </summary>
        public Guid Id { get; set; }
        #endregion

        #region 名称 —— string Name
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region 是否选中 —— bool? IsSelected
        /// <summary>
        /// 是否选中
        /// </summary>
        [DependencyProperty]
        public bool? IsSelected { get; set; }
        #endregion

        #region 是否勾选 —— bool? IsChecked
        /// <summary>
        /// 是否勾选
        /// </summary>
        [DependencyProperty]
        public bool? IsChecked { get; set; }
        #endregion

        #region 图标 —— string Icon
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        #endregion

        #region 分组键 —— string GroupKey
        /// <summary>
        /// 分组键
        /// </summary>
        public string GroupKey { get; set; }
        #endregion

        #endregion
    }
}
