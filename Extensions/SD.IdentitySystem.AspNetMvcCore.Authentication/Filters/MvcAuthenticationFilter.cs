using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using SD.Infrastructure.Constants;
using SD.Toolkits.AspNet;
using SD.Toolkits.OwinCore.Extensions;
using System;
using System.Net;

namespace SD.IdentitySystem.AspNetMvcCore.Authentication.Filters
{
    /// <summary>
    /// ASP.NET Core MVC身份认证过滤器
    /// </summary>
    public class MvcAuthenticationFilter : IAuthorizationFilter
    {
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
                return;
            }

            bool needAuthorize = AspNetSection.Setting.Authorized;
            bool allowAnonymous = this.HasAttr<AllowAnonymousAttribute>(context.ActionDescriptor);
            bool existsSession = OwinContextReader.Current.Session.TryGetValue(SessionKey.CurrentUser, out _);
            if (needAuthorize && !allowAnonymous && !existsSession)
            {
                //是不是Ajax请求
                if (IsAjaxRequest(context.HttpContext.Request))
                {
                    ObjectResult response = new ObjectResult("身份过期，请重新登录！")
                    {
                        StatusCode = (int)HttpStatusCode.Unauthorized
                    };
                    context.Result = response;
                    return;
                }

                //跳转至登录页
                context.HttpContext.Response.Redirect(AspNetSection.Setting.LoginPage.Value);
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

        #region # 判断是否是Ajax请求 —— static bool IsAjaxRequest(HttpRequest request)
        /// <summary>
        /// 判断是否是Ajax请求
        /// </summary>
        /// <param name="request">Http请求</param>
        /// <returns>是否是Ajax请求</returns>
        private static bool IsAjaxRequest(HttpRequest request)
        {
            #region # 验证

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            #endregion

            const string ajaxHeaderKey = "X-Requested-With";
            const string ajaxHeaderValue = "XMLHttpRequest";

            bool isAjax = false;
            bool hasAjaxHeader = request.Headers.ContainsKey(ajaxHeaderKey);
            if (hasAjaxHeader)
            {
                isAjax = string.Equals(request.Headers[ajaxHeaderKey], ajaxHeaderValue, StringComparison.OrdinalIgnoreCase);
            }

            return isAjax;
        }
        #endregion
    }
}
