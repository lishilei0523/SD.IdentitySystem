using Microsoft.Extensions.Primitives;
using SD.Infrastructure.Constants;
using SD.Toolkits.OwinCore.Extensions;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace SD.IdentitySystem.WCF.Authentication.AspNetCore
{
    /// <summary>
    /// WCF/ASP.NET Core客户端身份认证消息拦截器
    /// </summary>
    public class AuthenticationMessageInspector : IClientMessageInspector
    {
        /// <summary>
        /// 请求发送前事件
        /// </summary>
        /// <param name="request">请求消息</param>
        /// <param name="channel">信道</param>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            //ASP.NET Core获取公钥处理
            if (OwinContextReader.Current.Request.Headers.TryGetValue(SessionKey.CurrentPublicKey, out StringValues stringValues))
            {
                string publicKeyStr = stringValues.ToString();
                if (!string.IsNullOrWhiteSpace(publicKeyStr))
                {
                    Guid publicKey = new Guid(publicKeyStr);

                    //添加消息头
                    MessageHeader header = MessageHeader.CreateHeader(CommonConstants.WcfAuthHeaderName, CommonConstants.WcfAuthHeaderNamespace, publicKey);
                    request.Headers.Add(header);
                }
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
