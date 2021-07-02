using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.Presentation.Presenters;
using SD.Infrastructure.WPF.Caliburn.Aspects;
using SD.Infrastructure.WPF.Caliburn.Base;
using SD.Infrastructure.WPF.Extensions;
using SD.Infrastructure.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.Extensions;
using System.Threading.Tasks;

namespace SD.IdentitySystem.Client.ViewModels.User
{
    /// <summary>
    /// 用户关联角色视图
    /// </summary>
    public class RelateRoleViewModel : ScreenBase
    {
        #region # 字段及构造器

        /// <summary>
        /// 角色呈现器
        /// </summary>
        private readonly RolePresenter _rolePresenter;

        /// <summary>
        /// 用户服务契约接口代理
        /// </summary>
        private readonly ServiceProxy<IUserContract> _userContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public RelateRoleViewModel(RolePresenter rolePresenter, ServiceProxy<IUserContract> userContract)
        {
            this._rolePresenter = rolePresenter;
            this._userContract = userContract;
        }

        #endregion

        #region # 属性

        #region 用户名 —— string LoginId
        /// <summary>
        /// 用户名
        /// </summary>
        public string LoginId { get; set; }
        #endregion

        #region 角色数据项列表 —— ObservableCollection<Item> RoleItems
        /// <summary>
        /// 角色数据项列表
        /// </summary>
        [DependencyProperty]
        public ObservableCollection<Item> RoleItems { get; set; }
        #endregion

        #endregion

        #region # 方法

        //Initializations

        #region 加载 —— async Task Load(string loginId)
        /// <summary>
        /// 加载
        /// </summary>
        public async Task Load(string loginId)
        {
            this.LoginId = loginId;

            ICollection<Item> roleItems = await Task.Run(() => this._rolePresenter.GetUserRoleItems(this.LoginId));
            this.RoleItems = new ObservableCollection<Item>(roleItems);
            this.RoleItems.Group();
        }
        #endregion


        //Actions

        #region 提交 —— async void Submit()
        /// <summary>
        /// 提交
        /// </summary>
        public async void Submit()
        {
            this.Busy();

            IEnumerable<Guid> roleIds = this.RoleItems.Where(x => x.IsChecked == true).Select(x => x.Id);
            await Task.Run(() => this._userContract.Channel.RelateRolesToUser(this.LoginId, roleIds));

            await base.TryCloseAsync(true);
            this.Idle();
        }
        #endregion

        #endregion
    }
}
