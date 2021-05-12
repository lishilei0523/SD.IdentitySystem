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

namespace SD.IdentitySystem.Client.ViewModels
{
    /// <summary>
    /// 登录记录视图模型
    /// </summary>
    public class LoginRecordViewModel : Screen, IPaginatable
    {
        #region # 字段及构造器

        /// <summary>
        /// 用户服务契约接口
        /// </summary>
        private readonly IUserContract _userContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public LoginRecordViewModel(IUserContract userContract)
        {
            this._userContract = userContract;

            //默认值
            this.PageIndex = 1;
            this.PageSize = 20;
        }

        #endregion

        #region # 属性

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

        #region 加载登录记录列表 —— async Task LoadLoginRecords()
        /// <summary>
        /// 加载登录记录列表
        /// </summary>
        public async Task LoadLoginRecords()
        {
            LoadingIndicator.Suspend();
            PageModel<LoginRecordInfo> pageModel = await Task.Run(() => this._userContract.GetLoginRecordsByPage(null, null, null, this.PageIndex, this.PageSize));
            LoadingIndicator.Dispose();

            this.RowCount = pageModel.RowCount;
            this.PageCount = pageModel.PageCount;

            IEnumerable<Wrap<LoginRecordInfo>> wrapModels = pageModel.Datas.Select(x => new Wrap<LoginRecordInfo> { Model = x });
            this.LoginRecords = new ObservableCollection<Wrap<LoginRecordInfo>>(wrapModels);
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

        #endregion
    }
}
