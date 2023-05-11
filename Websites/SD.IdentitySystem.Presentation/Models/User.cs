using SD.Infrastructure.PresentationBase;

namespace SD.IdentitySystem.Presentation.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : ModelBase
    {
        #region 私钥 —— string PrivateKey
        /// <summary>
        /// 私钥
        /// </summary>
        public string PrivateKey { get; set; }
        #endregion

        #region 状态 —— string Status
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        #endregion

        #region 是否启用 —— bool Enabled
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
        #endregion
    }
}
