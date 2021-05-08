﻿using Caliburn.Micro;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using SD.Infrastructure.WPF.Aspects;
using SD.Infrastructure.WPF.Extensions;
using SD.IOC.Core.Mediators;
using System;
using System.Windows.Input;

namespace SD.IdentitySystem.Client.ViewModels
{
    /// <summary>
    /// 登录视图模型
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
            base.DisplayName = string.Empty;

            //自动登录
            this.LoginId = CommonConstants.AdminLoginId;
            this.Password = CommonConstants.InitialPassword;
            this.Login();
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
            get
            {
                return new RelayCommand(x => this.Login());
            }
        }
        #endregion

        #endregion

        #region # 方法

        #region 登录 —— void Login()
        /// <summary>
        /// 登录
        /// </summary>
        public void Login()
        {
            LoginInfo loginInfo = this._authenticationContract.Login(this.LoginId, this.Password);

            //存入Session
            AppDomain.CurrentDomain.SetData(SessionKey.CurrentUser, loginInfo);

            //跳转到主窗体
            HomeViewModel homeViewModel = ResolveMediator.Resolve<HomeViewModel>();
            this._windowManager.ShowWindow(homeViewModel);

            //关闭当前窗口
            this.TryClose();
        }
        #endregion

        #endregion
    }
}