using Microsoft.Owin;
using Owin;
using SD.IdentitySystem.AppService.Host;
using SD.IdentitySystem.WCF.Authentication.Owin;
using SD.IdentitySystem.WebApi.Authentication.Filters;
using SD.IOC.Integration.WebApi.SelfHost;
using SD.Toolkits.Owin.Middlewares;
using SD.Toolkits.WebApi.Extensions;
using SD.Toolkits.WebApi.Filters;
using Swashbuckle.Application;
using System;
using System.IO;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(Startup))]
namespace SD.IdentitySystem.AppService.Host
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
            //配置中间件
            appBuilder.Use<CacheOwinContextMiddleware>();
            appBuilder.Use<PublicKeyExchangeMiddleware>();

            //配置Swagger
            httpConfiguration.EnableSwagger(config =>
            {
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
                config.SingleApiVersion("v1.0", "身份认证系统 WebApi 接口文档");
            }).EnableSwaggerUi();

            //配置路由
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Routes.MapHttpRoute(
                "DefaultApi",
                "{controller}/{action}/{id}",
                new { id = RouteParameter.Optional }
            );

            //POST请求多参数绑定
            httpConfiguration.RegisterWrapParameterBindingRule();

            //允许跨域
            httpConfiguration.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            //添加过滤器
            httpConfiguration.Filters.Add(new WebApiAuthenticationFilter());
            httpConfiguration.Filters.Add(new WebApiExceptionFilter());
        }
    }
}
