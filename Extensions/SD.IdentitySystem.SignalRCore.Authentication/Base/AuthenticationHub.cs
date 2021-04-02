using Microsoft.AspNetCore.SignalR;
using SD.Infrastructure.MessageBase;
using SD.Infrastructure.SignalRCore.Server.Base;
using System.Linq;

namespace SD.IdentitySystem.SignalRCore.Authentication.Base
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
                IClientProxy clientProxy = base.Clients.Users(message.ReceiverAccounts);
                clientProxy.SendAsync(nameof(this.Exchange), message);
            }
            else
            {
                base.Exchange(message);
            }
        }
    }
}
