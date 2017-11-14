using Caliburn.Micro;
using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace SD.IdentitySystem.Client.ViewModels
{
    /// <summary>
    /// 应用程序外壳
    /// </summary>
    public class ShellViewModel : Screen
    {
        #region # 依赖注入构造器
        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public ShellViewModel(IDocumentManager documentManager)
        {
            this.DocumentManager = documentManager;
            //初始化计时器
            this.InitTimer();
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
            set { this.Set(ref this._currentTime, value); }
        }
        #endregion

        #region 主窗体 —— IDocumentManager DocumentManager
        /// <summary>
        /// 主窗体
        /// </summary>
        public IDocumentManager DocumentManager { get; private set; }
        #endregion

        #endregion

        #region # 方法

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

        #region 访问我的码云 —— void LaunchGitOsc()
        /// <summary>
        /// 访问我的码云
        /// </summary>
        public void LaunchGitOsc()
        {
            Process.Start("https://gitee.com/lishilei0523");
        }
        #endregion

        #endregion
    }
}
