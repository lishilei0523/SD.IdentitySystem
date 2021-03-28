using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SD.IdentitySystem.WebApiCore.Authentication.Filters;
using SD.Infrastructure.AspNetCore.Server.Middlewares;
using SD.Toolkits.OwinCore.Middlewares;
using System;
using System.IO;
using System.Reflection;

namespace SD.IdentitySystem.AppService.Host
{
    public class Startup
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            //配置跨域
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(_ => true)
                        .AllowCredentials();
                }));

            //添加Swagger
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "身份认证系统 WebApi 接口文档"
                });

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });

            //添加身份认证过滤器
            services.AddControllers(options =>
            {
                options.Filters.Add<WebApiAuthenticationFilter>();
            });
        }

        /// <summary>
        /// 配置应用程序
        /// </summary>
        public void Configure(IApplicationBuilder appBuilder)
        {
            //配置全局中间件
            appBuilder.UseMiddleware<GlobalMiddleware>();

            //配置缓存OwinContext中间件
            appBuilder.UseMiddleware<CacheOwinContextMiddleware>();

            //配置跨域
            appBuilder.UseCors("CorsPolicy");

            //配置Swagger中间件
            appBuilder.UseSwagger();
            appBuilder.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1.0/swagger.json", "身份认证系统 WebApi 接口文档 v1.0");
            });

            appBuilder.UseRouting();
            appBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
