﻿using ArxOne.MrAdvice.Advice;
using SD.Infrastructure.Attributes;
using SD.Infrastructure.Constants;
using SD.Infrastructure.CustomExceptions;
using SD.Infrastructure.Membership;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Authorization.Aspects
{
    /// <summary>
    /// 需授权AOP特性
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public sealed class RequireAuthorizationAspect : Attribute, IMethodAdvice
    {
        /// <summary>
        /// 拦截方法
        /// </summary>
        /// <param name="context">方法元数据</param>
        public void Advise(MethodAdviceContext context)
        {
            object[] attributes = context.TargetMethod.GetCustomAttributes(typeof(RequireAuthorizationAttribute), false);
            RequireAuthorizationAttribute attribute = attributes.Any() ? (RequireAuthorizationAttribute)attributes[0] : null;
            if (GlobalSetting.AuthorizationEnabled && attribute != null)
            {
                LoginInfo loginInfo = MembershipMediator.GetLoginInfo();
                if (loginInfo == null)
                {
                    throw new NoPermissionException("当前登录信息为空，请重新登录！");
                }

                IEnumerable<string> ownedAuthorityPaths = loginInfo.LoginAuthorityInfos.Select(x => x.Path);
                if (!ownedAuthorityPaths.Contains(attribute.AuthorityPath))
                {
                    throw new NoPermissionException("您没有权限，请联系系统管理员！");
                }
            }

            context.Proceed();
        }
    }
}
