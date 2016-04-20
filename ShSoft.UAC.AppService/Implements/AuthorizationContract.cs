using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using ShSoft.Framework2016.Infrastructure.Global.Transaction;
using ShSoft.Framework2016.Infrastructure.IDTO;
using ShSoft.UAC.AppService.Maps;
using ShSoft.UAC.Domain.Entities;
using ShSoft.UAC.Domain.IRepositories;
using ShSoft.UAC.Domain.Mediators;
using ShSoft.UAC.IAppService.DTOs.Inputs;
using ShSoft.UAC.IAppService.DTOs.Outputs;
using ShSoft.UAC.IAppService.Interfaces;

namespace ShSoft.UAC.AppService.Implements
{
    /// <summary>
    /// 权限服务契约实现
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class AuthorizationContract : IAuthorizationContract
    {
        #region # 字段及依赖注入构造器

        /// <summary>
        /// 领域服务中介者
        /// </summary>
        private readonly DomainServiceMediator _svcMediator;

        /// <summary>
        /// 仓储中介者
        /// </summary>
        private readonly RepositoryMediator _repMediator;

        /// <summary>
        /// 单元事务
        /// </summary>
        private readonly IUnitOfWorkUAC _unitOfWork;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="svcMediator">领域服务中介者</param>
        /// <param name="repMediator">仓储中介者</param>
        /// <param name="unitOfWork">单元事务</param>
        public AuthorizationContract(DomainServiceMediator svcMediator, RepositoryMediator repMediator, IUnitOfWorkUAC unitOfWork)
        {
            this._svcMediator = svcMediator;
            this._repMediator = repMediator;
            this._unitOfWork = unitOfWork;
        }

        #endregion

        ////////////////////////////////命令部分////////////////////////////////

        #region # 批量创建权限 —— IEnumerable<Guid> CreateAuthorities(string systemKindNo...
        /// <summary>
        /// 批量创建权限
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="authorityParams">权限参数模型集</param>
        /// <returns>权限Id集</returns>
        public IEnumerable<Guid> CreateAuthorities(string systemKindNo, IEnumerable<AuthorityParam> authorityParams)
        {
            InfoSystemKind systemKind = this._unitOfWork.Resolve<InfoSystemKind>(systemKindNo);

            List<Guid> authorityIds = new List<Guid>();

            foreach (AuthorityParam param in authorityParams)
            {
                Authority authority = new Authority(param.AuthorityName, param.EnglishName, param.Description, param.AssemblyName, param.Namespace, param.ClassName, param.MethodName);

                //验证
                this._svcMediator.InfoSystemKindSvc.AssertAuthorityNotExsits(systemKindNo, authority.AuthorityPath);

                systemKind.CreateAuthority(authority);
                authorityIds.Add(authority.Id);
            }

            this._unitOfWork.RegisterSave(systemKind);
            this._unitOfWork.UnitedCommit();

            return authorityIds;
        }
        #endregion

        #region # 修改权限 —— void UpdateAuthority(string systemKindNo...
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="authorityId">权限Id</param>
        /// <param name="authorityParam">权限参数模型</param>
        public void UpdateAuthority(string systemKindNo, Guid authorityId, AuthorityParam authorityParam)
        {
            InfoSystemKind systemKind = this._unitOfWork.Resolve<InfoSystemKind>(systemKindNo);

            Authority authority = systemKind.GetAuthority(authorityId);

            authority.UpdateInfo(authorityParam.AuthorityName, authorityParam.EnglishName, authorityParam.Description, authorityParam.AssemblyName, authorityParam.Namespace, authorityParam.ClassName, authorityParam.MethodName);

            this._unitOfWork.RegisterSave(systemKind);
            this._unitOfWork.UnitedCommit();
        }
        #endregion

        #region # 为权限设置菜单 —— void AppendMenu(string systemKindNo, Guid menuId...
        /// <summary>
        /// 为权限设置菜单
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="menuId">菜单Id（叶子节点）</param>
        /// <param name="authorityIds">权限Id集</param>
        public void AppendMenu(string systemKindNo, Guid menuId, IEnumerable<Guid> authorityIds)
        {
            InfoSystemKind systemKind = this._unitOfWork.Resolve<InfoSystemKind>(systemKindNo);

            Menu menu = systemKind.GetMenu(menuId);

            foreach (Guid authorityId in authorityIds)
            {
                Authority currentAuthority = systemKind.GetAuthority(authorityId);
                currentAuthority.AppendMenu(menu);
            }

            this._unitOfWork.RegisterSave(systemKind);
            this._unitOfWork.UnitedCommit();
        }
        #endregion

        #region # 删除权限 —— void RemoveAuthority(string systemKindNo, Guid authorityId)
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="authorityId">权限Id</param>
        public void RemoveAuthority(string systemKindNo, Guid authorityId)
        {
            this._unitOfWork.RegisterRemove<Authority>(authorityId);
            this._unitOfWork.UnitedCommit();
        }
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
        public Guid CreateMenu(string systemKindNo, string menuName, int sort, string url, string icon, Guid? parentId)
        {
            //验证参数
            this._svcMediator.InfoSystemKindSvc.AssertMenuNotExists(systemKindNo, parentId, menuName);

            InfoSystemKind systemKind = this._unitOfWork.Resolve<InfoSystemKind>(systemKindNo);

            Menu parentMenu = parentId == null ? null : systemKind.GetMenu(parentId.Value);
            Menu menu = new Menu(menuName, sort, url, icon, parentMenu);

            systemKind.CreateMenu(menu);

            this._unitOfWork.RegisterSave(systemKind);
            this._unitOfWork.UnitedCommit();

            return menu.Id;
        }
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
        public void UpdateMenu(string systemKindNo, Guid menuId, string menuName, int sort, string url, string icon)
        {
            InfoSystemKind systemKind = this._unitOfWork.Resolve<InfoSystemKind>(systemKindNo);

            Menu currentMenu = systemKind.GetMenu(menuId);

            #region # 验证参数

            if (menuName != currentMenu.Name)
            {
                Guid? parentId = currentMenu.ParentNode == null ? (Guid?)null : currentMenu.ParentNode.Id;
                this._svcMediator.InfoSystemKindSvc.AssertMenuNotExists(systemKindNo, parentId, menuName);
            }

            #endregion

            currentMenu.UpdateInfo(menuName, sort, url, icon);

            this._unitOfWork.RegisterSave(systemKind);
            this._unitOfWork.UnitedCommit();
        }
        #endregion

        #region # 删除菜单 —— void RemoveMenu(string systemKindNo, Guid menuId)
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="menuId">菜单Id</param>
        public void RemoveMenu(string systemKindNo, Guid menuId)
        {
            InfoSystemKind systemKind = this._unitOfWork.Resolve<InfoSystemKind>(systemKindNo);
            Menu currentMenu = systemKind.GetMenu(menuId);

            systemKind.RemoveMenu(currentMenu);

            this._unitOfWork.RegisterSave(systemKind);
            this._unitOfWork.UnitedCommit();
        }
        #endregion


        ////////////////////////////////查询部分////////////////////////////////

        #region # 获取信息系统类别列表 —— IEnumerable<InfoSystemKindInfo> GetSystemKinds()
        /// <summary>
        /// 获取信息系统类别列表
        /// </summary>
        /// <returns>信息系统类别列表</returns>
        public IEnumerable<InfoSystemKindInfo> GetSystemKinds()
        {
            IEnumerable<InfoSystemKind> kinds = this._repMediator.InfoSystemKindRep.FindAll();

            return kinds.Select(x => x.ToDTO());
        }
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
        public PageModel<AuthorityInfo> GetAuthoritiesByPage(string systemKindNo, string keywords, int pageIndex, int pageSize)
        {
            int rowCount, pageCount;

            IEnumerable<Authority> authorities = this._repMediator.AuthorityRep.FindByPage(systemKindNo, keywords, pageIndex, pageSize, out rowCount, out pageCount);

            IEnumerable<AuthorityInfo> authorityInfos = authorities.Select(x => x.ToDTO());

            return new PageModel<AuthorityInfo>(authorityInfos, pageIndex, pageSize, pageCount, rowCount);
        }
        #endregion

        #region # 获取权限列表 —— IEnumerable<AuthorityInfo> GetAuthorities(string systemKindNo)
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>权限列表</returns>
        public IEnumerable<AuthorityInfo> GetAuthorities(string systemKindNo)
        {
            IEnumerable<Authority> authorities = this._repMediator.AuthorityRep.FindBySystemKind(systemKindNo);

            return authorities.Select(x => x.ToDTO());
        }
        #endregion

        #region # 根据菜单Id获取权限列表 —— IEnumerable<AuthorityInfo> GetAuthoritysByMenu(...
        /// <summary>
        /// 根据菜单Id获取权限列表
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="menuId">菜单Id</param>
        /// <returns>权限列表</returns>
        public IEnumerable<AuthorityInfo> GetAuthoritysByMenu(string systemKindNo, Guid menuId)
        {
            //验证
            this._svcMediator.InfoSystemKindSvc.AssertMenuIsLeaf(systemKindNo, menuId);

            InfoSystemKind currentSystemKind = this._repMediator.InfoSystemKindRep.Single(systemKindNo);
            Menu currentMenu = currentSystemKind.GetMenu(menuId);
            IEnumerable<Authority> authorities = currentMenu.GetAuthorities();

            return authorities.Select(x => x.ToDTO());
        }
        #endregion

        #region # 获取权限Id集 —— IEnumerable<Guid> GetAuthorities(string systemKindNo)
        /// <summary>
        /// 获取权限Id集
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>权限Id集</returns>
        public IEnumerable<Guid> GetAuthorityIds(string systemKindNo)
        {
            return this._repMediator.AuthorityRep.FindAuthorityIds(systemKindNo);
        }
        #endregion

        #region # 获取菜单树 —— IEnumerable<MenuInfo> GetMenus(string systemKindNo)
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>菜单树</returns>
        public IEnumerable<MenuInfo> GetMenus(string systemKindNo)
        {
            IEnumerable<Menu> menus = this._repMediator.MenuRep.FindBySystemKind(systemKindNo);

            return menus.Select(x => x.ToDTO());
        }
        #endregion
    }
}