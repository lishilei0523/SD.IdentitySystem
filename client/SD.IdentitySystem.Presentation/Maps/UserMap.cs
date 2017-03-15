using SD.Common.PoweredByLee;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;

namespace SD.IdentitySystem.Presentation.Maps
{
    /// <summary>
    /// 用户映射工具类
    /// </summary>
    public static class UserMap
    {
        #region # 用户视图模型映射 —— static UserView ToViewModel(this UserInfo...
        /// <summary>
        /// 用户视图模型映射
        /// </summary>
        /// <param name="userInfo">用户数据传输对象</param>
        /// <returns>用户视图模型</returns>
        public static UserView ToViewModel(this UserInfo userInfo)
        {
            UserView userView = Transform<UserInfo, UserView>.Map(userInfo);

            return userView;
        }
        #endregion

        #region # 用户登录记录视图模型映射 —— static LoginRecordView ToViewModel(this LoginRecordInfo...
        /// <summary>
        /// 用户登录记录视图模型映射
        /// </summary>
        /// <param name="loginRecordInfo">用户登录记录数据传输对象</param>
        /// <returns>登录记录视图模型</returns>
        public static LoginRecordView ToViewModel(this LoginRecordInfo loginRecordInfo)
        {
            LoginRecordView loginRecordView = Transform<LoginRecordInfo, LoginRecordView>.Map(loginRecordInfo);

            return loginRecordView;
        }
        #endregion
    }
}
