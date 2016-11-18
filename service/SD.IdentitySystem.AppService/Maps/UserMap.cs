using System.Collections.Generic;
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
        #region # 信息系统映射 —— static InfoSystemInfo ToDTO(this InfoSystem infoSystem...
        /// <summary>
        /// 信息系统映射
        /// </summary>
        /// <param name="infoSystem">信息系统领域模型</param>
        /// <param name="infoSystemKindInfos">信息系统类别数据传输对象字典</param>
        /// <returns>信息系统数据传输对象</returns>
        public static InfoSystemInfo ToDTO(this InfoSystem infoSystem, IDictionary<string, InfoSystemKindInfo> infoSystemKindInfos)
        {
            InfoSystemInfo systemInfo = Transform<InfoSystem, InfoSystemInfo>.Map(infoSystem);

            systemInfo.InfoSystemKindInfo = infoSystemKindInfos[infoSystem.SystemKindNo];

            return systemInfo;
        }
        #endregion

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