using SD.Infrastructure.PresentationBase;

namespace SD.IdentitySystem.IPresentation.Models
{
    /// <summary>
    /// 角色模型
    /// </summary>
    public class Role : ModelBase
    {
        #region 角色描述 —— string Description
        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }
        #endregion


        //Others

        #region 信息系统编号 —— string SystemNo
        /// <summary>
        /// 信息系统编号
        /// </summary>
        public string SystemNo { get; set; }
        #endregion

        #region 信息系统名称 —— string SystemName
        /// <summary>
        /// 信息系统名称
        /// </summary>
        public string SystemName { get; set; }
        #endregion
    }
}
