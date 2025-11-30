using SD.Infrastructure.DTOBase;
using System;
using System.Runtime.Serialization;

namespace SD.IdentitySystem.IAppService.DTOs.Outputs
{
    /// <summary>
    /// 登录记录数据传输对象
    /// </summary>
    [DataContract]
    public class LoginRecordInfo : BaseDTO
    {
        #region 公钥 —— Guid PublicKey
        /// <summary>
        /// 公钥
        /// </summary>
        [DataMember]
        public Guid PublicKey { get; set; }
        #endregion

        #region 用户名 —— string LoginId
        /// <summary>
        /// 用户名
        /// </summary>
        [DataMember]
        public string LoginId { get; set; }
        #endregion

        #region 真实姓名 —— string RealName
        /// <summary>
        /// 真实姓名
        /// </summary>
        [DataMember]
        public string RealName { get; set; }
        #endregion

        #region IP地址 —— string IP
        /// <summary>
        /// IP地址
        /// </summary>
        [DataMember]
        public string IP { get; set; }
        #endregion

        #region 客户端Id —— string ClientId
        /// <summary>
        /// 客户端Id
        /// </summary>
        [DataMember]
        public string ClientId { get; set; }
        #endregion
    }
}
