using System.Collections.Generic;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using ShSoft.Common.PoweredByLee;

namespace SD.IdentitySystem.AppService.Maps
{
    /// <summary>
    /// 权限相关映射工具类
    /// </summary>
    public static class AuthorizationMap
    {
        #region # 菜单映射 —— static MenuInfo ToDTO(this Menu menu...
        /// <summary>
        /// 菜单映射
        /// </summary>
        /// <param name="menu">菜单领域模型</param>
        /// <param name="systemInfos">信息系统数据传输对象字典</param>
        /// <returns>菜单数据传输对象</returns>
        public static MenuInfo ToDTO(this Menu menu, IDictionary<string, InfoSystemInfo> systemInfos)
        {
            MenuInfo menuInfo = Transform<Menu, MenuInfo>.Map(menu);

            menuInfo.InfoSystemInfo = systemInfos[menu.SystemNo];

            menuInfo.ParentMenu = menu.ParentNode == null ? null : menu.ParentNode.ToDTO(systemInfos);

            return menuInfo;
        }
        #endregion

        #region # 权限映射 —— static AuthorityInfo ToDTO(this Authority authority...
        /// <summary>
        /// 权限映射
        /// </summary>
        /// <param name="authority">权限领域模型</param>
        /// <param name="systemInfos">信息系统数据传输对象字典</param>
        /// <returns>权限数据传输对象</returns>
        public static AuthorityInfo ToDTO(this Authority authority, IDictionary<string, InfoSystemInfo> systemInfos)
        {
            AuthorityInfo authorityInfo = Transform<Authority, AuthorityInfo>.Map(authority);

            authorityInfo.InfoSystemInfo = systemInfos[authority.SystemNo];

            return authorityInfo;
        }
        #endregion

        #region # 角色映射 —— static RoleInfo ToDTO(this Role role...
        /// <summary>
        /// 角色映射
        /// </summary>
        /// <param name="role">角色领域模型</param>
        /// <param name="systemInfos">信息系统数据传输对象字典</param>
        /// <returns>角色数据传输对象</returns>
        public static RoleInfo ToDTO(this Role role, IDictionary<string, InfoSystemInfo> systemInfos)
        {
            RoleInfo roleInfo = Transform<Role, RoleInfo>.Map(role);

            roleInfo.InfoSystemInfo = systemInfos[role.SystemNo];

            return roleInfo;
        }
        #endregion
    }
}