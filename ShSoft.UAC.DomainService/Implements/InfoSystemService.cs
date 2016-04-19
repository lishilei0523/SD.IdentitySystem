using System;
using System.Collections.Generic;
using ShSoft.UAC.Domain.Entities;
using ShSoft.UAC.Domain.IDomainServices;
using ShSoft.UAC.Domain.IRepositories;
using ShSoft.UAC.Domain.Mediators;

namespace ShSoft.UAC.DomainService.Implements
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

        #region # 获取代理人角色 —— Role GetAgentRole(string systemNo)
        /// <summary>
        /// 获取代理人角色
        /// </summary>
        /// <returns>代理人角色</returns>
        public Role GetAgentRole(string systemNo)
        {
            InfoSystem currenSystem = this._unitOfWork.Resolve<InfoSystem>(systemNo);
            Role agentRole = currenSystem.GetAgentRole();

            #region # 非空验证

            if (agentRole == null)
            {
                //创建代理人角色
                agentRole = new Role(InfoSystem.AgentRoleName, InfoSystem.AgentRoleName);

                //为当前信息系统添加角色
                currenSystem.CreateRole(agentRole);

                //为角色授权
                InfoSystemKind currentKind = this._unitOfWork.Resolve<InfoSystemKind>(currenSystem.InfoSystemKindNo);
                agentRole.SetAuthorities(currentKind.Authorities);
            }

            #endregion

            //清空代理人的所有用户
            agentRole.RemoveUsers();
            this._unitOfWork.Commit();

            return agentRole;
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
