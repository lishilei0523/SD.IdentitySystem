using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using ShSoft.Common.PoweredByLee;

namespace SD.IdentitySystem.Presentation.Maps
{
    /// <summary>
    /// 信息系统类别映射工具类
    /// </summary>
    public static class InfoSytemKindMap
    {
        #region # 信息系统类别视图模型映射 —— static InfoSystemKindView ToViewModel(this...
        /// <summary>
        /// 信息系统类别视图模型映射
        /// </summary>
        /// <param name="systemKindInfo">信息系统类别数据传输对象</param>
        /// <returns>信息系统类别视图模型</returns>
        public static InfoSystemKindView ToViewModel(this InfoSystemKindInfo systemKindInfo)
        {
            InfoSystemKindView systemKindView = Transform<InfoSystemKindInfo, InfoSystemKindView>.Map(systemKindInfo);

            systemKindView.ApplicationTypeName = systemKindInfo.ApplicationType.GetEnumMember();

            return systemKindView;
        }
        #endregion
    }
}
