using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using ShSoft.Common.PoweredByLee;

namespace SD.IdentitySystem.AppService.Maps
{
    /// <summary>
    /// 用户相关映射工具类
    /// </summary>
    public static class UserMap
    {
        #region # 用户映射 —— static UserInfo ToDTO(this User user)
        /// <summary>
        /// 用户映射
        /// </summary>
        /// <param name="user">用户领域模型</param>
        /// <returns>用户数据传输对象</returns>
        public static UserInfo ToDTO(this User user)
        {
            return Transform<User, UserInfo>.Map(user);
        }
        #endregion

        #region # 登录记录映射 —— static LoginRecordInfo ToDTO(this LoginRecord...
        /// <summary>
        /// 登录记录映射
        /// </summary>
        /// <param name="loginRecord">登录记录户领域模型</param>
        /// <returns>登录记录数据传输对象</returns>
        public static LoginRecordInfo ToDTO(this LoginRecord loginRecord)
        {
            return Transform<LoginRecord, LoginRecordInfo>.Map(loginRecord);
        }
        #endregion
    }
}