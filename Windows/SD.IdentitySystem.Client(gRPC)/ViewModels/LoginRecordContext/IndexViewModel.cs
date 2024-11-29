﻿using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.WPF.Caliburn.Aspects;
using SD.Infrastructure.WPF.Caliburn.Base;
using SD.Infrastructure.WPF.Extensions;
using SD.Infrastructure.WPF.Interfaces;
using SD.Infrastructure.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace SD.IdentitySystem.Client.ViewModels.LoginRecordContext
{
    /// <summary>
    /// 登录记录首页视图模型
    /// </summary>
    public class IndexViewModel : ScreenBase, IPaginatable
    {
        #region # 字段及构造器

        /// <summary>
        /// 用户管理服务契约接口代理
        /// </summary>
        private readonly ServiceProxy<IUserContract> _userContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public IndexViewModel(ServiceProxy<IUserContract> userContract)
        {
            this._userContract = userContract;

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

        #region 开始时间 —— DateTime? StartTime
        /// <summary>
        /// 开始时间
        /// </summary>
        [DependencyProperty]
        public DateTime? StartTime { get; set; }
        #endregion

        #region 结束时间 —— DateTime? EndTime
        /// <summary>
        /// 结束时间
        /// </summary>
        [DependencyProperty]
        public DateTime? EndTime { get; set; }
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

        #region 登录记录列表 —— ObservableCollection<Wrap<LoginRecordInfo>> LoginRecords
        /// <summary>
        /// 登录记录列表
        /// </summary>
        [DependencyProperty]
        public ObservableCollection<Wrap<LoginRecordInfo>> LoginRecords { get; set; }
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
            await this.ReloadLoginRecords();
        }
        #endregion


        //Actions

        #region 加载登录记录列表 —— async void LoadLoginRecords()
        /// <summary>
        /// 加载登录记录列表
        /// </summary>
        public async void LoadLoginRecords()
        {
            await this.ReloadLoginRecords();
        }
        #endregion


        //Private

        #region 加载登录记录列表 —— async Task ReloadLoginRecords()
        /// <summary>
        /// 加载登录记录列表
        /// </summary>
        private async Task ReloadLoginRecords()
        {
            this.Busy();

            PageModel<LoginRecordInfo> pageModel = await Task.Run(() => this._userContract.Channel.GetLoginRecordsByPage(this.Keywords, this.StartTime, this.EndTime, this.PageIndex, this.PageSize));
            this.RowCount = pageModel.RowCount;
            this.PageCount = pageModel.PageCount;

            IEnumerable<Wrap<LoginRecordInfo>> wrapModels = pageModel.Datas.Select(x => x.Wrap());
            this.LoginRecords = new ObservableCollection<Wrap<LoginRecordInfo>>(wrapModels);

            this.Idle();
        }
        #endregion

        #endregion
    }
}
