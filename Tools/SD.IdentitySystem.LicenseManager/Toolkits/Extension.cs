using SD.IdentitySystem.LicenseManager.Models;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace SD.IdentitySystem.LicenseManager.Toolkits
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    internal static class Extension
    {
        #region # 静态字段及静态构造器

        /// <summary>
        /// 二进制序列化器
        /// </summary>
        private static readonly BinaryFormatter _BinaryFormatter;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static Extension()
        {
            Extension._BinaryFormatter = new BinaryFormatter();
        }

        #endregion

        #region # 字符串计算MD5方法 —— static string ToHash(this string text)
        /// <summary>
        /// 字符串计算MD5扩展方法
        /// </summary>
        public static string ToHash(this string text)
        {
            byte[] buffer = Encoding.Default.GetBytes(text);
            using (MD5 md5 = MD5.Create())
            {
                buffer = md5.ComputeHash(buffer);
                StringBuilder md5Builder = new StringBuilder();
                foreach (byte @byte in buffer)
                {
                    md5Builder.Append(@byte.ToString("x2"));
                }
                return md5Builder.ToString();
            }
        }
        #endregion

        #region # 字符串序列化二进制数组扩展方法 —— static byte[] ToBuffer(this string text)
        /// <summary>
        /// 字符串序列化二进制数组扩展方法
        /// </summary>
        public static byte[] ToBuffer(this string text)
        {
            #region # 验证参数

            if (text == null)
            {
                throw new ArgumentNullException(nameof(text), @"源对象不可为空！");
            }

            #endregion

            using (MemoryStream stream = new MemoryStream())
            {
                Extension._BinaryFormatter.Serialize(stream, text);

                return stream.ToArray();
            }
        }
        #endregion

        #region # 二进制字符串反序列化许可证扩展方法 —— static License License(this string binaryText)
        /// <summary>
        /// 二进制字符串反序列化对象扩展方法
        /// </summary>
        public static License ToLicense(this string binaryText)
        {
            #region # 验证参数

            if (string.IsNullOrEmpty(binaryText))
            {
                throw new ArgumentNullException(nameof(binaryText), @"二进制字符串不可为空！");
            }

            #endregion

            using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(binaryText)))
            {
                License license = (License)Extension._BinaryFormatter.Deserialize(stream);

                return license;
            }
        }
        #endregion

        #region # 二进制数据反序列化字符串扩展方法 —— static string GetString(this byte[] buffer)
        /// <summary>
        /// 二进制数据反序列化字符串扩展方法
        /// </summary>
        public static string GetString(this byte[] buffer)
        {
            #region # 验证参数

            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer), @"byte数组不可为null！");
            }

            #endregion

            using (MemoryStream stream = new MemoryStream(buffer))
            {
                string text = Extension._BinaryFormatter.Deserialize(stream).ToString();

                return text;
            }
        }
        #endregion

        #region # 许可证序列化二进制字符串扩展方法 —— static string ToBinaryString(this License license)
        /// <summary>
        /// 许可证序列化二进制字符串扩展方法
        /// </summary>
        public static string ToBinaryString(this License license)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Extension._BinaryFormatter.Serialize(stream, license);
                return Convert.ToBase64String(stream.ToArray());
            }
        }
        #endregion

        #region # 字符串加密扩展方法 —— static string Encrypt(this string text...
        /// <summary>
        /// 字符串加密扩展方法
        /// </summary>
        public static string Encrypt(this string text, string key = null)
        {
            key = string.IsNullOrEmpty(key) ? "744FBCAD-3BA6-40FB-9A75-B6C81E25403E" : key;

            using (DESCryptoServiceProvider desCryptoService = new DESCryptoServiceProvider())
            {
                string keyHash8 = key.ToHash().Substring(0, 8);
                desCryptoService.Key = Encoding.ASCII.GetBytes(keyHash8);
                desCryptoService.IV = Encoding.ASCII.GetBytes(keyHash8);

                byte[] inputByteArray = Encoding.Default.GetBytes(text);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, desCryptoService.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                        cryptoStream.FlushFinalBlock();

                        StringBuilder stringBuilder = new StringBuilder();

                        foreach (byte byt in memoryStream.ToArray())
                        {
                            stringBuilder.AppendFormat("{0:X2}", byt);
                        }

                        return stringBuilder.ToString();
                    }
                }
            }
        }
        #endregion

        #region # 字符串解密扩展方法 —— static string Decrypt(this string text...
        /// <summary>
        /// 字符串解密扩展方法
        /// </summary>
        public static string Decrypt(this string text, string key = null)
        {
            key = string.IsNullOrEmpty(key) ? "744FBCAD-3BA6-40FB-9A75-B6C81E25403E" : key;
            int length = text.Length / 2;

            byte[] inputByteArray = new byte[length];

            for (int index = 0; index < length; index++)
            {
                inputByteArray[index] = Convert.ToByte(text.Substring(index * 2, 2), 16);
            }

            using (DESCryptoServiceProvider desCryptoService = new DESCryptoServiceProvider())
            {
                string keyHash8 = key.ToHash().Substring(0, 8);
                desCryptoService.Key = Encoding.ASCII.GetBytes(keyHash8);
                desCryptoService.IV = Encoding.ASCII.GetBytes(keyHash8);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, desCryptoService.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                        cryptoStream.FlushFinalBlock();

                        return Encoding.Default.GetString(memoryStream.ToArray());
                    }
                }
            }
        }
        #endregion
    }
}
