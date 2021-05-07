using System;
using System.Windows.Threading;

namespace SD.IdentitySystem.Client.Views
{
    /// <summary>
    /// 首页视图
    /// </summary>
    public partial class HomeView
    {
        public HomeView()
        {
            this.InitializeComponent();

            const string timeFormat = " yyyy年MM月dd日 HH时mm分ss秒 dddd";
            DispatcherTimer showTime = new DispatcherTimer();
            showTime.Tick += (sender, e) => this.TbCurrentTime.Text = DateTime.Now.ToString(timeFormat);
            showTime.Interval = new TimeSpan(0, 0, 1);
            showTime.Start();

            this.TbCurrentTime.Text = DateTime.Now.ToString(timeFormat);
        }
    }
}
