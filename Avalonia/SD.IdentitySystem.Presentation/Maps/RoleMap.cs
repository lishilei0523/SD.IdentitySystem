using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.Infrastructure.Avalonia.Models;

namespace SD.IdentitySystem.Presentation.Maps
{
    /// <summary>
    /// 角色映射
    /// </summary>
    public static class RoleMap
    {
        #region # 角色数据项映射 —— static Item ToItem(this RoleInfo role)
        /// <summary>
        /// 角色数据项映射
        /// </summary>
        public static Item ToItem(this RoleInfo role)
        {
            return new Item(role.Id, null, role.Name, false, false, role.InfoSystemInfo.Name);
        }
        #endregion
    }
}
