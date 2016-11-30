using ShSoft.Infrastructure;

namespace SD.IdentitySystem.IPresentation.ViewModels.Outputs
{
    /// <summary>
    /// 角色视图模型
    /// </summary>
    public class RoleView : ViewModel
    {
        #region 信息系统类别编号 —— string SystemKindNo
        /// <summary>
        /// 信息系统类别编号
        /// </summary>
        public string SystemKindNo { get; set; }
        #endregion

        #region 角色描述 —— string Description
        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }
        #endregion
    }
}
