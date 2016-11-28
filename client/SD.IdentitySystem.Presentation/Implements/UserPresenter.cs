using System;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using ShSoft.ValueObjects.Structs;

namespace SD.IdentitySystem.Presentation.Implements
{
    /// <summary>
    /// 用户呈现器实现
    /// </summary>
    public class UserPresenter : IUserPresenter
    {
        #region # 字段及构造器

        /// <summary>
        /// 身份认证服务接口
        /// </summary>
        private readonly IAuthenticationContract _authenticationContract;

        /// <summary>
        /// 用户服务接口
        /// </summary>
        private readonly IUserContract _userContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="authenticationContract">身份认证服务接口</param>
        /// <param name="userContract">用户服务接口</param>
        public UserPresenter(IAuthenticationContract authenticationContract, IUserContract userContract)
        {
            this._authenticationContract = authenticationContract;
            this._userContract = userContract;
        }

        #endregion

        #region # 登录 —— LoginInfo Login(string loginId, string password...
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        /// <param name="ip">IP地址</param>
        /// <returns>公钥</returns>
        public LoginInfo Login(string loginId, string password, string ip)
        {
            try
            {
                return this._authenticationContract.Login(loginId, password, ip);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
