using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.WPF.Caliburn.Aspects;
using SD.Infrastructure.WPF.Caliburn.Base;
using System.ServiceModel.Extensions;
using System.Threading.Tasks;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels.InfoSystemContext
{
    /// <summary>
    /// 信息系统修改视图模型
    /// </summary>
    public class UpdateViewModel : ScreenBase
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限管理服务契约接口代理
        /// </summary>
        private readonly ServiceProxy<IAuthorizationContract> _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public UpdateViewModel(ServiceProxy<IAuthorizationContract> authorizationContract)
        {
            this._authorizationContract = authorizationContract;
        }

        #endregion

        #region # 属性

        #region 信息系统编号 —— string InfoSystemNo
        /// <summary>
        /// 信息系统编号
        /// </summary>
        public string InfoSystemNo { get; set; }
        #endregion

        #region 信息系统名称 —— string InfoSystemName
        /// <summary>
        /// 信息系统名称
        /// </summary>
        [DependencyProperty]
        public string InfoSystemName { get; set; }
        #endregion

        #endregion

        #region # 方法

        //Initializations

        #region 加载 —— void Load(InfoSystemInfo infoSystem)
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="infoSystem">信息系统</param>
        public void Load(InfoSystemInfo infoSystem)
        {
            this.InfoSystemNo = infoSystem.Number;
            this.InfoSystemName = infoSystem.Name;
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

            if (string.IsNullOrWhiteSpace(this.InfoSystemNo))
            {
                MessageBox.Show("信息系统编号不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.InfoSystemName))
            {
                MessageBox.Show("信息系统名称不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            this.Busy();

            await Task.Run(() => this._authorizationContract.Channel.UpdateInfoSystem(this.InfoSystemNo, this.InfoSystemName));

            this.Idle();
            await base.TryCloseAsync(true);
            this.ToastSuccess("修改成功！");
        }
        #endregion

        #endregion
    }
}
