using SD.CacheManager;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using System;
using System.Linq;
#if NET462_OR_GREATER
using System.ServiceModel;
using System.ServiceModel.Channels;
#endif
#if NETSTANDARD2_0_OR_GREATER || NET6_0_OR_GREATER
using CoreWCF;
using CoreWCF.Channels;
#endif

// ReSharper disable once CheckNamespace
namespace SD.IdentitySystem
{
    /// <summary>
    /// WCF Membership提供者
    /// </summary>
    public class MembershipProvider : IMembershipProvider
    {
        /// <summary>
        /// 设置登录信息
        /// </summary>
        /// <param name="loginInfo">登录信息</param>
        public void SetLoginInfo(LoginInfo loginInfo)
        {
            if (OperationContext.Current != null)
            {
                MessageHeader header = MessageHeader.CreateHeader(CommonConstants.WCFAuthenticationHeader, GlobalSetting.ApplicationId, loginInfo.PublicKey);
                MessageHeaders headers = OperationContext.Current.IncomingMessageHeaders;
                headers.Add(header);
            }
        }

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns>登录信息</returns>
        public LoginInfo GetLoginInfo()
        {
            if (OperationContext.Current != null)
            {
                //获取消息头
                MessageHeaders headers = OperationContext.Current.IncomingMessageHeaders;
                if (!headers.Any(x => x.Name == CommonConstants.WCFAuthenticationHeader && x.Namespace == GlobalSetting.ApplicationId))
                {
                    return null;
                }

                Guid publicKey = headers.GetHeader<Guid>(CommonConstants.WCFAuthenticationHeader, GlobalSetting.ApplicationId);
                LoginInfo loginInfo = CacheMediator.Get<LoginInfo>(publicKey.ToString());

                return loginInfo;
            }

            return null;
        }
    }
}
