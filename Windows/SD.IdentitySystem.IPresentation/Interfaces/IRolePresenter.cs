using SD.IdentitySystem.IPresentation.Models.Outputs;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.PresentationBase;
using SD.Infrastructure.WPF.Models;
using System;
using System.Collections.Generic;

namespace SD.IdentitySystem.IPresentation.Interfaces
{
    /// <summary>
    /// 角色呈现器接口
    /// </summary>
    public interface IRolePresenter : IPresenter
    {
        #region # 获取角色 —— Role GetRole(Guid roleId)
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>角色</returns>
        Role GetRole(Guid roleId);
        #endregion

        #region # 获取角色列表 —— IEnumerable<Role> GetRoles(string systemNo)
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>角色列表</returns>
        IEnumerable<Role> GetRoles(string systemNo);
        #endregion

        #region # 获取用户角色列表 —— IEnumerable<Role> GetUserRoles(string loginId)
        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="loginId">用户登录名</param>
        /// <returns>角色列表</returns>
        IEnumerable<Role> GetUserRoles(string loginId);
        #endregion

        #region # 获取信息系统/角色树 —— IEnumerable<Node> GetInfoSystemRoleTree()
        /// <summary>
        /// 获取信息系统/角色树
        /// </summary>
        /// <returns>信息系统/角色树</returns>
        IEnumerable<Node> GetSystemRoleTree();
        #endregion

        #region # 获取用户的信息系统/角色树 —— IEnumerable<Node> GetUserSystemRoleTree(string loginId)
        /// <summary>
        /// 获取用户的信息系统/角色树
        /// </summary>
        /// <param name="loginId">用户登录名</param>
        /// <returns>信息系统/角色树</returns>
        IEnumerable<Node> GetUserSystemRoleTree(string loginId);
        #endregion

        #region # 分页获取角色列表 —— PageModel<Role> GetRolesByPage(string keywords...
        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>角色列表</returns>
        PageModel<Role> GetRolesByPage(string keywords, string systemNo, int pageIndex, int pageSize);
        #endregion
    }
}
