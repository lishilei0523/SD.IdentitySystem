using Microsoft.AspNet.SignalR.Client;
using SD.IdentitySystem.SignalR.Client.Toolkits;
using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using System.Collections.Generic;

namespace SD.IdentitySystem.SignalR.Client.Factories
{
    /// <summary>
    /// 认证Hub连接工厂
    /// </summary>
    public static class HubConnectionFactory
    {
        /// <summary>
        /// 创建认证Hub连接
        /// </summary>
        public static HubConnection CreateAuthenticConnection(string url, LoginInfo loginInfo)
        {
            HubConnection connection = new HubConnection(url);
            connection.Headers.Add(SessionKey.CurrentUser, loginInfo.ToJson());

            return connection;
        }

        /// <summary>
        /// 创建认证Hub连接
        /// </summary>
        public static HubConnection CreateAuthenticConnection(string url, LoginInfo loginInfo, IDictionary<string, string> queryString, bool useDefaultUrl)
        {
            HubConnection connection = new HubConnection(url, queryString, useDefaultUrl);
            connection.Headers.Add(SessionKey.CurrentUser, loginInfo.ToJson());

            return connection;
        }
    }
}
