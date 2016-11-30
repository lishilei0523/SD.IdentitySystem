using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using ShSoft.Infrastructure.MVC;
using ShSoft.Infrastructure.MVC.Filters;
using System;
using System.Web.Mvc;

namespace SD.IdentitySystem.Website.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [ExceptionFilter]
    public class UserController : BaseController
    {
        #region # 字段及构造器

        /// <summary>
        /// 用户呈现器接口
        /// </summary>
        private readonly IUserPresenter _userPresenter;

        /// <summary>
        /// 身份认证服务接口
        /// </summary>
        private readonly IAuthenticationContract _authenticationContract;

        /// <summary>
        /// 字段及依赖注入构造器
        /// </summary>
        /// <param name="userPresenter">用户呈现器接口</param>
        /// <param name="authenticationContract">身份认证服务接口</param>
        public UserController(IUserPresenter userPresenter, IAuthenticationContract authenticationContract)
        {
            this._userPresenter = userPresenter;
            this._authenticationContract = authenticationContract;
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

            if (base.ValidCode != null && base.ValidCode != validCode)
            {
                //清空验证码
                base.ClearValidCode();

                throw new InvalidOperationException("验证码错误！");
            }

            #endregion

            //清空验证码
            base.ClearValidCode();

            //获取客户端IP地址
            string currentIp = base.Request.UserHostAddress;

            //验证登录
            base.LoginInfo = this._authenticationContract.Login(loginId, password, currentIp);
        }
        #endregion


        //查询部分

    }
}
