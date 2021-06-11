using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.Presentation.Maps;
using SD.Infrastructure.WPF.Models;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Extensions;

namespace SD.IdentitySystem.Presentation.Presenters
{
    /// <summary>
    /// 角色呈现器
    /// </summary>
    public class RolePresenter
    {
        #region # 字段及构造器

        /// <summary>
        /// 用户服务契约接口代理
        /// </summary>
        private readonly ServiceProxy<IUserContract> _userContract;

        /// <summary>
        /// 权限服务契约接口代理
        /// </summary>
        private readonly ServiceProxy<IAuthorizationContract> _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public RolePresenter(ServiceProxy<IUserContract> userContract, ServiceProxy<IAuthorizationContract> authorizationContract)
        {
            this._authorizationContract = authorizationContract;
            this._userContract = userContract;
        }

        #endregion

        #region # 获取用户角色数据项列表 —— ICollection<Item> GetUserRoleItems(string loginId)
        /// <summary>
        /// 获取用户角色数据项列表
        /// </summary>
        /// <returns>角色数据项列表</returns>
        public ICollection<Item> GetUserRoleItems(string loginId)
        {
            RoleInfo[] roles = this._authorizationContract.Channel.GetRoles(null, null, null).ToArray();
            RoleInfo[] userRoles = this._userContract.Channel.GetUserRoles(loginId, null).ToArray();

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
