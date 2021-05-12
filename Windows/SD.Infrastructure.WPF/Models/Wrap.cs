using Caliburn.Micro;
using SD.Infrastructure.WPF.Aspects;

namespace SD.Infrastructure.WPF.Models
{
    /// <summary>
    /// 包裹模型
    /// </summary>
    public class Wrap<T> : PropertyChangedBase
    {
        #region # 构造器

        #region 00.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        public Wrap()
        {
            //默认值
            this.IsSelected = false;
            this.IsChecked = false;
        }
        #endregion

        #region 01.创建包裹模型构造器
        /// <summary>
        /// 创建包裹模型构造器
        /// </summary>
        /// <param name="model">数据模型</param>
        public Wrap(T model)
            : this()
        {
            this.Model = model;
        }
        #endregion

        #region 02.创建包裹模型构造器
        /// <summary>
        /// 创建包裹模型构造器
        /// </summary>
        /// <param name="isSelected">是否选中</param>
        /// <param name="isChecked">是否勾选</param>
        /// <param name="model">数据模型</param>
        public Wrap(bool? isSelected, bool? isChecked, T model)
        {
            this.IsSelected = isSelected;
            this.IsChecked = isChecked;
            this.Model = model;
        }
        #endregion

        #endregion

        #region # 属性

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

        #region 数据模型 —— T Model
        /// <summary>
        /// 数据模型
        /// </summary>
        public T Model { get; set; }
        #endregion 

        #endregion
    }
}
