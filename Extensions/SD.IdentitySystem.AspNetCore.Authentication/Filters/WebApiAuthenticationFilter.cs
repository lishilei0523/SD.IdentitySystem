using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using SD.CacheManager;
using SD.Infrastructure;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using SD.Toolkits.AspNet;
using System;
using System.Net;

namespace SD.IdentitySystem.AspNetCore.Authentication.Filters
{
    /// <summary>
    /// ASP.NET Core WebApi身份认证过滤器
    /// </summary>
    public class WebApiAuthenticationFilter : IAuthorizationFilter
    {
        #region # 字段

        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object _Sync = new object();

        #endregion


        //Implements

        #region # 执行授权过滤器事件 —— void OnAuthorization(AuthorizationFilterContext context)
        /// <summary>
        /// 执行授权过滤器事件
        /// </summary>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //判断是否是ApiController
            if (context.ActionDescriptor is ControllerActionDescriptor actionDescriptor &&
                actionDescriptor.ControllerTypeInfo.IsDefined(typeof(ApiControllerAttribute), true))
            {
                bool needAuthorize = AspNetSetting.Authorized;
                bool allowAnonymous = this.HasAttr<AllowAnonymousAttribute>(context.ActionDescriptor);
                if (needAuthorize && !allowAnonymous)
                {
                    if (!context.HttpContext.Request.Headers.TryGetValue(SessionKey.CurrentPublicKey, out StringValues header))
                    {
                        ObjectResult response = new ObjectResult("身份认证消息头不存在，请检查程序！")
                        {
                            StatusCode = (int)HttpStatusCode.Unauthorized
                        };
                        context.Result = response;
                    }
                    else
                    {
                        //读取消息头中的公钥
                        Guid publicKey = new Guid(header.ToString());

                        //认证
                        lock (_Sync)
                        {
                            //以公钥为键，查询分布式缓存，如果有值则通过，无值则不通过
                            LoginInfo loginInfo = CacheMediator.Get<LoginInfo>(publicKey.ToString());

                            if (loginInfo == null)
                            {
                                ObjectResult response = new ObjectResult("身份过期，请重新登录！")
                                {
                                    StatusCode = (int)HttpStatusCode.Unauthorized
                                };
                                context.Result = response;
                            }
                            else
                            {
                                //通过后，重新设置缓存过期时间
                                int timeout = FrameworkSection.Setting.AuthenticationTimeout.Value.HasValue
                                    ? FrameworkSection.Setting.AuthenticationTimeout.Value.Value
                                    : 20;
                                CacheMediator.Set(publicKey.ToString(), loginInfo, DateTime.Now.AddMinutes(timeout));
                            }
                        }
                    }
                }
            }
        }
        #endregion


        //Private

        #region # Controller/Action是否有某特性标签 —— bool HasAttr<T>(ActionDescriptor...
        /// <summary>
        /// Controller/Action是否有某特性标签
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="actionDescriptor">Action方法元数据</param>
        /// <returns>是否拥有该特性</returns>
        public bool HasAttr<T>(ActionDescriptor actionDescriptor) where T : Attribute
        {
            Type type = typeof(T);
            if (actionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                bool actionDefined = controllerActionDescriptor.MethodInfo.IsDefined(type, true);
                if (actionDefined)
                {
                    return true;
                }

                bool controllerDefined = controllerActionDescriptor.ControllerTypeInfo.IsDefined(type, true);
                if (controllerDefined)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

            return false;
        }
        #endregion
    }
}
