using SD.Infrastructure.Membership;

// ReSharper disable once CheckNamespace
namespace SD.IdentitySystem
{
    /// <summary>
    /// Membership提供者接口
    /// </summary>
    public interface IMembershipProvider
    {
        /// <summary>
        /// 设置登录信息
        /// </summary>
        /// <param name="loginInfo">登录信息</param>
        void SetLoginInfo(LoginInfo loginInfo);

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns>登录信息</returns>
        LoginInfo GetLoginInfo();
    }
}
