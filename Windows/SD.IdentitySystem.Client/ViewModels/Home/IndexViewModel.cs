using Caliburn.Micro;
using SD.IdentitySystem.Client.ViewModels.User;
using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using SD.Infrastructure.WPF.Caliburn.Aspects;
using SD.Infrastructure.WPF.Caliburn.Base;
using SD.Infrastructure.WPF.Extensions;
using SD.IOC.Core.Mediators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace SD.IdentitySystem.Client.ViewModels.Home
{
    /// <summary>
    /// 首页视图模型
    /// </summary>
    public class IndexViewModel : OneActiveConductorBase
    {
        #region # 字段及构造器

        /// <summary>
        /// 窗体管理器
        /// </summary>
        private readonly IWindowManager _windowManager;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public IndexViewModel(IWindowManager windowManager)
        {
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

        #region 登录菜单列表 —— ObservableCollection<LoginMenuInfo> LoginMenuInfos
        /// <summary>
        /// 登录菜单列表
        /// </summary>
        [DependencyProperty]
        public ObservableCollection<LoginMenuInfo> LoginMenuInfos { get; set; }
        #endregion

        #region 只读属性 - 登录信息 —— LoginInfo LoginInfo
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

        //Initializations

        #region 初始化 —— override Task OnInitializeAsync(CancellationToken cancellationToken)
        /// <summary>
        /// 初始化
        /// </summary>
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            this.ReloadMenus();

            return Task.CompletedTask;
        }
        #endregion


        //Actions

        #region 导航至菜单 —— async void Navigate(LoginMenuInfo menu)
        /// <summary>
        /// 导航至菜单
        /// </summary>
        /// <param name="menu">菜单</param>
        public async void Navigate(LoginMenuInfo menu)
        {
            if (!string.IsNullOrWhiteSpace(menu.Url))
            {
                this.Busy();

                Type type = Type.GetType(menu.Url);
                IScreen document = (IScreen)ResolveMediator.Resolve(type);
                document.DisplayName = menu.Name;

                await this.ActivateItem(document);
                this.ActiveDocument = document;

                this.Idle();
            }
        }
        #endregion

        #region 关闭文档 —— async void DeactivateItem(IScreen item...
        /// <summary>
        /// 关闭文档
        /// </summary>
        public async void DeactivateItem(IScreen item, bool close)
        {
            await base.DeactivateItemAsync(item, close);
            if (!base.Items.Any())
            {
                this.BingVisibility = Visibility.Visible;
            }
        }
        #endregion

        #region 注销登录 —— async void Logout()
        /// <summary>
        /// 注销登录
        /// </summary>
        public async void Logout()
        {
            MessageBoxResult result = MessageBox.Show("确定要注销吗？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                //清空Session
                AppDomain.CurrentDomain.SetData(SessionKey.CurrentUser, null);

                //跳转到登录窗体
                LoginViewModel loginViewModel = ResolveMediator.Resolve<LoginViewModel>();
                await this._windowManager.ShowWindowAsync(loginViewModel);

                //关闭当前窗口
                await base.TryCloseAsync();
            }
        }
        #endregion

        #region 修改密码 —— async void UpdatePassword()
        /// <summary>
        /// 修改密码
        /// </summary>
        public async void UpdatePassword()
        {
            UpdatePasswordViewModel viewModel = ResolveMediator.Resolve<UpdatePasswordViewModel>();
            await this._windowManager.ShowDialogAsync(viewModel);
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


        //Private

        #region 加载菜单 —— void ReloadMenus()
        /// <summary>
        /// 加载菜单
        /// </summary>
        public void ReloadMenus()
        {
            IEnumerable<LoginMenuInfo> loginMenuInfos = this.LoginInfo.LoginMenuInfos.Where(x => x.SystemNo == "00" && x.ApplicationType == ApplicationType.Windows);
            this.LoginMenuInfos = new ObservableCollection<LoginMenuInfo>(loginMenuInfos);
        }
        #endregion

        #region 激活文档 —— async Task ActivateItem(IScreen item)
        /// <summary>
        /// 激活文档
        /// </summary>
        /// <param name="document">文档</param>
        private async Task ActivateItem(IScreen document)
        {
            if (base.Items.Any(x => x.DisplayName == document.DisplayName))
            {
                IScreen screen = base.Items.Single(x => x.DisplayName == document.DisplayName);
                this.ActiveDocument = screen;
            }
            else
            {
                await base.ActivateItemAsync(document);
            }

            this.BingVisibility = Visibility.Collapsed;
        }
        #endregion

        #region 初始化计时器 —— void InitTimer()
        /// <summary>
        /// 初始化计时器
        /// </summary>
        private void InitTimer()
        {
            const string timeFormat = " yyyy年MM月dd日 HH时mm分ss秒 dddd";
            DispatcherTimer showTime = new DispatcherTimer();
            showTime.Tick += (sender, eventArgs) => this.CurrentTime = DateTime.Now.ToString(timeFormat);
            showTime.Interval = new TimeSpan(0, 0, 1);
            showTime.Start();

            this.CurrentTime = DateTime.Now.ToString(timeFormat);
        }
        #endregion

        #endregion
    }
}
