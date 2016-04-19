using ShSoft.Framework2016.Common.PoweredByLee;
using ShSoft.UAC.Domain.Entities;
using ShSoft.UAC.IAppService.DTOs.Inputs;
using ShSoft.UAC.IAppService.DTOs.Outputs;

namespace ShSoft.UAC.AppService.Maps
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

        #region # 菜单映射 —— static MenuInfo ToDTO(this Menu menu)
        /// <summary>
        /// 菜单映射
        /// </summary>
        /// <param name="menu">菜单领域模型</param>
        /// <returns>菜单数据传输对象</returns>
        public static MenuInfo ToDTO(this Menu menu)
        {
            MenuInfo menuInfo = Transform<Menu, MenuInfo>.Map(menu);
            menuInfo.ParentMenu = menu.ParentNode == null ? null : menu.ParentNode.ToDTO();
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

        #region # 权限映射领域模型 —— static Authority ToDomainModel(this AuthorityParam authorityParam)
        /// <summary>
        /// 权限映射领域模型
        /// </summary>
        /// <param name="authorityParam">权限参数模型</param>
        /// <returns>权限领域模型</returns>
        public static Authority ToDomainModel(this AuthorityParam authorityParam)
        {
            return new Authority(authorityParam.AuthorityName, authorityParam.EnglishName, authorityParam.Description, authorityParam.AssemblyName, authorityParam.Namespace, authorityParam.ClassName, authorityParam.MethodName);
        }
        #endregion
    }
}