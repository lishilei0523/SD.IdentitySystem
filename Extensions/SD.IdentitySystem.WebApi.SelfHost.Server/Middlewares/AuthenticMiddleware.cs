using Microsoft.Owin;
using SD.CacheManager;
using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using SD.Infrastructure.WebApi;
using System;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace SD.IdentitySystem.WebApi.SelfHost.Server.Middlewares
{
    /// <summary>
    /// 身份认证中间件
    /// </summary>
    public class AuthenticMiddleware : OwinMiddleware
    {
        /// <summary>
        /// 登录信息
        /// </summary>
        private static readonly AsyncLocal<LoginInfo> _LoginInfo;

        /// <summary>
        /// 身份过期时间
        /// </summary>
        private static readonly int _Timeout;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static AuthenticMiddleware()
        {
            _LoginInfo = new AsyncLocal<LoginInfo>();
            if (!string.IsNullOrWhiteSpace(GlobalSetting.AuthenticationTimeout))
            {
                if (!int.TryParse(GlobalSetting.AuthenticationTimeout, out _Timeout))
                {
                    //默认20分钟
                    _Timeout = 20;
                }
            }
            else
            {
                //默认20分钟
                _Timeout = 20;
            }
        }

        /// <summary>
        /// 默认构造器
        /// </summary>
        public AuthenticMiddleware(OwinMiddleware next)
            : base(next)
        {

        }

        /// <summary>
        /// 当前登录信息
        /// </summary>
        public static LoginInfo LoginInfo
        {
            get { return _LoginInfo.Value; }
        }

        /// <summary>
        /// 执行中间件
        /// </summary>
        public override Task Invoke(IOwinContext context)
        {
            if (WebApiSection.Setting.Authorized)
            {
                //读Header
                string publicKey = context.Request.Headers.Get(SessionKey.CurrentPublicKey);
                if (string.IsNullOrWhiteSpace(publicKey))
                {
                    //读QueryString
                    publicKey = context.Request.Query[SessionKey.CurrentPublicKey];
                }
                if (string.IsNullOrWhiteSpace(publicKey))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Response.Headers.Append("ErrorMessage", "Public key not found");

                    return base.Next.Invoke(context);
                }

                LoginInfo loginInfo = CacheMediator.Get<LoginInfo>(publicKey);
                if (loginInfo == null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Response.Headers.Append("ErrorMessage", "Login info expired");

                    return base.Next.Invoke(context);
                }

                IIdentity identity = new GenericIdentity(loginInfo.LoginId);
                context.Request.User = new GenericPrincipal(identity, null);
                _LoginInfo.Value = loginInfo;

                //通过后，重新设置缓存过期时间
                CacheMediator.Set(publicKey, loginInfo, DateTime.Now.AddMinutes(_Timeout));

                return base.Next.Invoke(context);
            }

            return base.Next.Invoke(context);
        }
    }
}
