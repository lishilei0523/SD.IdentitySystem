using System.Runtime.Serialization;
using ShSoft.Framework2016.Infrastructure.IDTO;

namespace SD.UAC.IAppService.DTOs.Outputs
{
    /// <summary>
    /// 信息系统数据传输对象
    /// </summary>
    [DataContract(Namespace = "http://ShSoft.UAC.IAppService.DTOs.Outputs")]
    public class InfoSystemInfo : BaseDTO
    {
        #region 组织编号 —— string OrganizationNo
        /// <summary>
        /// 组织编号
        /// </summary>
        [DataMember]
        public string OrganizationNo { get; set; }
        #endregion

        #region 管理员登录名 —— string AdminLoginId
        /// <summary>
        /// 管理员登录名
        /// </summary>
        [DataMember]
        public string AdminLoginId { get; set; }
        #endregion


        //导航属性

        #region 信息系统类别 —— InfoSystemKindInfo InfoSystemKindInfo
        /// <summary>
        /// 信息系统类别
        /// </summary>
        [DataMember]
        public InfoSystemKindInfo InfoSystemKindInfo { get; set; }
        #endregion
    }
}
