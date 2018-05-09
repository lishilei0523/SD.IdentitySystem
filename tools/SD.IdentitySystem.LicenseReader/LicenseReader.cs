using SD.IdentitySystem.Tookits;
using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using System.IO;

// ReSharper disable once CheckNamespace
namespace SD.IdentitySystem
{
    /// <summary>
    /// 许可证读取器
    /// </summary>
    public static class LicenseReader
    {
        /// <summary>
        /// 获取许可证
        /// </summary>
        /// <returns>许可证</returns>
        public static License GetLicense()
        {
            using (FileStream fileStream = new FileStream(CommonConstants.LicenseFileName, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    byte[] buffer = new byte[fileStream.Length];
                    binaryReader.Read(buffer, 0, (int)fileStream.Length);

                    string aesString = buffer.GetString();
                    string binaryString = aesString.Decrypt();

                    License license = binaryString.ToLicense();

                    return license;
                }
            }
        }
    }
}