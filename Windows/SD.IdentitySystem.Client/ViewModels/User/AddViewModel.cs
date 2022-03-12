using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.WPF.Caliburn.Aspects;
using SD.Infrastructure.WPF.Caliburn.Base;
using System.ServiceModel.Extensions;
using System.Threading.Tasks;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels.User
{
    /// <summary>
    /// 用户创建视图模型
    /// </summary>
    public class AddViewModel : ScreenBase
    {
        #region # 字段及构造器

        /// <summary>
        /// 用户管理服务契约接口代理
        /// </summary>
        private readonly ServiceProxy<IUserContract> _userContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public AddViewModel(ServiceProxy<IUserContract> userContract)
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

        #region 真实姓名 —— string RealName
        /// <summary>
        /// 真实姓名
        /// </summary>
        [DependencyProperty]
        public string RealName { get; set; }
        #endregion

        #region 密码 —— string Password
        /// <summary>
        /// 密码
        /// </summary>
        [DependencyProperty]
        public string Password { get; set; }
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

        //Actions

        #region 提交 —— async void Submit()
        /// <summary>
        /// 提交
        /// </summary>
        public async void Submit()
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(this.LoginId))
            {
                MessageBox.Show("用户名不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.RealName))
            {
                MessageBox.Show("真实姓名不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.Password))
            {
                MessageBox.Show("密码不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.ConfirmedPassword))
            {
                MessageBox.Show("确认密码不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (this.Password != this.ConfirmedPassword)
            {
                MessageBox.Show("两次密码输入不一致！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            this.Busy();

            await Task.Run(() => this._userContract.Channel.CreateUser(this.LoginId, this.RealName, this.Password));

            this.Idle();
            await base.TryCloseAsync(true);
            this.ToastSuccess("创建成功！");
        }
        #endregion

        #endregion
    }
}
