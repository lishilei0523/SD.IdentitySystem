using MahApps.Metro.Controls;
using SD.IdentitySystem.Client.Commons;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Constants;
using System;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels
{
    /// <summary>
    /// 修改密码ViewModel
    /// </summary>
    public class UpdatePasswordViewModel : FlyoutBase
    {
        #region # 依赖注入构造器

        /// <summary>
        /// 用户服务接口
        /// </summary>
        private readonly IUserContract _userContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="userContract">用户服务接口</param>
        public UpdatePasswordViewModel(IUserContract userContract)
        {
            this._userContract = userContract;

            //默认值
            this.Position = Position.Right;
            this.Margin = new Thickness(900, 30, 0, 30);
            this.PasswordChanged = false;
        }

        #endregion

        #region # 属性

        #region 标题 —— override string Title
        /// <summary>
        /// 标题
        /// </summary>
        public override string Title
        {
            get { return "修改密码"; }
        }
        #endregion

        #region 旧密码 —— string OldPassword
        /// <summary>
        /// 旧密码
        /// </summary>
        private string _oldPassword;

        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPassword
        {
            get { return this._oldPassword; }
            set { this.Set(ref this._oldPassword, value.Trim()); }
        }
        #endregion

        #region 新密码 —— string NewPassword
        /// <summary>
        /// 新密码
        /// </summary>
        private string _newPassword;

        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword
        {
            get { return this._newPassword; }
            set { this.Set(ref this._newPassword, value.Trim()); }
        }
        #endregion

        #region 确认密码 —— string ConfirmPassword
        /// <summary>
        /// 确认密码
        /// </summary>
        private string _confirmPassword;

        /// <summary>
        /// 确认密码
        /// </summary>
        public string ConfirmPassword
        {
            get { return this._confirmPassword; }
            set { this.Set(ref this._confirmPassword, value.Trim()); }
        }
        #endregion

        #region 密码是否已修改 —— bool PasswordChanged
        /// <summary>
        /// 密码是否已修改
        /// </summary>
        public bool PasswordChanged { get; private set; }
        #endregion

        #endregion

        #region # 方法

        #region 修改密码 —— async void UpdatePassword()
        /// <summary>
        /// 修改密码
        /// </summary>
        public async void UpdatePassword()
        {
            LoginInfo loginInfo = Membership.LoginInfo;

            #region # 验证

            if (loginInfo == null)
            {
                throw new InvalidOperationException("用户未登录！");
            }
            if (this.NewPassword != this.ConfirmPassword)
            {
                throw new InvalidOperationException("两次密码输入不一致！");
            }

            #endregion

            this._userContract.UpdatePassword(loginInfo.LoginId, this.OldPassword, this.NewPassword);
            await ElementManager.ShowMessage("OK", "修改成功，请重新登录！");

            this.PasswordChanged = true;
            this.Close();
        }
        #endregion

        #endregion
    }
}
