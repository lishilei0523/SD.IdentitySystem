using Microsoft.Owin;
using SD.CacheManager;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using SD.Toolkits.Owin.Extensions;
using System;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace SD.IdentitySystem
{
    /// <summary>
    /// OWIN Membership提供者
    /// </summary>
    public class MembershipProvider : IMembershipProvider
    {
        /// <summary>
        /// 设置登录信息
        /// </summary>
        /// <param name="loginInfo">登录信息</param>
        public void SetLoginInfo(LoginInfo loginInfo)
        {
            IOwinContext httpContext = OwinContextReader.Current;
            if (httpContext != null)
            {
                httpContext.Request.Headers.Add(SessionKey.PublicKey, new[] { loginInfo.PublicKey.ToString() });
            }
        }

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns>登录信息</returns>
        public LoginInfo GetLoginInfo()
        {
            IOwinContext httpContext = OwinContextReader.Current;
            if (httpContext != null && httpContext.Request.Headers.TryGetValue(SessionKey.PublicKey, out string[] headers))
            {
                Guid publicKey = new Guid(headers.Single());
                LoginInfo loginInfo = CacheMediator.Get<LoginInfo>(publicKey.ToString());

                return loginInfo;
            }

            return null;
        }
    }
}
