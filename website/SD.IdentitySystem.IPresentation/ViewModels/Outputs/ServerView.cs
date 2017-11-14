using SD.Infrastructure.PresentationBase;
using System;

namespace SD.IdentitySystem.IPresentation.ViewModels.Outputs
{
    /// <summary>
    /// 服务器视图模型
    /// </summary>
    public class ServerView : ViewModel
    {
        #region 服务停止日期 —— DateTime ServiceOverDate
        /// <summary>
        /// 服务停止日期
        /// </summary>
        public DateTime ServiceOverDate { get; set; }
        #endregion
    }
}
