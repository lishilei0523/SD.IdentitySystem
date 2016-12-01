using System.Collections.Generic;
using System.Linq;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using SD.IdentitySystem.Presentation.Maps;
using ShSoft.Infrastructure.DTOBase;

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

        #region # 分页获取用户列表 —— PageModel<UserView> GetUsers(string systemNo...
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>用户列表</returns>
        public PageModel<UserView> GetUsers(string systemNo, string keywords, int pageIndex, int pageSize)
        {
            PageModel<UserInfo> pageModel = this._userContract.GetUsers(systemNo, keywords, pageIndex, pageSize);

            IEnumerable<UserView> userViews = pageModel.Datas.Select(x => x.ToViewModel());

            return new PageModel<UserView>(userViews, pageModel.PageIndex, pageModel.PageSize, pageModel.PageCount, pageModel.RowCount);
        }
        #endregion

        #region # 获取用户 —— UserView GetUser(string loginId)
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <returns>用户</returns>
        public UserView GetUser(string loginId)
        {
            UserInfo userInfo = this._userContract.GetUser(loginId);

            return userInfo.ToViewModel();
        }
        #endregion
    }
}
