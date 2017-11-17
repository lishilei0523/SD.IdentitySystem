using Caliburn.Micro;
using SD.IdentitySystem.Client.Commons;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using SD.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Threading;

namespace SD.IdentitySystem.Client.ViewModels
{
    /// <summary>
    /// 应用程序外壳
    /// </summary>
    public class ShellViewModel : ElementManagerBase
    {
        #region # 依赖注入构造器

        /// <summary>
        /// 菜单呈现器接口
        /// </summary>
        private readonly IMenuPresenter _menuPresenter;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public ShellViewModel(IMenuPresenter menuPresenter)
        {
            this._menuPresenter = menuPresenter;

            //初始化计时器
            this.InitTimer();

            //初始化菜单
            this.InitMenus();
        }

        #endregion

        #region # 属性

        #region 当前时间 —— string CurrentTime
        /// <summary>
        /// 当前时间
        /// </summary>
        private string _currentTime;

        /// <summary>
        /// 当前时间
        /// </summary>
        public string CurrentTime
        {
            get { return this._currentTime; }
            private set { this.Set(ref this._currentTime, value); }
        }
        #endregion

        #region 菜单列表 —— BindableCollection<Node> Menus
        /// <summary>
        /// 菜单列表
        /// </summary>
        public BindableCollection<MenuView> Menus { get; private set; }
        #endregion

        #region 只读属性 - 当前登录用户 —— string CurrentUser
        /// <summary>
        /// 只读属性 - 当前登录用户
        /// </summary>
        public string CurrentUser
        {
            get
            {
                //存入Session
                LoginInfo loginInfo = (LoginInfo)AppDomain.CurrentDomain.GetData(SessionKey.CurrentUser);

                return loginInfo.LoginId;
            }
        }
        #endregion

        #endregion

        #region # 方法

        #region 访问我的码云 —— void LaunchGitOsc()
        /// <summary>
        /// 访问我的码云
        /// </summary>
        public void LaunchGitOsc()
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
            string timeFormat = "yyyy年MM月dd日 HH时mm分ss秒";

            this._currentTime = DateTime.Now.ToString(timeFormat);

            DispatcherTimer showTimer = new DispatcherTimer();
            showTimer.Tick += (x, y) => this.CurrentTime = DateTime.Now.ToString(timeFormat);
            showTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            showTimer.Start();
        }
        #endregion

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
            Type type = Type.GetType(menu.Path);
            ElementManager.OpenDocument(type);
        }
        #endregion

        #endregion
    }
}
