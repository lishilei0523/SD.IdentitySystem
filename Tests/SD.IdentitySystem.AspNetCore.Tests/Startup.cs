using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SD.Infrastructure.Constants;
using SD.Toolkits.AspNetCore.Middlewares;
using SD.Toolkits.Json;
using System.Text.Json;

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
            services.AddControllers().AddJsonOptions(options =>
            {
                //Camel命名设置
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

                //日期时间格式设置
                DateTimeConverter dateTimeConverter = new DateTimeConverter(CommonConstants.DateTimeFormat);
                options.JsonSerializerOptions.Converters.Add(dateTimeConverter);
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
