using Microsoft.AspNetCore.SignalR;

namespace SD.IdentitySystem.SignalR.Authentication.Providers
{
    /// <summary>
    /// 用户登录名提供者
    /// </summary>
    public sealed class LoginIdProvider : IUserIdProvider
    {
        /// <summary>
        /// 获取用户登录名
        /// </summary>
        /// <param name="connection">Hub连接</param>
        /// <returns>用户登录名</returns>
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Identity?.Name;
        }
    }
}
