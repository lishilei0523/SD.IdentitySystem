using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.IdentitySystem.LicenseManager.Models;
using SD.IdentitySystem.LicenseManager.Toolkits;
using SD.Infrastructure.Constants;

namespace SD.IdentitySystem.LicenseManager.Tests.TestCases
{
    /// <summary>
    /// 测试许可证读取器
    /// </summary>
    [TestClass]
    public class LicenseReaderTests
    {
        /// <summary>
        /// 读取许可证测试
        /// </summary>
        [TestMethod]
        public void TestReadLicense()
        {
            License? license = LicenseReader.GetLicense();

            Assert.IsTrue(license != null);
            Assert.IsTrue(license.Value.EnterpriseName == "SD");
            Assert.IsTrue(license.Value.UniqueCode == "a1cab08d01ffc87b9ecaa873745592f1");
            Assert.IsTrue(license.Value.ServiceExpiredDate == CommonConstants.MaxDate);
            Assert.IsTrue(license.Value.LicenseExpiredDate == CommonConstants.MaxDate);
        }
    }
}
