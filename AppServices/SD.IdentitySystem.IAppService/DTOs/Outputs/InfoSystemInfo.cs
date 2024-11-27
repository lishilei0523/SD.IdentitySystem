using SD.Infrastructure.Constants;
using SD.Infrastructure.DTOBase;
using System.Runtime.Serialization;

namespace SD.IdentitySystem.IAppService.DTOs.Outputs
{
    /// <summary>
    /// 信息系统数据传输对象
    /// </summary>
    [DataContract]
    public class InfoSystemInfo : BaseDTO
    {
        #region 管理员用户名 —— string AdminLoginId
        /// <summary>
        /// 管理员用户名
        /// </summary>
        [DataMember]
        public string AdminLoginId { get; set; }
        #endregion

        #region 应用程序类型 —— ApplicationType ApplicationType
        /// <summary>
        /// 应用程序类型
        /// </summary>
        [DataMember]
        public ApplicationType ApplicationType { get; set; }
        #endregion

        #region 主机名 —— string Host
        /// <summary>
        /// 主机名
        /// </summary>
        [DataMember]
        public string Host { get; set; }
        #endregion

        #region 端口 —— int? Port
        /// <summary>
        /// 端口
        /// </summary>
        [DataMember]
        public int? Port { get; set; }
        #endregion

        #region 首页 —— string Index
        /// <summary>
        /// 首页
        /// </summary>
        [DataMember]
        public string Index { get; set; }
        #endregion
    }
}
