using SD.CacheManager;
using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using System;
using System.Linq;
#if NET461_OR_GREATER
using System.ServiceModel;
using System.ServiceModel.Channels;
#endif
#if NETSTANDARD2_0_OR_GREATER
using CoreWCF;
using CoreWCF.Channels;
#endif

namespace SD.IdentitySystem.Membership.WCF
{
    /// <summary>
    /// WCF Membership提供者
    /// </summary>
    public class MembershipProvider : IMembershipProvider
    {
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
                if (!headers.Any(x => x.Name == CommonConstants.WcfAuthHeaderName && x.Namespace == CommonConstants.WcfAuthHeaderNamespace))
                {
                    return null;
                }

                Guid publicKey = headers.GetHeader<Guid>(CommonConstants.WcfAuthHeaderName, CommonConstants.WcfAuthHeaderNamespace);
                LoginInfo loginInfo = CacheMediator.Get<LoginInfo>(publicKey.ToString());

                return loginInfo;
            }

            return null;
        }
    }
}
