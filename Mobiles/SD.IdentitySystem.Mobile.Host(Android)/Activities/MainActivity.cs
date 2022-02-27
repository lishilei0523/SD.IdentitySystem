using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SD.IOC.Core.Mediators;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace SD.IdentitySystem.Mobile.Host.Activities
{
    /// <summary>
    /// Xamarin.Android主活动
    /// </summary>
    [Activity(Label = "SD.IdentitySystem", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : FormsAppCompatActivity
    {
        #region # 主活动创建事件 —— override void OnCreate(Bundle savedInstanceState)
        /// <summary>
        /// 主活动创建事件
        /// </summary>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);

            //初始化应用程序
            Startup startup = ResolveMediator.Resolve<Startup>();
            base.LoadApplication(startup);

            //注册未处理异常事件
            AndroidEnvironment.UnhandledExceptionRaiser += this.OnAndroidException;
            AppDomain.CurrentDomain.UnhandledException += this.OnAppDomainException;
        }
        #endregion

        #region # 请求权限事件 —— override void OnRequestPermissionsResult(int requestCode...
        /// <summary>
        /// 请求权限事件
        /// </summary>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        #endregion

        #region # Android异常事件 —— void OnAndroidException(object sender...
        /// <summary>
        /// Android异常事件
        /// </summary>
        private void OnAndroidException(object sender, RaiseThrowableEventArgs eventArgs)
        {
            this.RunOnUiThread(() =>
            {
                Exception exception = eventArgs.Exception;
                eventArgs.Handled = true;

                //提示消息
                string errorMessage = string.Empty;
                errorMessage = GetErrorMessage(exception.Message, ref errorMessage);
                UserDialogs.Instance.Alert(errorMessage);
                UserDialogs.Instance.HideLoading();
            });
        }
        #endregion

        #region # 应用程序域异常事件 —— void OnAppDomainException(object sender...
        /// <summary>
        /// 应用程序域异常事件
        /// </summary>
        private void OnAppDomainException(object sender, UnhandledExceptionEventArgs eventArgs)
        {
            this.RunOnUiThread(() =>
            {
                Exception exception = (Exception)eventArgs.ExceptionObject;

                //提示消息
                string errorMessage = string.Empty;
                errorMessage = GetErrorMessage(exception.Message, ref errorMessage);
                UserDialogs.Instance.Alert(errorMessage);
                UserDialogs.Instance.HideLoading();
            });
        }
        #endregion


        //Private

        #region # 获取错误消息 —— static string GetErrorMessage(string exceptionMessage...
        /// <summary>
        /// 获取错误消息
        /// </summary>
        /// <param name="exceptionMessage">异常消息</param>
        /// <param name="errorMessage">错误消息</param>
        /// <returns>错误消息</returns>
        private static string GetErrorMessage(string exceptionMessage, ref string errorMessage)
        {
            try
            {
                const string errorMessageKey = "ErrorMessage";
                JObject jObject = (JObject)JsonConvert.DeserializeObject(exceptionMessage);
                if (jObject != null && jObject.ContainsKey(errorMessageKey))
                {
                    errorMessage = jObject.GetValue(errorMessageKey)?.ToString();
                }
                else
                {
                    errorMessage = exceptionMessage;
                }

                GetErrorMessage(errorMessage, ref errorMessage);

                return errorMessage;
            }
            catch
            {
                return exceptionMessage;
            }
        }
        #endregion
    }
}