﻿using Microsoft.AspNetCore.Http;
using SD.CacheManager;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using SD.Toolkits.AspNet;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SD.IdentitySystem.SignalR.Authentication.Middlewares
{
    /// <summary>
    /// 身份认证中间件
    /// </summary>
    public class AuthenticationMiddleware
    {
        /// <summary>
        /// 请求委托
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public AuthenticationMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        /// <summary>
        /// 执行中间件
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            if (AspNetSetting.Authorized)
            {
                //读Header
                string publicKey = context.Request.Headers[SessionKey.PublicKey];
                if (string.IsNullOrWhiteSpace(publicKey))
                {
                    //读QueryString
                    publicKey = context.Request.Query[SessionKey.PublicKey];
                }
                if (string.IsNullOrWhiteSpace(publicKey))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Response.Headers.Append("ErrorMessage", "Public key not found");

                    await this._next.Invoke(context);
                }
                else
                {
                    LoginInfo loginInfo = CacheMediator.Get<LoginInfo>(publicKey);
                    if (loginInfo == null)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Response.Headers.Append("ErrorMessage", "Login info expired");

                        await this._next.Invoke(context);
                    }
                    else
                    {
                        IIdentity identity = new GenericIdentity(loginInfo.LoginId);
                        context.User = new GenericPrincipal(identity, null);

                        await this._next.Invoke(context);
                    }
                }
            }
            else
            {
                await this._next.Invoke(context);
            }
        }
    }
}
