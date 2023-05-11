using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.Presentation.Maps;
using SD.IdentitySystem.Presentation.Models;
using SD.Infrastructure.Constants;
using SD.Infrastructure.DTOBase;
using SD.Toolkits.EasyUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Presentation.Presenters
{
    /// <summary>
    /// 权限呈现器
    /// </summary>
    public class AuthorityPresenter
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限管理服务契约接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public AuthorityPresenter(IAuthorizationContract authorizationContract)
        {
            this._authorizationContract = authorizationContract;
        }

        #endregion

        #region # 获取权限 —— Authority GetAuthority(Guid authorityId)
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="authorityId">权限Id</param>
        /// <returns>权限</returns>
        public Authority GetAuthority(Guid authorityId)
        {
            AuthorityInfo authorityInfo = this._authorizationContract.GetAuthority(authorityId);

            return authorityInfo.ToModel();
        }
        #endregion

        #region # 获取信息系统的权限树 —— Node GetAuthorityTree(string infoSystemNo...
        /// <summary>
        /// 获取信息系统的权限树
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>权限树</returns>
        public Node GetAuthorityTree(string infoSystemNo, ApplicationType? applicationType)
        {
            InfoSystemInfo infoSystemInfo = this._authorizationContract.GetInfoSystem(infoSystemNo);
            IEnumerable<Authority> authorities = this.GetAuthoritiesByInfoSystem(infoSystemNo, applicationType);
            Node node = infoSystemInfo.ToModel().ToNode(authorities);

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
            IEnumerable<Authority> roleAuthorities = this.GetAuthoritiesByRole(currentRole).ToArray();

            //获取角色所在信息系统的权限树
            Node authroityTree = this.GetAuthorityTree(currentRole.InfoSystemNo, null);
            IEnumerable<Node> authroityNodes = authroityTree.children.SelectMany(x => x.children);

            //遍历子节点集（权限集）
            foreach (Node node in authroityNodes)
            {
                //如果角色中含有该权限，则选中
                if (roleAuthorities.Any(x => x.Id == node.id))
                {
                    node.@checked = true;
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
            IEnumerable<Authority> menuAuthorities = this.GetAuthoritiesByMenu(currentMenu).ToArray();

            //获取菜单所在信息系统的权限树
            Node authroityTree = this.GetAuthorityTree(currentMenu.InfoSystemNo, currentMenu.ApplicationType);
            IEnumerable<Node> authroityNodes = authroityTree.children.SelectMany(x => x.children);

            //遍历子节点集（权限集）
            foreach (Node node in authroityNodes)
            {
                //如果菜单中含有该权限，则选中
                if (menuAuthorities.Any(x => x.Id == node.id))
                {
                    node.@checked = true;
                }
            }

            return authroityTree;
        }
        #endregion

        #region # 分页获取权限列表 —— PageModel<Authority> GetAuthoritiesByPage(string keywords...
        /// <summary>
        /// 分页获取权限列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>权限列表</returns>
        public PageModel<Authority> GetAuthoritiesByPage(string keywords, string infoSystemNo, ApplicationType? applicationType, int pageIndex, int pageSize)
        {
            PageModel<AuthorityInfo> pageModel = this._authorizationContract.GetAuthoritiesByPage(keywords, infoSystemNo, applicationType, pageIndex, pageSize);
            IEnumerable<Authority> authorities = pageModel.Datas.Select(x => x.ToModel());

            return new PageModel<Authority>(authorities, pageModel.PageIndex, pageModel.PageSize, pageModel.PageCount, pageModel.RowCount);
        }
        #endregion


        //Private

        #region # 根据信息系统获取权限列表 —— IEnumerable<Authority> GetAuthoritiesByInfoSystem(...
        /// <summary>
        /// 根据信息系统获取权限列表
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>权限列表</returns>
        private IEnumerable<Authority> GetAuthoritiesByInfoSystem(string infoSystemNo, ApplicationType? applicationType)
        {
            IEnumerable<AuthorityInfo> authorityInfos = this._authorizationContract.GetAuthorities(null, infoSystemNo, applicationType, null, null);
            IEnumerable<Authority> authorities = authorityInfos.Select(x => x.ToModel());

            return authorities;
        }
        #endregion

        #region # 根据角色获取权限列表 —— IEnumerable<Authority> GetAuthoritiesByRole(...
        /// <summary>
        /// 根据角色获取权限列表
        /// </summary>
        /// <param name="role">角色</param>
        /// <returns>权限列表</returns>
        private IEnumerable<Authority> GetAuthoritiesByRole(RoleInfo role)
        {
            IEnumerable<AuthorityInfo> authorityInfos = this._authorizationContract.GetAuthorities(null, role.InfoSystemNo, null, null, role.Id);
            IEnumerable<Authority> authorities = authorityInfos.Select(x => x.ToModel());

            return authorities;
        }
        #endregion

        #region # 根据菜单获取权限列表 —— IEnumerable<Authority> GetAuthoritiesByMenu(...
        /// <summary>
        /// 根据菜单获取权限列表
        /// </summary>
        /// <param name="menu">菜单</param>
        /// <returns>权限列表</returns>
        private IEnumerable<Authority> GetAuthoritiesByMenu(MenuInfo menu)
        {
            IEnumerable<AuthorityInfo> authorityInfos = this._authorizationContract.GetAuthorities(null, menu.InfoSystemNo, menu.ApplicationType, menu.Id, null);
            IEnumerable<Authority> authorities = authorityInfos.Select(x => x.ToModel());

            return authorities;
        }
        #endregion
    }
}
