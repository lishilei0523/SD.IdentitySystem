using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.Presentation.Maps;
using SD.Infrastructure.WPF.Models;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Presentation.Presenters
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

        #region # 获取角色数据项列表 —— ICollection<Item> GetRoleItems()
        /// <summary>
        /// 获取角色数据项列表
        /// </summary>
        /// <returns>角色数据项列表</returns>
        public ICollection<Item> GetRoleItems()
        {
            IEnumerable<RoleInfo> roles = this._authorizationContract.GetRoles(null, null, null);
            IEnumerable<Item> roleItems = roles.Select(x => x.ToItem());

            return roleItems.ToList();
        }
        #endregion

        #region # 获取用户角色数据项列表 —— ICollection<Item> GetUserRoleItems(string loginId)
        /// <summary>
        /// 获取用户角色数据项列表
        /// </summary>
        /// <returns>角色数据项列表</returns>
        public ICollection<Item> GetUserRoleItems(string loginId)
        {
            RoleInfo[] roles = this._authorizationContract.GetRoles(null, null, null).ToArray();
            RoleInfo[] userRoles = this._userContract.GetUserRoles(loginId, null).ToArray();

            ICollection<Item> roleItems = roles.Select(x => x.ToItem()).ToList();
            foreach (Item roleItem in roleItems)
            {
                if (userRoles.Any(x => x.Id == roleItem.Id))
                {
                    roleItem.IsChecked = true;
                }
            }

            return roleItems;
        }
        #endregion
    }
}
