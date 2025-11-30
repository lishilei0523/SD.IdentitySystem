using SD.Infrastructure.DTOBase;
using System.Runtime.Serialization;

namespace SD.IdentitySystem.IAppService.DTOs.Outputs
{
    /// <summary>
    /// 用户数据传输对象
    /// </summary>
    [DataContract]
    public class UserInfo : BaseDTO
    {
        #region 私钥 —— string PrivateKey
        /// <summary>
        /// 私钥
        /// </summary>
        [DataMember]
        public string PrivateKey { get; set; }
        #endregion

        #region 是否启用 —— bool Enabled
        /// <summary>
        /// 是否启用
        /// </summary>
        [DataMember]
        public bool Enabled { get; set; }
        #endregion
    }
}
