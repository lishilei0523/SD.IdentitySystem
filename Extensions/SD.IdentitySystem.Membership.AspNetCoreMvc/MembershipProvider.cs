using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using SD.CacheManager;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using SD.Toolkits.OwinCore.Extensions;
using System;
using System.Text.Json;

// ReSharper disable once CheckNamespace
namespace SD.IdentitySystem
{
    /// <summary>
    /// ASP.NET Core MVC Membership提供者
    /// </summary>
    public class MembershipProvider : IMembershipProvider
    {
        /// <summary>
        /// 设置登录信息
        /// </summary>
        /// <param name="loginInfo">登录信息</param>
        public void SetLoginInfo(LoginInfo loginInfo)
        {
            HttpContext httpContext = OwinContextReader.Current;
            if (httpContext != null)
            {
                httpContext.Request.Headers.Add(SessionKey.PublicKey, new StringValues(loginInfo.PublicKey.ToString()));
                httpContext.Session.SetString(GlobalSetting.ApplicationId, JsonSerializer.Serialize(loginInfo));
            }
        }

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns>登录信息</returns>
        public LoginInfo GetLoginInfo()
        {
            HttpContext httpContext = OwinContextReader.Current;
            Guid? publicKey = null;
            if (httpContext != null && httpContext.Request.Headers.TryGetValue(SessionKey.PublicKey, out StringValues header))
            {
                publicKey = new Guid(header.ToString());
            }
            if (httpContext != null && httpContext.Session.IsAvailable && !publicKey.HasValue)
            {
                string loginInfoJson = httpContext.Session.GetString(GlobalSetting.ApplicationId);
                if (!string.IsNullOrWhiteSpace(loginInfoJson))
                {
                    LoginInfo loginInfoSession = JsonSerializer.Deserialize<LoginInfo>(loginInfoJson);
                    publicKey = loginInfoSession.PublicKey;
                }
            }

            if (publicKey.HasValue)
            {
                LoginInfo loginInfo = CacheMediator.Get<LoginInfo>(publicKey.ToString());

                return loginInfo;
            }

            return null;
        }
    }
}
