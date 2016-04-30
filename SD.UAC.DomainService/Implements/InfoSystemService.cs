using System;
using SD.UAC.Domain.Entities;
using SD.UAC.Domain.IDomainServices;
using SD.UAC.Domain.IRepositories;
using SD.UAC.Domain.Mediators;

namespace SD.UAC.DomainService.Implements
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
        private readonly IUnitOfWorkUAC _unitOfWork;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="repMediator">仓储中介者</param>
        /// <param name="unitOfWork">单元事务接口</param>
        public InfoSystemService(RepositoryMediator repMediator, IUnitOfWorkUAC unitOfWork)
        {
            this._repMediator = repMediator;
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region # 是否存在角色 —— bool ExistsRole(string systemNo, Guid? roleId, string roleName)
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
                return this._repMediator.RoleRep.ExistsName(roleName);
            }
            Role currentRole = this._repMediator.RoleRep.Single(roleId.Value);

            if (currentRole.Name != roleName)
            {
                return this._repMediator.RoleRep.ExistsName(roleName);
            }
            return false;
        }
        #endregion

        #region 没用

        /// <summary>
        /// 获取聚合根实体关键字
        /// </summary>
        /// <param name="entity">聚合根实体对象</param>
        /// <returns>关键字</returns>
        public string GetKeywords(InfoSystem entity)
        {
            throw new NotImplementedException("内部已实现");
        }


        #endregion
    }
}
