using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using SD.IdentitySystem.StubServer.Interfaces;
using ShSoft.ValueObjects;

namespace SD.IdentitySystem.StubServer.Implements
{
    /// <summary>
    /// 服务契约实现
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ServerContract : IServerContract
    {
        /// <summary>
        /// 获取消息头
        /// </summary>
        /// <returns>消息头</returns>
        public string GetHeader()
        {
            //获取消息头
            MessageHeaders headers = OperationContext.Current.IncomingMessageHeaders;

            #region # 验证消息头

            if (!headers.Any(x => x.Name == Constants.WcfAuthHeaderName && x.Namespace == Constants.WcfAuthHeaderNamespace))
            {
                throw new NullReferenceException("身份认证消息头不存在，请检查程序！");
            }

            #endregion

            //读取消息头中的公钥
            Guid publishKey = headers.GetHeader<Guid>(Constants.WcfAuthHeaderName, Constants.WcfAuthHeaderNamespace);

            return publishKey.ToString();
        }
    }
}