using System;
using SD.IdentitySystem.Domain.Entities;
using ShSoft.Infrastructure.DomainServiceBase;

namespace SD.IdentitySystem.Domain.IDomainServices
{
    /// <summary>
    /// 信息系统领域服务接口
    /// </summary>
    public interface IInfoSystemService : IDomainService<InfoSystem>
    {
        #region # 是否存在角色 —— bool ExistsRole(string systemKindNo, Guid? roleId...
        /// <summary>
        /// 是否存在角色
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="roleName">角色名称</param>
        /// <returns>是否存在</returns>
        bool ExistsRole(string systemKindNo, Guid? roleId, string roleName);
        #endregion
    }
}
