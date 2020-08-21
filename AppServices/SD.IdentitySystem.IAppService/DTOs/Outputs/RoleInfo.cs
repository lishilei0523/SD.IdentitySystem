using System.Runtime.Serialization;
using SD.Infrastructure.DTOBase;

namespace SD.IdentitySystem.IAppService.DTOs.Outputs
{
    /// <summary>
    /// 角色数据传输对象
    /// </summary>
    [DataContract(Namespace = "http://SD.IdentitySystem.IAppService.DTOs.Outputs")]
    public class RoleInfo : BaseDTO
    {
        #region 信息系统编号 —— string SystemNo
        /// <summary>
        /// 信息系统编号
        /// </summary>
        [DataMember]
        public string SystemNo { get; set; }
        #endregion

        #region 角色描述 —— string Description
        /// <summary>
        /// 角色描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }
        #endregion


        //导航属性

        #region 导航属性 - 信息系统 —— InfoSystemInfo InfoSystemInfo
        /// <summary>
        /// 导航属性 - 信息系统
        /// </summary>
        [DataMember]
        public InfoSystemInfo InfoSystemInfo { get; set; }
        #endregion
    }
}
