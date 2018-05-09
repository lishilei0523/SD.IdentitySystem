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
            License license = LicenseReader.GetLicense();

            Assert.IsTrue(license.EnterpriseName == "SD");
            Assert.IsTrue(license.UniqueCode == "0c0d247db5ad777ede9b88839887a217");
            Assert.IsTrue(license.ExpiredDate == CommonConstants.MaxDateTime);
        }
    }
}
