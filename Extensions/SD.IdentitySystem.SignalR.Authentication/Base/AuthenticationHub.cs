using SD.Infrastructure.MessageBase;
using SD.Infrastructure.SignalR.Server.Base;
using System.Linq;

namespace SD.IdentitySystem.SignalR.Authentication.Base
{
    /// <summary>
    /// 认证消息Hub基类
    /// </summary>
    public abstract class AuthenticationHub<T> : MessageHub<T> where T : IMessage
    {
        /// <summary>
        /// 交换消息
        /// </summary>
        /// <param name="message">消息</param>
        public override void Exchange(T message)
        {
            if (message.ReceiverAccounts != null && message.ReceiverAccounts.Any())
            {
                IMessageHub<T> messageHub = base.Clients.Users(message.ReceiverAccounts);
                messageHub.Exchange(message);
            }
            else
            {
                base.Exchange(message);
            }
        }
    }
}
