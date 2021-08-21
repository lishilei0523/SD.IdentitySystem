using SD.Infrastructure.PresentationBase;

namespace SD.IdentitySystem.IPresentation.Models
{
    /// <summary>
    /// 用户模型
    /// </summary>
    public class User : ModelBase
    {
        #region 私钥 —— string PrivateKey
        /// <summary>
        /// 私钥
        /// </summary>
        public string PrivateKey { get; set; }
        #endregion

        #region 是否启用 —— bool Enabled
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
        #endregion
    }
}
