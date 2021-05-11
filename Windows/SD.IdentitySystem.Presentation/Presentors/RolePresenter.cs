using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.Presentation.Maps;
using SD.Infrastructure.WPF.Models;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Presentation.Presentors
{
    /// <summary>
    /// 角色呈现器
    /// </summary>
    public class RolePresenter
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限服务接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 用户服务接口
        /// </summary>
        private readonly IUserContract _userContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="authorizationContract">权限服务接口</param>
        /// <param name="userContract">用户服务接口</param>
        public RolePresenter(IAuthorizationContract authorizationContract, IUserContract userContract)
        {
            this._authorizationContract = authorizationContract;
            this._userContract = userContract;
        }

        #endregion

        #region # 获取信息系统/角色树 —— IEnumerable<Node> GetSystemRoleTree()
        /// <summary>
        /// 获取信息系统/角色树
        /// </summary>
        /// <returns>信息系统/角色树</returns>
        public IEnumerable<Node> GetSystemRoleTree()
        {
            IEnumerable<InfoSystemInfo> infoSystems = this._authorizationContract.GetInfoSystems();

            IList<Node> tree = new List<Node>();
            foreach (InfoSystemInfo infoSystem in infoSystems)
            {
                IEnumerable<RoleInfo> roles = this._authorizationContract.GetRoles(null, null, infoSystem.Number);
                Node node = infoSystem.ToNode(roles);
                tree.Add(node);
            }

            return tree;
        }
        #endregion

        #region # 获取用户的信息系统/角色树 —— IEnumerable<Node> GetUserSystemRoleTree(string loginId)
        /// <summary>
        /// 获取用户的信息系统/角色树
        /// </summary>
        /// <param name="loginId">用户登录名</param>
        /// <returns>信息系统/角色树</returns>
        public IEnumerable<Node> GetUserSystemRoleTree(string loginId)
        {
            //获取当前用户及其角色集
            IEnumerable<RoleInfo> userRoles = this._authorizationContract.GetRoles(null, loginId, null).ToArray();

            //获取信息系统/角色树
            IEnumerable<Node> roleTree = this.GetSystemRoleTree().ToArray();

            //遍历信息系统
            foreach (Node system in roleTree)
            {
                //遍历角色
                foreach (Node role in system.SubNodes)
                {
                    //如果用户有该角色，则选中
                    if (userRoles.Any(x => x.Id == role.Id))
                    {
                        role.IsChecked = true;
                    }
                }
            }

            return roleTree;
        }
        #endregion
    }
}
