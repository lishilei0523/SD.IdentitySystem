using System;
using SD.UAC.Domain.Entities;
using SD.UAC.Domain.IDomainServices;
using SD.UAC.Domain.Mediators;

namespace SD.UAC.DomainService.Implements
{
    /// <summary>
    /// 信息系统类别领域服务实现
    /// </summary>
    public class InfoSystemKindService : IInfoSystemKindService
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
        public InfoSystemKindService(RepositoryMediator repMediator)
        {
            this._repMediator = repMediator;
        }

        #endregion

        #region # 断言权限不存在 —— void AssertAuthorityNotExsits(string systemKindNo, string authorityPath)
        /// <summary>
        /// 断言权限不存在
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="authorityPath">权限路径</param>
        public void AssertAuthorityNotExsits(string systemKindNo, string authorityPath)
        {
            if (this._repMediator.AuthorityRep.Exists(systemKindNo, authorityPath))
            {
                throw new ArgumentOutOfRangeException("authorityPath", string.Format("信息系统类别\"{0}\"中已存在该权限！", systemKindNo));
            }
        }
        #endregion

        #region # 断言菜单名称不存在 —— void AssertMenuNotExists(string systemKindNo...
        /// <summary>
        /// 断言菜单名称不存在
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="parentId">上级菜单Id</param>
        /// <param name="menuName">菜单名称</param>
        public void AssertMenuNotExists(string systemKindNo, Guid? parentId, string menuName)
        {
            if (this._repMediator.MenuRep.Exists(parentId, menuName))
            {
                throw new ArgumentOutOfRangeException("menuName", @"在给定菜单级别下菜单名称已存在！");
            }
        }
        #endregion

        #region 没用

        /// <summary>
        /// 获取聚合根实体关键字
        /// </summary>
        /// <param name="entity">聚合根实体对象</param>
        /// <returns>关键字</returns>
        public string GetKeywords(InfoSystemKind entity)
        {
            throw new NotImplementedException("内部已实现");
        }


        #endregion
    }
}
