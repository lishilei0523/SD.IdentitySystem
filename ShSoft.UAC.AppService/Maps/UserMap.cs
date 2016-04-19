using System.Collections.Generic;
using System.Linq;
using ShSoft.Framework2016.Common.PoweredByLee;
using ShSoft.UAC.Domain.Entities;
using ShSoft.UAC.Domain.Mediators;
using ShSoft.UAC.IAppService.DTOs.Outputs;

namespace ShSoft.UAC.AppService.Maps
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
        /// <param name="repMediator">仓储中介者</param>
        /// <returns>信息系统数据传输对象</returns>
        public static InfoSystemInfo ToDTO(this InfoSystem infoSystem, RepositoryMediator repMediator)
        {
            InfoSystemInfo systemInfo = Transform<InfoSystem, InfoSystemInfo>.Map(infoSystem);

            InfoSystemKind currentKind = repMediator.InfoSystemKindRep.Single(infoSystem.InfoSystemKindNo);

            systemInfo.InfoSystemKindInfo = currentKind.ToDTO();

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

        #region # 用户映射（含角色列表） —— static UserInfo ToDTO(this User user, string systemNo)
        /// <summary>
        /// 用户映射（含角色列表）
        /// </summary>
        /// <param name="user">用户领域模型</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>用户数据传输对象</returns>
        public static UserInfo ToDTO(this User user, string systemNo)
        {
            UserInfo userInfo = Transform<User, UserInfo>.Map(user);
            IEnumerable<Role> roles = user.GetRoles(systemNo);
            userInfo.RoleInfos = roles.Select(x => x.ToDTO());

            return userInfo;
        }
        #endregion

        #region # 角色映射 —— static RoleInfo ToDTO(this Role role)
        /// <summary>
        /// 角色映射
        /// </summary>
        /// <param name="role">角色领域模型</param>
        /// <returns>角色数据传输对象</returns>
        public static RoleInfo ToDTO(this Role role)
        {
            return Transform<Role, RoleInfo>.Map(role);
        }
        #endregion

        #region # 角色映射（带权限集） —— static RoleInfo ToDTOWithAuthority(this Role role)
        /// <summary>
        /// 角色映射（带权限集）
        /// </summary>
        /// <param name="role">角色领域模型</param>
        /// <returns>角色数据传输对象</returns>
        public static RoleInfo ToDTOWithAuthority(this Role role)
        {
            RoleInfo info = Transform<Role, RoleInfo>.Map(role);
            info.AuthorityInfos = role.GetAuthorities().Select(x => x.ToDTO());

            return info;
        }
        #endregion
    }
}