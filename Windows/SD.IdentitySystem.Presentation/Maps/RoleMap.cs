using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IPresentation.Models.Outputs;
using SD.Infrastructure.WPF.Models;
using SD.Toolkits.Mapper;

namespace SD.IdentitySystem.Presentation.Maps
{
    /// <summary>
    /// 角色映射工具类
    /// </summary>
    public static class RoleMap
    {
        #region # 角色模型映射 —— static Role ToModel(this RoleInfo...
        /// <summary>
        /// 角色模型映射
        /// </summary>
        /// <param name="roleInfo">角色数据传输对象</param>
        /// <returns>角色模型</returns>
        public static Role ToModel(this RoleInfo roleInfo)
        {
            Role role = roleInfo.Map<RoleInfo, Role>();

            role.SystemName = roleInfo.InfoSystemInfo.Name;

            return role;
        }
        #endregion

        #region # 角色树节点映射 —— static Node ToNode(this Role role)
        /// <summary>
        /// 角色树节点映射
        /// </summary>
        /// <param name="role">角色模型</param>
        /// <returns>树节点</returns>
        public static Node ToNode(this Role role)
        {
            return new Node(role.Id, role.Name, false, null);
        }
        #endregion
    }
}
