using Caliburn.Micro;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using SD.Infrastructure.WPF.Caliburn.Aspects;
using SD.Infrastructure.WPF.Caliburn.Base;
using SD.Infrastructure.WPF.Commands;
using SD.Infrastructure.WPF.Extensions;
using SD.IOC.Core.Mediators;
using System;
using System.ServiceModel.Extensions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SD.IdentitySystem.Client.ViewModels.Home
{
    /// <summary>
    /// 登录视图模型
    /// </summary>
    public class LoginViewModel : ScreenBase
    {
        #region # 字段及构造器

        /// <summary>
        /// 身份认证服务契约接口代理
        /// </summary>
        private readonly ServiceProxy<IAuthenticationContract> _authenticationContract;

        /// <summary>
        /// 窗体管理器
        /// </summary>
        private readonly IWindowManager _windowManager;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public LoginViewModel(ServiceProxy<IAuthenticationContract> authenticationContract, IWindowManager windowManager)
        {
            this._authenticationContract = authenticationContract;
            this._windowManager = windowManager;
        }

        #endregion

        #region # 属性

        #region 用户名 —— string LoginId
        /// <summary>
        /// 用户名
        /// </summary>
        [DependencyProperty]
        public string LoginId { get; set; }
        #endregion

        #region 密码 —— string Password
        /// <summary>
        /// 密码
        /// </summary>
        [DependencyProperty]
        public string Password { get; set; }
        #endregion

        #region 只读属性 - 登录命令 —— ICommand LoginCommand
        /// <summary>
        /// 登录命令
        /// </summary>
        public ICommand LoginCommand
        {
            get { return new RelayCommand(x => this.Login()); }
        }
        #endregion

        #endregion

        #region # 方法

        //Initializations

        #region 初始化 —— override void OnInitialize()
        /// <summary>
        /// 初始化
        /// </summary>
        protected override void OnInitialize()
        {
#if DEBUG
            //自动登录
            this.LoginId = CommonConstants.AdminLoginId;
            this.Password = CommonConstants.InitialPassword;
            this.Login();
#endif
        }

        #endregion


        //Actions

        #region 登录 —— async void Login()
        /// <summary>
        /// 登录
        /// </summary>
        public async void Login()
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(this.LoginId))
            {
                MessageBox.Show("用户名不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.Password))
            {
                MessageBox.Show("密码不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            this.Busy();

            LoginInfo loginInfo = await Task.Run(() => this._authenticationContract.Channel.Login(this.LoginId, this.Password));
            AppDomain.CurrentDomain.SetData(SessionKey.CurrentUser, loginInfo);

            IndexViewModel homeViewModel = ResolveMediator.Resolve<IndexViewModel>();
            this._windowManager.ShowWindow(homeViewModel);

            base.TryClose();
            this.Idle();
        }
        #endregion

        #endregion
    }
}
