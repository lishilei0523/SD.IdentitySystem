using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.Presentation.Presenters;
using SD.Infrastructure.WPF.Caliburn.Aspects;
using SD.Infrastructure.WPF.Caliburn.Base;
using SD.Infrastructure.WPF.Extensions;
using SD.Infrastructure.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.Extensions;
using System.Threading.Tasks;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels.Role
{
    /// <summary>
    /// 角色创建视图模型
    /// </summary>
    public class AddViewModel : ScreenBase
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限呈现器
        /// </summary>
        private readonly AuthorityPresenter _authorityPresenter;

        /// <summary>
        /// 权限服务契约接口代理
        /// </summary>
        private readonly ServiceProxy<IAuthorizationContract> _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public AddViewModel(AuthorityPresenter authorityPresenter, ServiceProxy<IAuthorizationContract> authorizationContract)
        {
            this._authorityPresenter = authorityPresenter;
            this._authorizationContract = authorizationContract;
        }

        #endregion

        #region # 属性

        #region 角色名称 —— string RoleName
        /// <summary>
        /// 角色名称
        /// </summary>
        [DependencyProperty]
        public string RoleName { get; set; }
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

        #region 信息系统列表 —— ObservableCollection<InfoSystemInfo> InfoSystems
        /// <summary>
        /// 信息系统列表
        /// </summary>
        [DependencyProperty]
        public ObservableCollection<InfoSystemInfo> InfoSystems { get; set; }
        #endregion

        #region 权限数据项列表 —— ObservableCollection<Item> AuthorityItems
        /// <summary>
        /// 权限数据项列表
        /// </summary>
        [DependencyProperty]
        public ObservableCollection<Item> AuthorityItems { get; set; }
        #endregion

        #endregion

        #region # 方法

        //Initializations

        #region 初始化 —— override async void OnInitialize()
        /// <summary>
        /// 初始化
        /// </summary>
        protected override async void OnInitialize()
        {
            IEnumerable<InfoSystemInfo> infoSystems = await Task.Run(() => this._authorizationContract.Channel.GetInfoSystems());
            this.InfoSystems = new ObservableCollection<InfoSystemInfo>(infoSystems);
        }
        #endregion


        //Actions

        #region 加载权限列表 —— async void LoadAuthorities()
        /// <summary>
        /// 加载权限列表
        /// </summary>
        public async void LoadAuthorities()
        {
            this.Busy();

            await this.ReloadAuthorities();

            this.Idle();
        }
        #endregion

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
            if (string.IsNullOrWhiteSpace(this.RoleName))
            {
                MessageBox.Show("角色名称不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            this.Busy();

            IEnumerable<Guid> authorityIds = this.AuthorityItems.Where(x => x.IsChecked == true).Select(x => x.Id);
            await Task.Run(() => this._authorizationContract.Channel.CreateRole(this.SelectedInfoSystem.Number, this.RoleName, this.Description, authorityIds));

            base.TryClose(true);
            this.Idle();
        }
        #endregion


        //Private

        #region 加载权限列表 —— async Task ReloadAuthorities()
        /// <summary>
        /// 加载权限列表
        /// </summary>
        private async Task ReloadAuthorities()
        {
            if (this.SelectedInfoSystem == null)
            {
                this.AuthorityItems = new ObservableCollection<Item>();
            }
            else
            {
                ICollection<Item> authorityItems = await Task.Run(() => this._authorityPresenter.GetSystemAuthorityItems(this.SelectedInfoSystem.Number));
                this.AuthorityItems = new ObservableCollection<Item>(authorityItems);
            }

            this.AuthorityItems.Group();
        }
        #endregion

        #endregion
    }
}
