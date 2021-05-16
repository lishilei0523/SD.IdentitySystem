using Caliburn.Micro;
using SD.Common;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.WPF.Aspects;
using SD.Infrastructure.WPF.Base;
using SD.Infrastructure.WPF.Extensions;
using SD.Infrastructure.WPF.Interfaces;
using SD.Infrastructure.WPF.Models;
using SD.IOC.Core.Mediators;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels.Authority
{
    /// <summary>
    /// 权限首页视图模型
    /// </summary>
    public class IndexViewModel : ScreenBase, IPaginatable
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限服务契约接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 窗口管理器
        /// </summary>
        private readonly IWindowManager _windowManager;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public IndexViewModel(IAuthorizationContract authorizationContract, IWindowManager windowManager)
        {
            this._authorizationContract = authorizationContract;
            this._windowManager = windowManager;

            //默认值
            this.PageIndex = 1;
            this.PageSize = 20;
        }

        #endregion

        #region # 属性

        #region 关键字 —— string Keywords
        /// <summary>
        /// 关键字
        /// </summary>
        [DependencyProperty]
        public string Keywords { get; set; }
        #endregion

        #region 页码 —— int PageIndex
        /// <summary>
        /// 页码
        /// </summary>
        [DependencyProperty]
        public int PageIndex { get; set; }
        #endregion

        #region 页容量 —— int PageSize
        /// <summary>
        /// 页容量
        /// </summary>
        [DependencyProperty]
        public int PageSize { get; set; }
        #endregion

        #region 总记录数 —— int RowCount
        /// <summary>
        /// 总记录数
        /// </summary>
        [DependencyProperty]
        public int RowCount { get; set; }
        #endregion

        #region 总页数 —— int PageCount
        /// <summary>
        /// 总页数
        /// </summary>
        [DependencyProperty]
        public int PageCount { get; set; }
        #endregion

        #region 已选信息系统 —— InfoSystemInfo SelectedInfoSystem
        /// <summary>
        /// 已选信息系统
        /// </summary>
        [DependencyProperty]
        public InfoSystemInfo SelectedInfoSystem { get; set; }
        #endregion

        #region 权限列表 —— ObservableCollection<Wrap<AuthorityInfo>> Authorities
        /// <summary>
        /// 权限列表
        /// </summary>
        [DependencyProperty]
        public ObservableCollection<Wrap<AuthorityInfo>> Authorities { get; set; }
        #endregion

        #region 信息系统列表 —— ObservableCollection<InfoSystemInfo> InfoSystems
        /// <summary>
        /// 信息系统列表
        /// </summary>
        [DependencyProperty]
        public ObservableCollection<InfoSystemInfo> InfoSystems { get; set; }
        #endregion

        #endregion

        #region # 方法

        #region 初始化 —— override async void OnInitialize()
        /// <summary>
        /// 初始化
        /// </summary>
        protected override async void OnInitialize()
        {
            IEnumerable<InfoSystemInfo> infoSystems = await Task.Run(() => this._authorizationContract.GetInfoSystems());
            this.InfoSystems = new ObservableCollection<InfoSystemInfo>(infoSystems);
            await this.LoadAuthorities();
        }
        #endregion

        #region 全选 —— void CheckAll()
        /// <summary>
        /// 全选
        /// </summary>
        public void CheckAll()
        {
            this.Authorities.ForEach(x => x.IsChecked = true);
        }
        #endregion

        #region 取消全选 —— void UncheckAll()
        /// <summary>
        /// 取消全选
        /// </summary>
        public void UncheckAll()
        {
            this.Authorities.ForEach(x => x.IsChecked = false);
        }
        #endregion

        #region 加载权限列表 —— async Task LoadAuthorities()
        /// <summary>
        /// 加载权限列表
        /// </summary>
        public async Task LoadAuthorities()
        {
            this.Busy();

            PageModel<AuthorityInfo> pageModel = await Task.Run(() => this._authorizationContract.GetAuthoritiesByPage(this.Keywords, this.SelectedInfoSystem?.Number, this.PageIndex, this.PageSize));
            this.RowCount = pageModel.RowCount;
            this.PageCount = pageModel.PageCount;

            IEnumerable<Wrap<AuthorityInfo>> wrapModels = pageModel.Datas.Select(x => x.Wrap());
            this.Authorities = new ObservableCollection<Wrap<AuthorityInfo>>(wrapModels);

            this.Idle();
        }
        #endregion

        #region 创建权限 —— async void CreateAuthority()
        /// <summary>
        /// 创建权限
        /// </summary>
        public async void CreateAuthority()
        {
            AddViewModel viewModel = ResolveMediator.Resolve<AddViewModel>();
            bool? result = this._windowManager.ShowDialog(viewModel);
            if (result == true)
            {
                await this.LoadAuthorities();
            }
        }
        #endregion

        #region 修改权限 —— async void UpdateAuthority(Wrap<AuthorityInfo> authority)
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="authority">权限</param>
        public async void UpdateAuthority(Wrap<AuthorityInfo> authority)
        {
            UpdateViewModel viewModel = ResolveMediator.Resolve<UpdateViewModel>();
            await viewModel.Load(authority.Model.Id);
            bool? result = this._windowManager.ShowDialog(viewModel);
            if (result == true)
            {
                await this.LoadAuthorities();
            }
        }
        #endregion

        #region 删除权限 —— async void RemoveAuthority(Wrap<AuthorityInfo> authority)
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="authority">权限</param>
        public async void RemoveAuthority(Wrap<AuthorityInfo> authority)
        {
            MessageBoxResult result = MessageBox.Show("您确定要删除吗？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                this.Busy();

                await Task.Run(() => this._authorizationContract.RemoveAuthority(authority.Model.Id));
                await this.LoadAuthorities();

                this.Idle();
            }
        }
        #endregion

        #region 批量删除权限 —— async void RemoveAuthorities()
        /// <summary>
        /// 批量删除权限
        /// </summary>
        public async void RemoveAuthorities()
        {
            #region # 加载勾选

            AuthorityInfo[] checkedAuthorities = this.Authorities.Where(x => x.IsChecked == true).Select(x => x.Model).ToArray();
            if (!checkedAuthorities.Any())
            {
                MessageBox.Show("请勾选要删除的权限！", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            #endregion

            MessageBoxResult result = MessageBox.Show("您确定要删除吗？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                this.Busy();

                await Task.Run(() => checkedAuthorities.ForEach(authority => this._authorizationContract.RemoveAuthority(authority.Id)));
                await this.LoadAuthorities();

                this.Idle();
            }
        }
        #endregion

        #endregion
    }
}
