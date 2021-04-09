using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using System;

namespace SD.IdentitySystem.Membership.Windows
{
    /// <summary>
    /// Windows Membership提供者
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
