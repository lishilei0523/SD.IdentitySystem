using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.WPF.Aspects;
using SD.Infrastructure.WPF.Base;
using SD.Infrastructure.WPF.Extensions;
using System.Threading.Tasks;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels.User
{
    /// <summary>
    /// 重置密码视图模型
    /// </summary>
    public class ResetPasswordViewModel : ScreenBase
    {
        #region # 字段及构造器

        /// <summary>
        /// 用户服务接口
        /// </summary>
        private readonly IUserContract _userContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public ResetPasswordViewModel(IUserContract userContract)
        {
            this._userContract = userContract;
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

        #region 是否繁忙 —— bool IsBusy
        /// <summary>
        /// 是否繁忙
        /// </summary>
        [DependencyProperty]
        public bool IsBusy { get; set; }
        #endregion

        #endregion

        #region # 方法

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

        #region 提交 —— async void Submit()
        /// <summary>
        /// 提交
        /// </summary>
        public async void Submit()
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(this.NewPassword))
            {
                MessageBox.Show("新密码不可为空！", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.ConfirmedPassword))
            {
                MessageBox.Show("确认密码不可为空！", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (this.NewPassword != this.ConfirmedPassword)
            {
                MessageBox.Show("两次密码输入不一致", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            this.Busy();

            await Task.Run(() => this._userContract.ResetPassword(this.LoginId, this.NewPassword));

            base.TryClose(true);
            this.Idle();
        }
        #endregion

        #endregion
    }
}
