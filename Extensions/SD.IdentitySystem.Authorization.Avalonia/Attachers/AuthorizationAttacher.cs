using Avalonia;
using Avalonia.Data;
using SD.Infrastructure.Constants;
using SD.Infrastructure.CustomExceptions;
using SD.Infrastructure.Membership;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Authorization.Avalonia.Attachers
{
    /// <summary>
    /// 授权附加器
    /// </summary>
    public class AuthorizationAttacher
    {
        #region # 构造器

        /// <summary>
        /// 权限路径依赖属性
        /// </summary>
        public static readonly AttachedProperty<string> AuthorityPathProperty;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static AuthorizationAttacher()
        {
            //注册依赖属性
            AuthorityPathProperty = AvaloniaProperty.RegisterAttached<AuthorizationAttacher, Visual, string>("AuthorityPath", null, false, BindingMode.TwoWay);
            AuthorityPathProperty.Changed.AddClassHandler<Visual>(OnAuthorityPathChanged);
        }

        #endregion

        #region # Getter and Setter

        #region 获取权限路径 —— static string GetAuthorityPath(AvaloniaObject...
        /// <summary>
        /// 获取权限路径
        /// </summary>
        public static string GetAuthorityPath(AvaloniaObject dependencyObject)
        {
            return dependencyObject.GetValue(AuthorityPathProperty);
        }
        #endregion

        #region 设置权限路径 —— static void SetAuthorityPath(AvaloniaObject...
        /// <summary>
        /// 设置权限路径
        /// </summary>
        public static void SetAuthorityPath(AvaloniaObject dependencyObject, string value)
        {
            dependencyObject.SetValue(AuthorityPathProperty, value);
        }
        #endregion

        #endregion

        #region # 回调方法

        #region 权限路径改变回调方法 —— static void OnAuthorityPathChanged(Visual...
        /// <summary>
        /// 权限路径改变回调方法
        /// </summary>
        private static void OnAuthorityPathChanged(Visual visual, AvaloniaPropertyChangedEventArgs eventArgs)
        {
            if (GlobalSetting.AuthorizationEnabled)
            {
                string authorityPath = eventArgs.NewValue?.ToString();
                LoginInfo loginInfo = MembershipMediator.GetLoginInfo();

                #region # 验证

                if (string.IsNullOrWhiteSpace(authorityPath))
                {
                    throw new NullReferenceException("权限路径不可为空！");
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
                    visual.IsVisible = true;
                }
                else
                {
                    visual.IsVisible = false;
                }
            }
        }
        #endregion

        #endregion
    }
}
