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
        #region # 信息系统类别映射 —— static InfoSystemKindInfo ToDTO(this InfoSystemKind systemKind)
        /// <summary>
        /// 信息系统类别映射
        /// </summary>
        /// <param name="systemKind">信息系统类别领域模型</param>
        /// <returns>信息系统类别数据传输对象</returns>
        public static InfoSystemKindInfo ToDTO(this InfoSystemKind systemKind)
        {
            return Transform<InfoSystemKind, InfoSystemKindInfo>.Map(systemKind);
        }
        #endregion

        #region # 菜单映射 —— static MenuInfo ToDTO(this Menu menu...
        /// <summary>
        /// 菜单映射
        /// </summary>
        /// <param name="menu">菜单领域模型</param>
        /// <param name="infoSystemKindInfos">信息系统类别数据传输对象字典</param>
        /// <returns>菜单数据传输对象</returns>
        public static MenuInfo ToDTO(this Menu menu, IDictionary<string, InfoSystemKindInfo> infoSystemKindInfos)
        {
            MenuInfo menuInfo = Transform<Menu, MenuInfo>.Map(menu);

            menuInfo.InfoSystemKindInfo = infoSystemKindInfos[menu.SystemKindNo];

            menuInfo.ParentMenu = menu.ParentNode == null ? null : menu.ParentNode.ToDTO(infoSystemKindInfos);

            return menuInfo;
        }
        #endregion

        #region # 权限映射 —— static AuthorityInfo ToDTO(this Authority authority)
        /// <summary>
        /// 权限映射
        /// </summary>
        /// <param name="authority">权限领域模型</param>
        /// <returns>权限数据传输对象</returns>
        public static AuthorityInfo ToDTO(this Authority authority)
        {
            return Transform<Authority, AuthorityInfo>.Map(authority);
        }
        #endregion

        #region # 角色映射 —— static RoleInfo ToDTO(this Role role)
        /// <summary>
        /// 角色映射
        /// </summary>
        /// <param name="role">角色领域模型</param>
        /// <returns>角色数据传输对象</returns>
        public static RoleInfo ToDTO(this Role role)
        {
            return Transform<Role, RoleInfo>.Map(role);
        }
        #endregion
    }
}