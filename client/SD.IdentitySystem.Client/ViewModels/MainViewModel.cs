using Caliburn.Micro;
using SD.IdentitySystem.Client.Commons;
using SD.IdentitySystem.Client.Models;

namespace SD.IdentitySystem.Client.ViewModels
{
    /// <summary>
    /// 主窗体
    /// </summary>
    public class MainViewModel : DocumentManagerBase
    {
        #region # 构造器
        /// <summary>
        /// 构造器
        /// </summary>
        public MainViewModel()
        {
            this.Menus = new BindableCollection<MenuView>();

            //初始化菜单
            this.InitMenus();
        }
        #endregion

        #region # 属性

        #region 菜单列表 —— BindableCollection<Menu> Menus
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
            MenuView menu1 = new MenuView("文件", "File", 1);
            MenuView menu11 = new MenuView("新建", "SD.IdentitySystem.Client.ViewModels.TestViewModel", 1);
            MenuView menu12 = new MenuView("打开", "Open", 2);
            menu1.children.Add(menu11);
            menu1.children.Add(menu12);

            MenuView menu2 = new MenuView("编辑", "Eidt", 2);
            MenuView menu21 = new MenuView("撤销", "U", 1);
            MenuView menu22 = new MenuView("重做", "F", 2);
            menu2.children.Add(menu21);
            menu2.children.Add(menu22);


            this.Menus.Add(menu1);
            this.Menus.Add(menu2);
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
