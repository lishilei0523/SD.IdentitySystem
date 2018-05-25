using Microsoft.Win32;
using System;
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
            //常量
            const string keyPrefix = @"SYSTEM\CurrentControlSet\Control\Network\{4D36E972-E325-11CE-BFC1-08002BE10318}";
            const string connection = "Connection";
            const string pnpInstanceIdKey = "PnpInstanceID";
            const string pci = "PCI";
            const string wireless = "wireless";
            const string mediaSubTypeKey = "MediaSubType";

            PhysicalAddress physicalAddress = null;
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            #region # 优先物理网卡

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                string registryKeyPath = $@"{keyPrefix}\{networkInterface.Id}\{connection}";
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(registryKeyPath, false);
                string pnpInstanceId = registryKey?.GetValue(pnpInstanceIdKey, string.Empty).ToString();

                //真实网卡
                if (pnpInstanceId != null && pnpInstanceId.Length > 3 && pnpInstanceId.Substring(0, 3) == pci)
                {
                    //物理网卡
                    if (networkInterface.NetworkInterfaceType.ToString().ToLower().IndexOf(wireless, StringComparison.Ordinal) == -1)
                    {
                        physicalAddress = networkInterface.GetPhysicalAddress();
                        break;
                    }
                }
            }

            #endregion

            #region # 次优无线网卡

            if (physicalAddress == null)
            {
                foreach (NetworkInterface networkInterface in networkInterfaces)
                {
                    string registryKeyPath = $@"{keyPrefix}\{networkInterface.Id}\{connection}";
                    RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(registryKeyPath, false);
                    string pnpInstanceId = registryKey?.GetValue(pnpInstanceIdKey, string.Empty).ToString();

                    //真实网卡
                    if (pnpInstanceId != null && pnpInstanceId.Length > 3 && pnpInstanceId.Substring(0, 3) == pci)
                    {
                        //无线网卡
                        physicalAddress = networkInterface.GetPhysicalAddress();
                        break;
                    }
                }
            }

            #endregion

            #region # 再次虚拟网卡

            if (physicalAddress == null)
            {
                foreach (NetworkInterface networkInterface in networkInterfaces)
                {
                    string registryKeyPath = $@"{keyPrefix}\{networkInterface.Id}\{connection}";
                    RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(registryKeyPath, false);
                    int? mediaSubType = registryKey == null
                        ? (int?)null
                        : Convert.ToInt32(registryKey.GetValue(mediaSubTypeKey, 0));

                    //再次虚拟网卡
                    if (mediaSubType == 1 || mediaSubType == 0)
                    {
                        physicalAddress = networkInterface.GetPhysicalAddress();
                        break;
                    }
                }
            }

            #endregion

            if (physicalAddress == null)
            {
                throw new SystemException("请检查机器是否存在网卡！");
            }

            string machineCode = physicalAddress.ToString().ToHash();

            return machineCode;
        }
        #endregion
    }
}
