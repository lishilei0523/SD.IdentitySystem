using SD.Infrastructure.WPF.Interfaces;

namespace SD.Infrastructure.WPF.Extensions
{
    /// <summary>
    /// 繁忙指示器
    /// </summary>
    public static class BusyIndicator
    {
        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object _Sync = new object();

        /// <summary>
        /// 挂起加载指示器
        /// </summary>
        public static void Busy(this IBusy loadable)
        {
            lock (_Sync)
            {
                loadable.IsBusy = true;
            }
        }

        /// <summary>
        /// 释放加载指示器
        /// </summary>
        public static void Idle(this IBusy loadable)
        {
            lock (_Sync)
            {
                loadable.IsBusy = false;
            }
        }
    }
}
