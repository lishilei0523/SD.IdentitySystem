using SD.Infrastructure.PresentationBase;

namespace SD.IdentitySystem.IPresentation.Models
{
    /// <summary>
    /// 权限模型
    /// </summary>
    public class Authority : ModelBase
    {
        #region 权限路径 —— string AuthorityPath
        /// <summary>
        /// 权限路径
        /// </summary>
        public string AuthorityPath { get; set; }
        #endregion

        #region 描述 —— string Description
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        #endregion


        //Others

        #region 信息系统编号 —— string InfoSystemNo
        /// <summary>
        /// 信息系统编号
        /// </summary>
        public string InfoSystemNo { get; set; }
        #endregion

        #region 信息系统名称 —— string InfoSystemName
        /// <summary>
        /// 信息系统名称
        /// </summary>
        public string InfoSystemName { get; set; }
        #endregion

        #region 应用程序类型名称 —— string ApplicationTypeName
        /// <summary>
        /// 应用程序类型名称
        /// </summary>
        public string ApplicationTypeName { get; set; }
        #endregion
    }
}
