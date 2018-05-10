using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.IdentitySystem.LicenseManager.Models;
using SD.IdentitySystem.LicenseManager.Tookits;
using SD.Infrastructure.Constants;

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
            Assert.IsTrue(license.Value.UniqueCode == "1e81f3a4948ee64ba1106dc4c0d759a9");
            Assert.IsTrue(license.Value.ExpiredDate == CommonConstants.MaxDateTime);
        }
    }
}
