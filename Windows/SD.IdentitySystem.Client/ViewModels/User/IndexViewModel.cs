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

namespace SD.IdentitySystem.Client.ViewModels.User
{
    /// <summary>
    /// 用户首页视图模型
    /// </summary>
    public class IndexViewModel : Screen, IPaginatable
    {
        #region # 字段及构造器

        /// <summary>
        /// 用户服务契约接口
        /// </summary>
        private readonly IUserContract _userContract;

        /// <summary>
        /// 权限服务契约接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public IndexViewModel(IUserContract userContract, IAuthorizationContract authorizationContract)
        {
            this._userContract = userContract;
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

        #region 初始化 —— override async void OnInitialize()
        /// <summary>
        /// 初始化
        /// </summary>
        protected override async void OnInitialize()
        {
            IEnumerable<InfoSystemInfo> infoSystems = await Task.Run(() => this._authorizationContract.GetInfoSystems());
            this.InfoSystems = new ObservableCollection<InfoSystemInfo>(infoSystems);
            await this.LoadUsers();
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

        #region 加载用户列表 —— async Task LoadUsers()
        /// <summary>
        /// 加载用户列表
        /// </summary>
        public async Task LoadUsers()
        {
            LoadingIndicator.Suspend();

            PageModel<UserInfo> pageModel = await Task.Run(() => this._userContract.GetUsersByPage(this.Keywords, this.SelectedInfoSystem?.Number, null, this.PageIndex, this.PageSize));
            this.RowCount = pageModel.RowCount;
            this.PageCount = pageModel.PageCount;

            IEnumerable<Wrap<UserInfo>> wrapModels = pageModel.Datas.Select(x => x.Wrap());
            this.Users = new ObservableCollection<Wrap<UserInfo>>(wrapModels);

            LoadingIndicator.Dispose();
        }
        #endregion

        #region 创建用户 —— void CreateUser()
        /// <summary>
        /// 创建用户
        /// </summary>
        public void CreateUser()
        {
            //TODO 实现 创建用户
            MessageBox.Show("创建用户");
        }
        #endregion

        #region 启用用户 —— async void EnableUser(Wrap<UserInfo> user)
        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="user">用户</param>
        public async void EnableUser(Wrap<UserInfo> user)
        {
            LoadingIndicator.Suspend();

            await Task.Run(() => this._userContract.EnableUser(user.Model.Number));
            await this.LoadUsers();

            LoadingIndicator.Dispose();
        }
        #endregion

        #region 停用用户 —— async void DisableUser(Wrap<UserInfo> user)
        /// <summary>
        /// 停用用户
        /// </summary>
        /// <param name="user">用户</param>
        public async void DisableUser(Wrap<UserInfo> user)
        {
            LoadingIndicator.Suspend();

            await Task.Run(() => this._userContract.DisableUser(user.Model.Number));
            await this.LoadUsers();

            LoadingIndicator.Dispose();
        }
        #endregion

        #region 删除用户 —— async void RemoveUser(Wrap<UserInfo> user)
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user">用户</param>
        public async void RemoveUser(Wrap<UserInfo> user)
        {
            MessageBoxResult result = MessageBox.Show("您确定要删除吗？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                LoadingIndicator.Suspend();

                await Task.Run(() => this._userContract.RemoveUser(user.Model.Number));
                await this.LoadUsers();

                LoadingIndicator.Dispose();
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
                MessageBox.Show("请勾选要删除的用户！", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            #endregion

            MessageBoxResult result = MessageBox.Show("您确定要删除吗？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                LoadingIndicator.Suspend();

                await Task.Run(() => checkedUsers.ForEach(user => this._userContract.RemoveUser(user.Number)));
                await this.LoadUsers();

                LoadingIndicator.Dispose();
            }
        }
        #endregion

        #region 重置密码 —— void ResetPassword(Wrap<UserInfo> user)
        /// <summary>
        /// 重置密码
        /// </summary>
        public void ResetPassword(Wrap<UserInfo> user)
        {
            //TODO 实现 重置密码
            MessageBox.Show("重置密码");
        }
        #endregion

        #region 重置私钥 —— void ResetPrivateKey(Wrap<UserInfo> user)
        /// <summary>
        /// 重置私钥
        /// </summary>
        public void ResetPrivateKey(Wrap<UserInfo> user)
        {
            //TODO 实现 重置私钥
            MessageBox.Show("重置私钥");
        }
        #endregion

        #region 分配角色 —— void RelateRoles(Wrap<UserInfo> user)
        /// <summary>
        /// 分配角色
        /// </summary>
        public void RelateRoles(Wrap<UserInfo> user)
        {
            //TODO 实现 分配角色
            MessageBox.Show("分配角色");
        }
        #endregion

        #endregion
    }
}
