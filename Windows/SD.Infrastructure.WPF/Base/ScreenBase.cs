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
        /// <summary>
        /// 是否繁忙
        /// </summary>
        [DependencyProperty]
        public bool IsBusy { get; set; }
    }
}
