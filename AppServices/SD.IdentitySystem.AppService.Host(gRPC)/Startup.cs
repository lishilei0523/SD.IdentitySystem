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
    /// 应用程序启动器
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            //添加gRPC
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
        /// 配置应用程序
        /// </summary>
        public void Configure(IApplicationBuilder appBuilder)
        {
            appBuilder.UseMiddleware<GlobalMiddleware>();
            appBuilder.UseMiddleware<CacheOwinContextMiddleware>();
            appBuilder.UseRouting();
            appBuilder.UseEndpoints(endpoints =>
            {
                //映射gRPC
                endpoints.MapGrpcService<AuthenticationContract>();
                endpoints.MapGrpcService<AuthorizationContract>();
                endpoints.MapGrpcService<UserContract>();
            });
        }
    }
}
