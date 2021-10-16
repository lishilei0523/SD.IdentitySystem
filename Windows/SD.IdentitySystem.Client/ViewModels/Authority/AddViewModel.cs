using SD.Common;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.WPF.Caliburn.Aspects;
using SD.Infrastructure.WPF.Caliburn.Base;
using SD.Infrastructure.WPF.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel.Extensions;
using System.Threading.Tasks;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels.Authority
{
    /// <summary>
    /// 权限创建视图模型
    /// </summary>
    public class AddViewModel : ScreenBase
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限服务契约接口代理
        /// </summary>
        private readonly ServiceProxy<IAuthorizationContract> _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public AddViewModel(ServiceProxy<IAuthorizationContract> authorizationContract)
        {
            this._authorizationContract = authorizationContract;
        }

        #endregion

        #region # 属性

        #region 权限名称 —— string AuthorityName
        /// <summary>
        /// 权限名称
        /// </summary>
        [DependencyProperty]
        public string AuthorityName { get; set; }
        #endregion

        #region 权限路径 —— string AuthorityPath
        /// <summary>
        /// 权限路径
        /// </summary>
        [DependencyProperty]
        public string AuthorityPath { get; set; }
        #endregion

        #region 英文名称 —— string EnglishName
        /// <summary>
        /// 英文名称
        /// </summary>
        [DependencyProperty]
        public string EnglishName { get; set; }
        #endregion

        #region 程序集名称 —— string AssemblyName
        /// <summary>
        /// 程序集名称
        /// </summary>
        [DependencyProperty]
        public string AssemblyName { get; set; }
        #endregion

        #region 命名空间 —— string Namespace
        /// <summary>
        /// 命名空间
        /// </summary>
        [DependencyProperty]
        public string Namespace { get; set; }
        #endregion

        #region 类名 —— string ClassName
        /// <summary>
        /// 类名
        /// </summary>
        [DependencyProperty]
        public string ClassName { get; set; }
        #endregion

        #region 方法名 —— string MethodName
        /// <summary>
        /// 方法名
        /// </summary>
        [DependencyProperty]
        public string MethodName { get; set; }
        #endregion

        #region 描述 —— string Description
        /// <summary>
        /// 描述
        /// </summary>
        [DependencyProperty]
        public string Description { get; set; }
        #endregion

        #region 已选信息系统 —— InfoSystemInfo SelectedInfoSystem
        /// <summary>
        /// 已选信息系统
        /// </summary>
        [DependencyProperty]
        public InfoSystemInfo SelectedInfoSystem { get; set; }
        #endregion

        #region 已选应用程序类型 —— ApplicationType? SelectedApplicationType
        /// <summary>
        /// 已选应用程序类型
        /// </summary>
        [DependencyProperty]
        public ApplicationType? SelectedApplicationType { get; set; }
        #endregion

        #region 信息系统列表 —— ObservableCollection<InfoSystemInfo> InfoSystems
        /// <summary>
        /// 信息系统列表
        /// </summary>
        [DependencyProperty]
        public ObservableCollection<InfoSystemInfo> InfoSystems { get; set; }
        #endregion

        #region 应用程序类型字典 —— IDictionary<string, string> ApplicationTypes
        /// <summary>
        /// 应用程序类型字典
        /// </summary>
        [DependencyProperty]
        public IDictionary<string, string> ApplicationTypes { get; set; }
        #endregion

        #endregion

        #region # 方法

        //Initializations

        #region 加载 —— void Load(IEnumerable<InfoSystemInfo> infoSystems)
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="infoSystems">信息系统集</param>
        public void Load(IEnumerable<InfoSystemInfo> infoSystems)
        {
            this.InfoSystems = new ObservableCollection<InfoSystemInfo>(infoSystems);
            this.ApplicationTypes = typeof(ApplicationType).GetEnumMembers();
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

            if (this.SelectedInfoSystem == null)
            {
                MessageBox.Show("信息系统不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!this.SelectedApplicationType.HasValue)
            {
                MessageBox.Show("应用程序类型不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.AuthorityName))
            {
                MessageBox.Show("权限名称不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            this.Busy();

            await Task.Run(() => this._authorizationContract.Channel.CreateAuthority(this.SelectedInfoSystem.Number, this.SelectedApplicationType.Value, this.AuthorityName, this.AuthorityPath, this.EnglishName, this.AssemblyName, this.Namespace, this.ClassName, this.MethodName, this.Description));

            await base.TryCloseAsync(true);
            this.Idle();
        }
        #endregion

        #endregion
    }
}
