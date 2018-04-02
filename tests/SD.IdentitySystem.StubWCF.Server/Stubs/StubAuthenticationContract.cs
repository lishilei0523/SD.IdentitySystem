using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.MemberShip;
using System;

namespace SD.IdentitySystem.StubWCF.Server.Stubs
{
    public class StubAuthenticationContract : IAuthenticationContract
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        /// <returns>登录信息</returns>
        public LoginInfo Login(string loginId, string password)
        {
            return null;
        }

        /// <summary>
        /// 认证
        /// </summary>
        /// <param name="publicKey">公钥</param>
        public void Authenticate(Guid publicKey)
        {

        }
    }
}