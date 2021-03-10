using Microsoft.Owin;
using Owin;
using SD.IdentitySystem.AppService.WebApi;
using SD.IdentitySystem.WCFAuthentication.Owin;
using SD.IOC.Integration.WebApi.SelfHost;
using SD.Toolkits.Owin.Middlewares;
using SD.Toolkits.WebApi.Extensions;
using Swashbuckle.Application;
using System.Web.Http;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(Startup))]
namespace SD.IdentitySystem.AppService.WebApi
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
            httpConfiguration.EnableSwagger(config =>
            {
                config.IncludeXmlComments("SD.IdentitySystem.AppService.WebApi.xml");
                config.SingleApiVersion("v1.0", "身份认证系统 WebApi 接口文档");
            }).EnableSwaggerUi();

            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Routes.MapHttpRoute(
                "DefaultApi",
                "{controller}/{action}/{id}",
                new { id = RouteParameter.Optional }
            );

            //POST请求多参数绑定
            httpConfiguration.RegisterWrapParameterBindingRules();

            //允许跨域
            httpConfiguration.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            appBuilder.Use<CacheOwinContextMiddleware>();
            appBuilder.Use<PublicKeyExchangeMiddleware>();
        }
    }
}
