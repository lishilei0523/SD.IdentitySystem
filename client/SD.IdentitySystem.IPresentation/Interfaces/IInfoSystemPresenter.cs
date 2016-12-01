using System.Collections.Generic;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using ShSoft.Infrastructure;

namespace SD.IdentitySystem.IPresentation.Interfaces
{
    /// <summary>
    /// 信息系统呈现器接口
    /// </summary>
    public interface IInfoSystemPresenter : IPresenter
    {
        #region # 获取信息系统列表 —— IEnumerable<InfoSystemView> GetInfoSystems()
        /// <summary>
        /// 获取信息系统列表
        /// </summary>
        /// <returns>信息系统列表</returns>
        IEnumerable<InfoSystemView> GetInfoSystems();
        #endregion

        #region # 获取信息系统 —— InfoSystemView GetInfoSystem(string systemNo)
        /// <summary>
        /// 获取信息系统
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>信息系统</returns>
        InfoSystemView GetInfoSystem(string systemNo);
        #endregion
    }
}
