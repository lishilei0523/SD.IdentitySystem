using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.Presentation.Maps;
using SD.Infrastructure.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Presentation.Presentors
{
    /// <summary>
    /// 权限呈现器
    /// </summary>
    public class AuthorityPresenter
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限服务接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="authorizationContract">权限服务接口</param>
        public AuthorityPresenter(IAuthorizationContract authorizationContract)
        {
            this._authorizationContract = authorizationContract;
        }

        #endregion

        #region # 获取信息系统的权限树 —— Node GetAuthorityTree(string systemNo)
        /// <summary>
        /// 获取信息系统的权限树
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>权限树</returns>
        public Node GetAuthorityTree(string systemNo)
        {
            IEnumerable<AuthorityInfo> authorities = this._authorizationContract.GetAuthorities(null, systemNo, null, null);
            InfoSystemInfo infoSystem = this._authorizationContract.GetInfoSystem(systemNo);
            Node node = infoSystem.ToNode(authorities);

            return node;
        }
        #endregion

        #region # 获取角色的权限树 —— Node GetAuthorityTreeByRole(Guid roleId)
        /// <summary>
        /// 获取角色的权限树
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>权限树</returns>
        public Node GetAuthorityTreeByRole(Guid roleId)
        {
            //获取当前角色及其权限集
            RoleInfo currentRole = this._authorizationContract.GetRole(roleId);
            IEnumerable<AuthorityInfo> roleAuthorities = this._authorizationContract.GetAuthorities(null, null, null, roleId).ToArray();

            //获取角色所在信息系统的权限树
            Node authroityTree = this.GetAuthorityTree(currentRole.SystemNo);

            //遍历子节点集（权限集）
            foreach (Node node in authroityTree.SubNodes)
            {
                //如果角色中含有该权限，则选中
                if (roleAuthorities.Any(x => x.Id == node.Id))
                {
                    node.IsChecked = true;
                }
            }

            return authroityTree;
        }
        #endregion

        #region # 获取菜单的权限树 —— Node GetAuthorityTreeByMenu(Guid menuId)
        /// <summary>
        /// 获取菜单的权限树
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns>权限树</returns>
        public Node GetAuthorityTreeByMenu(Guid menuId)
        {
            //获取当前菜单及其权限集
            MenuInfo currentMenu = this._authorizationContract.GetMenu(menuId);
            IEnumerable<AuthorityInfo> menuAuthorities = this._authorizationContract.GetAuthorities(null, null, menuId, null).ToArray();

            //获取菜单所在信息系统的权限树
            Node authroityTree = this.GetAuthorityTree(currentMenu.SystemNo);

            //遍历子节点集（权限集）
            foreach (Node node in authroityTree.SubNodes)
            {
                //如果菜单中含有该权限，则选中
                if (menuAuthorities.Any(x => x.Id == node.Id))
                {
                    node.IsChecked = true;
                }
            }

            return authroityTree;
        }
        #endregion
    }
}
