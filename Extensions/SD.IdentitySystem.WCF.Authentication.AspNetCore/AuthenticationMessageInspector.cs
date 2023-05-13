using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using SD.Toolkits.OwinCore.Extensions;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text.Json;

namespace SD.IdentitySystem.WCF.Authentication.AspNetCore
{
    /// <summary>
    /// WCF/ASP.NET Core客户端身份认证消息拦截器
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
            //ASP.NET Core获取公钥处理
            Guid? publicKey = null;
            if (OwinContextReader.Current.Request.Headers.TryGetValue(SessionKey.PublicKey, out StringValues stringValues))
            {
                string publicKeyStr = stringValues.ToString();
                publicKey = new Guid(publicKeyStr);
            }
            else
            {
                string loginInfoJson = OwinContextReader.Current.Session.GetString(GlobalSetting.ApplicationId);
                if (!string.IsNullOrWhiteSpace(loginInfoJson))
                {
                    LoginInfo loginInfo = JsonSerializer.Deserialize<LoginInfo>(loginInfoJson);
                    publicKey = loginInfo.PublicKey;
                }
            }

            if (publicKey.HasValue)
            {
                //添加消息头
                MessageHeader header = MessageHeader.CreateHeader(CommonConstants.WCFAuthenticationHeader, GlobalSetting.ApplicationId, publicKey.Value);
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
