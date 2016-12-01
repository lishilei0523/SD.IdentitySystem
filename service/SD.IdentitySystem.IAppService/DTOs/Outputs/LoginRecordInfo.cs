using System;
using System.Runtime.Serialization;
using ShSoft.Infrastructure.DTOBase;

namespace SD.IdentitySystem.IAppService.DTOs.Outputs
{
    /// <summary>
    /// 登录记录数据传输对象
    /// </summary>
    /// <remarks>勿忘[DataMember]</remarks>
    [DataContract(Namespace = "http://SD.IdentitySystem.IAppService.DTOs.Outputs")]
    public class LoginRecordInfo : BaseDTO
    {
        #region 公钥 —— Guid PublicKey
        /// <summary>
        /// 公钥
        /// </summary>
        [DataMember]
        public Guid PublicKey { get; set; }
        #endregion

        #region 登录名 —— string LoginId
        /// <summary>
        /// 登录名
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
    }
}
