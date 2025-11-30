using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.Presentation.Models;
using SD.Toolkits.Mapper;

namespace SD.IdentitySystem.Presentation.Maps
{
    /// <summary>
    /// 用户映射
    /// </summary>
    public static class UserMap
    {
        #region # 用户映射 —— static User ToModel(this UserInfo...
        /// <summary>
        /// 用户映射
        /// </summary>
        public static User ToModel(this UserInfo userInfo)
        {
            User user = userInfo.Map<UserInfo, User>();
            user.Status = user.Enabled ? "已启用" : "已停用";

            return user;
        }
        #endregion

        #region # 登录记录映射 —— static LoginRecord ToModel(this LoginRecordInfo...
        /// <summary>
        /// 登录记录映射
        /// </summary>
        public static LoginRecord ToModel(this LoginRecordInfo loginRecordInfo)
        {
            LoginRecord loginRecord = loginRecordInfo.Map<LoginRecordInfo, LoginRecord>();

            return loginRecord;
        }
        #endregion
    }
}
