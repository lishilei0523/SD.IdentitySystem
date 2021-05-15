namespace SD.Infrastructure.WPF.Interfaces
{
    /// <summary>
    /// 可加载接口
    /// </summary>
    public interface ILoadable
    {
        #region # 属性

        #region 是否正在加载 —— bool IsLoading
        /// <summary>
        /// 是否正在加载
        /// </summary>
        bool IsLoading { get; set; }
        #endregion

        #endregion
    }
}
