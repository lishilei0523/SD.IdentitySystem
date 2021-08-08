using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SD.Infrastructure.Constants;
using SD.Toolkits.OwinCore.Middlewares;

namespace SD.IdentitySystem.AspNetCore.Tests
{
    /// <summary>
    /// 应用程序启动器
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                //Camel命名设置
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                //日期时间格式设置
                IsoDateTimeConverter dateTimeConverter = new IsoDateTimeConverter()
                {
                    DateTimeFormat = CommonConstants.TimeFormat
                };
                options.SerializerSettings.Converters.Add(dateTimeConverter);
            });
        }

        /// <summary>
        /// 配置应用程序
        /// </summary>
        public void Configure(IApplicationBuilder appBuilder)
        {
            //配置中间件
            appBuilder.UseMiddleware<CacheOwinContextMiddleware>();

            //配置路由
            appBuilder.UseRouting();
            appBuilder.UseEndpoints(routeBuilder => routeBuilder.MapControllers());
        }
    }
}
