using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SD.IdentitySystem.AspNetCore.Authentication.Filters;
using SD.Infrastructure.Constants;
using SD.Toolkits.AspNetCore.Filters;
using SD.Toolkits.OwinCore.Middlewares;
using SD.Toolkits.Redis;

namespace SD.IdentitySystem.Client
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
            services.AddControllersWithViews(options =>
            {
                //添加过滤器
                options.Filters.Add(new MvcExceptionFilter());
                options.Filters.Add(new MvcAuthenticationFilter());
            }).AddNewtonsoftJson(options =>
            {
                //JSON命名格式设置
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();

                //日期时间格式设置
                IsoDateTimeConverter dateTimeConverter = new IsoDateTimeConverter()
                {
                    DateTimeFormat = CommonConstants.DateTimeFormat
                };
                options.SerializerSettings.Converters.Add(dateTimeConverter);
            });

            //添加Session及共享
            services.AddSession();
            services.AddStackExchangeRedisCache(options => options.ConfigurationOptions = RedisManager.RedisConfigurationOptions);
            services.AddDataProtection(options => options.ApplicationDiscriminator = GlobalSetting.ApplicationId);
        }

        /// <summary>
        /// 配置应用程序
        /// </summary>
        public void Configure(IApplicationBuilder appBuilder)
        {
            appBuilder.UseMiddleware<CacheOwinContextMiddleware>();
            appBuilder.UseStaticFiles();
            appBuilder.UseRouting();
            appBuilder.UseSession();
            appBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
