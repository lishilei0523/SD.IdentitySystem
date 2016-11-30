using ShSoft.Infrastructure;

namespace SD.IdentitySystem.IPresentation.ViewModels.Outputs
{
    /// <summary>
    /// 信息系统视图模型
    /// </summary>
    public class InfoSystemView : ViewModel
    {
        #region 管理员登录名 —— string AdminLoginId
        /// <summary>
        /// 管理员登录名
        /// </summary>
        public string AdminLoginId { get; set; }
        #endregion

        #region 信息系统类别编号 —— string SystemKindNo
        /// <summary>
        /// 信息系统类别编号
        /// </summary>
        public string SystemKindNo { get; set; }
        #endregion


        //Others

        #region 信息系统类别名称 —— string SystemKindName
        /// <summary>
        /// 信息系统类别名称
        /// </summary>
        public string SystemKindName { get; set; }
        #endregion
    }
}
