using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using SD.IdentitySystem.Presentation.Maps;
using ShSoft.Infrastructure.DTOBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Presentation.Implements
{
    /// <summary>
    /// 角色呈现器实现
    /// </summary>
    public class RolePresenter : IRolePresenter
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限服务接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="authorizationContract">权限服务接口</param>
        public RolePresenter(IAuthorizationContract authorizationContract)
        {
            this._authorizationContract = authorizationContract;
        }

        #endregion

        #region # 获取角色列表 —— IEnumerable<RoleView> GetRoles(string systemNo)
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>角色列表</returns>
        public IEnumerable<RoleView> GetRoles(string systemNo)
        {
            IEnumerable<RoleInfo> roleInfos = this._authorizationContract.GetRoles(systemNo);

            IEnumerable<RoleView> roleViews = roleInfos.Select(x => x.ToViewModel());

            return roleViews;
        }
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
        public PageModel<RoleView> GetRolesByPage(string systemNo, string keywords, int pageIndex, int pageSize)
        {
            PageModel<RoleInfo> pageModel = this._authorizationContract.GetRolesByPage(systemNo, keywords, pageIndex, pageSize);

            IEnumerable<RoleView> roleViews = pageModel.Datas.Select(x => x.ToViewModel());

            return new PageModel<RoleView>(roleViews, pageModel.PageIndex, pageModel.PageSize, pageModel.PageCount, pageModel.RowCount);
        }
        #endregion

        #region # 获取角色 —— RoleView GetRole(Guid roleId)
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>角色</returns>
        public RoleView GetRole(Guid roleId)
        {
            RoleInfo roleInfo = this._authorizationContract.GetRole(roleId);

            return roleInfo.ToViewModel();
        }
        #endregion
    }
}
