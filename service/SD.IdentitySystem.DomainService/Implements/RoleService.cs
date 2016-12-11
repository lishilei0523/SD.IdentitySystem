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
        #region # 字段及依赖注入构造器

        /// <summary>
        /// 仓储中介者
        /// </summary>
        private readonly RepositoryMediator _repMediator;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="repMediator">仓储中介者</param>
        public RoleService(RepositoryMediator repMediator)
        {
            this._repMediator = repMediator;
        }

        #endregion

        #region # 是否存在角色 —— bool ExistsRole(string systemNo, Guid? roleId...
        /// <summary>
        /// 是否存在角色
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="roleName">角色名称</param>
        /// <returns>是否存在</returns>
        public bool ExistsRole(string systemNo, Guid? roleId, string roleName)
        {
            if (roleId == null)
            {
                return this._repMediator.RoleRep.Exists(systemNo, roleName);
            }

            Role currentRole = this._repMediator.RoleRep.Single(roleId.Value);

            if (currentRole.Name != roleName)
            {
                return this._repMediator.RoleRep.Exists(systemNo, roleName);
            }

            return false;
        }
        #endregion

        #region # 获取聚合根实体关键字 —— string GetKeywords(Role entity)
        /// <summary>
        /// 获取聚合根实体关键字
        /// </summary>
        /// <param name="entity">聚合根实体对象</param>
        /// <returns>关键字</returns>
        public string GetKeywords(Role entity)
        {
            return entity.Keywords;
        }
        #endregion
    }
}
