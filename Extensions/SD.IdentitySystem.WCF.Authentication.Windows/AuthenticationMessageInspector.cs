using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace SD.IdentitySystem.WCF.Authentication.Windows
{
    /// <summary>
    /// WCF/Windows客户端身份认证消息拦截器
    /// </summary>
    internal class AuthenticationMessageInspector : IClientMessageInspector
    {
        /// <summary>
        /// 请求发送前事件
        /// </summary>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            //Windows客户端获取公钥处理
            object loginInfo = AppDomain.CurrentDomain.GetData(GlobalSetting.ApplicationId);
            if (loginInfo != null)
            {
                Guid publicKey = ((LoginInfo)loginInfo).PublicKey;

                //添加消息头
                MessageHeader header = MessageHeader.CreateHeader(CommonConstants.WCFAuthenticationHeader, GlobalSetting.ApplicationId, publicKey);
                request.Headers.Add(header);
            }

            return null;
        }

        /// <summary>
        /// 请求响应后事件
        /// </summary>
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {

        }
    }
}
