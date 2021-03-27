using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using SD.CacheManager;
using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using SD.Toolkits.Owin.Core.Extensions;
using System;

namespace SD.IdentitySystem.Membership.WebApiCore
{
    /// <summary>
    /// ASP.NET Core WebApi Membership提供者
    /// </summary>
    public class MembershipProvider : IMembershipProvider
    {
        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns>登录信息</returns>
        public LoginInfo GetLoginInfo()
        {
            HttpContext httpContext = OwinContextReader.Current;
            if (httpContext != null && httpContext.Request.Headers.TryGetValue(SessionKey.CurrentPublicKey, out StringValues header))
            {
                Guid publicKey = new Guid(header.ToString());
                LoginInfo loginInfo = CacheMediator.Get<LoginInfo>(publicKey.ToString());

                return loginInfo;
            }

            return null;
        }
    }
}
