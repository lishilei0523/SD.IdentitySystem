using Caliburn.Micro;
using SD.Common;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.WPF.Aspects;
using SD.Infrastructure.WPF.Extensions;
using SD.Infrastructure.WPF.Interfaces;
using SD.Infrastructure.WPF.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels.Role
{
    /// <summary>
    /// 角色首页视图模型
    /// </summary>
    public class IndexViewModel : Screen, IPaginatable
    {
        #region # 字段及构造器

        /// <summary>
        /// 角色服务契约接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public IndexViewModel(IAuthorizationContract authorizationContract)
        {
            this._authorizationContract = authorizationContract;

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

        #region 角色列表 —— ObservableCollection<Wrap<RoleInfo>> Roles
        /// <summary>
        /// 角色列表
        /// </summary>
        [DependencyProperty]
        public ObservableCollection<Wrap<RoleInfo>> Roles { get; set; }
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
            await this.LoadRoles();
        }
        #endregion

        #region 全选 —— void CheckAll()
        /// <summary>
        /// 全选
        /// </summary>
        public void CheckAll()
        {
            this.Roles.ForEach(x => x.IsChecked = true);
        }
        #endregion

        #region 取消全选 —— void UncheckAll()
        /// <summary>
        /// 取消全选
        /// </summary>
        public void UncheckAll()
        {
            this.Roles.ForEach(x => x.IsChecked = false);
        }
        #endregion

        #region 加载角色列表 —— async Task LoadRoles()
        /// <summary>
        /// 加载角色列表
        /// </summary>
        public async Task LoadRoles()
        {
            LoadingIndicator.Suspend();

            PageModel<RoleInfo> pageModel = await Task.Run(() => this._authorizationContract.GetRolesByPage(this.Keywords, this.SelectedInfoSystem?.Number, this.PageIndex, this.PageSize));
            this.RowCount = pageModel.RowCount;
            this.PageCount = pageModel.PageCount;

            IEnumerable<Wrap<RoleInfo>> wrapModels = pageModel.Datas.Select(x => x.Wrap());
            this.Roles = new ObservableCollection<Wrap<RoleInfo>>(wrapModels);

            LoadingIndicator.Dispose();
        }
        #endregion

        #region 创建角色 —— void CreateRole()
        /// <summary>
        /// 创建角色
        /// </summary>
        public void CreateRole()
        {
            //TODO 实现
            MessageBox.Show("创建角色");
        }
        #endregion

        #region 修改角色 —— void UpdateRole(Wrap<RoleInfo> role)
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="role">角色</param>
        public void UpdateRole(Wrap<RoleInfo> role)
        {
            //TODO 实现
            MessageBox.Show("修改角色");
        }
        #endregion

        #region 删除角色 —— async void RemoveRole(Wrap<RoleInfo> role)
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="role">角色</param>
        public async void RemoveRole(Wrap<RoleInfo> role)
        {
            MessageBoxResult result = MessageBox.Show("您确定要删除吗？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                LoadingIndicator.Suspend();

                await Task.Run(() => this._authorizationContract.RemoveRole(role.Model.Id));
                await this.LoadRoles();

                LoadingIndicator.Dispose();
            }
        }
        #endregion

        #region 批量删除角色 —— async void RemoveRoles()
        /// <summary>
        /// 批量删除角色
        /// </summary>
        public async void RemoveRoles()
        {
            #region # 加载勾选

            RoleInfo[] checkedRoles = this.Roles.Where(x => x.IsChecked == true).Select(x => x.Model).ToArray();
            if (!checkedRoles.Any())
            {
                MessageBox.Show("请勾选要删除的角色！", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            #endregion

            MessageBoxResult result = MessageBox.Show("您确定要删除吗？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                LoadingIndicator.Suspend();

                await Task.Run(() => checkedRoles.ForEach(role => this._authorizationContract.RemoveRole(role.Id)));
                await this.LoadRoles();

                LoadingIndicator.Dispose();
            }
        }
        #endregion

        #endregion
    }
}
