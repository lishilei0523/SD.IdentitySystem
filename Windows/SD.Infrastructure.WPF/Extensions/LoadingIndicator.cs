using System;
using System.ComponentModel;

namespace SD.Infrastructure.WPF.Extensions
{
    /// <summary>
    /// 加载指示器
    /// </summary>
    public static class LoadingIndicator
    {
        #region # 事件与属性

        /// <summary>
        /// 属性值变更事件
        /// </summary>
        public static event EventHandler<PropertyChangedEventArgs> IsBusyChanged;

        /// <summary>
        /// 是否繁忙
        /// </summary>
        [ThreadStatic]
        private static bool _IsBusy;

        /// <summary>
        /// 是否繁忙
        /// </summary>
        public static bool IsBusy
        {
            get { return LoadingIndicator._IsBusy; }
            set { LoadingIndicator._IsBusy = value; LoadingIndicator.IsBusyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(LoadingIndicator.IsBusy))); }
        }

        #endregion

        #region # 方法

        /// <summary>
        /// 挂起加载指示器
        /// </summary>
        public static void Suspend()
        {
            LoadingIndicator.IsBusy = true;
        }

        /// <summary>
        /// 释放加载指示器
        /// </summary>
        public static void Dispose()
        {
            LoadingIndicator.IsBusy = false;
        }

        #endregion
    }
}
