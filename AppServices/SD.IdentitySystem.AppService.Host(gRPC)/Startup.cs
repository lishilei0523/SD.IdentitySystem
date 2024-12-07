using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SD.IdentitySystem.AppService.Implements;
using SD.IdentitySystem.Grpc.Authentication;
using SD.Infrastructure.AspNetCore.Server.Middlewares;
using SD.Toolkits.Grpc.Server.Interceptors;
using SD.Toolkits.OwinCore.Middlewares;
using ServiceModel.Grpc.Configuration;

namespace SD.IdentitySystem.AppService.Host
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
            //���gRPC
            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
                options.MaxSendMessageSize = int.MaxValue;
                options.MaxReceiveMessageSize = int.MaxValue;
                options.Interceptors.Add<ExceptionInterceptor>();
                options.Interceptors.Add<CacheServerCallContextInterceptor>();
                options.Interceptors.Add<AuthenticationInterceptor>();
            });
            services.AddServiceModelGrpc(options =>
            {
                options.DefaultMarshallerFactory = MessagePackMarshallerFactory.Default;
            });
        }

        /// <summary>
        /// ����Ӧ�ó���
        /// </summary>
        public void Configure(IApplicationBuilder appBuilder)
        {
            appBuilder.UseMiddleware<GlobalMiddleware>();
            appBuilder.UseMiddleware<CacheOwinContextMiddleware>();
            appBuilder.UseRouting();
            appBuilder.UseEndpoints(endpoints =>
            {
                //ӳ��gRPC
                endpoints.MapGrpcService<AuthenticationContract>();
                endpoints.MapGrpcService<AuthorizationContract>();
                endpoints.MapGrpcService<UserContract>();
            });
        }
    }
}
