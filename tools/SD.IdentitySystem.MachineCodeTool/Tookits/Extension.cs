using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

namespace SD.IdentitySystem.MachineCodeTool.Tookits
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    internal static class Extension
    {
        #region # 获取机器唯一码 —— static string GetMachineCode()
        /// <summary>
        /// 获取机器唯一码
        /// </summary>
        /// <returns>机器唯一码</returns>
        public static string GetMachineCode()
        {
            const string keyPrefix = @"SYSTEM\CurrentControlSet\Control\Network\{4D36E972-E325-11CE-BFC1-08002BE10318}";

            PhysicalAddress physicalAddress = null;

            IEnumerable<NetworkInterface> networkInterfaces = NetworkInterface.GetAllNetworkInterfaces().Where(x => x.NetworkInterfaceType == NetworkInterfaceType.Ethernet);

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                string registryKeyPath = $@"{keyPrefix}\{networkInterface.Id}\Connection";
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(registryKeyPath, false);
                string pnpInstanceId = registryKey?.GetValue("PnpInstanceID", string.Empty).ToString();

                if (pnpInstanceId != null && pnpInstanceId.Length > 3 && pnpInstanceId.Substring(0, 3) == "PCI")
                {
                    physicalAddress = networkInterface.GetPhysicalAddress();
                }
            }

            if (physicalAddress == null)
            {
                throw new SystemException("物理网卡不存在，请联系管理员！");
            }

            string machineCode = physicalAddress.ToString().ToMD5();

            return machineCode;
        }
        #endregion


        //Private

        #region # 计算字符串MD5值扩展方法 —— static string ToMD5(this string text)
        /// <summary>
        /// 计算字符串MD5值扩展方法
        /// </summary>
        /// <param name="text">待转换的字符串</param>
        /// <returns>MD5值</returns>
        private static string ToMD5(this string text)
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
    }
}
