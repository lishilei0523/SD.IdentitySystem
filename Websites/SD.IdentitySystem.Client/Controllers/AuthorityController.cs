using SD.Common;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.Models;
using SD.Infrastructure.Attributes;
using SD.Infrastructure.Constants;
using SD.Infrastructure.DTOBase;
using SD.Toolkits.EasyUI;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SD.IdentitySystem.Client.Controllers
{
    /// <summary>
    /// 权限控制器
    /// </summary>
    public class AuthorityController : Controller
    {
        #region # 字段及构造器

        /// <summary>
        /// 信息系统呈现器接口
        /// </summary>
        private readonly IInfoSystemPresenter _infoSystemPresenter;

        /// <summary>
        /// 权限呈现器接口
        /// </summary>
        private readonly IAuthorityPresenter _authorityPresenter;

        /// <summary>
        /// 权限管理服务契约接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public AuthorityController(IInfoSystemPresenter infoSystemPresenter, IAuthorityPresenter authorityPresenter, IAuthorizationContract authorizationContract)
        {
            this._infoSystemPresenter = infoSystemPresenter;
            this._authorityPresenter = authorityPresenter;
            this._authorizationContract = authorizationContract;
        }

        #endregion


        //视图部分

        #region # 加载首页视图 —— ViewResult Index()
        /// <summary>
        /// 加载首页视图
        /// </summary>
        /// <returns>首页视图</returns>
        [HttpGet]
        [RequireAuthorization("权限管理首页视图")]
        public ViewResult Index()
        {
            IEnumerable<InfoSystem> infoSystems = this._infoSystemPresenter.GetInfoSystems();
            IDictionary<int, string> applicationTypeDescriptions = typeof(ApplicationType).GetEnumDictionary();

            base.ViewBag.InfoSystems = infoSystems;
            base.ViewBag.ApplicationTypeDescriptions = applicationTypeDescriptions;

            return base.View();
        }
        #endregion

        #region # 加载创建权限视图 —— ViewResult Add()
        /// <summary>
        /// 加载创建权限视图
        /// </summary>
        /// <returns>创建权限视图</returns>
        [HttpGet]
        [RequireAuthorization("创建权限视图")]
        public ViewResult Add()
        {
            IEnumerable<InfoSystem> infoSystems = this._infoSystemPresenter.GetInfoSystems();
            IDictionary<int, string> applicationTypeDescriptions = typeof(ApplicationType).GetEnumDictionary();

            base.ViewBag.InfoSystems = infoSystems;
            base.ViewBag.ApplicationTypeDescriptions = applicationTypeDescriptions;

            return base.View();
        }
        #endregion

        #region # 加载修改权限视图 —— ViewResult Update(Guid id)
        /// <summary>
        /// 加载修改权限视图
        /// </summary>
        /// <param name="id">权限Id</param>
        /// <returns>修改权限视图</returns>
        [HttpGet]
        [RequireAuthorization("修改权限视图")]
        public ViewResult Update(Guid id)
        {
            Authority currentAuthority = this._authorityPresenter.GetAuthority(id);

            return base.View(currentAuthority);
        }
        #endregion


        //命令部分

        #region # 创建权限 —— void CreateAuthority(string infoSystemNo, ApplicationType applicationType...
        /// <summary>
        /// 创建权限
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="authorityName">权限名称</param>
        /// <param name="authorityPath">权限路径</param>
        /// <param name="englishName">英文名称</param>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="namespace">命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="description">描述</param>
        [HttpPost]
        [RequireAuthorization("创建权限")]
        public void CreateAuthority(string infoSystemNo, ApplicationType applicationType, string authorityName, string authorityPath, string englishName, string assemblyName, string @namespace, string className, string methodName, string description)
        {
            this._authorizationContract.CreateAuthority(infoSystemNo, applicationType, authorityName, authorityPath, englishName, assemblyName, @namespace, className, methodName, description);
        }
        #endregion

        #region # 修改权限 —— void UpdateAuthority(Guid authorityId, string authorityName...
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="authorityId">权限Id</param>
        /// <param name="authorityName">权限名称</param>
        /// <param name="authorityPath">权限路径</param>
        /// <param name="englishName">英文名称</param>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="namespace">命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="description">描述</param>
        [HttpPost]
        [RequireAuthorization("修改权限")]
        public void UpdateAuthority(Guid authorityId, string authorityName, string authorityPath, string englishName, string assemblyName, string @namespace, string className, string methodName, string description)
        {
            this._authorizationContract.UpdateAuthority(authorityId, authorityName, authorityPath, englishName, assemblyName, @namespace, className, methodName, description);
        }
        #endregion

        #region # 删除权限 —— void RemoveAuthority(Guid id)
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="id">权限Id</param>
        [HttpPost]
        [RequireAuthorization("删除权限")]
        public void RemoveAuthority(Guid id)
        {
            this._authorizationContract.RemoveAuthority(id);
        }
        #endregion

        #region # 批量删除权限 —— void RemoveAuthorities(IEnumerable<Guid> authorityIds)
        /// <summary>
        /// 批量删除权限
        /// </summary>
        /// <param name="authorityIds">权限Id集</param>
        [HttpPost]
        [RequireAuthorization("批量删除权限")]
        public void RemoveAuthorities(IEnumerable<Guid> authorityIds)
        {
            foreach (Guid authorityId in authorityIds)
            {
                this._authorizationContract.RemoveAuthority(authorityId);
            }
        }
        #endregion


        //查询部分

        #region # 获取信息系统/权限树 —— JsonResult GetAuthorityTree(string id)
        /// <summary>
        /// 获取信息系统/权限树
        /// </summary>
        /// <param name="id">信息系统编号</param>
        /// <returns>信息系统/权限树</returns>
        [RequireAuthorization("获取信息系统-权限树")]
        public JsonResult GetAuthorityTree(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return base.Json(null, JsonRequestBehavior.AllowGet);
            }

            Node node = this._authorityPresenter.GetAuthorityTree(id, null);
            IEnumerable<Node> tree = new[] { node };

            return base.Json(tree, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region # 获取角色的权限树 —— JsonResult GetAuthorityTreeByRole(Guid id)
        /// <summary>
        /// 获取角色的权限树
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns>权限树</returns>
        [RequireAuthorization("获取角色的权限树")]
        public JsonResult GetAuthorityTreeByRole(Guid id)
        {
            Node node = this._authorityPresenter.GetAuthorityTreeByRole(id);
            IEnumerable<Node> tree = new[] { node };

            return base.Json(tree, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region # 获取菜单的权限树 —— JsonResult GetAuthorityTreeByMenu(Guid id)
        /// <summary>
        /// 获取菜单的权限树
        /// </summary>
        /// <param name="id">菜单Id</param>
        /// <returns>权限树</returns>
        [RequireAuthorization("获取菜单的权限树")]
        public JsonResult GetAuthorityTreeByMenu(Guid id)
        {
            Node node = this._authorityPresenter.GetAuthorityTreeByMenu(id);
            IEnumerable<Node> tree = new[] { node };

            return base.Json(tree, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region # 分页获取权限列表 —— JsonResult GetAuthoritiesByPage(string keywords...
        /// <summary>
        /// 分页获取权限列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="page">页码</param>
        /// <param name="rows">页容量</param>
        /// <returns>权限列表</returns>
        [RequireAuthorization("分页获取权限列表")]
        public JsonResult GetAuthoritiesByPage(string keywords, string infoSystemNo, ApplicationType? applicationType, int page, int rows)
        {
            PageModel<Authority> pageModel = this._authorityPresenter.GetAuthoritiesByPage(keywords, infoSystemNo, applicationType, page, rows);
            Grid<Authority> grid = new Grid<Authority>(pageModel.RowCount, pageModel.Datas);

            return base.Json(grid, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
