using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.Models;
using SD.Infrastructure.AspNetMvc;
using SD.Infrastructure.Attributes;
using SD.Infrastructure.Constants;
using SD.Infrastructure.DTOBase;
using SD.Infrastructure.MemberShip;
using SD.Toolkits.EasyUI;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SD.IdentitySystem.Client.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public class UserController : Controller
    {
        #region # 字段及构造器

        /// <summary>
        /// 用户呈现器接口
        /// </summary>
        private readonly IUserPresenter _userPresenter;

        /// <summary>
        /// 信息系统呈现器接口
        /// </summary>
        private readonly IInfoSystemPresenter _systemPresenter;

        /// <summary>
        /// 身份认证服务接口
        /// </summary>
        private readonly IAuthenticationContract _authenticationContract;

        /// <summary>
        /// 用户服务接口
        /// </summary>
        private readonly IUserContract _userContract;

        /// <summary>
        /// 字段及依赖注入构造器
        /// </summary>
        /// <param name="userPresenter">用户呈现器接口</param>
        /// <param name="authenticationContract">身份认证服务接口</param>
        /// <param name="userContract">用户服务接口</param>
        /// <param name="systemPresenter">信息系统呈现器接口</param>
        public UserController(IUserPresenter userPresenter, IAuthenticationContract authenticationContract, IUserContract userContract, IInfoSystemPresenter systemPresenter)
        {
            this._userPresenter = userPresenter;
            this._authenticationContract = authenticationContract;
            this._userContract = userContract;
            this._systemPresenter = systemPresenter;
        }

        #endregion


        //视图部分

        #region # 加载登录视图 —— ViewResult Login()
        /// <summary>
        /// 加载登录视图
        /// </summary>
        /// <returns>登录视图</returns>
        [AllowAnonymous]
        [HttpGet]
        public ViewResult Login()
        {
            return this.View();
        }
        #endregion

        #region # 加载首页视图 —— ViewResult Index()
        /// <summary>
        /// 加载首页视图
        /// </summary>
        /// <returns>首页视图</returns>
        [HttpGet]
        [RequireAuthorization("用户管理首页视图")]
        public ViewResult Index()
        {
            IEnumerable<InfoSystem> systems = this._systemPresenter.GetInfoSystems();
            base.ViewBag.InfoSystems = systems;

            return this.View();
        }
        #endregion

        #region # 加载创建用户视图 —— ViewResult Add()
        /// <summary>
        /// 加载创建用户视图
        /// </summary>
        /// <returns>创建用户视图</returns>
        [HttpGet]
        [RequireAuthorization("创建用户视图")]
        public ViewResult Add()
        {
            return base.View();
        }
        #endregion

        #region # 加载重置密码视图 —— ViewResult ResetPassword(string id)
        /// <summary>
        /// 加载重置密码视图
        /// </summary>
        /// <param name="id">用户登录名</param>
        /// <returns>重置密码视图</returns>
        [HttpGet]
        [RequireAuthorization("重置密码视图")]
        public ViewResult ResetPassword(string id)
        {
            base.ViewBag.LoginId = id;

            return base.View();
        }
        #endregion

        #region # 加载重置私钥视图 —— ViewResult ResetPrivateKey(string id)
        /// <summary>
        /// 加载重置私钥视图
        /// </summary>
        /// <param name="id">用户登录名</param>
        /// <returns>重置私钥视图</returns>
        [HttpGet]
        [RequireAuthorization("重置私钥视图")]
        public ViewResult ResetPrivateKey(string id)
        {
            base.ViewBag.LoginId = id;

            return base.View();
        }
        #endregion

        #region # 加载分配角色视图 —— ViewResult SetRole(string id)
        /// <summary>
        /// 加载分配角色视图
        /// </summary>
        /// <param name="id">用户登录名</param>
        /// <returns>分配角色视图</returns>
        [HttpGet]
        [RequireAuthorization("分配角色视图")]
        public ViewResult SetRole(string id)
        {
            base.ViewBag.LoginId = id;

            return base.View();
        }
        #endregion


        //命令部分

        #region # 登录 —— void Login(string loginId, string password...
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="validCode">验证码</param>
        [AllowAnonymous]
        [HttpPost]
        public void Login(string loginId, string password, string validCode)
        {
            #region # 校验验证码

            string currentValidCode = MvcExtension.GetValidCode();
            if (currentValidCode != validCode)
            {
                //清空验证码
                MvcExtension.ClearValidCode();

                throw new InvalidOperationException("验证码错误！");
            }

            #endregion

            //清空验证码
            MvcExtension.ClearValidCode();

            //验证登录
            LoginInfo loginInfo = this._authenticationContract.Login(loginId, password);
            HttpContext.Session[SessionKey.CurrentUser] = loginInfo;
        }
        #endregion

        #region # 注销 —— void Logout()
        /// <summary>
        /// 注销
        /// </summary>
        [HttpPost]
        public void Logout()
        {
            MvcExtension.Logout();
        }
        #endregion

        #region # 修改密码 —— void UpdatePassword(string loginId, string oldPassword...
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <param name="confirmPassword">确认密码</param>
        [HttpPost]
        [RequireAuthorization("修改密码")]
        public void UpdatePassword(string loginId, string oldPassword, string newPassword, string confirmPassword)
        {
            #region # 验证

            if (newPassword != confirmPassword)
            {
                throw new InvalidOperationException("两次密码输入不一致");
            }

            #endregion

            this._userContract.UpdatePassword(loginId, oldPassword, newPassword);
        }
        #endregion

        #region # 创建用户 —— void CreateUser(string loginId, string realName...
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="loginId">用户登录名</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="password">密码</param>
        /// <param name="confirmPassword">确认密码</param>
        [HttpPost]
        [RequireAuthorization("创建用户")]
        public void CreateUser(string loginId, string realName, string password, string confirmPassword)
        {
            #region # 验证

            if (password != confirmPassword)
            {
                throw new ArgumentOutOfRangeException(nameof(password), "两次密码输入不一致！");
            }

            #endregion

            this._userContract.CreateUser(loginId, realName, password);
        }
        #endregion

        #region # 删除用户 —— void RemoveUser(string id)
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户登录名</param>
        [HttpPost]
        [RequireAuthorization("删除用户")]
        public void RemoveUser(string id)
        {
            this._userContract.RemoveUser(id);
        }
        #endregion

        #region # 批量删除用户 —— void RemoveUsers(IEnumerable<string> loginIds)
        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="loginIds">用户登录名集</param>
        [HttpPost]
        [RequireAuthorization("批量删除用户")]
        public void RemoveUsers(IEnumerable<string> loginIds)
        {
            loginIds = loginIds ?? new string[0];

            foreach (string loginId in loginIds)
            {
                this._userContract.RemoveUser(loginId);
            }
        }
        #endregion

        #region # 重置密码 —— void ResetPassword(string loginId, string newPassword...
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="loginId">用户登录名</param>
        /// <param name="newPassword">新密码</param>
        /// <param name="confirmPassword">确认密码</param>
        [HttpPost]
        [RequireAuthorization("重置密码")]
        public void ResetPassword(string loginId, string newPassword, string confirmPassword)
        {
            this._userContract.ResetPassword(loginId, newPassword);
        }
        #endregion

        #region # 重置私钥 —— void ResetPrivateKey(string loginId, string privateKey)
        /// <summary>
        /// 重置私钥
        /// </summary>
        /// <param name="loginId">用户登录名</param>
        /// <param name="privateKey">私钥</param>
        [HttpPost]
        [RequireAuthorization("重置私钥")]
        public void ResetPrivateKey(string loginId, string privateKey)
        {
            this._userContract.SetPrivateKey(loginId, privateKey);
        }
        #endregion

        #region # 启用用户 —— void EnableUser(string id)
        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="id">登录名</param>
        [HttpPost]
        [RequireAuthorization("启用用户")]
        public void EnableUser(string id)
        {
            this._userContract.EnableUser(id);
        }
        #endregion

        #region # 停用用户 —— void DisableUser(string id)
        /// <summary>
        /// 停用用户
        /// </summary>
        /// <param name="id">登录名</param>
        [HttpPost]
        [RequireAuthorization("停用用户")]
        public void DisableUser(string id)
        {
            this._userContract.DisableUser(id);
        }
        #endregion

        #region # 分配角色 —— void SetRoles(string loginId, IEnumerable<Guid> roleIds)
        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="loginId">用户登录名</param>
        /// <param name="roleIds">角色Id集</param>
        [HttpPost]
        [RequireAuthorization("分配角色")]
        public void SetRoles(string loginId, IEnumerable<Guid> roleIds)
        {
            this._userContract.RelateRolesToUser(loginId, roleIds);
        }
        #endregion


        //查询部分

        #region # 获取验证码图片 —— FileContentResult GetValidCode()
        /// <summary>
        /// 获取验证码图片
        /// </summary>
        /// <returns>验证码图片二进制内容</returns>
        [AllowAnonymous]
        public FileContentResult GetValidCode()
        {
            FileContentResult validCodeImage = MvcExtension.GetValidCodeImage();

            return validCodeImage;
        }
        #endregion

        #region # 分页获取用户列表 —— JsonResult GetUsersByPage(string keywords...
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">页容量</param>
        /// <returns>用户列表</returns>
        [RequireAuthorization("分页获取用户列表")]
        public JsonResult GetUsersByPage(string keywords, string systemNo, int page, int rows)
        {
            PageModel<User> pageModel = this._userPresenter.GetUsersByPage(keywords, systemNo, page, rows);
            Grid<User> grid = new Grid<User>(pageModel.RowCount, pageModel.Datas);

            return base.Json(grid, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
