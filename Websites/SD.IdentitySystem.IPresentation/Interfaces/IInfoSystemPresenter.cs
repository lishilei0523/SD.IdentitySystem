using SD.IdentitySystem.IPresentation.Models.Outputs;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.PresentationBase;
using System.Collections.Generic;

namespace SD.IdentitySystem.IPresentation.Interfaces
{
    /// <summary>
    /// 信息系统呈现器接口
    /// </summary>
    public interface IInfoSystemPresenter : IPresenter
    {
        #region # 获取信息系统 —— InfoSystem GetInfoSystem(string systemNo)
        /// <summary>
        /// 获取信息系统
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>信息系统</returns>
        InfoSystem GetInfoSystem(string systemNo);
        #endregion

        #region # 获取信息系统列表 —— IEnumerable<InfoSystem> GetInfoSystems()
        /// <summary>
        /// 获取信息系统列表
        /// </summary>
        /// <returns>信息系统列表</returns>
        IEnumerable<InfoSystem> GetInfoSystems();
        #endregion

        #region # 分页获取信息系统列表 —— PageModel<InfoSystem> GetInfoSystemsByPage(string keywords...
        /// <summary>
        /// 分页获取信息系统列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>信息系统列表</returns>
        PageModel<InfoSystem> GetInfoSystemsByPage(string keywords, int pageIndex, int pageSize);
        #endregion
    }
}
