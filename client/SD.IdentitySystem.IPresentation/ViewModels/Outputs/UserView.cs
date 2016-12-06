using ShSoft.Infrastructure.MVC;
using System;

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

        #region 创建时间 —— DateTime AddedTime
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AddedTime { get; set; }
        #endregion
    }
}
