using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace SD.IdentitySystem.LicenseManager.Tookits
{
    /// <summary>
    /// 唯一码
    /// </summary>
    public static class UniqueCode
    {
        #region # 计算机器唯一码 —— static string Compute()
        /// <summary>
        /// 计算机器唯一码
        /// </summary>
        public static string Compute()
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

            string machineCode = physicalAddress.ToString().ToHash();

            return machineCode;
        }
        #endregion
    }
}
