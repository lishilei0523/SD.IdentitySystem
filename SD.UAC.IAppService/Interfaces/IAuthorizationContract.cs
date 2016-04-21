using System;
using System.Collections.Generic;
using System.ServiceModel;
using SD.UAC.IAppService.DTOs.Inputs;
using SD.UAC.IAppService.DTOs.Outputs;
using ShSoft.Framework2016.Infrastructure;
using ShSoft.Framework2016.Infrastructure.IDTO;

namespace SD.UAC.IAppService.Interfaces
{
    /// <summary>
    /// 权限服务契约接口
    /// </summary>
    [ServiceContract(Namespace = "http://ShSoft.UAC.IAppService.Interfaces")]
    public interface IAuthorizationContract : IApplicationService
    {
        ////////////////////////////////命令部分////////////////////////////////

        #region # 批量创建权限 —— IEnumerable<Guid> CreateAuthorities(string systemKindNo...
        /// <summary>
        /// 批量创建权限
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="authorityParams">权限参数模型集</param>
        /// <returns>权限Id集</returns>
        [OperationContract]
        IEnumerable<Guid> CreateAuthorities(string systemKindNo, IEnumerable<AuthorityParam> authorityParams);
        #endregion

        #region # 修改权限 —— void UpdateAuthority(string systemKindNo...
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="authorityId">权限Id</param>
        /// <param name="authorityParam">权限参数模型</param>
        [OperationContract]
        void UpdateAuthority(string systemKindNo, Guid authorityId, AuthorityParam authorityParam);
        #endregion

        #region # 为权限设置菜单 —— void AppendMenu(string systemKindNo, Guid menuId...
        /// <summary>
        /// 为权限设置菜单
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="menuId">菜单Id（叶子节点）</param>
        /// <param name="authorityIds">权限Id集</param>
        [OperationContract]
        void AppendMenu(string systemKindNo, Guid menuId, IEnumerable<Guid> authorityIds);
        #endregion

        #region # 删除权限 —— void RemoveAuthority(string systemKindNo, Guid authorityId)
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="authorityId">权限Id</param>
        [OperationContract]
        void RemoveAuthority(string systemKindNo, Guid authorityId);
        #endregion

        #region # 创建菜单 —— Guid CreateMenu(string systemKindNo, string menuName...
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="menuName">菜单名称</param>
        /// <param name="sort">排序（倒序）</param>
        /// <param name="url">链接地址</param>
        /// <param name="icon">图标</param>
        /// <param name="parentId">上级菜单Id</param>
        /// <returns>菜单Id</returns>
        [OperationContract]
        Guid CreateMenu(string systemKindNo, string menuName, int sort, string url, string icon, Guid? parentId);
        #endregion

        #region # 修改菜单 —— void UpdateMenu(string systemKindNo, Guid menuId, string menuName...
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="menuId">菜单Id</param>
        /// <param name="menuName">菜单名称</param>
        /// <param name="sort">排序（倒序）</param>
        /// <param name="url">链接地址</param>
        /// <param name="icon">图标</param>
        [OperationContract]
        void UpdateMenu(string systemKindNo, Guid menuId, string menuName, int sort, string url, string icon);
        #endregion

        #region # 删除菜单 —— void RemoveMenu(string systemKindNo, Guid menuId)
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="menuId">菜单Id</param>
        [OperationContract]
        void RemoveMenu(string systemKindNo, Guid menuId);
        #endregion

        ////////////////////////////////查询部分////////////////////////////////

        #region # 获取信息系统类别列表 —— IEnumerable<InfoSystemKindInfo> GetSystemKinds()
        /// <summary>
        /// 获取信息系统类别列表
        /// </summary>
        /// <returns>信息系统类别列表</returns>
        [OperationContract]
        IEnumerable<InfoSystemKindInfo> GetSystemKinds();
        #endregion

        #region # 分页获取权限列表 —— PageModel<AuthorityInfo> GetAuthoritiesByPage(string systemKindNo...
        /// <summary>
        /// 分页获取权限列表
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>权限列表</returns>
        [OperationContract]
        PageModel<AuthorityInfo> GetAuthoritiesByPage(string systemKindNo, string keywords, int pageIndex, int pageSize);
        #endregion

        #region # 获取权限列表 —— IEnumerable<AuthorityInfo> GetAuthorities(string systemKindNo)
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>权限列表</returns>
        [OperationContract]
        IEnumerable<AuthorityInfo> GetAuthorities(string systemKindNo);
        #endregion

        #region # 根据菜单Id获取权限列表 —— IEnumerable<AuthorityInfo> GetAuthoritysByMenu(...
        /// <summary>
        /// 根据菜单Id获取权限列表
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="menuId">菜单Id</param>
        /// <returns>权限列表</returns>
        [OperationContract]
        IEnumerable<AuthorityInfo> GetAuthoritysByMenu(string systemKindNo, Guid menuId);
        #endregion

        #region # 获取权限Id集 —— IEnumerable<Guid> GetAuthorities(string systemKindNo)
        /// <summary>
        /// 获取权限Id集
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>权限Id集</returns>
        [OperationContract]
        IEnumerable<Guid> GetAuthorityIds(string systemKindNo);
        #endregion

        #region # 获取菜单树 —— IEnumerable<MenuInfo> GetMenus(string systemKindNo)
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>菜单树</returns>
        [OperationContract]
        IEnumerable<MenuInfo> GetMenus(string systemKindNo);
        #endregion
    }
}
