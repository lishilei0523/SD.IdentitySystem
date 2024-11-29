using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SD.CacheManager;
using SD.Infrastructure.Constants;
using SD.Infrastructure.CustomExceptions;
using SD.Infrastructure.Membership;
using SD.Toolkits.AspNet;
using System;
using System.Threading.Tasks;

namespace SD.IdentitySystem.Grpc.Authentication.Interceptors
{
    /// <summary>
    /// gRPC身份认证拦截器
    /// </summary>
    public class AuthenticationInterceptor : Interceptor
    {
        #region # 拦截Unary服务处理 —— override Task<TResponse> UnaryServerHandler(TRequest request...
        /// <summary>
        /// 拦截Unary服务处理
        /// </summary>
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            Authenticate(context);

            return await continuation.Invoke(request, context);
        }
        #endregion

        #region # 拦截客户端Streaming服务处理 —— override Task<TResponse> ClientStreamingServerHandler(...
        /// <summary>
        /// 拦截客户端Streaming服务处理
        /// </summary>
        public override async Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, ServerCallContext context, ClientStreamingServerMethod<TRequest, TResponse> continuation)
        {
            Authenticate(context);

            return await continuation.Invoke(requestStream, context);
        }
        #endregion

        #region # 拦截服务端Streaming服务处理 —— override Task ServerStreamingServerHandler(TRequest request...
        /// <summary>
        /// 拦截服务端Streaming服务处理
        /// </summary>
        public override async Task ServerStreamingServerHandler<TRequest, TResponse>(TRequest request, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, ServerStreamingServerMethod<TRequest, TResponse> continuation)
        {
            Authenticate(context);

            await continuation.Invoke(request, responseStream, context);
        }
        #endregion

        #region # 拦截双向Streaming服务处理 —— override Task DuplexStreamingServerHandler(IAsyncStreamReader...
        /// <summary>
        /// 拦截双向Streaming服务处理
        /// </summary>
        public override async Task DuplexStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, DuplexStreamingServerMethod<TRequest, TResponse> continuation)
        {
            Authenticate(context);

            await continuation.Invoke(requestStream, responseStream, context);
        }
        #endregion


        //Private

        #region # 认证 —— static void Authenticate(ServerCallContext context)
        /// <summary>
        /// 认证
        /// </summary>
        private static void Authenticate(ServerCallContext context)
        {
            HttpContext httpContext = context.GetHttpContext();
            Endpoint endpoint = httpContext.GetEndpoint();

            #region # 验证

            if (endpoint == null)
            {
                return;
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
        }
        #endregion
    }
}
