using SD.Common;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.Presentation.Presentors;
using SD.Infrastructure.Constants;
using SD.Infrastructure.WPF.Aspects;
using SD.Infrastructure.WPF.Base;
using SD.Infrastructure.WPF.Extensions;
using SD.Infrastructure.WPF.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels.Menu
{
    /// <summary>
    /// 菜单创建视图模型
    /// </summary>
    public class AddViewModel : ScreenBase
    {
        #region # 字段及构造器

        /// <summary>
        /// 菜单呈现器
        /// </summary>
        private readonly MenuPresenter _menuPresenter;

        /// <summary>
        /// 角色服务契约接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public AddViewModel(MenuPresenter menuPresenter, IAuthorizationContract authorizationContract)
        {
            this._menuPresenter = menuPresenter;
            this._authorizationContract = authorizationContract;
        }

        #endregion

        #region # 属性

        #region 上级菜单名称 —— string ParentMenuName
        /// <summary>
        /// 上级菜单名称
        /// </summary>
        [DependencyProperty]
        public string ParentMenuName { get; set; }
        #endregion

        #region 菜单名称 —— string MenuName
        /// <summary>
        /// 菜单名称
        /// </summary>
        [DependencyProperty]
        public string MenuName { get; set; }
        #endregion

        #region 链接地址 —— string Url
        /// <summary>
        /// 链接地址
        /// </summary>
        [DependencyProperty]
        public string Url { get; set; }
        #endregion

        #region 路径 —— string Path
        /// <summary>
        /// 路径
        /// </summary>
        [DependencyProperty]
        public string Path { get; set; }
        #endregion

        #region 图标 —— string Icon
        /// <summary>
        /// 图标
        /// </summary>
        [DependencyProperty]
        public string Icon { get; set; }
        #endregion

        #region 排序 —— int? Sort
        /// <summary>
        /// 排序
        /// </summary>
        [DependencyProperty]
        public int? Sort { get; set; }
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

        #region 上级菜单 —— Node ParentMenu
        /// <summary>
        /// 上级菜单
        /// </summary>
        [DependencyProperty]
        public Node ParentMenu { get; set; }
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

        #region 菜单树 —— ObservableCollection<Node> MenuTree
        /// <summary>
        /// 菜单树
        /// </summary>
        [DependencyProperty]
        public ObservableCollection<Node> MenuTree { get; set; }
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
            IEnumerable<InfoSystemInfo> infoSystems = await Task.Run(() => this._authorizationContract.GetInfoSystems());
            this.InfoSystems = new ObservableCollection<InfoSystemInfo>(infoSystems);
            this.ApplicationTypes = typeof(ApplicationType).GetEnumMembers();
        }
        #endregion


        //Actions

        #region 加载菜单树 —— async void LoadMenuTree()
        /// <summary>
        /// 加载菜单树
        /// </summary>
        public async void LoadMenuTree()
        {
            this.Busy();

            await this.ReloadMenuTree();

            this.Idle();
        }
        #endregion

        #region 选中上级菜单 —— void SelectParentMenu(Node node)
        /// <summary>
        /// 选中上级菜单
        /// </summary>
        /// <param name="node">节点</param>
        public void SelectParentMenu(Node node)
        {
            this.ParentMenuName = node.Name;
            this.ParentMenu = node;
        }
        #endregion

        #region 清空选中上级菜单 —— void ClearSelectParentMenu()
        /// <summary>
        /// 清空选中上级菜单
        /// </summary>
        public void ClearSelectParentMenu()
        {
            this.ParentMenuName = null;
            this.ParentMenu = null;
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
            if (!this.SelectedApplicationType.HasValue)
            {
                MessageBox.Show("应用程序类型不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.MenuName))
            {
                MessageBox.Show("菜单名称不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!this.Sort.HasValue)
            {
                MessageBox.Show("排序不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            this.Busy();

            await Task.Run(() => this._authorizationContract.CreateMenu(this.SelectedInfoSystem.Number, this.SelectedApplicationType.Value, this.MenuName, this.Sort.Value, this.Url, this.Path, this.Icon, this.ParentMenu?.Id));

            base.TryClose(true);
            this.Idle();
        }
        #endregion


        //Privates

        #region 加载菜单树 —— async Task ReloadMenuTree()
        /// <summary>
        /// 加载菜单树
        /// </summary>
        public async Task ReloadMenuTree()
        {
            #region # 验证

            if (this.SelectedInfoSystem == null || this.SelectedApplicationType == null)
            {
                this.MenuTree = new ObservableCollection<Node>();
                return;
            }

            #endregion

            ICollection<Node> menuTree = await Task.Run(() => this._menuPresenter.GetMenuTree(this.SelectedInfoSystem.Number, this.SelectedApplicationType.Value));
            this.MenuTree = new ObservableCollection<Node>(menuTree);
        }
        #endregion

        #endregion
    }
}
