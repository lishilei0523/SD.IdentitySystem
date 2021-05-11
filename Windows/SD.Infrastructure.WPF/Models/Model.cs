namespace SD.Infrastructure.WPF.Models
{
    /// <summary>
    /// 模型
    /// </summary>
    public class Model<T>
    {
        #region 是否选中 —— bool? IsSelected
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool? IsSelected { get; set; }
        #endregion

        #region 是否勾选 —— bool? IsChecked
        /// <summary>
        /// 是否勾选
        /// </summary>
        public bool? IsChecked { get; set; }
        #endregion

        #region 数据模型 —— T Data
        /// <summary>
        /// 数据模型
        /// </summary>
        public T Data { get; set; }
        #endregion
    }
}
