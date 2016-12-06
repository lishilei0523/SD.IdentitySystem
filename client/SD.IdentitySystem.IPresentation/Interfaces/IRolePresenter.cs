using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using ShSoft.Infrastructure.DTOBase;
using ShSoft.Infrastructure.MVC;
using System;
using System.Collections.Generic;

namespace SD.IdentitySystem.IPresentation.Interfaces
{
    /// <summary>
    /// 角色呈现器接口
    /// </summary>
    public interface IRolePresenter : IPresenter
    {
        #region # 获取角色列表 —— IEnumerable<RoleView> GetRoles(string systemNo)
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>角色列表</returns>
        IEnumerable<RoleView> GetRoles(string systemNo);
        #endregion

        #region # 分页获取角色列表 —— PageModel<RoleView> GetRolesByPage(string systemNo...
        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>角色列表</returns>
        PageModel<RoleView> GetRolesByPage(string systemNo, string keywords, int pageIndex, int pageSize);
        #endregion

        #region # 获取角色 —— RoleView GetRole(Guid roleId)
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>角色</returns>
        RoleView GetRole(Guid roleId);
        #endregion
    }
}
