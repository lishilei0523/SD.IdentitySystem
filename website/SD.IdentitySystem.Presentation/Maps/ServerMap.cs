using SD.Common.PoweredByLee;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;

namespace SD.IdentitySystem.Presentation.Maps
{
    /// <summary>
    /// 服务器映射工具类
    /// </summary>
    public static class ServerMap
    {
        #region # 服务器映射 —— static ServerView ToViewModel(this ServerInfo serverInfo)
        /// <summary>
        /// 服务器映射
        /// </summary>
        public static ServerView ToViewModel(this ServerInfo serverInfo)
        {
            ServerView serverView = Transform<ServerInfo, ServerView>.Map(serverInfo);

            return serverView;
        }
        #endregion
    }
}
