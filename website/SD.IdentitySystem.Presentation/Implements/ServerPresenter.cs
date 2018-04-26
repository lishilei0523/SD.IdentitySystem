using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using SD.IdentitySystem.Presentation.Maps;
using SD.Infrastructure.DTOBase;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Presentation.Implements
{
    /// <summary>
    /// 服务器呈现器实现
    /// </summary>
    public class ServerPresenter : IServerPresenter
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限服务接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="authorizationContract">权限服务接口</param>
        public ServerPresenter(IAuthorizationContract authorizationContract)
        {
            this._authorizationContract = authorizationContract;
        }

        #endregion

        #region # 分页获取服务器列表 —— PageModel<ServerView> GetServersByPage(string keywords...
        /// <summary>
        /// 分页获取服务器列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>服务器列表</returns>
        public PageModel<ServerView> GetServersByPage(string keywords, int pageIndex, int pageSize)
        {
            PageModel<ServerInfo> pageModel = this._authorizationContract.GetServersByPage(keywords, pageIndex, pageSize);
            IEnumerable<ServerView> serverViews = pageModel.Datas.Select(x => x.ToViewModel());

            return new PageModel<ServerView>(serverViews, pageModel.PageIndex, pageModel.PageSize, pageModel.PageCount, pageModel.RowCount);
        }
        #endregion

        #region # 获取服务器 —— ServerView GetServer(string uniqueCode)
        /// <summary>
        /// 获取服务器
        /// </summary>
        /// <param name="uniqueCode">服务器机器唯一码</param>
        /// <returns>服务器</returns>
        public ServerView GetServer(string uniqueCode)
        {
            ServerInfo currentServerInfo = this._authorizationContract.GetServer(uniqueCode);
            ServerView currentServerView = currentServerInfo.ToViewModel();

            return currentServerView;
        }
        #endregion
    }
}
