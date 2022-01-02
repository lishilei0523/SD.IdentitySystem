using SD.Infrastructure.Constants;
using SD.Infrastructure.CustomExceptions;
using SD.Infrastructure.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace SD.IdentitySystem.Authorization.Xamarin.Attachers
{
    /// <summary>
    /// 授权附加器
    /// </summary>
    public class AuthorizationAttacher
    {
        #region # 构造器

        /// <summary>
        /// 静态构造器
        /// </summary>
        static AuthorizationAttacher()
        {
            //注册依赖属性
            AuthorityPathProperty = BindableProperty.CreateAttached("AuthorityPath", typeof(string), typeof(AuthorizationAttacher), null, BindingMode.TwoWay, null, OnAuthorityPathChanged);
        }

        #endregion

        #region # 依赖属性

        #region 权限路径 —— BindableProperty AuthorityPathProperty
        /// <summary>
        /// 权限路径
        /// </summary>
        public static readonly BindableProperty AuthorityPathProperty;
        #endregion

        #endregion

        #region # Getter and Setter

        #region 获取权限路径 —— static string GetAuthorityPath(BindableObject...
        /// <summary>
        /// 获取权限路径
        /// </summary>
        public static string GetAuthorityPath(BindableObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(AuthorityPathProperty);
        }
        #endregion

        #region 设置权限路径 —— static void SetAuthorityPath(BindableObject...
        /// <summary>
        /// 设置权限路径
        /// </summary>
        public static void SetAuthorityPath(BindableObject dependencyObject, string value)
        {
            dependencyObject.SetValue(AuthorityPathProperty, value);
        }
        #endregion

        #endregion

        #region # 回调方法

        #region 权限路径改变回调方法 —— static void OnAuthorityPathChanged(BindableObject...
        /// <summary>
        /// 权限路径改变回调方法
        /// </summary>
        private static void OnAuthorityPathChanged(BindableObject dependencyObject, object oldValue, object newValue)
        {
            if (GlobalSetting.AuthorizationEnabled)
            {
                VisualElement element = (VisualElement)dependencyObject;
                string authorityPath = newValue?.ToString();
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
                    element.IsVisible = true;
                }
                else
                {
                    element.IsVisible = false;
                }
            }
        }
        #endregion

        #endregion
    }
}
