using System.Runtime.Serialization;
using ShSoft.Infrastructure.DTOBase;

namespace SD.IdentitySystem.IAppService.DTOs.Outputs
{
    /// <summary>
    /// 角色数据传输对象
    /// </summary>
    [DataContract(Namespace = "http://SD.IdentitySystem.IAppService.DTOs.Outputs")]
    public class RoleInfo : BaseDTO
    {
        #region 信息系统类别编号 —— string SystemKindNo
        /// <summary>
        /// 信息系统类别编号
        /// </summary>
        [DataMember]
        public string SystemKindNo { get; set; }
        #endregion

        #region 角色描述 —— string Description
        /// <summary>
        /// 角色描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }
        #endregion
    }
}
