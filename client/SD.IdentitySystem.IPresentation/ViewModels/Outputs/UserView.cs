using SD.Infrastructure.PresentationBase;

namespace SD.IdentitySystem.IPresentation.ViewModels.Outputs
{
    /// <summary>
    /// 用户视图模型
    /// </summary>
    public class UserView : ViewModel
    {
        #region 是否启用 —— bool Enabled
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
        #endregion
    }
}
