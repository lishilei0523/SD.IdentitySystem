using SD.Infrastructure.DTOBase;
using System.Runtime.Serialization;

namespace SD.IdentitySystem.IAppService.DTOs.Outputs
{
    /// <summary>
    /// 角色数据传输对象
    /// </summary>
    [DataContract(Namespace = "http://SD.IdentitySystem.IAppService.DTOs.Outputs")]
    public class RoleInfo : BaseDTO
    {
        #region 信息系统编号 —— string InfoSystemNo
        /// <summary>
        /// 信息系统编号
        /// </summary>
        [DataMember]
        public string InfoSystemNo { get; set; }
        #endregion

        #region 描述 —— string Description
        /// <summary>
        /// 描述
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
