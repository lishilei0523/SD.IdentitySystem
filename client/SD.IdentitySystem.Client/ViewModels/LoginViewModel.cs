using Caliburn.Micro;
using SD.IdentitySystem.Client.Commons;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Constants;
using SD.IOC.Core.Mediator;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace SD.IdentitySystem.Client.ViewModels
{
    /// <summary>
    /// 登录ViewModel
    /// </summary>
    public class LoginViewModel : Screen
    {
        #region # 依赖注入构造器

        /// <summary>
        /// 身份认证服务接口
        /// </summary>
        private readonly IAuthenticationContract _authenticationContract;

        /// <summary>
        /// 窗体管理器
        /// </summary>
        private readonly IWindowManager _windowManager;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="authenticationContract">身份认证服务接口</param>
        /// <param name="windowManager">窗体管理器</param>
        public LoginViewModel(IAuthenticationContract authenticationContract, IWindowManager windowManager)
        {
            this._authenticationContract = authenticationContract;
            this._windowManager = windowManager;

            //默认值
            base.DisplayName = null;

            //自动登录
            //this.LoginId = "admin";
            //this.Password = "888888";
            //this.Login();
        }

        #endregion

        #region # 属性

        #region 用户名 —— string LoginId
        /// <summary>
        /// 用户名
        /// </summary>
        private string _loginId;

        /// <summary>
        /// 用户名
        /// </summary>
        public string LoginId
        {
            get { return this._loginId; }
            private set { this.Set(ref this._loginId, value); }
        }
        #endregion

        #region 密码 —— string Password
        /// <summary>
        /// 密码
        /// </summary>
        private string _password;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return this._password; }
            private set { this.Set(ref this._password, value); }
        }
        #endregion

        #region 只读属性 - 登录命令 —— ICommand LoginCommand
        /// <summary>
        /// 登录命令
        /// </summary>
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(x => this.Login());
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

        #region 登录 —— void Login()
        /// <summary>
        /// 登录
        /// </summary>
        public void Login()
        {
            LoginInfo loginInfo = this._authenticationContract.Login(this.LoginId, this.Password);

            //存入Session
            AppDomain.CurrentDomain.SetData(SessionKey.CurrentUser, loginInfo);

            //初始化元素管理器
            ElementManager.Init();

            //跳转到主窗体
            ShellViewModel shellViewModel = ResolveMediator.Resolve<ShellViewModel>();
            this._windowManager.ShowWindow(shellViewModel);

            //关闭当前窗口
            this.TryClose();
        }
        #endregion

        #endregion
    }
}
