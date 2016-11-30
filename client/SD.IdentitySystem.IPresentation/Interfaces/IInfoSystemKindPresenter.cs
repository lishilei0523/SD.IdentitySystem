using System.Collections.Generic;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using ShSoft.Infrastructure;

namespace SD.IdentitySystem.IPresentation.Interfaces
{
    /// <summary>
    /// 信息系统类别呈现器接口
    /// </summary>
    public interface IInfoSystemKindPresenter : IPresenter
    {
        #region # 获取信息系统类别列表 —— IEnumerable<InfoSystemKindView> GetSystemKinds()
        /// <summary>
        /// 获取信息系统类别列表
        /// </summary>
        /// <returns>信息系统类别列表</returns>
        IEnumerable<InfoSystemKindView> GetSystemKinds();
        #endregion
    }
}
