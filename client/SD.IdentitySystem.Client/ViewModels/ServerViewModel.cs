using SD.IdentitySystem.Client.Commons;

namespace SD.IdentitySystem.Client.ViewModels
{
    /// <summary>
    /// 服务器ViewModel
    /// </summary>
    public class ServerViewModel : DocumentBase
    {
        #region 标题 —— override string Title
        /// <summary>
        /// 标题
        /// </summary>
        public override string Title
        {
            get { return "服务器管理"; }
        }
        #endregion
    }
}
