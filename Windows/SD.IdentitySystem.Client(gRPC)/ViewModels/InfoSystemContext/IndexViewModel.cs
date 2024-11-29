﻿using Caliburn.Micro;
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

namespace SD.IdentitySystem.Client.ViewModels.InfoSystemContext
{
    /// <summary>
    /// 信息系统首页视图模型
    /// </summary>
    public class IndexViewModel : ScreenBase, IPaginatable
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限管理服务契约接口代理
        /// </summary>
        private readonly ServiceProxy<IAuthorizationContract> _authorizationContract;

        /// <summary>
        /// 窗体管理器
        /// </summary>
        private readonly IWindowManager _windowManager;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public IndexViewModel(ServiceProxy<IAuthorizationContract> authorizationContract, IWindowManager windowManager)
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

        //Initializations

        #region 初始化 —— override async Task OnInitializeAsync(CancellationToken cancellationToken)
        /// <summary>
        /// 初始化
        /// </summary>
        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await this.ReloadInfoSystems();
        }
        #endregion


        //Actions

        #region 加载信息系统列表 —— async void LoadInfoSystems()
        /// <summary>
        /// 加载信息系统列表
        /// </summary>
        public async void LoadInfoSystems()
        {
            await this.ReloadInfoSystems();
        }
        #endregion

        #region 创建信息系统 —— async void CreateInfoSystem()
        /// <summary>
        /// 创建信息系统
        /// </summary>
        public async void CreateInfoSystem()
        {
            AddViewModel viewModel = ResolveMediator.Resolve<AddViewModel>();
            bool? result = await this._windowManager.ShowDialogAsync(viewModel);
            if (result == true)
            {
                await this.ReloadInfoSystems();
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
            viewModel.Load(infoSystem.Model);

            bool? result = await this._windowManager.ShowDialogAsync(viewModel);
            if (result == true)
            {
                await this.ReloadInfoSystems();
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
            viewModel.Load(infoSystem.Model);

            bool? result = await this._windowManager.ShowDialogAsync(viewModel);
            if (result == true)
            {
                await this.ReloadInfoSystems();
            }
        }
        #endregion


        //Private

        #region 加载信息系统列表 —— async Task ReloadInfoSystems()
        /// <summary>
        /// 加载信息系统列表
        /// </summary>
        private async Task ReloadInfoSystems()
        {
            this.Busy();

            PageModel<InfoSystemInfo> pageModel = await Task.Run(() => this._authorizationContract.Channel.GetInfoSystemsByPage(this.Keywords, this.PageIndex, this.PageSize));
            this.RowCount = pageModel.RowCount;
            this.PageCount = pageModel.PageCount;

            IEnumerable<Wrap<InfoSystemInfo>> wrapModels = pageModel.Datas.Select(x => x.Wrap());
            this.InfoSystems = new ObservableCollection<Wrap<InfoSystemInfo>>(wrapModels);

            this.Idle();
        }
        #endregion

        #endregion
    }
}
