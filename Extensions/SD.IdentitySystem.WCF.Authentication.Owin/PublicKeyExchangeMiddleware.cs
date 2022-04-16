using Microsoft.Owin;
using SD.Infrastructure.Constants;
using System.Threading.Tasks;

namespace SD.IdentitySystem.WCF.Authentication.Owin
{
    /// <summary>
    /// 公钥交换中间件
    /// </summary>
    public class PublicKeyExchangeMiddleware : OwinMiddleware
    {
        /// <summary>
        /// 默认构造器
        /// </summary>
        public PublicKeyExchangeMiddleware(OwinMiddleware next)
            : base(next)
        {

        }

        /// <summary>
        /// 执行中间件
        /// </summary>
        public override Task Invoke(IOwinContext context)
        {
            //读Header
            string publicKey = context.Request.Headers.Get(SessionKey.PublicKey);
            if (string.IsNullOrWhiteSpace(publicKey))
            {
                //读QueryString
                publicKey = context.Request.Query[SessionKey.PublicKey];
            }

            //将公钥缓存至OwinContext
            context.Set(SessionKey.PublicKey, publicKey);

            return base.Next.Invoke(context);
        }
    }
}
