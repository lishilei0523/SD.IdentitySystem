using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.Presentation.Maps;
using SD.Infrastructure.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Extensions;

namespace SD.IdentitySystem.Presentation.Presenters
{
    /// <summary>
    /// 权限呈现器
    /// </summary>
    public class AuthorityPresenter
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限服务契约接口代理
        /// </summary>
        private readonly ServiceProxy<IAuthorizationContract> _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public AuthorityPresenter(ServiceProxy<IAuthorizationContract> authorizationContract)
        {
            this._authorizationContract = authorizationContract;
        }

        #endregion

        #region # 获取角色相关权限数据项列表 —— ICollection<Item> GetRoleAuthorityItems(Guid roleId)
        /// <summary>
        /// 获取角色相关权限数据项列表
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>权限数据项列表</returns>
        public ICollection<Item> GetRoleAuthorityItems(Guid roleId)
        {
            IAuthorizationContract authorizationContract = this._authorizationContract.Channel;
            RoleInfo role = authorizationContract.GetRole(roleId);
            AuthorityInfo[] systemAuthorities = authorizationContract.GetAuthorities(null, role.SystemNo, null, null, null).ToArray();
            AuthorityInfo[] roleSystemAuthorities = authorizationContract.GetAuthorities(null, null, null, null, role.Id).ToArray();

            ICollection<Item> authorityItems = systemAuthorities.Select(x => x.ToItem()).ToList();
            foreach (Item authorityItem in authorityItems)
            {
                if (roleSystemAuthorities.Any(x => x.Id == authorityItem.Id))
                {
                    authorityItem.IsChecked = true;
                }
            }

            return authorityItems;
        }
        #endregion

        #region # 获取菜单相关权限数据项列表 —— ICollection<Item> GetMenuAuthorityItems(Guid menuId)
        /// <summary>
        /// 获取菜单相关权限数据项列表
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns>权限数据项列表</returns>
        public ICollection<Item> GetMenuAuthorityItems(Guid menuId)
        {
            IAuthorizationContract authorizationContract = this._authorizationContract.Channel;
            MenuInfo menu = authorizationContract.GetMenu(menuId);
            AuthorityInfo[] systemAuthorities = authorizationContract.GetAuthorities(null, menu.SystemNo, menu.ApplicationType, null, null).ToArray();
            AuthorityInfo[] menuSystemAuthorities = authorizationContract.GetAuthorities(null, menu.SystemNo, menu.ApplicationType, menu.Id, null).ToArray();

            ICollection<Item> authorityItems = systemAuthorities.Select(x => x.ToItem()).ToList();
            foreach (Item authorityItem in authorityItems)
            {
                if (menuSystemAuthorities.Any(x => x.Id == authorityItem.Id))
                {
                    authorityItem.IsChecked = true;
                }
            }

            return authorityItems;
        }
        #endregion

        #region # 获取信息系统相关权限数据项列表 —— ICollection<Item> GetSystemAuthorityItems(string systemNo)
        /// <summary>
        /// 获取信息系统相关权限数据项列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>权限数据项列表</returns>
        public ICollection<Item> GetSystemAuthorityItems(string systemNo)
        {
            IEnumerable<AuthorityInfo> systemAuthorities = this._authorizationContract.Channel.GetAuthorities(null, systemNo, null, null, null);
            IEnumerable<Item> authorityItems = systemAuthorities.Select(x => x.ToItem());

            return authorityItems.ToList();
        }
        #endregion
    }
}
