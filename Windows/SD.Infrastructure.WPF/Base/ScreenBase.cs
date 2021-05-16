using Caliburn.Micro;
using SD.Infrastructure.WPF.Aspects;
using SD.Infrastructure.WPF.Interfaces;

namespace SD.Infrastructure.WPF.Base
{
    /// <summary>
    /// Screen基类
    /// </summary>
    public class ScreenBase : Screen, IBusy
    {
        #region 是否繁忙 —— bool IsBusy
        /// <summary>
        /// 是否繁忙
        /// </summary>
        [DependencyProperty]
        public bool IsBusy { get; set; }
        #endregion
    }
}
