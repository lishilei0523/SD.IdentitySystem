using SD.Infrastructure;
using SD.Infrastructure.Membership;
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
        /// Membership提供者
        /// </summary>
        private static readonly IMembershipProvider _MembershipProvider;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static MembershipMediator()
        {
            string typeFullName = FrameworkSection.Setting.MembershipProvider.Type;

            #region # 验证

            if (string.IsNullOrWhiteSpace(typeFullName))
            {
                throw new ApplicationException("Membership提供者未配置！");
            }

            #endregion

            string[] typeFullNames = typeFullName.Split(',');
            string assemblyName = typeFullNames[1].Trim();
            string typeName = typeFullNames[0].Trim();
            Assembly implAssembly = Assembly.Load(assemblyName);
            Type implType = implAssembly.GetType(typeName);
            _MembershipProvider = (IMembershipProvider)Activator.CreateInstance(implType);
        }

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns>登录信息</returns>
        public static LoginInfo GetLoginInfo()
        {
            LoginInfo loginInfo = _MembershipProvider.GetLoginInfo();

            return loginInfo;
        }
    }
}
