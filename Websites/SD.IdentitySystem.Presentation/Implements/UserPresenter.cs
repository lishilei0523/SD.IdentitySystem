using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.Models.Outputs;
using SD.IdentitySystem.Presentation.Maps;
using SD.Infrastructure.DTOBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Presentation.Implements
{
    /// <summary>
    /// 用户呈现器实现
    /// </summary>
    public class UserPresenter : IUserPresenter
    {
        #region # 字段及构造器

        /// <summary>
        /// 用户服务接口
        /// </summary>
        private readonly IUserContract _userContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="userContract">用户服务接口</param>
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
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>用户列表</returns>
        public PageModel<User> GetUsersByPage(string keywords, string systemNo, int pageIndex, int pageSize)
        {
            PageModel<UserInfo> pageModel = this._userContract.GetUsersByPage(keywords, systemNo, null, pageIndex, pageSize);
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
