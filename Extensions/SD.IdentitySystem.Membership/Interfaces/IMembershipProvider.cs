using SD.Infrastructure.MemberShip;

// ReSharper disable once CheckNamespace
namespace SD.IdentitySystem
{
    /// <summary>
    /// Membership提供者接口
    /// </summary>
    public interface IMembershipProvider
    {
        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns>登录信息</returns>
        LoginInfo GetLoginInfo();
    }
}
