using System;
using SD.UAC.Domain.Entities;
using ShSoft.Framework2016.Infrastructure.IDomainService;

namespace SD.UAC.Domain.IDomainServices
{
    /// <summary>
    /// 信息系统领域服务接口
    /// </summary>
    public interface IInfoSystemService : IDomainService<InfoSystem>
    {
        #region # 是否存在角色 —— bool ExistsRole(string systemNo, Guid? roleId, string roleName)
        /// <summary>
        /// 是否存在角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="roleName">角色名称</param>
        /// <returns>是否存在</returns>
        bool ExistsRole(string systemNo, Guid? roleId, string roleName);
        #endregion
    }
}
