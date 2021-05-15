using Caliburn.Micro;
using SD.IdentitySystem.Client.ViewModels.User;
using SD.IdentitySystem.Presentation.Presentors;
using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using SD.Infrastructure.WPF.Aspects;
using SD.IOC.Core.Mediators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Models = SD.IdentitySystem.Presentation.Models;

namespace SD.IdentitySystem.Client.ViewModels.Home
{
    /// <summary>
    /// 首页视图模型
    /// </summary>
    public class IndexViewModel : Conductor<IScreen>.Collection.OneActive
    {
        #region # 字段及构造器

        /// <summary>
        /// 菜单呈现器
        /// </summary>
        private readonly MenuPresenter _menuPresenter;

        /// <summary>
        /// 窗体管理器
        /// </summary>
        private readonly IWindowManager _windowManager;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public IndexViewModel(MenuPresenter menuPresenter, IWindowManager windowManager)
        {
            this._menuPresenter = menuPresenter;
            this._windowManager = windowManager;

            //初始化定时器
            this.InitTimer();
        }

        #endregion

        #region # 属性

        #region 当前时间 —— string CurrentTime
        /// <summary>
        /// 当前时间
        /// </summary>
        [DependencyProperty]
        public string CurrentTime { get; set; }
        #endregion

        #region 菜单列表 —— IEnumerable<Menu> Menus
        /// <summary>
        /// 菜单列表
        /// </summary>
        [DependencyProperty]
        public IEnumerable<Models.Menu> Menus { get; private set; }
        #endregion

        #region 活动文档 —— IScreen ActiveDocument
        /// <summary>
        /// 活动文档
        /// </summary>
        [DependencyProperty]
        public IScreen ActiveDocument { get; set; }
        #endregion

        #region 必应可见性 —— Visibility BingVisibility
        /// <summary>
        /// 必应可见性
        /// </summary>
        [DependencyProperty]
        public Visibility BingVisibility { get; set; }
        #endregion

        #region 只读属性 - 登录信息 —— string LoginId
        /// <summary>
        /// 只读属性 - 登录信息
        /// </summary>
        public LoginInfo LoginInfo
        {
            get
            {
                LoginInfo loginInfo = MembershipMediator.GetLoginInfo();
                return loginInfo;
            }
        }
        #endregion

        #endregion

        #region # 方法

        #region 初始化 —— override async void OnInitialize()
        /// <summary>
        /// 初始化
        /// </summary>
        protected override async void OnInitialize()
        {
            await this.LoadMenus();
        }
        #endregion

        #region 加载菜单 —— async Task LoadMenus()
        /// <summary>
        /// 加载菜单
        /// </summary>
        public async Task LoadMenus()
        {
            IEnumerable<Models.Menu> menus = await Task.Run(() => this._menuPresenter.GetMenuTreeList("00", ApplicationType.Windows));
            this.Menus = menus;
        }
        #endregion

        #region 导航至菜单 —— void Navigate(Menu menu)
        /// <summary>
        /// 导航至菜单
        /// </summary>
        /// <param name="menu">菜单</param>
        public void Navigate(Models.Menu menu)
        {
            if (menu.IsLeaf && !string.IsNullOrWhiteSpace(menu.Url))
            {
                Type type = Type.GetType(menu.Url);
                IScreen document = (IScreen)ResolveMediator.Resolve(type);
                document.DisplayName = menu.Name;

                this.ActivateItem(document);
                this.ActiveDocument = document;
            }
        }
        #endregion

        #region 激活文档 —— override void ActivateItem(IScreen item)
        /// <summary>
        /// 激活文档
        /// </summary>
        /// <param name="document">文档</param>
        public override void ActivateItem(IScreen document)
        {
            if (base.Items.Any(x => x.DisplayName == document.DisplayName))
            {
                IScreen screen = base.Items.Single(x => x.DisplayName == document.DisplayName);
                this.ActiveDocument = screen;
            }
            else
            {
                base.ActivateItem(document);
            }

            this.BingVisibility = Visibility.Collapsed;
        }
        #endregion

        #region 关闭文档 —— override void DeactivateItem(IScreen item...
        /// <summary>
        /// 关闭文档
        /// </summary>
        public override void DeactivateItem(IScreen item, bool close)
        {
            base.DeactivateItem(item, close);
            if (!base.Items.Any())
            {
                this.BingVisibility = Visibility.Visible;
            }
        }
        #endregion

        #region 注销登录 —— void Logout()
        /// <summary>
        /// 注销登录
        /// </summary>
        public void Logout()
        {
            MessageBoxResult result = MessageBox.Show("您确定要注销吗？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                //清空Session
                AppDomain.CurrentDomain.SetData(SessionKey.CurrentUser, null);

                //跳转到登录窗体
                LoginViewModel loginViewModel = ResolveMediator.Resolve<LoginViewModel>();
                this._windowManager.ShowWindow(loginViewModel);

                //关闭当前窗口
                this.TryClose();
            }
        }
        #endregion

        #region 修改密码 —— void UpdatePassword()
        /// <summary>
        /// 修改密码
        /// </summary>
        public void UpdatePassword()
        {
            UpdatePasswordViewModel viewModel = ResolveMediator.Resolve<UpdatePasswordViewModel>();
            this._windowManager.ShowDialog(viewModel);
        }
        #endregion

        #region 访问我的码云 —— void LaunchGitee()
        /// <summary>
        /// 访问我的码云
        /// </summary>
        public void LaunchGitee()
        {
            Process.Start("https://gitee.com/lishilei0523");
        }
        #endregion

        #region 初始化计时器 —— void InitTimer()
        /// <summary>
        /// 初始化计时器
        /// </summary>
        public void InitTimer()
        {
            const string timeFormat = " yyyy年MM月dd日 HH时mm分ss秒 dddd";
            DispatcherTimer showTime = new DispatcherTimer();
            showTime.Tick += (sender, e) => this.CurrentTime = DateTime.Now.ToString(timeFormat);
            showTime.Interval = new TimeSpan(0, 0, 1);
            showTime.Start();

            this.CurrentTime = DateTime.Now.ToString(timeFormat);
        }
        #endregion

        #endregion
    }
}
