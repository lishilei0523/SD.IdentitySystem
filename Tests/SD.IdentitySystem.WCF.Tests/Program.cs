using Microsoft.Extensions.DependencyInjection;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.StubWCF.Client.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using SD.IOC.Core.Mediators;
using SD.IOC.Extension.NetFx;
using System;

namespace SD.IdentitySystem.WCF.Tests
{
    class Program
    {
        /// <summary>
        /// 此测试用例的目的在于测试消息头的发送是否成功
        /// </summary>
        static void Main()
        {
            //初始化容器
            Program.InitContainer();

            //伪造一个登录信息
            IAuthenticationContract authenticationContract = ResolveMediator.Resolve<IAuthenticationContract>();
            LoginInfo fakeLoginInfo = authenticationContract.Login(CommonConstants.AdminLoginId, CommonConstants.InitialPassword);

            //将登录信息存入约定位置
            AppDomain.CurrentDomain.SetData(SessionKey.CurrentUser, fakeLoginInfo);

            //实例化WCF客户端服务接口
            IClientContract clientContract = ResolveMediator.Resolve<IClientContract>();

            //调用服务获取消息头，
            string header = clientContract.GetHeader();

            //如果消息头内容即是上述伪造的公钥，即说明整个认证过程没问题
            if (fakeLoginInfo.PublicKey.ToString() == header)
            {
                Console.WriteLine("认证通过！");
            }

            ResolveMediator.Dispose();
            Console.ReadKey();
        }

        /// <summary>
        /// 初始化依赖注入容器
        /// </summary>
        static void InitContainer()
        {
            IServiceCollection builder = ResolveMediator.GetServiceCollection();
            builder.RegisterConfigs();

            ResolveMediator.Build();
        }
    }
}
