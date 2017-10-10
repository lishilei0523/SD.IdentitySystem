using SD.Infrastructure.DTOBase;
using System;
using System.Runtime.Serialization;

namespace SD.IdentitySystem.IAppService.DTOs.Outputs
{
    /// <summary>
    /// 服务器数据传输对象
    /// </summary>
    /// <remarks>勿忘[DataMember]</remarks>
    [DataContract(Namespace = "http://SD.IdentitySystem.IAppService.DTOs.Outputs")]
    public class ServerInfo : BaseDTO
    {
        #region 服务停止日期 —— DateTime ServiceOverDate
        /// <summary>
        /// 服务停止日期
        /// </summary>
        [DataMember]
        public DateTime ServiceOverDate { get; set; }
        #endregion
    }
}
