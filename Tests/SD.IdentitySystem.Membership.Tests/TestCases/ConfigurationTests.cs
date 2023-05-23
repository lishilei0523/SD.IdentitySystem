using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Common;
using SD.Infrastructure;
using System.Configuration;
using System.Reflection;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SD.IdentitySystem.Membership.Tests.TestCases
{
    /// <summary>
    /// 配置文件测试
    /// </summary>
    [TestClass]
    public class ConfigurationTests
    {
        #region # 测试初始化 —— void Initialize()
        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
#if NETCOREAPP3_1_OR_GREATER
            Assembly entryAssembly = Assembly.GetExecutingAssembly();
            Configuration configuration = ConfigurationExtension.GetConfigurationFromAssembly(entryAssembly);
            FrameworkSection.Initialize(configuration);
#endif
        }
        #endregion

        #region # 测试配置文件 —— void TestConfiguration()
        /// <summary>
        /// 测试配置文件
        /// </summary>
        [TestMethod]
        public void TestConfiguration()
        {
            const string type = "SD.IdentitySystem.MembershipProvider, SD.IdentitySystem.Membership.Common";

            Assert.AreEqual(type, FrameworkSection.Setting.MembershipProvider.Type);
        }
        #endregion
    }
}
