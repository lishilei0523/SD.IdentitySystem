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
        /// 用户服务接口
        /// </summary>
        private readonly IUserContract _userContract;

        /// <summary>
        /// 字段及依赖注入构造器
        /// </summary>
        /// <param name="userPresenter">用户呈现器接口</param>
        /// <param name="authenticationContract">身份认证服务接口</param>
        /// <param name="userContract">用户服务接口</param>
        public UserController(IUserPresenter userPresenter, IAuthenticationContract authenticationContract, IUserContract userContract)
        {
            this._userPresenter = userPresenter;
            this._authenticationContract = authenticationContract;
            this._userContract = userContract;
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

        #region # 修改密码 —— void UpdatePassword(string loginId, string oldPassword...
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <param name="confirmPassword">确认密码</param>
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


        //查询部分

    }
}
