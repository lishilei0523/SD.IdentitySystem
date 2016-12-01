using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using ShSoft.Common.PoweredByLee;

namespace SD.IdentitySystem.Presentation.Maps
{
    /// <summary>
    /// 角色映射工具类
    /// </summary>
    public static class RoleMap
    {
        #region # 角色视图模型映射 —— static RoleView ToViewModel(this RoleInfo...
        /// <summary>
        /// 角色视图模型映射
        /// </summary>
        /// <param name="roleInfo">角色数据传输对象</param>
        /// <returns>角色视图模型</returns>
        public static RoleView ToViewModel(this RoleInfo roleInfo)
        {
            RoleView roleView = Transform<RoleInfo, RoleView>.Map(roleInfo);

            roleView.SystemNo = roleInfo.InfoSystemInfo.Number;
            roleView.SystemName = roleInfo.InfoSystemInfo.Name;

            return roleView;
        }
        #endregion
    }
}
