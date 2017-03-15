using SD.IdentitySystem.IAppService.DTOs.Inputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.ViewModels.Formats.EasyUI;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.MVC;
using SD.ValueObjects.Attributes;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SD.Infrastructure.MVC.Filters;

namespace SD.IdentitySystem.Website.Controllers
{
    /// <summary>
    /// 权限控制器
    /// </summary>
    [ExceptionFilter]
    public class AuthorityController : BaseController
    {
        #region # 字段及构造器

        /// <summary>
        /// 信息系统呈现器接口
        /// </summary>
        private readonly IInfoSystemPresenter _systemPresenter;

        /// <summary>
        /// 权限呈现器接口
        /// </summary>
        private readonly IAuthorityPresenter _authorityPresenter;

        /// <summary>
        /// 权限服务接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="systemPresenter">信息系统呈现器接口</param>
        /// <param name="authorityPresenter">权限呈现器接口</param>
        /// <param name="authorizationContract">权限服务接口</param>
        public AuthorityController(IInfoSystemPresenter systemPresenter, IAuthorityPresenter authorityPresenter, IAuthorizationContract authorizationContract)
        {
            this._systemPresenter = systemPresenter;
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
            IEnumerable<InfoSystemView> systems = this._systemPresenter.GetInfoSystems();
            base.ViewBag.InfoSystems = systems;

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
            IEnumerable<InfoSystemView> systems = this._systemPresenter.GetInfoSystems();
            base.ViewBag.InfoSystems = systems;

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
            AuthorityView currentAuthority = this._authorityPresenter.GetAuthority(id);

            return base.View(currentAuthority);
        }
        #endregion


        //命令部分

        #region # 创建权限 —— void CreateAuthority(string systemNo, string authorityName...
        /// <summary>
        /// 创建权限
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="authorityName">权限名称</param>
        /// <param name="englishName">英文名称</param>
        /// <param name="description">描述</param>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="namespace">命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        [HttpPost]
        [RequireAuthorization("创建权限")]
        public void CreateAuthority(string systemNo, string authorityName, string englishName, string description, string assemblyName, string @namespace, string className, string methodName)
        {
            //构造参数模型
            AuthorityParam param = new AuthorityParam
            {
                AuthorityName = authorityName,
                EnglishName = englishName,
                Description = description,
                AssemblyName = assemblyName,
                Namespace = @namespace,
                ClassName = className,
                MethodName = methodName
            };

            this._authorizationContract.CreateAuthorities(systemNo, new[] { param });
        }
        #endregion

        #region # 修改权限 —— void UpdateAuthority(Guid authorityId, string authorityName...
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="authorityId">权限Id</param>
        /// <param name="authorityName">权限名称</param>
        /// <param name="englishName">英文名称</param>
        /// <param name="description">描述</param>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="namespace">命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        [HttpPost]
        [RequireAuthorization("修改权限")]
        public void UpdateAuthority(Guid authorityId, string authorityName, string englishName, string description, string assemblyName, string @namespace, string className, string methodName)
        {
            //构造参数模型
            AuthorityParam param = new AuthorityParam
            {
                AuthorityName = authorityName,
                EnglishName = englishName,
                Description = description,
                AssemblyName = assemblyName,
                Namespace = @namespace,
                ClassName = className,
                MethodName = methodName
            };

            this._authorizationContract.UpdateAuthority(authorityId, param);
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

        #region # 分页获取权限列表 —— JsonResult GetAuthorities(string systemNo...
        /// <summary>
        /// 分页获取权限列表
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="page">页码</param>
        /// <param name="rows">页容量</param>
        /// <returns>权限列表</returns>
        [RequireAuthorization("分页获取权限列表")]
        public JsonResult GetAuthorities(string systemNo, string keywords, int page, int rows)
        {
            PageModel<AuthorityView> pageModel = this._authorityPresenter.GetAuthoritiesByPage(systemNo, keywords, page, rows);

            Grid<AuthorityView> grid = new Grid<AuthorityView>(pageModel.RowCount, pageModel.Datas);

            return base.Json(grid, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region # 获取信息系统/权限树 —— JsonResult GetAuthorityTree(string id)
        /// <summary>
        /// 获取信息系统/权限树
        /// </summary>
        /// <param name="id">信息系统编号</param>
        /// <returns>信息系统/权限树</returns>
        [RequireAuthorization("获取信息系统-权限树")]
        public JsonResult GetAuthorityTree(string id)
        {
            Node node = this._authorityPresenter.GetAuthorityTree(id);

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
    }
}
