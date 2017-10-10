using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.MVC;
using System;

namespace SD.IdentitySystem.IPresentation.Interfaces
{
    /// <summary>
    /// 服务器呈现器接口
    /// </summary>
    public interface IServerPresenter : IPresenter
    {
        #region # 分页获取服务器列表 —— PageModel<ServerView> GetServersByPage(string keywords...
        /// <summary>
        /// 分页获取服务器列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>服务器列表</returns>
        PageModel<ServerView> GetServersByPage(string keywords, int pageIndex, int pageSize);
        #endregion

        #region # 获取服务器 —— ServerView GetServer(Guid serverId)
        /// <summary>
        /// 获取服务器
        /// </summary>
        /// <param name="serverId">服务器Id</param>
        /// <returns>服务器</returns>
        ServerView GetServer(Guid serverId);
        #endregion
    }
}
