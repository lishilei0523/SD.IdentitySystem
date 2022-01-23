using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Infrastructure;

namespace SD.IdentitySystem.Membership.Tests.TestCases
{
    /// <summary>
    /// 配置文件测试
    /// </summary>
    [TestClass]
    public class ConfigurationTests
    {
        /// <summary>
        /// 测试读取配置文件
        /// </summary>
        [TestMethod]
        public void TestReadConfigurations()
        {
            const string type = "SD.IdentitySystem.MembershipProvider, SD.IdentitySystem.Membership.Windows";

            Assert.AreEqual(type, FrameworkSection.Setting.MembershipProvider.Type);
        }
    }
}
