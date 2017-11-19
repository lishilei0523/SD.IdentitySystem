using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using SD.IdentitySystem.Client.Commons;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.WPF.Interfaces;

namespace SD.IdentitySystem.Client.ViewModels.Servers
{
    /// <summary>
    /// 服务器ViewModel
    /// </summary>
    public class ServerViewModel : DocumentBase, IPageable
    {
        #region # 依赖注入构造器

        /// <summary>
        /// 服务器呈现器接口
        /// </summary>
        private readonly IServerPresenter _serverPresenter;

        /// <summary>
        /// 权限服务接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="serverPresenter">服务器呈现器接口</param>
        /// <param name="authorizationContract">权限服务接口</param>
        public ServerViewModel(IServerPresenter serverPresenter, IAuthorizationContract authorizationContract)
        {
            this._serverPresenter = serverPresenter;
            this._authorizationContract = authorizationContract;

            //默认值
            this.PageIndex = 1;
            this.PageSize = 15;

            this.RefreshData();
        }

        #endregion

        #region # 属性

        #region 标题 —— override string Title
        /// <summary>
        /// 标题
        /// </summary>
        public override string Title
        {
            get { return "服务器管理"; }
        }
        #endregion

        #region 关键字 —— string Keywords
        /// <summary>
        /// 关键字
        /// </summary>
        private string _keywords;

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keywords
        {
            get { return this._keywords; }
            set { this.Set(ref this._keywords, value); }
        }
        #endregion

        #region 页码 —— int PageIndex
        /// <summary>
        /// 页码
        /// </summary>
        private int _pageIndex;

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex
        {
            get { return this._pageIndex; }
            set { this.Set(ref this._pageIndex, value); }
        }
        #endregion

        #region 页容量 —— int PageSize
        /// <summary>
        /// 页容量
        /// </summary>
        private int _pageSize;

        /// <summary>
        /// 页容量
        /// </summary>
        public int PageSize
        {
            get { return this._pageSize; }
            set { this.Set(ref this._pageSize, value); }
        }
        #endregion

        #region 总记录数 —— int RowCount
        /// <summary>
        /// 总记录数
        /// </summary>
        private int _rowCount;

        /// <summary>
        /// 总记录数
        /// </summary>
        public int RowCount
        {
            get { return this._rowCount; }
            set { this.Set(ref this._rowCount, value); }
        }
        #endregion

        #region 服务器列表 —— BindableCollection<ServerView> Servers
        /// <summary>
        /// 服务器列表
        /// </summary>
        private BindableCollection<ServerView> _servers;

        /// <summary>
        /// 服务器列表
        /// </summary>
        public BindableCollection<ServerView> Servers
        {
            get { return this._servers; }
            set { this.Set(ref this._servers, value); }
        }
        #endregion

        #endregion

        #region # 方法

        #region 刷新数据 —— void RefreshData()
        /// <summary>
        /// 刷新数据
        /// </summary>
        public void RefreshData()
        {
            PageModel<ServerView> pageModel = this._serverPresenter.GetServersByPage(this.Keywords, this.PageIndex, this.PageSize);
            this.RowCount = pageModel.RowCount;
            this.Servers = new BindableCollection<ServerView>(pageModel.Datas);
        }
        #endregion

        #region 创建服务器 —— void CreateServer()
        /// <summary>
        /// 创建服务器
        /// </summary>
        public void CreateServer()
        {
            CreateServerViewModel flyout = ElementManager.OpenFlyout<CreateServerViewModel>();
            flyout.FlyoutCloseEvent += x => this.RefreshData();
        }
        #endregion

        #region 修改服务器 —— void UpdateServer(ServerView server)
        /// <summary>
        /// 修改服务器
        /// </summary>
        public void UpdateServer(ServerView server)
        {
            UpdateServerViewModel flyout = ElementManager.OpenFlyout<UpdateServerViewModel>();
            flyout.Initialize(server.Id, server.Name, server.ServiceOverDate);

            flyout.FlyoutCloseEvent += x => this.RefreshData();
        }
        #endregion

        #region 删除服务器 —— async void RemoveServer(ServerView server)
        /// <summary>
        /// 删除服务器
        /// </summary>
        /// <param name="server">服务器</param>
        public async void RemoveServer(ServerView server)
        {
            MessageDialogResult result = await ElementManager.ShowMessage("警告", "确定要删除吗？", MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                this._authorizationContract.RemoveServer(server.Id);
                this.Servers.Remove(server);
            }
        }
        #endregion

        #endregion
    }
}
