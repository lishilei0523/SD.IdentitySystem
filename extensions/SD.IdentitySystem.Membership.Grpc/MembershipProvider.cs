using Grpc.Core;
using SD.CacheManager;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using SD.Toolkits.Grpc.Server.Extensions;

// ReSharper disable once CheckNamespace
namespace SD.IdentitySystem
{
    /// <summary>
    /// gRPC Membership提供者
    /// </summary>
    public class MembershipProvider : IMembershipProvider
    {
        /// <summary>
        /// 设置登录信息
        /// </summary>
        /// <param name="loginInfo">登录信息</param>
        public void SetLoginInfo(LoginInfo loginInfo)
        {
            ServerCallContext context = ServerCallContextReader.Current;
            if (context != null)
            {
                context.RequestHeaders.Add(SessionKey.PublicKey.ToLower(), loginInfo.PublicKey.ToString());
            }
        }

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns>登录信息</returns>
        public LoginInfo GetLoginInfo()
        {
            ServerCallContext context = ServerCallContextReader.Current;
            if (context != null)
            {
                string headerKey = SessionKey.PublicKey.ToLower();
                Metadata.Entry headerEntry = context.RequestHeaders.Get(headerKey);
                string publicKey = headerEntry?.Value;
                if (!string.IsNullOrWhiteSpace(publicKey))
                {
                    LoginInfo loginInfo = CacheMediator.Get<LoginInfo>(publicKey);

                    return loginInfo;
                }

                return null;

            }

            return null;
        }
    }
}
