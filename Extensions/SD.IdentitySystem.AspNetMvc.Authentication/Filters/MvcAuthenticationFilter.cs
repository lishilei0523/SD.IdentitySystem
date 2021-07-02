using SD.Infrastructure.Constants;
using SD.Toolkits.AspNet;
using System;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SD.IdentitySystem.AspNetMvc.Authentication.Filters
{
    /// <summary>
    /// ASP.NET MVC身份认证过滤器
    /// </summary>
    public class MvcAuthenticationFilter : IAuthorizationFilter
    {
        //Implements

        #region # 执行授权过滤器事件 —— void OnAuthorization(AuthorizationContext context)
        /// <summary>
        /// 执行授权过滤器事件
        /// </summary>
        public void OnAuthorization(AuthorizationContext context)
        {
            bool needAuthorize = AspNetSection.Setting.Authorized;
            bool allowAnonymous = this.HasAttr<AllowAnonymousAttribute>(context.ActionDescriptor);
            bool existsSession = HttpContext.Current.Session[SessionKey.CurrentUser] != null;
            if (needAuthorize && !allowAnonymous && !existsSession)
            {
                //设置状态码为401
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                //是不是Ajax请求
                if (context.HttpContext.Request.IsAjaxRequest())
                {
                    context.HttpContext.Response.Write("身份过期，请重新登录！");
                    return;
                }

                //构造脚本
                StringBuilder scriptBuilder = new StringBuilder();
                scriptBuilder.Append("<script type=\"text/javascript\">");
                scriptBuilder.Append("window.top.location.href=");
                scriptBuilder.Append($"\"{AspNetSection.Setting.LoginPage.Value}\"");
                scriptBuilder.Append("</script>");

                //跳转至登录页
                context.HttpContext.Response.Write(scriptBuilder.ToString());
            }
        }
        #endregion


        //Private

        #region # Controller/Action是否有某特性标签 —— bool HasAttr<T>(ActionDescriptor...
        /// <summary>
        /// Controller/Action是否有某特性标签
        /// </summary>
        /// <typeparam name="T">特性标签类型</typeparam>
        /// <param name="action">ActionDescriptor</param>
        /// <returns>是否拥有该特性</returns>
        private bool HasAttr<T>(ActionDescriptor action) where T : Attribute
        {
            Type type = typeof(T);
            if (action.IsDefined(type, false))
            {
                return true;
            }
            if (action.ControllerDescriptor.IsDefined(type, false))
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
