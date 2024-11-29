﻿using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SD.CacheManager;
using SD.Infrastructure.Constants;
using SD.Infrastructure.CustomExceptions;
using SD.Infrastructure.Membership;
using SD.Toolkits.AspNet;
using SD.Toolkits.OwinCore.Extensions;
using System;
using System.Threading.Tasks;

namespace SD.IdentitySystem.Grpc.Authentication.Interceptors
{
    /// <summary>
    /// gRPC身份认证拦截器
    /// </summary>
    public class AuthenticationInterceptor : Interceptor
    {
        /// <summary>
        /// 拦截服务请求
        /// </summary>
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            Endpoint endpoint = OwinContextReader.Current.GetEndpoint();

            #region # 验证

            if (endpoint == null)
            {
                return await continuation.Invoke(request, context);
            }

            #endregion

            bool needAuthorize = AspNetSetting.Authorized;
            bool allowAnonymous = endpoint.Metadata.GetMetadata<AllowAnonymousAttribute>() != null;
            if (needAuthorize && !allowAnonymous)
            {
                string headerKey = SessionKey.PublicKey.ToLower();
                Metadata.Entry headerEntry = context.RequestHeaders.Get(headerKey);
                string publicKey = headerEntry?.Value;
                if (string.IsNullOrWhiteSpace(publicKey))
                {
                    const string message = "身份认证消息头不存在，请检查程序！";
                    NoPermissionException innerException = new NoPermissionException(message);
                    Status status = new Status(StatusCode.Unauthenticated, message, innerException);
                    throw new RpcException(status);
                }

                LoginInfo loginInfo = CacheMediator.Get<LoginInfo>(publicKey);
                if (loginInfo == null)
                {
                    const string message = "身份过期，请重新登录！";
                    NoPermissionException innerException = new NoPermissionException(message);
                    Status status = new Status(StatusCode.Unauthenticated, message, innerException);
                    throw new RpcException(status);
                }

                //通过后，重新设置缓存过期时间
                CacheMediator.Set(publicKey, loginInfo, DateTime.Now.AddMinutes(GlobalSetting.AuthenticationTimeout));
            }

            return await continuation.Invoke(request, context);
        }
    }
}
