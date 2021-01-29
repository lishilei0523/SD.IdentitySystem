using Microsoft.Owin;
using Owin;
using SD.IdentitySystem.WCFAuthentication.Owin;
using SD.IdentitySystem.WebApi.Tests;
using SD.IOC.Integration.WebApi.SelfHost;
using SD.Toolkits.Owin.Middlewares;
using SD.Toolkits.WebApi.Extensions;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]
namespace SD.IdentitySystem.WebApi.Tests
{
    public class Startup : StartupBase
    {
        /// <summary>
        /// 配置应用程序
        /// </summary>
        /// <param name="appBuilder">应用程序建造者</param>
        /// <param name="httpConfiguration">Http配置</param>
        protected override void Configuration(IAppBuilder appBuilder, HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Routes.MapHttpRoute(
                "DefaultApi",
                "{controller}/{action}/{id}",
                new { id = RouteParameter.Optional }
            );

            //POST请求多参数绑定
            httpConfiguration.ParameterBindingRules.Insert(0, WrapPostParameterBinding.CreateBindingForMarkedParameters);

            appBuilder.Use<CacheOwinContextMiddleware>();
            appBuilder.Use<PublicKeyExchangeMiddleware>();
        }
    }
}
