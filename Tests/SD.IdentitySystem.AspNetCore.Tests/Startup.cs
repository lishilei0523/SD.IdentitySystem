using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SD.Infrastructure.Constants;
using SD.Toolkits.AspNetCore.Middlewares;
using SD.Toolkits.Json;
using System.Text.Json;

namespace SD.IdentitySystem.AspNetCore.Tests
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
            services.AddControllers().AddJsonOptions(options =>
            {
                //Camel��������
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

                //����ʱ���ʽ����
                DateTimeConverter dateTimeConverter = new DateTimeConverter(CommonConstants.DateTimeFormat);
                options.JsonSerializerOptions.Converters.Add(dateTimeConverter);
            });
        }

        /// <summary>
        /// ����Ӧ�ó���
        /// </summary>
        public void Configure(IApplicationBuilder appBuilder)
        {
            //�����м��
            appBuilder.UseMiddleware<CacheOwinContextMiddleware>();

            //����·��
            appBuilder.UseRouting();
            appBuilder.UseEndpoints(routeBuilder => routeBuilder.MapControllers());
        }
    }
}
