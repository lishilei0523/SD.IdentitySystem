using SD.Common;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.WPF.Aspects;
using SD.Infrastructure.WPF.Base;
using SD.Infrastructure.WPF.Extensions;
using SD.Infrastructure.WPF.Interfaces;
using SD.Infrastructure.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SD.IdentitySystem.Client.ViewModels.LoginRecord
{
    /// <summary>
    /// 登录记录首页视图模型
    /// </summary>
    public class IndexViewModel : ScreenBase, IPaginatable
    {
        #region # 字段及构造器

        /// <summary>
        /// 用户服务契约接口
        /// </summary>
        private readonly IUserContract _userContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public IndexViewModel(IUserContract userContract)
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

        #region 初始化 —— override async void OnInitialize()
        /// <summary>
        /// 初始化
        /// </summary>
        protected override async void OnInitialize()
        {
            await this.LoadLoginRecords();
        }
        #endregion

        #region 全选 —— void CheckAll()
        /// <summary>
        /// 全选
        /// </summary>
        public void CheckAll()
        {
            this.LoginRecords.ForEach(x => x.IsChecked = true);
        }
        #endregion

        #region 取消全选 —— void UncheckAll()
        /// <summary>
        /// 取消全选
        /// </summary>
        public void UncheckAll()
        {
            this.LoginRecords.ForEach(x => x.IsChecked = false);
        }
        #endregion

        #region 加载登录记录列表 —— async Task LoadLoginRecords()
        /// <summary>
        /// 加载登录记录列表
        /// </summary>
        public async Task LoadLoginRecords()
        {
            this.Busy();

            PageModel<LoginRecordInfo> pageModel = await Task.Run(() => this._userContract.GetLoginRecordsByPage(this.Keywords, this.StartTime, this.EndTime, this.PageIndex, this.PageSize));
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
