using SD.Common;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.Infrastructure.WPF.Models;

namespace SD.IdentitySystem.Presentation.Maps
{
    /// <summary>
    /// 权限映射工具类
    /// </summary>
    public static class AuthorityMap
    {
        #region # 权限数据项映射 —— static Item ToItem(this AuthorityInfo authority)
        /// <summary>
        /// 权限数据项映射
        /// </summary>
        public static Item ToItem(this AuthorityInfo authority)
        {
            return new Item(authority.Id, authority.Name, false, false, authority.ApplicationType.GetEnumMember());
        }
        #endregion
    }
}
