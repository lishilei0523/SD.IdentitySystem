﻿using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.WPF.Caliburn.Aspects;
using SD.Infrastructure.WPF.Caliburn.Base;
using System.ServiceModel.Extensions;
using System.Threading.Tasks;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels.UserContext
{
    /// <summary>
    /// 用户重置密码视图模型
    /// </summary>
    public class ResetPasswordViewModel : ScreenBase
    {
        #region # 字段及构造器

        /// <summary>
        /// 用户管理服务契约接口代理
        /// </summary>
        private readonly ServiceProxy<IUserContract> _userContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public ResetPasswordViewModel(ServiceProxy<IUserContract> userContract)
        {
            this._userContract = userContract;
        }

        #endregion

        #region # 属性

        #region 用户名 —— string LoginId
        /// <summary>
        /// 用户名
        /// </summary>
        public string LoginId { get; set; }
        #endregion

        #region 新密码 —— string NewPassword
        /// <summary>
        /// 新密码
        /// </summary>
        [DependencyProperty]
        public string NewPassword { get; set; }
        #endregion

        #region 确认密码 —— string ConfirmedPassword
        /// <summary>
        /// 确认密码
        /// </summary>
        [DependencyProperty]
        public string ConfirmedPassword { get; set; }
        #endregion

        #endregion

        #region # 方法

        //Initializations

        #region 加载 —— void Load(string loginId)
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="loginId">用户名</param>
        public void Load(string loginId)
        {
            this.LoginId = loginId;
        }
        #endregion


        //Actions

        #region 提交 —— async void Submit()
        /// <summary>
        /// 提交
        /// </summary>
        public async void Submit()
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(this.NewPassword))
            {
                MessageBox.Show("新密码不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.ConfirmedPassword))
            {
                MessageBox.Show("确认密码不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (this.NewPassword != this.ConfirmedPassword)
            {
                MessageBox.Show("两次密码输入不一致！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            this.Busy();

            await Task.Run(() => this._userContract.Channel.ResetPassword(this.LoginId, this.NewPassword));

            this.Idle();
            await base.TryCloseAsync(true);
            this.ToastSuccess("重置密码成功！");
        }
        #endregion

        #endregion
    }
}
