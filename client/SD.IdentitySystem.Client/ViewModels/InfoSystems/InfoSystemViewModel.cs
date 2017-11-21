using Caliburn.Micro;
using SD.IdentitySystem.Client.Commons;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.WPF.Interfaces;

namespace SD.IdentitySystem.Client.ViewModels.InfoSystems
{
    /// <summary>
    /// 信息系统ViewModel
    /// </summary>
    public class InfoSystemViewModel : DocumentBase, IPageable
    {
        #region # 依赖注入构造器

        /// <summary>
        /// 信息系统呈现器接口
        /// </summary>
        private readonly IInfoSystemPresenter _systemPresenter;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="systemPresenter">信息系统呈现器接口</param>
        public InfoSystemViewModel(IInfoSystemPresenter systemPresenter)
        {
            this._systemPresenter = systemPresenter;

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
            get { return "信息系统管理"; }
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

        #region 服务器列表 —— BindableCollection<InfoSystemView> InfoSystems
        /// <summary>
        /// 服务器列表
        /// </summary>
        private BindableCollection<InfoSystemView> _infoSystems;

        /// <summary>
        /// 服务器列表
        /// </summary>
        public BindableCollection<InfoSystemView> InfoSystems
        {
            get { return this._infoSystems; }
            set { this.Set(ref this._infoSystems, value); }
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
            PageModel<InfoSystemView> pageModel = this._systemPresenter.GetInfoSystems(this.Keywords, this.PageIndex, this.PageSize);
            this.RowCount = pageModel.RowCount;
            this.InfoSystems = new BindableCollection<InfoSystemView>(pageModel.Datas);
        }
        #endregion

        #region 创建信息系统 —— void CreateInfoSystem()
        /// <summary>
        /// 创建信息系统
        /// </summary>
        public void CreateInfoSystem()
        {
            CreateInfoSystemViewModel flyout = ElementManager.OpenFlyout<CreateInfoSystemViewModel>();
            flyout.FlyoutCloseEvent += x => this.RefreshData();
        }
        #endregion

        #region 初始化信息系统 —— void InitInfoSystem(InfoSystemView infoSystem)
        /// <summary>
        /// 初始化信息系统
        /// </summary>
        /// <param name="infoSystem">信息系统</param>
        public void InitInfoSystem(InfoSystemView infoSystem)
        {

        }
        #endregion

        #endregion
    }
}
