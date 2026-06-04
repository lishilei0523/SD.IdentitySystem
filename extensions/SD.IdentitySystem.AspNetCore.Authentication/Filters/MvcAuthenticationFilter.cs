using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using SD.Infrastructure.Constants;
using SD.Toolkits.AspNet;
using SD.Toolkits.AspNetCore.Extensions;
using System.Net;

namespace SD.IdentitySystem.AspNetCore.Authentication.Filters
{
    /// <summary>
    /// ASP.NET Core MVC身份认证过滤器
    /// </summary>
    public class MvcAuthenticationFilter : IAuthorizationFilter
    {
        /// <summary>
        /// 发生授权事件
        /// </summary>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //判断是否是ApiController
            if (context.ActionDescriptor is ControllerActionDescriptor actionDescriptor &&
                actionDescriptor.ControllerTypeInfo.IsDefined(typeof(ApiControllerAttribute), true))
            {
                return;
            }

            bool needAuthorize = AspNetSetting.Authorized;
            bool allowAnonymous = context.ActionDescriptor.HasAttr<AllowAnonymousAttribute>();
            bool existsSession = context.HttpContext.Session.TryGetValue(GlobalSetting.ApplicationId, out _);
            if (needAuthorize && !allowAnonymous && !existsSession)
            {
                //是不是Ajax请求
                if (context.HttpContext.Request.IsAjaxRequest())
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
    }
}
