using SD.Infrastructure.PresentationBase;

namespace SD.IdentitySystem.IPresentation.Models
{
    /// <summary>
    /// 用户登录记录模型
    /// </summary>
    public class LoginRecord : ModelBase
    {
        #region 用户名 —— string LoginId
        /// <summary>
        /// 用户名
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

        #region 客户端Id —— string ClientId
        /// <summary>
        /// 客户端Id
        /// </summary>
        public string ClientId { get; set; }
        #endregion
    }
}
