using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;

// ReSharper disable once CheckNamespace
namespace SD.IdentitySystem.Tests
{
    /// <summary>
    /// 测试许可证读取器
    /// </summary>
    [TestClass]
    public class TestLicenseReader
    {
        /// <summary>
        /// 读取许可证测试
        /// </summary>
        [TestMethod]
        public void ReadLicenseTest()
        {
            License? license = LicenseReader.GetLicense();

            Assert.IsTrue(license != null);
            Assert.IsTrue(license.Value.EnterpriseName == "SD");
            Assert.IsTrue(license.Value.UniqueCode == "fa51222f13a938fa1978803d85cc82ed");
            Assert.IsTrue(license.Value.ExpiredDate == CommonConstants.MaxDateTime);
        }
    }
}
