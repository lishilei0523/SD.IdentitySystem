namespace SD.Infrastructure.WPF.Interfaces
{
    /// <summary>
    /// 可分页接口
    /// </summary>
    public interface IPaginatable
    {
        #region 页码 —— int PageIndex
        /// <summary>
        /// 页码
        /// </summary>
        int PageIndex { get; set; }
        #endregion

        #region 页容量 —— int PageSize
        /// <summary>
        /// 页容量
        /// </summary>
        int PageSize { get; set; }
        #endregion

        #region 总记录数 —— int RowCount
        /// <summary>
        /// 总记录数
        /// </summary>
        int RowCount { get; set; }
        #endregion

        #region 总页数 —— int PageCount
        /// <summary>
        /// 总页数
        /// </summary>
        int PageCount { get; set; }
        #endregion 
    }
}
