using Caliburn.Micro.Xamarin.Forms;
using SD.Infrastructure.Membership;
using SD.Infrastructure.Xamarin.Caliburn.Aspects;
using SD.Infrastructure.Xamarin.Caliburn.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SD.IdentitySystem.Mobile.ViewModels.Home
{
    /// <summary>
    /// 首页视图模型
    /// </summary>
    public class IndexViewModel : OneActiveConductorBase
    {
        #region # 字段及构造器

        /// <summary>
        /// 导航服务
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public IndexViewModel(INavigationService navigationService)
        {
            this._navigationService = navigationService;
        }

        #endregion

        #region # 属性

        #region 已选菜单项 —— LoginMenuInfo SelectedMenuItem
        /// <summary>
        /// 已选菜单项
        /// </summary>
        [DependencyProperty]
        public LoginMenuInfo SelectedMenuItem { get; set; }
        #endregion

        #region 菜单项列表 —— ObservableCollection<LoginMenuInfo> MenuItems
        /// <summary>
        /// 菜单项列表
        /// </summary>
        [DependencyProperty]
        public ObservableCollection<LoginMenuInfo> MenuItems { get; set; }
        #endregion

        #endregion

        #region # 方法

        //Actions

        #region 导航至菜单 —— async void Navigate()
        /// <summary>
        /// 导航至菜单
        /// </summary>
        public async void Navigate()
        {
            #region # 验证

            if (this.SelectedMenuItem == null)
            {
                this.Toast("请选择菜单！");
            }

            #endregion

            if (this.SelectedMenuItem.SubMenuInfos != null && this.SelectedMenuItem.SubMenuInfos.Any())
            {
                this._navigationService
                    .For<IndexViewModel>()
                    .WithParam(x => x.MenuItems, new ObservableCollection<LoginMenuInfo>(this.SelectedMenuItem.SubMenuInfos))
                    .Navigate(false);
            }
            else
            {
                Type viewModelType = Type.GetType(this.SelectedMenuItem.Url);
                await this._navigationService.NavigateToViewModelAsync(viewModelType, null, false);
            }
        }
        #endregion 

        #endregion
    }
}
