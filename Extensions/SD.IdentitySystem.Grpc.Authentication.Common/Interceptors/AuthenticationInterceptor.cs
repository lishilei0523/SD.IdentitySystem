﻿using Grpc.Core;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using SD.Toolkits.Grpc.Client.Interfaces;
using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace SD.IdentitySystem.Grpc.Authentication.Common
{
    /// <summary>
    /// ASP.NET Core gRPC通用客户端身份认证拦截器
    /// </summary>
    public class AuthenticationInterceptor : IAuthInterceptor
    {
        /// <summary>
        /// 身份认证拦截
        /// </summary>
        /// <param name="context">拦截上下文</param>
        /// <param name="metadata">元数据</param>
        public Task AuthIntercept(AuthInterceptorContext context, Metadata metadata)
        {
            //通用客户端获取公钥处理
            object loginInfo = AppDomain.CurrentDomain.GetData(GlobalSetting.ApplicationId);
            if (loginInfo != null)
            {
                Guid publicKey = ((LoginInfo)loginInfo).PublicKey;

                //添加登录信息元数据
                metadata.Add(SessionKey.PublicKey, publicKey.ToString());
            }

            return Task.CompletedTask;
        }
    }
}