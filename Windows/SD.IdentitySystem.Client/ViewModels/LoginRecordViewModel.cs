using Caliburn.Micro;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.WPF.Aspects;
using SD.Infrastructure.WPF.Interfaces;
using SD.Infrastructure.WPF.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SD.IdentitySystem.Client.ViewModels
{
    /// <summary>
    /// 登录记录视图模型
    /// </summary>
    public class LoginRecordViewModel : Screen, IPaginatable
    {
        private readonly IUserContract _userContract;

        public LoginRecordViewModel(IUserContract userContract)
        {
            this._userContract = userContract;

            //默认值
            this.PageIndex = 1;
            this.PageSize = 30;
        }



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



        [DependencyProperty]
        public ObservableCollection<Wrap<LoginRecordInfo>> LoginRecords { get; set; }


        public void LoadLoginRecords()
        {
            PageModel<LoginRecordInfo> pageModel = this._userContract.GetLoginRecordsByPage(null, null, null, this.PageIndex, this.PageSize);
            this.RowCount = pageModel.RowCount;
            this.PageCount = pageModel.PageCount;

            IEnumerable<Wrap<LoginRecordInfo>> wrapModels = pageModel.Datas.Select(x => new Wrap<LoginRecordInfo> { Model = x });
            this.LoginRecords = new ObservableCollection<Wrap<LoginRecordInfo>>(wrapModels);
        }
    }
}
