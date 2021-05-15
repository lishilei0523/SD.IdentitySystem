using Caliburn.Micro;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.WPF.Aspects;
using SD.Infrastructure.WPF.Extensions;
using System.Threading.Tasks;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels.InfoSystem
{
    /// <summary>
    /// 初始化信息系统视图模型
    /// </summary>
    public class InitViewModel : Screen
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限服务契约接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public InitViewModel(IAuthorizationContract authorizationContract)
        {
            this._authorizationContract = authorizationContract;
        }

        #endregion

        #region # 属性

        #region 信息系统编号 —— string InfoSystemNo
        /// <summary>
        /// 信息系统编号
        /// </summary>
        [DependencyProperty]
        public string InfoSystemNo { get; set; }
        #endregion

        #region 主机名 —— string Host
        /// <summary>
        /// 主机名
        /// </summary>
        [DependencyProperty]
        public string Host { get; set; }
        #endregion

        #region 端口 —— int? Port
        /// <summary>
        /// 端口
        /// </summary>
        [DependencyProperty]
        public int? Port { get; set; }
        #endregion

        #region 首页 —— string Index
        /// <summary>
        /// 首页
        /// </summary>
        [DependencyProperty]
        public string Index { get; set; }
        #endregion

        #endregion

        #region # 方法

        #region 加载 —— async void Load(string infoSystemNo)
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        public async void Load(string infoSystemNo)
        {
            InfoSystemInfo infoSystem = await Task.Run(() => this._authorizationContract.GetInfoSystem(infoSystemNo));

            this.InfoSystemNo = infoSystem.Number;
            this.Host = infoSystem.Host;
            this.Port = infoSystem.Port;
            this.Index = infoSystem.Index;
        }
        #endregion

        #region 提交 —— async void Submit()
        /// <summary>
        /// 提交
        /// </summary>
        public async void Submit()
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(this.Host))
            {
                MessageBox.Show("主机名/IP地址不可为空！", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!this.Port.HasValue)
            {
                MessageBox.Show("端口号不可为空！", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.Index))
            {
                MessageBox.Show("首页不可为空！", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            LoadingIndicator.Suspend();
            await Task.Run(() => this._authorizationContract.InitInfoSystem(this.InfoSystemNo, this.Host, this.Port.Value, this.Index));
            LoadingIndicator.Dispose();

            base.TryClose(true);
        }
        #endregion

        #endregion
    }
}
