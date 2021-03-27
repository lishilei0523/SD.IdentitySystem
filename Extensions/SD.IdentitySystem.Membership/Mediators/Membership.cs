using SD.Infrastructure.MemberShip;
using SD.IOC.Core.Mediators;

// ReSharper disable once CheckNamespace
namespace SD.IdentitySystem
{
    /// <summary>
    /// Membership管理工具类
    /// </summary>
    public static class Membership
    {
        /// <summary>
        /// 当前登录信息
        /// </summary>
        public static LoginInfo LoginInfo
        {
            get
            {
                IMembershipProvider membershipProvider = ResolveMediator.ResolveOptional<IMembershipProvider>();
                LoginInfo loginInfo = membershipProvider?.GetLoginInfo();

                return loginInfo;
            }
        }
    }
}
