using MahApps.Metro.Controls;
using SD.IdentitySystem.Client.Commons;
using SD.IdentitySystem.IAppService.Interfaces;
using System;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels.Servers
{
    /// <summary>
    /// 创建服务器ViewModel
    /// </summary>
    public class CreateServerViewModel : FlyoutBase
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
        public CreateServerViewModel(IAuthorizationContract authorizationContract)
        {
            this._authorizationContract = authorizationContract;

            //默认值
            base.Position = Position.Right;
            base.Margin = new Thickness(700, 30, 0, 30);
            this.ServiceOverDate = DateTime.Today;
        }

        #endregion

        #region # 属性

        #region 标题 —— override string Title
        /// <summary>
        /// 标题
        /// </summary>
        public override string Title
        {
            get { return "创建服务器"; }
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

        #region 唯一码 —— string UniqueCode
        /// <summary>
        /// 唯一码
        /// </summary>
        private string _uniqueCode;

        /// <summary>
        /// 唯一码
        /// </summary>
        public string UniqueCode
        {
            get { return this._uniqueCode; }
            private set { this.Set(ref this._uniqueCode, value); }
        }
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

        #region 创建服务器 —— async void CreateServer()
        /// <summary>
        /// 创建服务器
        /// </summary>
        public async void CreateServer()
        {
            this._authorizationContract.CreateServer(this.UniqueCode, this.HostName, this.ServiceOverDate);

            this.ExecuteOk = true;
            await ElementManager.ShowMessage("OK", "创建成功！");
            this.Close();
        }
        #endregion

        #endregion
    }
}
