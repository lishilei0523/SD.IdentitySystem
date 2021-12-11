using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IPresentation.Models;
using SD.Toolkits.Mapper;

namespace SD.IdentitySystem.Presentation.Maps
{
    /// <summary>
    /// 用户映射
    /// </summary>
    public static class UserMap
    {
        #region # 用户模型映射 —— static User ToModel(this UserInfo...
        /// <summary>
        /// 用户模型映射
        /// </summary>
        public static User ToModel(this UserInfo userInfo)
        {
            User user = userInfo.Map<UserInfo, User>();
            user.Status = user.Enabled ? "已启用" : "已停用";

            return user;
        }
        #endregion

        #region # 用户登录记录模型映射 —— static LoginRecord ToModel(this LoginRecordInfo...
        /// <summary>
        /// 用户登录记录模型映射
        /// </summary>
        public static LoginRecord ToModel(this LoginRecordInfo loginRecordInfo)
        {
            LoginRecord loginRecord = loginRecordInfo.Map<LoginRecordInfo, LoginRecord>();

            return loginRecord;
        }
        #endregion
    }
}
