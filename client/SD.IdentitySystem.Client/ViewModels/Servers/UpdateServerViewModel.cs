using MahApps.Metro.Controls;
using SD.IdentitySystem.Client.Commons;
using SD.IdentitySystem.IAppService.Interfaces;
using System;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels.Servers
{
    /// <summary>
    /// 修改服务器ViewModel
    /// </summary>
    public class UpdateServerViewModel : FlyoutBase
    {
        #region # 依赖注入构造器

        /// <summary>
        /// 权限服务接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="authorizationContract">权限服务接口</param>
        public UpdateServerViewModel(IAuthorizationContract authorizationContract)
        {
            this._authorizationContract = authorizationContract;

            //默认值
            this.Position = Position.Right;
            this.Margin = new Thickness(700, 30, 0, 30);
            this.Initialized = false;
        }

        #endregion

        #region # 属性

        #region 标题 —— override string Title
        /// <summary>
        /// 标题
        /// </summary>
        public override string Title
        {
            get { return "修改服务器"; }
        }
        #endregion

        #region 服务器Id —— Guid ServerId
        /// <summary>
        /// 服务器Id
        /// </summary>
        private Guid _serverId;

        /// <summary>
        /// 服务器Id
        /// </summary>
        public Guid ServerId
        {
            get { return this._serverId; }
            private set { this.Set(ref this._serverId, value); }
        }
        #endregion

        #region 主机名 —— string HostName
        /// <summary>
        /// 主机名
        /// </summary>
        private string _hostName;

        /// <summary>
        /// 主机名
        /// </summary>
        public string HostName
        {
            get { return this._hostName; }
            private set { this.Set(ref this._hostName, value); }
        }
        #endregion

        #region 是否已初始化 —— bool Initialized
        /// <summary>
        /// 是否已初始化
        /// </summary>
        public bool Initialized { get; private set; }
        #endregion

        #region 服务停止日期 —— DateTime ServiceOverDate
        /// <summary>
        /// 服务停止日期
        /// </summary>
        private DateTime _serviceOverDate;

        /// <summary>
        /// 服务停止日期
        /// </summary>
        public DateTime ServiceOverDate
        {
            get { return this._serviceOverDate; }
            private set { this.Set(ref this._serviceOverDate, value.Date); }
        }

        #endregion

        #endregion

        #region # 方法

        #region 初始化 —— void Initialize(Guid serverId, string hostName...
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="serverId">服务器Id</param>
        /// <param name="hostName">主机名</param>
        /// <param name="serviceOverDate">服务停止日期</param>
        public void Initialize(Guid serverId, string hostName, DateTime serviceOverDate)
        {
            this.ServerId = serverId;
            this.HostName = hostName;
            this.ServiceOverDate = serviceOverDate;
            this.Initialized = true;
        }
        #endregion

        #region 修改服务器 —— async void UpdateServer()
        /// <summary>
        /// 修改服务器
        /// </summary>
        public async void UpdateServer()
        {
            //验证
            if (this.Initialized)
            {
                this._authorizationContract.UpdateServerHostName(this.ServerId, this.HostName);
                this._authorizationContract.UpdateServiceOverDate(this.ServerId, this.ServiceOverDate);
                await ElementManager.ShowMessage("OK", "修改成功！");
                this.Close();
            }
        }
        #endregion

        #endregion
    }
}
