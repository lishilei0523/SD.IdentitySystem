using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using ShSoft.Infrastructure.MVC;
using System.Collections.Generic;
using ShSoft.Infrastructure.DTOBase;

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

        #region # 分页获取信息系统列表 —— PageModel<InfoSystemView> GetInfoSystems(string keywords...
        /// <summary>
        /// 分页获取信息系统列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>信息系统列表</returns>
        PageModel<InfoSystemView> GetInfoSystems(string keywords, int pageIndex, int pageSize);
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
