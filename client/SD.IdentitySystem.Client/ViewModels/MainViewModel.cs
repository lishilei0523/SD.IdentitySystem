using Caliburn.Micro;
using SD.IdentitySystem.Client.Commons;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using System.Collections.Generic;

namespace SD.IdentitySystem.Client.ViewModels
{
    /// <summary>
    /// 主窗体
    /// </summary>
    public class MainViewModel : DocumentManagerBase
    {
        #region # 依赖注入构造器

        /// <summary>
        /// 菜单呈现器接口
        /// </summary>
        private readonly IMenuPresenter _menuPresenter;

        /// <summary>
        /// 构造器
        /// </summary>
        public MainViewModel(IMenuPresenter menuPresenter)
        {
            this._menuPresenter = menuPresenter;

            //初始化菜单
            this.InitMenus();
        }
        #endregion

        #region # 属性

        #region 菜单列表 —— BindableCollection<Node> Menus
        /// <summary>
        /// 菜单列表
        /// </summary>
        public BindableCollection<MenuView> Menus { get; set; }
        #endregion

        #endregion

        #region # 方法

        #region 初始化菜单 —— void InitMenus()
        /// <summary>
        /// 初始化菜单
        /// </summary>
        public void InitMenus()
        {
            IEnumerable<MenuView> menus = this._menuPresenter.GetMenuTreeGrid("00");
            this.Menus = new BindableCollection<MenuView>(menus);
        }
        #endregion

        #region 导航至菜单 —— void Navigate(Menu menu)
        /// <summary>
        /// 导航至菜单
        /// </summary>
        /// <param name="menu">菜单</param>
        public void Navigate(MenuView menu)
        {
            DocumentBase document = this[menu.Url];

            if (document != null)
            {
                document.Open();
            }
        }
        #endregion

        #endregion
    }
}
