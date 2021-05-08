using System.Windows;
using System.Windows.Controls;

namespace SD.Infrastructure.WPF.Extensions
{
    /// <summary>
    /// 密码扩展
    /// </summary>
    public static class PasswordExtension
    {
        #region # 构造器

        /// <summary>
        /// 静态构造器
        /// </summary>
        static PasswordExtension()
        {
            //注册依赖属性
            _Password = DependencyProperty.RegisterAttached(nameof(Password), typeof(string), typeof(PasswordExtension), new FrameworkPropertyMetadata(string.Empty, OnPasswordChanged));
            _IsAttached = DependencyProperty.RegisterAttached(nameof(IsAttached), typeof(bool), typeof(PasswordExtension), new PropertyMetadata(false, PasswordExtension.OnAttachedChanged));
            _IsUpdating = DependencyProperty.RegisterAttached(nameof(IsUpdating), typeof(bool), typeof(PasswordExtension));
        }

        #endregion

        #region # 依赖属性

        #region 密码 —— DependencyProperty Password

        /// <summary>
        /// 密码依赖属性
        /// </summary>
        private static readonly DependencyProperty _Password;

        /// <summary>
        /// 密码
        /// </summary>
        public static DependencyProperty Password
        {
            get { return _Password; }
        }

        #endregion

        #region 是否已附加 —— DependencyProperty IsAttached

        /// <summary>
        /// 是否已附加依赖属性
        /// </summary>
        private static readonly DependencyProperty _IsAttached;

        /// <summary>
        /// 是否已附加
        /// </summary>
        public static DependencyProperty IsAttached
        {
            get { return _IsAttached; }
        }

        #endregion

        #region 是否正在更新 —— DependencyProperty IsUpdating

        /// <summary>
        /// 是否正在更新依赖属性
        /// </summary>
        private static readonly DependencyProperty _IsUpdating;

        /// <summary>
        /// 是否正在更新
        /// </summary>
        public static DependencyProperty IsUpdating
        {
            get { return _IsUpdating; }
        }

        #endregion

        #endregion

        #region # Getters and Setters

        #region 获取密码 —— static string GetPassword(DependencyObject dependencyObject)
        /// <summary>
        /// 获取密码
        /// </summary>
        public static string GetPassword(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(_Password);
        }
        #endregion

        #region 设置密码 —— static void SetPassword(DependencyObject dependencyObject...
        /// <summary>
        /// 设置密码
        /// </summary>
        public static void SetPassword(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(_Password, value);
        }
        #endregion

        #region 获取是否已附加 —— static bool GetIsAttached(DependencyObject dependencyObject)
        /// <summary>
        /// 获取是否已附加
        /// </summary>
        public static bool GetIsAttached(DependencyObject dependencyObject)
        {
            return (bool)dependencyObject.GetValue(_IsAttached);
        }
        #endregion

        #region 设置是否已附加 —— static void SetIsAttached(DependencyObject dependencyObject...
        /// <summary>
        /// 设置是否已附加
        /// </summary>
        public static void SetIsAttached(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(_IsAttached, value);
        }
        #endregion

        #region 获取是否正在更新 —— static bool GetIsUpdating(DependencyObject dependencyObject)
        /// <summary>
        /// 获取是否正在更新
        /// </summary>
        private static bool GetIsUpdating(DependencyObject dependencyObject)
        {
            return (bool)dependencyObject.GetValue(_IsUpdating);
        }
        #endregion

        #region 设置是否正在更新 —— static void SetIsUpdating(DependencyObject dependencyObject...
        /// <summary>
        /// 设置是否正在更新
        /// </summary>
        private static void SetIsUpdating(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(_IsUpdating, value);
        }
        #endregion

        #endregion

        #region # 回调方法

        #region 密码改变回调方法 —— static void OnPasswordChanged(DependencyObject sender...
        /// <summary>
        /// 密码改变回调方法
        /// </summary>
        private static void OnPasswordChanged(DependencyObject sender, DependencyPropertyChangedEventArgs eventArgs)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            passwordBox.PasswordChanged -= PasswordChangedEventHandler;

            if (!GetIsUpdating(passwordBox))
            {
                passwordBox.Password = eventArgs.NewValue?.ToString() ?? string.Empty;
            }

            passwordBox.PasswordChanged += PasswordChangedEventHandler;
        }
        #endregion

        #region 是否已附加改变回调方法 —— static void OnAttachedChanged(DependencyObject sender...
        /// <summary>
        /// 是否已附加改变回调方法
        /// </summary>
        private static void OnAttachedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs eventArgs)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            if ((bool)eventArgs.OldValue)
            {
                passwordBox.PasswordChanged -= PasswordChangedEventHandler;
            }
            if ((bool)eventArgs.NewValue)
            {
                passwordBox.PasswordChanged += PasswordChangedEventHandler;
            }
        }
        #endregion

        #endregion

        #region # 事件处理程序

        #region 密码改变事件处理程序 —— static void PasswordChangedEventHandler(object sender...
        /// <summary>
        /// 密码改变事件处理程序
        /// </summary>
        private static void PasswordChangedEventHandler(object sender, RoutedEventArgs eventArgs)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            SetIsUpdating(passwordBox, true);
            SetPassword(passwordBox, passwordBox.Password);
            SetIsUpdating(passwordBox, false);
        }
        #endregion 

        #endregion
    }
}
