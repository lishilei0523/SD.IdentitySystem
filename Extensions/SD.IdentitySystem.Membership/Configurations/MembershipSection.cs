using SD.IdentitySystem.Membership.Configurations;
using System;
using System.Configuration;

// ReSharper disable once CheckNamespace
namespace SD.IdentitySystem
{
    /// <summary>
    /// SD.Membership配置
    /// </summary>
    public class MembershipSection : ConfigurationSection
    {
        #region # 字段及构造器

        /// <summary>
        /// 单例
        /// </summary>
        private static readonly MembershipSection _Setting;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static MembershipSection()
        {
            _Setting = (MembershipSection)ConfigurationManager.GetSection("sd.membership");

            #region # 非空验证

            if (_Setting == null)
            {
                throw new ApplicationException("SD.Membership节点未配置，请检查程序！");
            }

            #endregion
        }

        #endregion

        #region # 访问器 —— static MembershipSection Setting
        /// <summary>
        /// 访问器
        /// </summary>
        public static MembershipSection Setting
        {
            get { return _Setting; }
        }
        #endregion

        #region # Membership提供者节点 —— MembershipProviderElement MembershipProvider
        /// <summary>
        /// Membership提供者节点
        /// </summary>
        [ConfigurationProperty("membershipProvider", IsRequired = true)]
        public MembershipProviderElement MembershipProvider
        {
            get { return (MembershipProviderElement)this["membershipProvider"]; }
            set { this["membershipProvider"] = value; }
        }
        #endregion
    }
}
