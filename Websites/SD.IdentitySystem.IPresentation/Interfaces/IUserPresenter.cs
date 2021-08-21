using SD.IdentitySystem.IPresentation.Models;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.PresentationBase;
using System;

namespace SD.IdentitySystem.IPresentation.Interfaces
{
    /// <summary>
    /// 用户呈现器接口
    /// </summary>
    public interface IUserPresenter : IPresenter
    {
        #region # 分页获取用户列表 —— PageModel<User> GetUsersByPage(string keywords...
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>用户列表</returns>
        PageModel<User> GetUsersByPage(string keywords, string systemNo, int pageIndex, int pageSize);
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
        PageModel<LoginRecord> GetLoginRecordsByPage(string keywords, DateTime? startTime, DateTime? endTime, int pageIndex, int pageSize);
        #endregion
    }
}
