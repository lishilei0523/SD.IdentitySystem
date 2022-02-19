using Caliburn.Micro;
using SD.Common;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.WPF.Caliburn.Aspects;
using SD.Infrastructure.WPF.Caliburn.Base;
using SD.Infrastructure.WPF.Extensions;
using SD.Infrastructure.WPF.Interfaces;
using SD.Infrastructure.WPF.Models;
using SD.IOC.Core.Mediators;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.Extensions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels.User
{
    /// <summary>
    /// 用户首页视图模型
    /// </summary>
    public class IndexViewModel : ScreenBase, IPaginatable
    {
        #region # 字段及构造器

        /// <summary>
        /// 用户管理服务契约接口代理
        /// </summary>
        private readonly ServiceProxy<IUserContract> _userContract;

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
        public IndexViewModel(ServiceProxy<IUserContract> userContract, ServiceProxy<IAuthorizationContract> authorizationContract, IWindowManager windowManager)
        {
            this._userContract = userContract;
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

        #region 用户列表 —— ObservableCollection<Wrap<UserInfo>> Users
        /// <summary>
        /// 用户列表
        /// </summary>
        [DependencyProperty]
        public ObservableCollection<Wrap<UserInfo>> Users { get; set; }
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

        //Initializations

        #region 初始化 —— override async Task OnInitializeAsync(CancellationToken cancellationToken)
        /// <summary>
        /// 初始化
        /// </summary>
        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            IEnumerable<InfoSystemInfo> infoSystems = await Task.Run(() => this._authorizationContract.Channel.GetInfoSystems(null), cancellationToken);
            this.InfoSystems = new ObservableCollection<InfoSystemInfo>(infoSystems);

            await this.ReloadUsers();
        }
        #endregion


        //Actions

        #region 加载用户列表 —— async void LoadUsers()
        /// <summary>
        /// 加载用户列表
        /// </summary>
        public async void LoadUsers()
        {
            await this.ReloadUsers();
        }
        #endregion

        #region 创建用户 —— async void CreateUser()
        /// <summary>
        /// 创建用户
        /// </summary>
        public async void CreateUser()
        {
            AddViewModel viewModel = ResolveMediator.Resolve<AddViewModel>();
            bool? result = await this._windowManager.ShowDialogAsync(viewModel);
            if (result == true)
            {
                await this.ReloadUsers();
            }
        }
        #endregion

        #region 启用用户 —— async void EnableUser(Wrap<UserInfo> user)
        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="user">用户</param>
        public async void EnableUser(Wrap<UserInfo> user)
        {
            MessageBoxResult result = MessageBox.Show("确定要启用吗？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                this.Busy();

                await Task.Run(() => this._userContract.Channel.EnableUser(user.Model.Number));
                await this.ReloadUsers();

                this.Idle();
            }
        }
        #endregion

        #region 停用用户 —— async void DisableUser(Wrap<UserInfo> user)
        /// <summary>
        /// 停用用户
        /// </summary>
        /// <param name="user">用户</param>
        public async void DisableUser(Wrap<UserInfo> user)
        {
            MessageBoxResult result = MessageBox.Show("确定要停用吗？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                this.Busy();

                await Task.Run(() => this._userContract.Channel.DisableUser(user.Model.Number));
                await this.ReloadUsers();

                this.Idle();
            }
        }
        #endregion

        #region 删除用户 —— async void RemoveUser(Wrap<UserInfo> user)
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user">用户</param>
        public async void RemoveUser(Wrap<UserInfo> user)
        {
            MessageBoxResult result = MessageBox.Show("确定要删除吗？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                this.Busy();

                await Task.Run(() => this._userContract.Channel.RemoveUser(user.Model.Number));
                await this.ReloadUsers();

                this.Idle();
            }
        }
        #endregion

        #region 批量删除用户 —— async void RemoveUsers()
        /// <summary>
        /// 批量删除用户
        /// </summary>
        public async void RemoveUsers()
        {
            #region # 加载勾选

            UserInfo[] checkedUsers = this.Users.Where(x => x.IsChecked == true).Select(x => x.Model).ToArray();
            if (!checkedUsers.Any())
            {
                MessageBox.Show("请勾选要删除的用户！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            #endregion

            MessageBoxResult result = MessageBox.Show("确定要删除吗？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                this.Busy();

                IUserContract userContract = this._userContract.Channel;
                await Task.Run(() => checkedUsers.ForEach(user => userContract.RemoveUser(user.Number)));
                await this.ReloadUsers();

                this.Idle();
            }
        }
        #endregion

        #region 重置密码 —— async void ResetPassword(Wrap<UserInfo> user)
        /// <summary>
        /// 重置密码
        /// </summary>
        public async void ResetPassword(Wrap<UserInfo> user)
        {
            ResetPasswordViewModel viewModel = ResolveMediator.Resolve<ResetPasswordViewModel>();
            viewModel.Load(user.Model.Number);
            await this._windowManager.ShowDialogAsync(viewModel);
        }
        #endregion

        #region 重置私钥 —— async void ResetPrivateKey(Wrap<UserInfo> user)
        /// <summary>
        /// 重置私钥
        /// </summary>
        public async void ResetPrivateKey(Wrap<UserInfo> user)
        {
            ResetPrivateKeyViewModel viewModel = ResolveMediator.Resolve<ResetPrivateKeyViewModel>();
            viewModel.Load(user.Model);
            bool? result = await this._windowManager.ShowDialogAsync(viewModel);
            if (result == true)
            {
                await this.ReloadUsers();
            }
        }
        #endregion

        #region 分配角色 —— async void RelateRoles(Wrap<UserInfo> user)
        /// <summary>
        /// 分配角色
        /// </summary>
        public async void RelateRoles(Wrap<UserInfo> user)
        {
            this.Busy();

            RelateRoleViewModel viewModel = ResolveMediator.Resolve<RelateRoleViewModel>();
            await viewModel.Load(user.Model.Number);

            this.Idle();

            await this._windowManager.ShowDialogAsync(viewModel);
        }
        #endregion

        #region 全选 —— void CheckAll()
        /// <summary>
        /// 全选
        /// </summary>
        public void CheckAll()
        {
            this.Users.ForEach(x => x.IsChecked = true);
        }
        #endregion

        #region 取消全选 —— void UncheckAll()
        /// <summary>
        /// 取消全选
        /// </summary>
        public void UncheckAll()
        {
            this.Users.ForEach(x => x.IsChecked = false);
        }
        #endregion


        //Private

        #region 加载用户列表 —— async Task ReloadUsers()
        /// <summary>
        /// 加载用户列表
        /// </summary>
        public async Task ReloadUsers()
        {
            this.Busy();

            PageModel<UserInfo> pageModel = await Task.Run(() => this._userContract.Channel.GetUsersByPage(this.Keywords, this.SelectedInfoSystem?.Number, null, this.PageIndex, this.PageSize));
            this.RowCount = pageModel.RowCount;
            this.PageCount = pageModel.PageCount;

            IEnumerable<Wrap<UserInfo>> wrapModels = pageModel.Datas.Select(x => x.Wrap());
            this.Users = new ObservableCollection<Wrap<UserInfo>>(wrapModels);

            this.Idle();
        }
        #endregion

        #endregion
    }
}
