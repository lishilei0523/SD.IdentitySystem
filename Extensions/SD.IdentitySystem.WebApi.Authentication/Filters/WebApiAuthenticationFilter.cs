using SD.CacheManager;
using SD.Infrastructure;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using SD.Toolkits.AspNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SD.IdentitySystem.WebApi.Authentication.Filters
{
    /// <summary>
    /// WebApi身份认证过滤器
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

        #region # 是否允许多实例 —— bool AllowMultiple
        /// <summary>
        /// 是否允许多实例
        /// </summary>
        public bool AllowMultiple
        {
            get { return false; }
        }
        #endregion

        #region # 执行授权过滤器事件 —— async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(...
        /// <summary>
        /// 执行授权过滤器事件
        /// </summary>
        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext context, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            if (AspNetSetting.Authorized && !this.HasAttr<AllowAnonymousAttribute>(context.ActionDescriptor))
            {
                if (!context.Request.Headers.TryGetValues(SessionKey.CurrentPublicKey, out IEnumerable<string> headers))
                {
                    HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        Content = new StringContent("身份认证消息头不存在，请检查程序！")
                    };
                    return httpResponseMessage;
                }

                //读取消息头中的公钥
                Guid publicKey = new Guid(headers.Single());

                //认证
                lock (_Sync)
                {
                    //以公钥为键，查询分布式缓存，如果有值则通过，无值则不通过
                    LoginInfo loginInfo = CacheMediator.Get<LoginInfo>(publicKey.ToString());
                    if (loginInfo == null)
                    {
                        HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                        {
                            Content = new StringContent("身份过期，请重新登录！")
                        };
                        return httpResponseMessage;
                    }

                    //通过后，重新设置缓存过期时间
                    int timeout = FrameworkSection.Setting.AuthenticationTimeout.Value.HasValue
                        ? FrameworkSection.Setting.AuthenticationTimeout.Value.Value
                        : 20;
                    CacheMediator.Set(publicKey.ToString(), loginInfo, DateTime.Now.AddMinutes(timeout));

                    return continuation().Result;
                }
            }

            return await continuation();
        }
        #endregion


        //Private

        #region # Controller/Action是否有某特性标签 —— bool HasAttr<T>(HttpActionDescriptor...
        /// <summary>
        /// Controller/Action是否有某特性标签
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="actionDescriptor">Action方法元数据</param>
        /// <returns>是否拥有该特性</returns>
        public bool HasAttr<T>(HttpActionDescriptor actionDescriptor) where T : Attribute
        {
            ICollection<T> actionAttributes = actionDescriptor.GetCustomAttributes<T>();
            if (actionAttributes.Any())
            {
                return true;
            }

            ICollection<T> controllerAttributes = actionDescriptor.ControllerDescriptor.GetCustomAttributes<T>();
            if (controllerAttributes.Any())
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
