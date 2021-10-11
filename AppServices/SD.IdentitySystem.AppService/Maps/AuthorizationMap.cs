using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.Infrastructure.MemberShip;
using SD.Toolkits.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.AppService.Maps
{
    /// <summary>
    /// 权限相关映射工具类
    /// </summary>
    public static class AuthorizationMap
    {
        #region # 信息系统映射 —— static InfoSystemInfo ToDTO(this InfoSystem infoSystem)
        /// <summary>
        /// 信息系统映射
        /// </summary>
        public static InfoSystemInfo ToDTO(this InfoSystem infoSystem)
        {
            InfoSystemInfo systemInfo = infoSystem.Map<InfoSystem, InfoSystemInfo>();

            return systemInfo;
        }
        #endregion

        #region # 信息系统登录信息映射 —— static LoginSystemInfo ToLoginSystemInfo(this InfoSystem infoSystem)
        /// <summary>
        /// 信息系统登录信息映射
        /// </summary>
        public static LoginSystemInfo ToLoginSystemInfo(this InfoSystem infoSystem)
        {
            return new LoginSystemInfo(infoSystem.Number, infoSystem.Name, infoSystem.ApplicationType, infoSystem.Index);
        }
        #endregion

        #region # 菜单映射 —— static MenuInfo ToDTO(this Menu menu...
        /// <summary>
        /// 菜单映射
        /// </summary>
        public static MenuInfo ToDTO(this Menu menu, IDictionary<string, InfoSystemInfo> systemInfos)
        {
            MenuInfo menuInfo = menu.Map<Menu, MenuInfo>();
            menuInfo.InfoSystemInfo = systemInfos[menu.SystemNo];
            menuInfo.ParentMenuId = menu.ParentNode?.Id;

            return menuInfo;
        }
        #endregion

        #region # 菜单登录信息映射 —— static LoginMenuInfo ToLoginMenuInfoNode(this Menu menu)
        /// <summary>
        /// 菜单登录信息映射
        /// </summary>
        public static LoginMenuInfo ToLoginMenuInfoNode(this Menu menu)
        {
            return new LoginMenuInfo
            {
                SystemNo = menu.SystemNo,
                ApplicationType = menu.ApplicationType,
                ParentId = menu.ParentNode?.Id,
                Id = menu.Id,
                Name = menu.Name,
                Sort = menu.Sort,
                Icon = menu.Icon,
                Path = menu.Path,
                Url = menu.Url
            };
        }
        #endregion

        #region # 菜单登录信息树映射 —— static ICollection<LoginMenuInfo> ToLoginMenuInfoTree(this...
        /// <summary>
        /// 菜单登录信息树映射
        /// </summary>
        public static ICollection<LoginMenuInfo> ToLoginMenuInfoTree(this IEnumerable<Menu> menus, Guid? parentId)
        {
            //验证
            menus = menus?.ToArray() ?? Array.Empty<Menu>();

            //声明容器
            ICollection<LoginMenuInfo> loginMenuInfos = new HashSet<LoginMenuInfo>();

            //判断父级菜单Id是否为null
            if (!parentId.HasValue)
            {
                //从根级开始遍历
                foreach (Menu menu in menus.OrderBy(x => x.Sort).Where(x => x.IsRoot))
                {
                    LoginMenuInfo menuInfo = menu.ToLoginMenuInfoNode();
                    loginMenuInfos.Add(menuInfo);
                    menuInfo.SubMenuInfos = menus.ToLoginMenuInfoTree(menuInfo.Id);
                }
            }
            else
            {
                //从给定Id向下遍历
                foreach (Menu menu in menus.OrderBy(x => x.Sort).Where(x => x.ParentNode != null && x.ParentNode.Id == parentId.Value))
                {
                    LoginMenuInfo menuInfo = menu.ToLoginMenuInfoNode();
                    loginMenuInfos.Add(menuInfo);
                    menuInfo.SubMenuInfos = menus.ToLoginMenuInfoTree(menuInfo.Id);
                }
            }

            return loginMenuInfos;
        }
        #endregion

        #region # 权限映射 —— static AuthorityInfo ToDTO(this Authority authority...
        /// <summary>
        /// 权限映射
        /// </summary>
        public static AuthorityInfo ToDTO(this Authority authority, IDictionary<string, InfoSystemInfo> systemInfos)
        {
            AuthorityInfo authorityInfo = authority.Map<Authority, AuthorityInfo>();
            authorityInfo.InfoSystemInfo = systemInfos[authority.SystemNo];

            return authorityInfo;
        }
        #endregion

        #region # 权限登录信息映射 —— static LoginAuthorityInfo ToLoginAuthorityInfo(this Authority authority)
        /// <summary>
        /// 权限登录信息映射
        /// </summary>
        public static LoginAuthorityInfo ToLoginAuthorityInfo(this Authority authority)
        {
            return new LoginAuthorityInfo(authority.SystemNo, authority.ApplicationType, authority.Id, authority.Name, authority.AuthorityPath, authority.EnglishName);
        }
        #endregion

        #region # 角色映射 —— static RoleInfo ToDTO(this Role role...
        /// <summary>
        /// 角色映射
        /// </summary>
        public static RoleInfo ToDTO(this Role role, IDictionary<string, InfoSystemInfo> systemInfos)
        {
            RoleInfo roleInfo = role.Map<Role, RoleInfo>();
            roleInfo.InfoSystemInfo = systemInfos[role.SystemNo];

            return roleInfo;
        }
        #endregion
    }
}