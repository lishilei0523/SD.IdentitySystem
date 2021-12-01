using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IDomainServices;
using SD.IdentitySystem.Domain.Mediators;
using System;

namespace SD.IdentitySystem.DomainService.Implements
{
    /// <summary>
    /// 角色领域服务实现
    /// </summary>
    public class RoleService : IRoleService
    {
        #region # 字段及构造器

        /// <summary>
        /// 仓储中介者
        /// </summary>
        private readonly RepositoryMediator _repMediator;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public RoleService(RepositoryMediator repMediator)
        {
            this._repMediator = repMediator;
        }

        #endregion

        #region # 是否存在角色 —— bool ExistsRole(string infoSystemNo, Guid? roleId...
        /// <summary>
        /// 是否存在角色
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="roleName">角色名称</param>
        /// <returns>是否存在</returns>
        public bool ExistsRole(string infoSystemNo, Guid? roleId, string roleName)
        {
            if (!roleId.HasValue)
            {
                return this._repMediator.RoleRep.Exists(infoSystemNo, roleName);
            }

            Role role = this._repMediator.RoleRep.Single(roleId.Value);
            if (role.Name != roleName)
            {
                return this._repMediator.RoleRep.Exists(infoSystemNo, roleName);
            }

            return false;
        }
        #endregion
    }
}
