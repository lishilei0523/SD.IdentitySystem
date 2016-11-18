using System.Runtime.Serialization;
using ShSoft.Infrastructure.DTOBase;

namespace SD.IdentitySystem.IAppService.DTOs.Outputs
{
    /// <summary>
    /// 信息系统数据传输对象
    /// </summary>
    [DataContract(Namespace = "http://SD.IdentitySystem.IAppService.DTOs.Outputs")]
    public class InfoSystemInfo : BaseDTO
    {
        #region 管理员登录名 —— string AdminLoginId
        /// <summary>
        /// 管理员登录名
        /// </summary>
        [DataMember]
        public string AdminLoginId { get; set; }
        #endregion

        #region 信息系统类别编号 —— string SystemKindNo
        /// <summary>
        /// 信息系统类别编号
        /// </summary>
        [DataMember]
        public string SystemKindNo { get; set; }
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
