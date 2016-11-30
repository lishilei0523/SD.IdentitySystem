using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using ShSoft.Infrastructure;
using ShSoft.Infrastructure.DTOBase;

namespace SD.IdentitySystem.IPresentation.Interfaces
{
    /// <summary>
    /// 用户呈现器接口
    /// </summary>
    public interface IUserPresenter : IPresenter
    {
        #region # 分页获取用户列表 —— PageModel<UserView> GetUsers(string systemNo...
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>用户列表</returns>
        PageModel<UserView> GetUsers(string systemNo, string keywords, int pageIndex, int pageSize);
        #endregion

        #region # 获取用户 —— UserView GetUser(string loginId)
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <returns>用户</returns>
        UserView GetUser(string loginId);
        #endregion

        #region # 是否存在用户 —— bool ExistsUser(string loginId)
        /// <summary>
        /// 是否存在用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <returns>是否存在</returns>
        bool ExistsUser(string loginId);
        #endregion
    }
}
