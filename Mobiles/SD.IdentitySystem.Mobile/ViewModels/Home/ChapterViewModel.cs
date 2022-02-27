using Caliburn.Micro.Xamarin.Forms;
using SD.Infrastructure.Membership;
using SD.Infrastructure.Xamarin.Caliburn.Aspects;
using SD.Infrastructure.Xamarin.Caliburn.Base;
using SD.Infrastructure.Xamarin.Models;
using SD.IOC.Core.Mediators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SD.IdentitySystem.Mobile.ViewModels.Home
{
    /// <summary>
    /// 章节视图模型
    /// </summary>
    public class ChapterViewModel : ScreenBase
    {
        #region # 字段及构造器

        /// <summary>
        /// 创建章节视图模型构造器
        /// </summary>
        /// <param name="displayName">显示名称</param>
        /// <param name="icon">图标</param>
        /// <param name="menuItems">菜单项列表</param>
        public ChapterViewModel(string displayName, string icon, IEnumerable<LoginMenuInfo> menuItems)
        {
            this.DisplayName = displayName;
            this.Icon = icon;

            IEnumerable<MenuItemGroup> menuItemGroups =
                from menuItem in menuItems
                select new MenuItemGroup(menuItem.Name, menuItem.SubMenuInfos);
            this.MenuItemGroups = new ObservableCollection<MenuItemGroup>(menuItemGroups);
        }

        #endregion

        #region # 属性

        #region 图标 —— string Icon
        /// <summary>
        /// 图标
        /// </summary>
        [DependencyProperty]
        public string Icon { get; set; }
        #endregion

        #region 已选菜单项 —— LoginMenuInfo SelectedMenuItem
        /// <summary>
        /// 已选菜单项
        /// </summary>
        [DependencyProperty]
        public LoginMenuInfo SelectedMenuItem { get; set; }
        #endregion

        #region 菜单项组列表 —— ObservableCollection<MenuItemGroup> MenuItemGroups
        /// <summary>
        /// 菜单项组列表
        /// </summary>
        [DependencyProperty]
        public ObservableCollection<MenuItemGroup> MenuItemGroups { get; set; }
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

            Type viewModelType = Type.GetType(this.SelectedMenuItem.Url);
            INavigationService navigationService = ResolveMediator.Resolve<INavigationService>();
            await navigationService.NavigateToViewModelAsync(viewModelType, null, false);
        }
        #endregion 

        #endregion
    }
}
