using SD.IdentitySystem.LicenseManager.Models;
using System;
using System.IO;
using System.Linq;

namespace SD.IdentitySystem.LicenseManager.Toolkits
{
    /// <summary>
    /// 许可证读取器
    /// </summary>
    public static class LicenseReader
    {
        /// <summary>
        /// 分隔符
        /// </summary>
        private const string Separator = @"\";

        /// <summary>
        /// 许可证路径
        /// </summary>
        private static readonly string _LicencePath;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static LicenseReader()
        {
            string runtimeDir = AppDomain.CurrentDomain.BaseDirectory;
            string endChar = runtimeDir.Last().ToString();

            if (endChar == Separator)
            {
                _LicencePath = runtimeDir + Constants.LicenseFileName;
            }
            else
            {
                _LicencePath = runtimeDir + Separator + Constants.LicenseFileName;
            }
        }

        /// <summary>
        /// 获取许可证
        /// </summary>
        /// <param name="licensePath">许可证路径</param>
        /// <returns>许可证</returns>
        public static License? GetLicense(string licensePath = null)
        {
            #region # 验证

            if (string.IsNullOrEmpty(licensePath))
            {
                licensePath = _LicencePath;
            }
            if (!File.Exists(licensePath))
            {
                return null;
            }

            #endregion

            try
            {
                using (FileStream fileStream = new FileStream(licensePath, FileMode.Open))
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
            catch (Exception exception)
            {
                throw new ApplicationException("许可证异常，请联系管理员！", exception);
            }
        }
    }
}