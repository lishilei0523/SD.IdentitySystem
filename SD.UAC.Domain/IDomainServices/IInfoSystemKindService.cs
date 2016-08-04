using System;
using SD.UAC.Domain.Entities;
using ShSoft.Infrastructure.DomainServiceBase;

namespace SD.UAC.Domain.IDomainServices
{
    /// <summary>
    /// 信息系统类别领域服务接口
    /// </summary>
    public interface IInfoSystemKindService : IDomainService<InfoSystemKind>
    {
        #region # 断言权限不存在 —— void AssertAuthorityNotExsits(string systemKindNo, string authorityPath)
        /// <summary>
        /// 断言权限不存在
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="authorityPath">权限路径</param>
        void AssertAuthorityNotExsits(string systemKindNo, string authorityPath);
        #endregion

        #region # 断言菜单名称不存在 —— void AssertMenuNotExists(string systemKindNo...
        /// <summary>
        /// 断言菜单名称不存在
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="parentId">上级菜单Id</param>
        /// <param name="menuName">菜单名称</param>
        void AssertMenuNotExists(string systemKindNo, Guid? parentId, string menuName);
        #endregion
    }
}
