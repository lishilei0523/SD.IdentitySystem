using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IDomainServices;
using SD.IdentitySystem.Domain.IRepositories;
using SD.IdentitySystem.Domain.Mediators;
using System;

namespace SD.IdentitySystem.DomainService.Implements
{
    /// <summary>
    /// 信息系统领域服务实现
    /// </summary>
    public class InfoSystemService : IInfoSystemService
    {
        #region # 字段及依赖注入构造器

        /// <summary>
        /// 仓储中介者
        /// </summary>
        private readonly RepositoryMediator _repMediator;

        /// <summary>
        /// 单元事务接口
        /// </summary>
        private readonly IUnitOfWorkIdentity _unitOfWork;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="repMediator">仓储中介者</param>
        /// <param name="unitOfWork">单元事务接口</param>
        public InfoSystemService(RepositoryMediator repMediator, IUnitOfWorkIdentity unitOfWork)
        {
            this._repMediator = repMediator;
            this._unitOfWork = unitOfWork;
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

        #region # 获取聚合根实体关键字 —— string GetKeywords(InfoSystem entity)
        /// <summary>
        /// 获取聚合根实体关键字
        /// </summary>
        /// <param name="entity">聚合根实体对象</param>
        /// <returns>关键字</returns>
        public string GetKeywords(InfoSystem entity)
        {
            return entity.Keywords;
        }
        #endregion
    }
}
