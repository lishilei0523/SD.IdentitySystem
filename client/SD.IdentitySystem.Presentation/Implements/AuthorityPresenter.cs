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
    /// 权限呈现器实现
    /// </summary>
    public class AuthorityPresenter : IAuthorityPresenter
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
        public AuthorityPresenter(IAuthorizationContract authorizationContract)
        {
            this._authorizationContract = authorizationContract;
        }

        #endregion

        #region # 分页获取权限列表 —— PageModel<AuthorityView> GetAuthoritiesByPage(string systemKindNo...
        /// <summary>
        /// 分页获取权限列表
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>权限列表</returns>
        public PageModel<AuthorityView> GetAuthoritiesByPage(string systemKindNo, string keywords, int pageIndex, int pageSize)
        {
            PageModel<AuthorityInfo> pageModel = this._authorizationContract.GetAuthoritiesByPage(systemKindNo, keywords, pageIndex, pageSize);

            IEnumerable<AuthorityView> authorityViews = pageModel.Datas.Select(x => x.ToViewModel());

            return new PageModel<AuthorityView>(authorityViews, pageModel.PageIndex, pageModel.PageSize, pageModel.PageCount, pageModel.RowCount);
        }
        #endregion

        #region # 根据菜单获取权限列表 —— IEnumerable<AuthorityView> GetAuthoritiesByMenu(...
        /// <summary>
        /// 根据菜单获取权限列表
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns>权限列表</returns>
        public IEnumerable<AuthorityView> GetAuthoritiesByMenu(Guid menuId)
        {
            IEnumerable<AuthorityInfo> authorityInfos = this._authorizationContract.GetAuthoritiesByMenu(menuId);

            IEnumerable<AuthorityView> authorityViews = authorityInfos.Select(x => x.ToViewModel());

            return authorityViews;
        }
        #endregion

        #region # 根据角色获取权限列表 —— IEnumerable<AuthorityView> GetAuthoritiesByRole(...
        /// <summary>
        /// 根据角色获取权限列表
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>权限列表</returns>
        public IEnumerable<AuthorityView> GetAuthoritiesByRole(Guid roleId)
        {
            IEnumerable<AuthorityInfo> authorityInfos = this._authorizationContract.GetAuthoritiesByRole(roleId);

            IEnumerable<AuthorityView> authorityViews = authorityInfos.Select(x => x.ToViewModel());

            return authorityViews;
        }
        #endregion

        #region # 获取权限 —— AuthorityView GetAuthority(Guid authorityId)
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="authorityId">权限Id</param>
        /// <returns>权限视图模型</returns>
        public AuthorityView GetAuthority(Guid authorityId)
        {
            //TODO 添加服务接口
            //AuthorityInfo authorityInfo=this._authorizationContract.ge

            return null;
        }
        #endregion

        #region # 是否存在权限 —— bool ExistsAuthority(string assemblyName, string @namespace...
        /// <summary>
        /// 是否存在权限
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="namespace">命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        /// <returns>是否存在</returns>
        public bool ExistsAuthority(string assemblyName, string @namespace, string className, string methodName)
        {
            //TODO 添加服务接口

            return false;
        }
        #endregion
    }
}
