using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.Toolkits.Mapper;

namespace SD.IdentitySystem.AppService.Maps
{
    /// <summary>
    /// 用户相关映射
    /// </summary>
    public static class UserMap
    {
        #region # 用户映射 —— static UserInfo ToDTO(this User user)
        /// <summary>
        /// 用户映射
        /// </summary>
        public static UserInfo ToDTO(this User user)
        {
            UserInfo userInfo = user.Map<User, UserInfo>();

            return userInfo;
        }
        #endregion

        #region # 登录记录映射 —— static LoginRecordInfo ToDTO(this LoginRecord...
        /// <summary>
        /// 登录记录映射
        /// </summary>
        public static LoginRecordInfo ToDTO(this LoginRecord loginRecord)
        {
            LoginRecordInfo recordInfo = loginRecord.Map<LoginRecord, LoginRecordInfo>();

            return recordInfo;
        }
        #endregion
    }
}