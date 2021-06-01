using SD.Infrastructure.CustomExceptions;
using SD.Infrastructure.MemberShip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SD.IdentitySystem.Authorization.WPF.Attachers
{
    /// <summary>
    /// 授权附加器
    /// </summary>
    public static class AuthorizationAttacher
    {
        #region # 构造器

        /// <summary>
        /// 静态构造器
        /// </summary>
        static AuthorizationAttacher()
        {
            //注册依赖属性
            AuthorityPathProperty = DependencyProperty.RegisterAttached(nameof(AuthorityPath), typeof(string), typeof(AuthorizationAttacher), new PropertyMetadata(OnAuthorityPathChanged));
        }

        #endregion

        #region # 依赖属性

        #region 权限路径 —— DependencyProperty AuthorityPath

        /// <summary>
        /// 权限路径
        /// </summary>
        public static DependencyProperty AuthorityPathProperty;

        /// <summary>
        /// 权限路径
        /// </summary>
        public static DependencyProperty AuthorityPath
        {
            get { return AuthorityPathProperty; }
            set { AuthorityPathProperty = value; }
        }

        #endregion

        #endregion

        #region # Getter and Setter

        #region 获取权限路径 —— static bool GetAuthorityPath(DependencyObject...
        /// <summary>
        /// 获取权限路径
        /// </summary>
        public static string GetAuthorityPath(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(AuthorityPathProperty);
        }
        #endregion

        #region 设置权限路径 —— static void SetAuthorityPath(DependencyObject...
        /// <summary>
        /// 设置权限路径
        /// </summary>
        public static void SetAuthorityPath(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(AuthorityPathProperty, value);
        }
        #endregion

        #endregion

        #region # 回调方法

        #region 权限路径改变回调方法 —— static void OnAuthorityPathChanged(DependencyObject...
        /// <summary>
        /// 权限路径改变回调方法
        /// </summary>
        private static void OnAuthorityPathChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            UIElement element = (UIElement)dependencyObject;
            string authorityPath = eventArgs.NewValue?.ToString();
            LoginInfo loginInfo = MembershipMediator.GetLoginInfo();

            #region # 验证

            if (string.IsNullOrWhiteSpace(authorityPath))
            {
                throw new ArgumentNullException(nameof(AuthorityPath), "权限路径不可为空！");
            }
            if (loginInfo == null)
            {
                throw new NoPermissionException("当前登录信息为空，请重新登录！");
            }

            #endregion

            //从登录信息中取出权限集
            IEnumerable<string> ownedAuthorityPaths = loginInfo.LoginAuthorityInfos.Select(x => x.Path);

            //验证权限
            if (ownedAuthorityPaths.Contains(authorityPath))
            {
                element.Visibility = Visibility.Visible;
            }
            else
            {
                element.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #endregion
    }
}
