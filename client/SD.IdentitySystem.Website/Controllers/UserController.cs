using System;
using System.Web.Mvc;
using SD.IdentitySystem.IPresentation.Interfaces;
using ShSoft.Common.PoweredByLee;
using ShSoft.Infrastructure.Constants;

namespace SD.IdentitySystem.Website.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public class UserController : BaseController
    {
        #region # 字段及构造器

        /// <summary>
        /// 用户呈现器接口
        /// </summary>
        private readonly IUserPresenter _userPresenter;

        /// <summary>
        /// 字段及依赖注入构造器
        /// </summary>
        /// <param name="userPresenter">用户呈现器接口</param>
        public UserController(IUserPresenter userPresenter)
        {
            this._userPresenter = userPresenter;
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
        /// <param name="isRemember">记住密码</param>
        [AllowAnonymous]
        [HttpPost]
        public void Login(string loginId, string password, string validCode, bool isRemember)
        {
            #region # 校验验证码

            if (base.ValidCode != null && base.ValidCode != validCode)
            {
                //清空验证码
                base.ClearValidCode();

                throw new InvalidOperationException("验证码错误！");
            }

            #endregion

            try
            {
                //获取客户端IP地址
                string currentIp = base.Request.UserHostAddress;

                //验证登录
                base.LoginInfo = this._userPresenter.Login(loginId, password, currentIp);

                //判断用户是否记住密码
                if (isRemember)
                {
                    base.LoginIdCookie = loginId;
                    base.PasswordCookie = password;
                }
            }
            catch (Exception)
            {
                //清空验证码
                base.ClearValidCode();

                throw;
            }
        }
        #endregion

        #region # 注销 —— void Logout()
        /// <summary>
        /// 注销
        /// </summary>
        public void Logout()
        {
            base.LoginInfo = null;
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
            string validCode = ValidCodeGenerator.GenerateCode(4);
            byte[] buffer = ValidCodeGenerator.GenerateStream(validCode);

            base.ValidCode = validCode;

            return base.File(buffer, @"image/jpeg");
        }
        #endregion
    }
}
