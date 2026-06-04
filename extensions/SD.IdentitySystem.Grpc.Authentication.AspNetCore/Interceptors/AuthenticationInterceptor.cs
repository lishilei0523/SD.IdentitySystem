using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using SD.Toolkits.AspNetCore.Extensions;
using SD.Toolkits.Grpc.Client.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace SD.IdentitySystem.Grpc.Authentication
{
    /// <summary>
    /// gRPC/ASP.NET Core客户端身份认证拦截器
    /// </summary>
    public class AuthenticationInterceptor : IAuthInterceptor
    {
        /// <summary>
        /// 身份认证拦截
        /// </summary>
        public Task AuthIntercept(AuthInterceptorContext context, Metadata metadata)
        {
            //ASP.NET Core获取公钥处理
            HttpContext httpContext = OwinContextReader.Current;
            Guid? publicKey = null;
            if (httpContext != null && httpContext.Request.Headers.TryGetValue(SessionKey.PublicKey, out StringValues stringValues))
            {
                publicKey = new Guid(stringValues.ToString());
            }
            if (httpContext != null && !publicKey.HasValue)
            {
                string loginInfoJson = httpContext.Session.GetString(GlobalSetting.ApplicationId);
                if (!string.IsNullOrWhiteSpace(loginInfoJson))
                {
                    LoginInfo loginInfo = JsonSerializer.Deserialize<LoginInfo>(loginInfoJson);
                    publicKey = loginInfo.PublicKey;
                }
            }
            if (publicKey.HasValue)
            {
                //添加登录信息元数据
                metadata.Add(SessionKey.PublicKey, publicKey.ToString());
            }

            return Task.CompletedTask;
        }
    }
}
