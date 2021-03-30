using SD.Infrastructure.MemberShip;
using SD.IOC.Core.Mediators;

// ReSharper disable once CheckNamespace
namespace SD.IdentitySystem
{
    /// <summary>
    /// Membership中介者
    /// </summary>
    public static class MembershipMediator
    {
        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns>登录信息</returns>
        public static LoginInfo GetLoginInfo()
        {
            IMembershipProvider membershipProvider = ResolveMediator.ResolveOptional<IMembershipProvider>();
            LoginInfo loginInfo = membershipProvider?.GetLoginInfo();

            return loginInfo;
        }
    }
}
