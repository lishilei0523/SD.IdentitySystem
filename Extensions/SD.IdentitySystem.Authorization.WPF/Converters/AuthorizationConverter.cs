using SD.Infrastructure.CustomExceptions;
using SD.Infrastructure.MemberShip;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace SD.IdentitySystem.Authorization.WPF.Converters
{
    /// <summary>
    /// 授权转换器
    /// </summary>
    public class AuthorizationConverter : IValueConverter
    {
        /// <summary>
        /// 转换元素可见性
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FrameworkElement element = (FrameworkElement)value;
            string authorityPath = element?.Tag?.ToString();
            LoginInfo loginInfo = MembershipMediator.GetLoginInfo();

            #region # 验证

            if (string.IsNullOrWhiteSpace(authorityPath))
            {
                throw new ArgumentNullException(nameof(FrameworkElement.Tag), "权限路径不可为空！");
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
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
