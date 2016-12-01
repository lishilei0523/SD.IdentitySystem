using System;
using System.Collections.Generic;
using System.Linq;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using SD.IdentitySystem.Presentation.Maps;
using ShSoft.Infrastructure.DTOBase;

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

        #region # 获取角色列表 —— IEnumerable<RoleView> GetRoles(string systemKindNo)
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>角色列表</returns>
        public IEnumerable<RoleView> GetRoles(string systemKindNo)
        {
            IEnumerable<RoleInfo> roleInfos = this._authorizationContract.GetRoles(systemKindNo);

            IEnumerable<RoleView> roleViews = roleInfos.Select(x => x.ToViewModel());

            return roleViews;
        }
        #endregion

        #region # 分页获取角色列表 —— PageModel<RoleView> GetRolesByPage(string systemKindNo...
        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>角色列表</returns>
        public PageModel<RoleView> GetRolesByPage(string systemKindNo, string keywords, int pageIndex, int pageSize)
        {
            PageModel<RoleInfo> pageModel = this._authorizationContract.GetRolesByPage(systemKindNo, keywords, pageIndex, pageSize);

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
