using System.Runtime.Serialization;
using ShSoft.Infrastructure.DTOBase;

namespace SD.IdentitySystem.IAppService.DTOs.Outputs
{
    /// <summary>
    /// 用户数据传输对象
    /// </summary>
    [DataContract(Namespace = "http://SD.IdentitySystem.IAppService.DTOs.Outputs")]
    public class UserInfo : BaseDTO
    {
        #region 是否启用 —— bool Enabled
        /// <summary>
        /// 是否启用
        /// </summary>
        [DataMember]
        public bool Enabled { get; set; }
        #endregion
    }
}
