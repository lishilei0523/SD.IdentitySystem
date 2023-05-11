using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.Presentation.Maps;
using SD.IdentitySystem.Presentation.Models;
using SD.Infrastructure.DTOBase;
using SD.Toolkits.EasyUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Presentation.Presenters
{
    /// <summary>
    /// 角色呈现器实现
    /// </summary>
    public class RolePresenter
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限管理服务契约接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 用户管理服务契约接口
        /// </summary>
        private readonly IUserContract _userContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public RolePresenter(IAuthorizationContract authorizationContract, IUserContract userContract)
        {
            this._authorizationContract = authorizationContract;
            this._userContract = userContract;
        }

        #endregion

        #region # 获取角色 —— Role GetRole(Guid roleId)
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>角色</returns>
        public Role GetRole(Guid roleId)
        {
            RoleInfo roleInfo = this._authorizationContract.GetRole(roleId);

            return roleInfo.ToModel();
        }
        #endregion

        #region # 获取用户的信息系统/角色树 —— IEnumerable<Node> GetUserInfoSystemRoleTree(string loginId)
        /// <summary>
        /// 获取用户的信息系统/角色树
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <returns>信息系统/角色树</returns>
        public IEnumerable<Node> GetUserInfoSystemRoleTree(string loginId)
        {
            //获取当前用户及其角色集
            IEnumerable<Role> userRoles = this.GetUserRoles(loginId).ToArray();

            //获取信息系统/角色树
            IEnumerable<Node> roleTree = this.GetInfoSystemRoleTree().ToArray();

            //遍历信息系统
            foreach (Node infoSystemNode in roleTree)
            {
                //遍历角色
                foreach (Node role in infoSystemNode.children)
                {
                    //如果用户有该角色，则选中
                    if (userRoles.Any(x => x.Id == role.id))
                    {
                        role.@checked = true;
                    }
                }
            }

            return roleTree;
        }
        #endregion

        #region # 分页获取角色列表 —— PageModel<Role> GetRolesByPage(string keywords...
        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>角色列表</returns>
        public PageModel<Role> GetRolesByPage(string keywords, string infoSystemNo, int pageIndex, int pageSize)
        {
            PageModel<RoleInfo> pageModel = this._authorizationContract.GetRolesByPage(keywords, infoSystemNo, pageIndex, pageSize);
            IEnumerable<Role> roles = pageModel.Datas.Select(x => x.ToModel());

            return new PageModel<Role>(roles, pageModel.PageIndex, pageModel.PageSize, pageModel.PageCount, pageModel.RowCount);
        }
        #endregion


        //Private

        #region # 获取角色列表 —— IEnumerable<Role> GetRoles(string infoSystemNo)
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <returns>角色列表</returns>
        private IEnumerable<Role> GetRoles(string infoSystemNo)
        {
            IEnumerable<RoleInfo> roleInfos = this._authorizationContract.GetRoles(null, null, infoSystemNo);
            IEnumerable<Role> roles = roleInfos.Select(x => x.ToModel());

            return roles;
        }
        #endregion

        #region # 获取用户角色列表 —— IEnumerable<Role> GetUserRoles(string loginId)
        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <returns>角色列表</returns>
        private IEnumerable<Role> GetUserRoles(string loginId)
        {
            IEnumerable<RoleInfo> roleInfos = this._userContract.GetUserRoles(loginId, null);
            IEnumerable<Role> roles = roleInfos.Select(x => x.ToModel());

            return roles;
        }
        #endregion

        #region # 获取信息系统/角色树 —— IEnumerable<Node> GetInfoSystemRoleTree()
        /// <summary>
        /// 获取信息系统/角色树
        /// </summary>
        /// <returns>信息系统/角色树</returns>
        private IEnumerable<Node> GetInfoSystemRoleTree()
        {
            IEnumerable<InfoSystemInfo> infoSystems = this._authorizationContract.GetInfoSystems(null);
            IList<Node> tree = new List<Node>();
            foreach (InfoSystemInfo infoSystemNode in infoSystems)
            {
                IEnumerable<Role> roles = this.GetRoles(infoSystemNode.Number);

                Node node = infoSystemNode.ToModel().ToNode(roles);
                tree.Add(node);
            }

            return tree;
        }
        #endregion
    }
}
