using SD.IdentitySystem.Mobile.ViewModels.Home;
using SD.Infrastructure.Xamarin.Caliburn.Base;
using SD.IOC.Core.Mediators;
using Xamarin.Forms;

namespace SD.IdentitySystem.Mobile
{
    /// <summary>
    /// Xamarin.Forms应用程序
    /// </summary>
    public partial class Startup : CaliburnFormsApplication
    {
        #region # 字段及构造器

        /// <summary>
        /// 创建Xamarin.Forms应用程序构造器
        /// </summary>
        public Startup()
        {
            this.Initialize();
            this.InitializeComponent();
            base.MainPage = ResolveMediator.Resolve<NavigationPage>();
        }

        #endregion

        #region # 应用程序启动事件 —— override async void OnStart()
        /// <summary>
        /// 应用程序启动事件
        /// </summary>
        protected override async void OnStart()
        {
            await base.DisplayRootViewForAsync<LoginViewModel>();
        }
        #endregion

        #region # 应用程序休眠事件 —— override void OnSleep()
        /// <summary>
        /// 应用程序休眠事件
        /// </summary>
        protected override void OnSleep()
        {
            base.OnSleep();
        }
        #endregion

        #region # 应用程序恢复事件 —— override void OnResume()
        /// <summary>
        /// 应用程序恢复事件
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();
        }
        #endregion
    }
}
