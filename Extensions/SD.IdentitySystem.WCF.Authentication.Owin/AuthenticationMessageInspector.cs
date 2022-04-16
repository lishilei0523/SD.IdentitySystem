using SD.Infrastructure.Constants;
using SD.Toolkits.Owin.Extensions;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace SD.IdentitySystem.WCF.Authentication.Owin
{
    /// <summary>
    /// WCF/OWIN客户端身份认证消息拦截器
    /// </summary>
    internal class AuthenticationMessageInspector : IClientMessageInspector
    {
        /// <summary>
        /// 请求发送前事件
        /// </summary>
        /// <param name="request">请求消息</param>
        /// <param name="channel">信道</param>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            //OWIN获取公钥处理
            string publicKeyStr = OwinContextReader.Current.Get<string>(SessionKey.PublicKey);
            if (!string.IsNullOrWhiteSpace(publicKeyStr))
            {
                Guid publicKey = new Guid(publicKeyStr);

                //添加消息头
                MessageHeader header = MessageHeader.CreateHeader(CommonConstants.WCFAuthenticationHeader, GlobalSetting.ApplicationId, publicKey);
                request.Headers.Add(header);
            }

            return null;
        }

        /// <summary>
        /// 请求响应后事件
        /// </summary>
        /// <param name="reply">响应消息</param>
        /// <param name="correlationState">相关状态</param>
        public void AfterReceiveReply(ref Message reply, object correlationState) { }
    }
}
