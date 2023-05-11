using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.Presentation.Maps;
using SD.IdentitySystem.Presentation.Models;
using SD.Infrastructure.DTOBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Presentation.Presenters
{
    /// <summary>
    /// 用户呈现器
    /// </summary>
    public class UserPresenter
    {
        #region # 字段及构造器

        /// <summary>
        /// 用户管理服务契约接口
        /// </summary>
        private readonly IUserContract _userContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public UserPresenter(IUserContract userContract)
        {
            this._userContract = userContract;
        }

        #endregion

        #region # 分页获取用户列表 —— PageModel<User> GetUsersByPage(string keywords...
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>用户列表</returns>
        public PageModel<User> GetUsersByPage(string keywords, string infoSystemNo, int pageIndex, int pageSize)
        {
            PageModel<UserInfo> pageModel = this._userContract.GetUsersByPage(keywords, infoSystemNo, null, pageIndex, pageSize);
            IEnumerable<User> users = pageModel.Datas.Select(x => x.ToModel());

            return new PageModel<User>(users, pageModel.PageIndex, pageModel.PageSize, pageModel.PageCount, pageModel.RowCount);
        }
        #endregion

        #region # 分页获取登录记录列表 —— PageModel<LoginRecord> GetLoginRecordsByPage(string keywords...
        /// <summary>
        /// 分页获取登录记录列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>登录记录列表</returns>
        public PageModel<LoginRecord> GetLoginRecordsByPage(string keywords, DateTime? startTime, DateTime? endTime, int pageIndex, int pageSize)
        {
            PageModel<LoginRecordInfo> pageModel = this._userContract.GetLoginRecordsByPage(keywords, startTime, endTime, pageIndex, pageSize);
            IEnumerable<LoginRecord> records = pageModel.Datas.Select(x => x.ToModel());

            return new PageModel<LoginRecord>(records, pageModel.PageIndex, pageModel.PageSize, pageModel.PageCount, pageModel.RowCount);
        }
        #endregion
    }
}
