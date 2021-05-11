using Caliburn.Micro;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.Models.Outputs;
using SD.Infrastructure.WPF.Aspects;
using SD.Infrastructure.WPF.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

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
        private readonly IMenuPresenter _menuPresenter;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public MenuViewModel(IMenuPresenter menuPresenter)
        {
            this._menuPresenter = menuPresenter;
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

        #region 加载菜单列表 —— async void LoadMenus()
        /// <summary>
        /// 加载菜单列表
        /// </summary>
        public async void LoadMenus()
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

        #region 删除菜单 —— void Remove(Menu menu)
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menu">菜单</param>
        public void Remove(Menu menu)
        {
            Trace.WriteLine(menu);
        }
        #endregion

        #endregion
    }
}
