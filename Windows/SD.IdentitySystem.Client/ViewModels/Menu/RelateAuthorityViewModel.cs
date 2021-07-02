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

namespace SD.IdentitySystem.Client.ViewModels.Menu
{
    /// <summary>
    /// 菜单关联权限视图模型
    /// </summary>
    public class RelateAuthorityViewModel : ScreenBase
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限呈现器
        /// </summary>
        private readonly AuthorityPresenter _authorityPresenter;

        /// <summary>
        /// 权限服务契约接口代理
        /// </summary>
        private readonly ServiceProxy<IAuthorizationContract> _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public RelateAuthorityViewModel(AuthorityPresenter authorityPresenter, ServiceProxy<IAuthorizationContract> authorizationContract)
        {
            this._authorityPresenter = authorityPresenter;
            this._authorizationContract = authorizationContract;
        }

        #endregion

        #region # 属性

        #region 菜单Id —— Guid MenuId
        /// <summary>
        /// 菜单Id
        /// </summary>
        public Guid MenuId { get; set; }
        #endregion

        #region 权限数据项列表 —— ObservableCollection<Item> AuthorityItems
        /// <summary>
        /// 权限数据项列表
        /// </summary>
        [DependencyProperty]
        public ObservableCollection<Item> AuthorityItems { get; set; }
        #endregion

        #endregion

        #region # 方法

        //Initializations

        #region 加载 —— async Task Load(Guid menuId)
        /// <summary>
        /// 加载
        /// </summary>
        public async Task Load(Guid menuId)
        {
            this.MenuId = menuId;

            ICollection<Item> authorityItems = await Task.Run(() => this._authorityPresenter.GetMenuAuthorityItems(this.MenuId));
            this.AuthorityItems = new ObservableCollection<Item>(authorityItems);
            this.AuthorityItems.Group();
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

            IEnumerable<Guid> authorityIds = this.AuthorityItems.Where(x => x.IsChecked == true).Select(x => x.Id);
            await Task.Run(() => this._authorizationContract.Channel.RelateAuthoritiesToMenu(this.MenuId, authorityIds));

            await base.TryCloseAsync(true);
            this.Idle();
        }
        #endregion

        #endregion
    }
}
