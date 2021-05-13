using Caliburn.Micro;
using SD.Common;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.Presentation.Models;
using SD.IdentitySystem.Presentation.Presentors;
using SD.Infrastructure.WPF.Aspects;
using SD.Infrastructure.WPF.Extensions;
using SD.Toolkits.Recursion.Tree;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels
{
    /// <summary>
    /// 菜单视图模型
    /// </summary>
    public class MenuViewModel : Screen
    {
        #region # 字段及构造器

        /// <summary>
        /// 菜单呈现器
        /// </summary>
        private readonly MenuPresenter _menuPresenter;

        /// <summary>
        /// 权限服务契约接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public MenuViewModel(MenuPresenter menuPresenter, IAuthorizationContract authorizationContract)
        {
            this._menuPresenter = menuPresenter;
            this._authorizationContract = authorizationContract;
        }

        #endregion

        #region # 属性

        #region 菜单列表 —— ObservableCollection<Menu> Menus
        /// <summary>
        /// 菜单列表
        /// </summary>
        [DependencyProperty]
        public ObservableCollection<Menu> Menus { get; set; }
        #endregion

        #endregion

        #region # 方法

        #region 加载菜单列表 —— async Task LoadMenus()
        /// <summary>
        /// 加载菜单列表
        /// </summary>
        public async Task LoadMenus()
        {
            LoadingIndicator.Suspend();
            IEnumerable<Menu> menus = await Task.Run(() => this._menuPresenter.GetMenuTreeList(null, null));
            LoadingIndicator.Dispose();

            this.Menus = new ObservableCollection<Menu>(menus);
        }
        #endregion

        #region 关联权限 —— void RelateAuthorities(Menu menu)
        /// <summary>
        /// 关联权限
        /// </summary>
        /// <param name="menu">菜单</param>
        public void RelateAuthorities(Menu menu)
        {
            Trace.WriteLine(menu);
        }
        #endregion

        #region 修改菜单 —— void Update(Menu menu)
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="menu">菜单</param>
        public void Update(Menu menu)
        {
            Trace.WriteLine(menu);
        }
        #endregion

        #region 删除菜单 —— async void Remove(Menu menu)
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menu">菜单</param>
        public async void Remove(Menu menu)
        {
            MessageBoxResult result = MessageBox.Show("您确定要删除吗？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                LoadingIndicator.Suspend();
                await Task.Run(() => this._authorizationContract.RemoveMenu(menu.Id));
                await this.LoadMenus();
                LoadingIndicator.Dispose();
            }
        }
        #endregion

        #region 批量删除菜单 —— async void Removes()
        /// <summary>
        /// 批量删除菜单
        /// </summary>
        public async void Removes()
        {
            MessageBoxResult result = MessageBox.Show("您确定要删除吗？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                IList<Menu> checkedMenus = new List<Menu>();
                foreach (Menu menu in this.Menus)
                {
                    if (menu.IsChecked == true)
                    {
                        checkedMenus.Add(menu);
                    }

                    foreach (Menu subNode in menu.GetDeepSubNodes())
                    {
                        if (subNode.IsChecked == true)
                        {
                            checkedMenus.Add(subNode);
                        }
                    }
                }

                if (!checkedMenus.Any())
                {
                    MessageBox.Show("请选中要删除的菜单！", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                LoadingIndicator.Suspend();
                await Task.Run(() => checkedMenus.ForEach(menu => this._authorizationContract.RemoveMenu(menu.Id)));
                await this.LoadMenus();
                LoadingIndicator.Dispose();
            }
        }
        #endregion

        #endregion
    }
}
