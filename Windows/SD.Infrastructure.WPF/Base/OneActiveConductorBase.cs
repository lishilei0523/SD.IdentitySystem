using Caliburn.Micro;
using SD.Infrastructure.WPF.Aspects;
using SD.Infrastructure.WPF.Interfaces;

namespace SD.Infrastructure.WPF.Base
{
    /// <summary>
    /// 单活动Conductor基类
    /// </summary>
    public class OneActiveConductorBase : Conductor<IScreen>.Collection.OneActive, IBusy
    {
        /// <summary>
        /// 是否繁忙
        /// </summary>
        [DependencyProperty]
        public bool IsBusy { get; set; }
    }
}
