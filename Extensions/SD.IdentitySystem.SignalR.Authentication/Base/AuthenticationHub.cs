using Microsoft.AspNetCore.SignalR;
using SD.Infrastructure.MessageBase;
using SD.Infrastructure.SignalR.Server.Base;
using System.Linq;
using System.Threading.Tasks;

namespace SD.IdentitySystem.SignalR.Authentication.Base
{
    /// <summary>
    /// 认证消息Hub基类
    /// </summary>
    public abstract class AuthenticationHub<T> : MessageHub<T> where T : TransientMessage
    {
        /// <summary>
        /// 交换消息
        /// </summary>
        /// <param name="message">消息</param>
        public override async Task Exchange(T message)
        {
            if (message.ReceiverAccounts != null && message.ReceiverAccounts.Any())
            {
                IClientProxy clientProxy = base.Clients.Users(message.ReceiverAccounts);
                await clientProxy.SendAsync(nameof(this.Exchange), message);
            }
            else
            {
                await base.Exchange(message);
            }
        }
    }
}
