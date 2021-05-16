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

namespace SD.IdentitySystem.Client.ViewModels.InfoSystem
{
    /// <summary>
    /// 信息系统首页视图模型
    /// </summary>
    public class IndexViewModel : ScreenBase, IPaginatable
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限服务契约接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 窗体管理器
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

        #region 信息系统列表 —— ObservableCollection<Wrap<InfoSystemInfo>> InfoSystems
        /// <summary>
        /// 信息系统列表
        /// </summary>
        [DependencyProperty]
        public ObservableCollection<Wrap<InfoSystemInfo>> InfoSystems { get; set; }
        #endregion

        #endregion

        #region # 方法

        #region 初始化 —— override void OnInitialize()
        /// <summary>
        /// 初始化
        /// </summary>
        protected override void OnInitialize()
        {
            this.LoadInfoSystems();
        }
        #endregion

        #region 全选 —— void CheckAll()
        /// <summary>
        /// 全选
        /// </summary>
        public void CheckAll()
        {
            this.InfoSystems.ForEach(x => x.IsChecked = true);
        }
        #endregion

        #region 取消全选 —— void UncheckAll()
        /// <summary>
        /// 取消全选
        /// </summary>
        public void UncheckAll()
        {
            this.InfoSystems.ForEach(x => x.IsChecked = false);
        }
        #endregion

        #region 加载信息系统列表 —— async void LoadInfoSystems()
        /// <summary>
        /// 加载信息系统列表
        /// </summary>
        public async void LoadInfoSystems()
        {
            this.Busy();

            PageModel<InfoSystemInfo> pageModel = await Task.Run(() => this._authorizationContract.GetInfoSystemsByPage(this.Keywords, this.PageIndex, this.PageSize));
            this.RowCount = pageModel.RowCount;
            this.PageCount = pageModel.PageCount;

            IEnumerable<Wrap<InfoSystemInfo>> wrapModels = pageModel.Datas.Select(x => x.Wrap());
            this.InfoSystems = new ObservableCollection<Wrap<InfoSystemInfo>>(wrapModels);

            this.Idle();
        }
        #endregion

        #region 创建信息系统 —— async void CreateInfoSystem()
        /// <summary>
        /// 创建信息系统
        /// </summary>
        public async void CreateInfoSystem()
        {
            AddViewModel viewModel = ResolveMediator.Resolve<AddViewModel>();
            bool? result = this._windowManager.ShowDialog(viewModel);
            if (result == true)
            {
                this.LoadInfoSystems();
            }
        }
        #endregion

        #region 修改信息系统 —— async void UpdateInfoSystem(...
        /// <summary>
        /// 修改信息系统
        /// </summary>
        /// <param name="infoSystem">信息系统</param>
        public async void UpdateInfoSystem(Wrap<InfoSystemInfo> infoSystem)
        {
            UpdateViewModel viewModel = ResolveMediator.Resolve<UpdateViewModel>();
            viewModel.Load(infoSystem.Model.Number);

            bool? result = this._windowManager.ShowDialog(viewModel);
            if (result == true)
            {
                this.LoadInfoSystems();
            }
        }
        #endregion

        #region 初始化信息系统 —— async void InitInfoSystem(...
        /// <summary>
        /// 初始化信息系统
        /// </summary>
        /// <param name="infoSystem">信息系统</param>
        public async void InitInfoSystem(Wrap<InfoSystemInfo> infoSystem)
        {
            InitViewModel viewModel = ResolveMediator.Resolve<InitViewModel>();
            viewModel.Load(infoSystem.Model.Number);

            bool? result = this._windowManager.ShowDialog(viewModel);
            if (result == true)
            {
                this.LoadInfoSystems();
            }
        }
        #endregion

        #endregion
    }
}
