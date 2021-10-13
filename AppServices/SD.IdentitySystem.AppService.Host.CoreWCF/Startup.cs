using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SD.Common;
using SD.IdentitySystem.AppService.Implements;
using SD.IdentitySystem.WCF.Authentication;
using SD.Infrastructure.WCF.Server;
using SD.IOC.Integration.WCF.Behaviors;
using System.Configuration;

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
            //添加WCF服务
            services.AddServiceModelServices();

            //添加WCF配置
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            services.AddServiceModelConfigurationManagerFile(configuration.FilePath);
        }

        /// <summary>
        /// 配置应用程序
        /// </summary>
        public void Configure(IApplicationBuilder appBuilder)
        {
            //配置WCF服务
            DependencyInjectionBehavior dependencyInjectionBehavior = new DependencyInjectionBehavior();
            InitializationBehavior initializationBehavior = new InitializationBehavior();
            AuthenticationBehavior authenticationBehavior = new AuthenticationBehavior();
            IServiceBehavior[] serviceBehaviors =
            {
                dependencyInjectionBehavior, initializationBehavior, authenticationBehavior
            };
            appBuilder.UseServiceModel(builder =>
            {
                builder.ConfigureServiceHostBase<AuthenticationContract>(host => host.Description.Behaviors.AddRange(serviceBehaviors));
                builder.ConfigureServiceHostBase<AuthorizationContract>(host => host.Description.Behaviors.AddRange(serviceBehaviors));
                builder.ConfigureServiceHostBase<UserContract>(host => host.Description.Behaviors.AddRange(serviceBehaviors));
            });
        }
    }
}
