using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SD.IdentitySystem.AspNetCore.Authentication.Filters;
using SD.Infrastructure.Constants;
using SD.Toolkits.AspNetCore.Filters;
using SD.Toolkits.AspNetCore.Middlewares;
using SD.Toolkits.Json;
using SD.Toolkits.Redis;

namespace SD.IdentitySystem.Client
{
    /// <summary>
    /// Ӧ�ó���������
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ���÷���
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                //��ӹ�����
                options.Filters.Add(new MvcExceptionFilter());
                options.Filters.Add(new MvcAuthenticationFilter());
            }).AddJsonOptions(options =>
            {
                //JSON������ʽ���� nullΪPascal��ʽ
                options.JsonSerializerOptions.PropertyNamingPolicy = null;

                //����ʱ���ʽ����
                DateTimeConverter dateTimeConverter = new DateTimeConverter(CommonConstants.DateTimeFormat);
                options.JsonSerializerOptions.Converters.Add(dateTimeConverter);
            });

            //���Session������
            services.AddSession();
            services.AddStackExchangeRedisCache(options => options.ConfigurationOptions = RedisManager.RedisConfigurationOptions);
            services.AddDataProtection(options => options.ApplicationDiscriminator = GlobalSetting.ApplicationId);
        }

        /// <summary>
        /// ����Ӧ�ó���
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
