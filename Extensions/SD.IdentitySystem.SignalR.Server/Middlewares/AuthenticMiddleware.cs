using Microsoft.Owin;
using SD.CacheManager;
using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using SD.Infrastructure.SignalR;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SD.IdentitySystem.SignalR.Server.Middlewares
{
    /// <summary>
    /// 身份认证中间件
    /// </summary>
    public class AuthenticMiddleware : OwinMiddleware
    {
        /// <summary>
        /// 默认构造器
        /// </summary>
        public AuthenticMiddleware(OwinMiddleware next)
            : base(next)
        {

        }

        /// <summary>
        /// 执行中间件
        /// </summary>
        public override Task Invoke(IOwinContext context)
        {
            if (SignalSection.Setting.Authorized)
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

                return base.Next.Invoke(context);
            }

            return base.Next.Invoke(context);
        }
    }
}
