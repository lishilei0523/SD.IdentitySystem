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
        private static MembershipSection _Setting;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static MembershipSection()
        {
            _Setting = null;
        }

        #endregion

        #region # 初始化 —— static void Initialize(Configuration configuration)
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="configuration">配置</param>
        public static void Initialize(Configuration configuration)
        {
            #region # 验证

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration), "配置不可为空！");
            }

            #endregion

            _Setting = (MembershipSection)configuration.GetSection("sd.membership");
        }
        #endregion

        #region # 访问器 —— static MembershipSection Setting
        /// <summary>
        /// 访问器
        /// </summary>
        public static MembershipSection Setting
        {
            get
            {
                if (_Setting == null)
                {
                    _Setting = (MembershipSection)ConfigurationManager.GetSection("sd.membership");
                }
                if (_Setting == null)
                {
                    throw new ApplicationException("SD.Membership节点未配置，请检查程序！");
                }

                return _Setting;
            }
        }
        #endregion

        #region # Membership提供者节点 —— MembershipProviderElement Provider
        /// <summary>
        /// Membership提供者节点
        /// </summary>
        [ConfigurationProperty("provider", IsRequired = true)]
        public MembershipProviderElement Provider
        {
            get { return (MembershipProviderElement)this["provider"]; }
            set { this["provider"] = value; }
        }
        #endregion
    }
}
