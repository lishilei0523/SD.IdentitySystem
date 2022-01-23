using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using System;

// ReSharper disable once CheckNamespace
namespace SD.IdentitySystem
{
    /// <summary>
    /// Xamarin Membership提供者
    /// </summary>
    public class MembershipProvider : IMembershipProvider
    {
        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns>登录信息</returns>
        public LoginInfo GetLoginInfo()
        {
            object loginInfo = AppDomain.CurrentDomain.GetData(SessionKey.CurrentUser);

            return loginInfo as LoginInfo;
        }
    }
}
