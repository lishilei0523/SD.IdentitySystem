using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.MemberShip;
using SD.Toolkits.WebApi.Extensions;
using System.Web.Http;

namespace SD.IdentitySystem.AppService.WebApi.Controllers
{
    /// <summary>
    /// 身份认证WebApi接口
    /// </summary>
    public class AuthenticationController : ApiController
    {
        #region # 字段及构造器

        /// <summary>
        /// 身份认证服务契约接口
        /// </summary>
        private readonly IAuthenticationContract _authenticationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public AuthenticationController(IAuthenticationContract authenticationContract)
        {
            this._authenticationContract = authenticationContract;
        }

        #endregion


        //命令部分

        #region # 登录 —— LoginInfo Login(string loginId, string password)
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        /// <returns>登录信息</returns>
        [HttpPost]
        [WrapPostParameters]
        public LoginInfo Login(string loginId, string password)
        {
            LoginInfo loginInfo = this._authenticationContract.Login(loginId, password);

            return loginInfo;
        }
        #endregion

        #region # 私钥登录 —— LoginInfo LoginByPrivateKey(string privateKey)
        /// <summary>
        /// 私钥登录
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <returns>登录信息</returns>
        [HttpPost]
        [WrapPostParameters]
        public LoginInfo LoginByPrivateKey(string privateKey)
        {
            LoginInfo loginInfo = this._authenticationContract.LoginByPrivateKey(privateKey);

            return loginInfo;
        }
        #endregion
    }
}