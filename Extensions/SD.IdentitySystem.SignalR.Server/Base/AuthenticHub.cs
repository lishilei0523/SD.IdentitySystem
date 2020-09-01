using SD.Infrastructure.MessageBase;
using SD.Infrastructure.SignalR;
using SD.Infrastructure.SignalR.Server.Base;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SD.IdentitySystem.SignalR.Server.Base
{
    /// <summary>
    /// 认证消息Hub基类
    /// </summary>
    public abstract class AuthenticHub<T> : MessageHub<T> where T : IMessage
    {
        #region # 静态字段及构造器

        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object _Sync;

        /// <summary>
        /// 用户/连接字典
        /// </summary>
        private static readonly IDictionary<string, string> _UserConnections;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static AuthenticHub()
        {
            _Sync = new object();
            _UserConnections = new ConcurrentDictionary<string, string>();
        }

        #endregion

        #region # 方法

        #region 交换消息 —— override void Exchange(T message)
        /// <summary>
        /// 交换消息
        /// </summary>
        /// <param name="message">消息</param>
        public override void Exchange(T message)
        {
            if (message.ReceiverAccounts != null && message.ReceiverAccounts.Any())
            {
                IList<string> receiverConnectionIds = _UserConnections.Where(x => message.ReceiverAccounts.Contains(x.Key)).Select(x => x.Value).ToList();

                IMessageHub<T> messageHub = base.Clients.Clients(receiverConnectionIds);
                messageHub.Exchange(message);
            }
            else
            {
                base.Exchange(message);
            }
        }
        #endregion

        #region 客户端连接事件 —— override Task OnConnected()
        /// <summary>
        /// 客户端连接事件
        /// </summary>
        public override Task OnConnected()
        {
            if (SignalSection.Setting.Authorized)
            {
                lock (_Sync)
                {
                    string loginId = base.Context.User.Identity.Name;

                    //用户/连接字典
                    if (_UserConnections.ContainsKey(loginId))
                    {
                        _UserConnections[loginId] = base.Context.ConnectionId;
                    }
                    else
                    {
                        _UserConnections.Add(loginId, base.Context.ConnectionId);
                    }

                    return base.OnConnected();
                }
            }

            return base.OnConnected();
        }
        #endregion

        #endregion
    }
}
