using SD.IdentitySystem.StubWCF.Server.Interfaces;
using SD.Infrastructure.Constants;
using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace SD.IdentitySystem.StubWCF.Server.Implements
{
    /// <summary>
    /// WCF服务端契约实现
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, IncludeExceptionDetailInFaults = true)]
    public class ServerContract : IServerContract
    {
        /// <summary>
        /// 获取消息头
        /// </summary>
        /// <returns>消息头</returns>
        /// <remarks>此方法用于测试，将客户端发送的消息头返回给客户端</remarks>
        public string GetHeader()
        {
            //获取消息头
            MessageHeaders headers = OperationContext.Current.IncomingMessageHeaders;

            #region # 验证消息头

            if (!headers.Any(x => x.Name == CommonConstants.WCFAuthenticationHeader && x.Namespace == GlobalSetting.ApplicationId))
            {
                throw new NullReferenceException("身份认证消息头不存在，请检查程序！");
            }

            #endregion

            //读取消息头中的公钥
            Guid publishKey = headers.GetHeader<Guid>(CommonConstants.WCFAuthenticationHeader, GlobalSetting.ApplicationId);

            return publishKey.ToString();
        }
    }
}
