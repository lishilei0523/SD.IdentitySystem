using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using ShSoft.Common.PoweredByLee;

namespace SD.IdentitySystem.Presentation.Maps
{
    /// <summary>
    /// 信息系统映射工具类
    /// </summary>
    public static class InfoSytemMap
    {
        #region # 信息系统视图模型映射 —— static InfoSystemView ToViewModel(this InfoSystemInfo...
        /// <summary>
        /// 信息系统视图模型映射
        /// </summary>
        /// <param name="systemInfo">信息系统数据传输对象</param>
        /// <returns>信息系统视图模型</returns>
        public static InfoSystemView ToViewModel(this InfoSystemInfo systemInfo)
        {
            InfoSystemView systemView = Transform<InfoSystemInfo, InfoSystemView>.Map(systemInfo);

            systemView.SystemKindName = systemInfo.InfoSystemKindInfo.Name;

            return systemView;
        }
        #endregion
    }
}
