using Caliburn.Micro;
using SD.Common;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.Presentation.Presenters;
using SD.Infrastructure.Constants;
using SD.Infrastructure.WPF.Caliburn.Aspects;
using SD.Infrastructure.WPF.Caliburn.Base;
using SD.IOC.Core.Mediators;
using SD.Toolkits.Recursion.Tree;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.Extensions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Models = SD.IdentitySystem.Presentation.Models;

namespace SD.IdentitySystem.Client.ViewModels.Menu
{
    /// <summary>
    /// 菜单首页视图模型
    /// </summary>
    public class IndexViewModel : ScreenBase
    {
        #region # 字段及构造器

        /// <summary>
        /// 菜单呈现器
        /// </summary>
        private readonly MenuPresenter _menuPresenter;

        /// <summary>
        /// 权限管理服务契约接口代理
        /// </summary>
        private readonly ServiceProxy<IAuthorizationContract> _authorizationContract;

        /// <summary>
        /// 窗口管理器
        /// </summary>
        private readonly IWindowManager _windowManager;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public IndexViewModel(MenuPresenter menuPresenter, ServiceProxy<IAuthorizationContract> authorizationContract, IWindowManager windowManager)
        {
            this._menuPresenter = menuPresenter;
            this._authorizationContract = authorizationContract;
            this._windowManager = windowManager;
        }

        #endregion

        #region # 属性

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

        #region 菜单列表 —— ObservableCollection<Menu> Menus
        /// <summary>
        /// 菜单列表
        /// </summary>
        [DependencyProperty]
        public ObservableCollection<Models.Menu> Menus { get; set; }
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

        #region 初始化 —— override async Task OnInitializeAsync(CancellationToken cancellationToken)
        /// <summary>
        /// 初始化
        /// </summary>
        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            IEnumerable<InfoSystemInfo> infoSystems = await Task.Run(() => this._authorizationContract.Channel.GetInfoSystems(), cancellationToken);
            this.InfoSystems = new ObservableCollection<InfoSystemInfo>(infoSystems);
            this.ApplicationTypes = typeof(ApplicationType).GetEnumMembers();

            await this.ReloadMenus();
        }
        #endregion


        //Actions

        #region 加载菜单列表 —— async void LoadMenus()
        /// <summary>
        /// 加载菜单列表
        /// </summary>
        public async void LoadMenus()
        {
            await this.ReloadMenus();
        }
        #endregion

        #region 创建菜单 —— async void CreateMenu()
        /// <summary>
        /// 创建菜单
        /// </summary>
        public async void CreateMenu()
        {
            AddViewModel viewModel = ResolveMediator.Resolve<AddViewModel>();
            viewModel.Load(this.InfoSystems);
            bool? result = await this._windowManager.ShowDialogAsync(viewModel);
            if (result == true)
            {
                await this.ReloadMenus();
            }
        }
        #endregion

        #region 修改菜单 —— async void UpdateMenu(Menu menu)
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="menu">菜单</param>
        public async void UpdateMenu(Models.Menu menu)
        {
            UpdateViewModel viewModel = ResolveMediator.Resolve<UpdateViewModel>();
            viewModel.Load(menu);
            bool? result = await this._windowManager.ShowDialogAsync(viewModel);
            if (result == true)
            {
                await this.ReloadMenus();
            }
        }
        #endregion

        #region 删除菜单 —— async void RemoveMenu(Menu menu)
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menu">菜单</param>
        public async void RemoveMenu(Models.Menu menu)
        {
            MessageBoxResult result = MessageBox.Show("确定要删除吗？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                this.Busy();

                await Task.Run(() => this._authorizationContract.Channel.RemoveMenu(menu.Id));
                await this.ReloadMenus();

                this.Idle();
            }
        }
        #endregion

        #region 批量删除菜单 —— async void RemoveMenus()
        /// <summary>
        /// 批量删除菜单
        /// </summary>
        public async void RemoveMenus()
        {
            #region # 加载勾选

            IList<Models.Menu> checkedMenus = new List<Models.Menu>();
            foreach (Models.Menu menu in this.Menus)
            {
                if (menu.IsChecked == true)
                {
                    checkedMenus.Add(menu);
                }

                foreach (Models.Menu subNode in menu.GetDeepSubNodes())
                {
                    if (subNode.IsChecked == true)
                    {
                        checkedMenus.Add(subNode);
                    }
                }
            }
            if (!checkedMenus.Any())
            {
                MessageBox.Show("请勾选要删除的菜单！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            #endregion

            MessageBoxResult result = MessageBox.Show("确定要删除吗？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                this.Busy();

                IAuthorizationContract authorizationContract = this._authorizationContract.Channel;
                await Task.Run(() => checkedMenus.ForEach(menu => authorizationContract.RemoveMenu(menu.Id)));
                await this.ReloadMenus();

                this.Idle();
            }
        }
        #endregion

        #region 关联权限 —— async void RelateAuthorities(Menu menu)
        /// <summary>
        /// 关联权限
        /// </summary>
        /// <param name="menu">菜单</param>
        public async void RelateAuthorities(Models.Menu menu)
        {
            this.Busy();

            RelateAuthorityViewModel viewModel = ResolveMediator.Resolve<RelateAuthorityViewModel>();
            await viewModel.Load(menu.Id);

            this.Idle();

            await this._windowManager.ShowDialogAsync(viewModel);
        }
        #endregion


        //Private

        #region 加载菜单列表 —— async Task ReloadMenus()
        /// <summary>
        /// 加载菜单列表
        /// </summary>
        private async Task ReloadMenus()
        {
            this.Busy();

            string infoSystemNo = this.SelectedInfoSystem?.Number;
            ApplicationType? applicationType = this.SelectedApplicationType;

            IEnumerable<Models.Menu> menus = await Task.Run(() => this._menuPresenter.GetMenuTreeList(infoSystemNo, applicationType));
            this.Menus = new ObservableCollection<Models.Menu>(menus);

            this.Idle();
        }
        #endregion

        #endregion
    }
}
