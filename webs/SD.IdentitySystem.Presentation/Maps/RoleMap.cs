using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.Presentation.EasyUI;
using SD.IdentitySystem.Presentation.Models;
using SD.Toolkits.Mapper;

namespace SD.IdentitySystem.Presentation.Maps
{
    /// <summary>
    /// 角色映射
    /// </summary>
    public static class RoleMap
    {
        #region # 角色映射 —— static Role ToModel(this RoleInfo...
        /// <summary>
        /// 角色映射
        /// </summary>
        public static Role ToModel(this RoleInfo roleInfo)
        {
            Role role = roleInfo.Map<RoleInfo, Role>();
            role.InfoSystemName = roleInfo.InfoSystemInfo?.Name;

            return role;
        }
        #endregion

        #region # 角色EasyUI树节点映射 —— static Node ToNode(this Role role)
        /// <summary>
        /// 角色EasyUI树节点映射
        /// </summary>
        public static Node ToNode(this Role role)
        {
            var attributes = new
            {
                type = "role"
            };

            return new Node(role.Id, role.Name, "open", false, attributes);
        }
        #endregion
    }
}
