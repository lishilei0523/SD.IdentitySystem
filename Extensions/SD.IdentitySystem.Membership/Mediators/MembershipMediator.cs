using SD.Infrastructure.MemberShip;
using System;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace SD.IdentitySystem
{
    /// <summary>
    /// Membership中介者
    /// </summary>
    public static class MembershipMediator
    {
        /// <summary>
        /// Membership提供者实现类型
        /// </summary>
        private static readonly Type _MembershipProviderImplType;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static MembershipMediator()
        {
            Assembly implAssembly = Assembly.Load(MembershipSection.Setting.MembershipProvider.Assembly);
            _MembershipProviderImplType = implAssembly.GetType(MembershipSection.Setting.MembershipProvider.Type);

        }

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns>登录信息</returns>
        public static LoginInfo GetLoginInfo()
        {
            IMembershipProvider membershipProvider = (IMembershipProvider)Activator.CreateInstance(_MembershipProviderImplType);
            LoginInfo loginInfo = membershipProvider?.GetLoginInfo();

            return loginInfo;
        }
    }
}
