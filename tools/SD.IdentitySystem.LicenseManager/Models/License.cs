using System;

namespace SD.IdentitySystem.LicenseManager.Models
{
    /// <summary>
    /// 许可证
    /// </summary>
    [Serializable]
    public struct License
    {
        /// <summary>
        /// 创建许可证构造器
        /// </summary>
        /// <param name="enterpriseName">企业名称</param>
        /// <param name="uniqueCode">唯一码</param>
        /// <param name="serviceExpiredDate">服务过期日期</param>
        /// <param name="licenseExpiredDate">授权过期日期</param>
        public License(string enterpriseName, string uniqueCode, DateTime serviceExpiredDate, DateTime licenseExpiredDate)
            : this()
        {
            this.EnterpriseName = enterpriseName;
            this.UniqueCode = uniqueCode;
            this.ServiceExpiredDate = serviceExpiredDate;
            this.LicenseExpiredDate = licenseExpiredDate;
        }

        /// <summary>
        /// 企业名称
        /// </summary>
        public string EnterpriseName { get; set; }

        /// <summary>
        /// 唯一码
        /// </summary>
        public string UniqueCode { get; set; }

        /// <summary>
        /// 服务过期日期
        /// </summary>
        public DateTime ServiceExpiredDate { get; set; }

        /// <summary>
        /// 授权过期日期
        /// </summary>
        public DateTime LicenseExpiredDate { get; set; }
    }
}
