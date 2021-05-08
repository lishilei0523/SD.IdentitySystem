using Caliburn.Micro;
using SD.Infrastructure.MemberShip;
using SD.Infrastructure.WPF.Aspects;
using System;
using System.Windows.Threading;

namespace SD.IdentitySystem.Client.ViewModels
{
    /// <summary>
    /// 首页视图模型
    /// </summary>
    public class HomeViewModel : Conductor<IScreen>.Collection.OneActive
    {
        #region # 字段及构造器

        /// <summary>
        /// 窗体管理器
        /// </summary>
        private readonly IWindowManager _windowManager;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public HomeViewModel(IWindowManager windowManager)
        {
            this._windowManager = windowManager;

            //初始化计时器
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
