using SD.CacheManager;
using SD.Infrastructure.Constants;
using SD.Infrastructure.CustomExceptions;
using SD.Infrastructure.MemberShip;
using SD.Infrastructure.MessageBase;
using SD.Infrastructure.SignalR.Server.Base;
using System;
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

        #region # 事件

        /// <summary>
        /// 客户端连接事件
        /// </summary>
        public override Task OnConnected()
        {
            lock (_Sync)
            {
                //认证
                string publicKey = base.Context.Headers.Get(SessionKey.CurrentPublishKey);
                if (string.IsNullOrWhiteSpace(publicKey))
                {
                    throw new NullReferenceException("身份信息不存在，请检查程序！");
                }

                LoginInfo loginInfo = CacheMediator.Get<LoginInfo>(publicKey);
                if (loginInfo == null)
                {
                    throw new NoPermissionException("身份过期，请重新登录！");
                }

                //用户/连接字典
                if (_UserConnections.ContainsKey(loginInfo.LoginId))
                {
                    _UserConnections[loginInfo.LoginId] = base.Context.ConnectionId;
                }
                else
                {
                    _UserConnections.Add(loginInfo.LoginId, base.Context.ConnectionId);
                }

                return base.OnConnected();
            }
        }

        /// <summary>
        /// 客户端断开连接事件
        /// </summary>
        public override Task OnDisconnected(bool stopCalled)
        {
            lock (_Sync)
            {
                //认证
                string publicKey = base.Context.Headers.Get(SessionKey.CurrentPublishKey);
                if (string.IsNullOrWhiteSpace(publicKey))
                {
                    return base.OnDisconnected(stopCalled);
                }

                LoginInfo loginInfo = CacheMediator.Get<LoginInfo>(publicKey);
                if (loginInfo == null)
                {
                    return base.OnDisconnected(stopCalled);
                }

                //用户/连接字典
                if (_UserConnections.ContainsKey(loginInfo.LoginId))
                {
                    _UserConnections.Remove(publicKey);
                }

                return base.OnDisconnected(stopCalled);
            }
        }

        #endregion
    }
}
