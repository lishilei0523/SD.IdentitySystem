﻿using SD.CacheManager;
using SD.Common;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using System;

namespace SD.IdentitySystem.Windows.Tests.Stubs
{
    /// <summary>
    /// Stub身份认证服务契约实现
    /// </summary>
    public class StubAuthenticationContract : IAuthenticationContract
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <returns>登录信息</returns>
        public LoginInfo Logon(string privateKey)
        {
            return null;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        /// <param name="clientId">客户端Id</param>
        /// <returns>登录信息</returns>
        public LoginInfo Login(string loginId, string password, string clientId = null)
        {
            //生成公钥
            Guid publicKey = Guid.NewGuid();

            //生成登录信息
            LoginInfo loginInfo = new LoginInfo(loginId, CommonConstants.AdminLoginId, publicKey);
            loginInfo.ClientId = NetworkExtension.GetLocalMacAddress();

            //以公钥为键，登录信息为值，存入分布式缓存
            CacheMediator.Set(publicKey.ToString(), loginInfo, DateTime.Now.AddMinutes(20));

            return loginInfo;
        }
    }
}
