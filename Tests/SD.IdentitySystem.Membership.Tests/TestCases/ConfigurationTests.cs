using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            const string type = "SD.IdentitySystem.Membership.Windows.MembershipProvider";
            const string assembly = "SD.IdentitySystem.Membership.Windows";

            Assert.AreEqual(type, MembershipSection.Setting.Provider.Type);
            Assert.AreEqual(assembly, MembershipSection.Setting.Provider.Assembly);
        }
    }
}
