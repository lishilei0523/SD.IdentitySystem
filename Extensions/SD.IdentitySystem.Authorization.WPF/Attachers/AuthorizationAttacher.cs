using SD.Infrastructure.Constants;
using SD.Infrastructure.CustomExceptions;
using SD.Infrastructure.Membership;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SD.IdentitySystem.Authorization.WPF.Attachers
{
    /// <summary>
    /// 授权附加器
    /// </summary>
    public static class AuthorizationAttacher
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限路径依赖属性
        /// </summary>
        public static readonly DependencyProperty AuthorityPathProperty;

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
        public static DependencyProperty AuthorityPath
        {
            get => AuthorityPathProperty;
            set => AuthorityPathProperty = value;
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
            if (GlobalSetting.AuthorizationEnabled)
            {
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

                //从登录信息中取出权限列表
                string[] ownedAuthorityPaths = loginInfo.LoginAuthorityInfos.Select(x => x.Path).ToArray();

                //验证权限
                if (dependencyObject is UIElement uiElement)
                {
                    if (ownedAuthorityPaths.Contains(authorityPath))
                    {
                        uiElement.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        uiElement.Visibility = Visibility.Collapsed;
                    }
                }
                if (dependencyObject is DataGridTemplateColumn columnElement)
                {
                    if (ownedAuthorityPaths.Contains(authorityPath))
                    {
                        columnElement.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        columnElement.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }
        #endregion

        #endregion
    }
}
