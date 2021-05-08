using SD.Infrastructure.PresentationBase;

namespace SD.IdentitySystem.IPresentation.Models.Outputs
{
    /// <summary>
    /// 用户登录记录视图模型
    /// </summary>
    public class LoginRecord : ViewModel
    {
        #region 登录名 —— string LoginId
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginId { get; set; }
        #endregion

        #region 真实姓名 —— string RealName
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        #endregion

        #region IP地址 —— string IP
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }
        #endregion
    }
}
