using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using SD.CacheManager;
using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using SD.Toolkits.Owin.Core.Extensions;
using System;

// ReSharper disable once CheckNamespace
namespace SD.IdentitySystem
{
    /// <summary>
    /// Membership管理工具类
    /// </summary>
    public static class Membership
    {
        /// <summary>
        /// 当前登录信息
        /// </summary>
        public static LoginInfo LoginInfo
        {
            get
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
}
