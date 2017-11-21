using MahApps.Metro.Controls;
using SD.Common.PoweredByLee;
using SD.IdentitySystem.Client.Commons;
using SD.IdentitySystem.IAppService.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ApplicationType = SD.Infrastructure.Constants.ApplicationType;

namespace SD.IdentitySystem.Client.ViewModels.InfoSystems
{
    /// <summary>
    /// 创建信息系统ViewModel
    /// </summary>
    public class CreateInfoSystemViewModel : FlyoutBase
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
        public CreateInfoSystemViewModel(IAuthorizationContract authorizationContract)
        {
            this._authorizationContract = authorizationContract;

            //默认值
            base.Position = Position.Right;
            base.Margin = new Thickness(700, 30, 0, 30);

            this.ApplicationTypes = typeof(ApplicationType).GetEnumDictionary().ToDictionary(x => (ApplicationType)x.Key, x => x.Value);
        }

        #endregion

        #region # 属性

        #region 标题 —— override string Title
        /// <summary>
        /// 标题
        /// </summary>
        public override string Title
        {
            get { return "创建信息系统"; }
        }
        #endregion

        #region 信息系统编号 —— string InfoSystemNo
        /// <summary>
        /// 信息系统编号
        /// </summary>
        private string _infoSystemNo;

        /// <summary>
        /// 信息系统编号
        /// </summary>
        public string InfoSystemNo
        {
            get { return this._infoSystemNo; }
            set { this.Set(ref this._infoSystemNo, value); }
        }
        #endregion

        #region 信息系统名称 —— string InfoSystemName
        /// <summary>
        /// 信息系统名称
        /// </summary>
        private string _infoSystemName;

        /// <summary>
        /// 信息系统名称
        /// </summary>
        public string InfoSystemName
        {
            get { return this._infoSystemName; }
            set { this.Set(ref this._infoSystemName, value); }
        }
        #endregion

        #region 系统管理员账号 —— string AdminLoginId
        /// <summary>
        /// 系统管理员账号
        /// </summary>
        private string _adminLoginId;

        /// <summary>
        /// 系统管理员账号
        /// </summary>
        public string AdminLoginId
        {
            get { return this._adminLoginId; }
            set { this.Set(ref this._adminLoginId, value); }
        }
        #endregion

        #region 应用程序类型 —— ApplicationType ApplicationType
        /// <summary>
        /// 应用程序类型
        /// </summary>
        private ApplicationType _applicationType;

        /// <summary>
        /// 应用程序类型
        /// </summary>
        public ApplicationType ApplicationType
        {
            get { return this._applicationType; }
            set { this.Set(ref this._applicationType, value); }
        }
        #endregion

        #region 应用程序类型字典 —— IDictionary<ApplicationType, string> ApplicationTypes
        /// <summary>
        /// 应用程序类型字典
        /// </summary>
        private IDictionary<ApplicationType, string> _applicationTypes;

        /// <summary>
        /// 应用程序类型字典
        /// </summary>
        public IDictionary<ApplicationType, string> ApplicationTypes
        {
            get { return this._applicationTypes; }
            set { this.Set(ref this._applicationTypes, value); }
        }
        #endregion

        #endregion

        #region # 方法

        #region 创建信息系统 —— async void CreateInfoSystem()
        /// <summary>
        /// 创建信息系统
        /// </summary>
        public async void CreateInfoSystem()
        {
            this._authorizationContract.CreateInfoSystem(this.InfoSystemNo, this.InfoSystemName, this.AdminLoginId, this.ApplicationType);

            this.ExecuteOk = true;
            await ElementManager.ShowMessage("OK", "创建成功！");
            this.Close();
        }
        #endregion

        #endregion
    }
}
