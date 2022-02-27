using Android.App;
using Android.Content;
using AndroidX.AppCompat.App;

namespace SD.IdentitySystem.Mobile.Host.Activities
{
    /// <summary>
    /// 初始屏幕
    /// </summary>
    [Activity(Theme = "@style/SplashTheme", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        /// <summary>
        /// 活动恢复事件
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();
            this.StartActivity(new Intent(Android.App.Application.Context, typeof(MainActivity)));
        }
    }
}