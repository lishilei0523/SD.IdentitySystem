using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using System;

// ReSharper disable once CheckNamespace
namespace SD.IdentitySystem
{
    /// <summary>
    /// Windows Membership提供者
    /// </summary>
    public class MembershipProvider : IMembershipProvider
    {
        /// <summary>
        /// 设置登录信息
        /// </summary>
        /// <param name="loginInfo">登录信息</param>
        public void SetLoginInfo(LoginInfo loginInfo)
        {
            AppDomain.CurrentDomain.SetData(GlobalSetting.ApplicationId, loginInfo);
        }

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns>登录信息</returns>
        public LoginInfo GetLoginInfo()
        {
            object loginInfo = AppDomain.CurrentDomain.GetData(GlobalSetting.ApplicationId);

            return loginInfo as LoginInfo;
        }
    }
}
