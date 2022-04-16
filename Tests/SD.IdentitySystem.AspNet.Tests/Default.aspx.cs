using Microsoft.Extensions.DependencyInjection;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.StubWCF.Server.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using SD.IOC.Core.Mediators;
using SD.IOC.Extension.NetFx;
using System;
using System.Web.UI;

namespace SD.IdentitySystem.AspNet.Tests
{
    public partial class Default : Page
    {
        /// <summary>
        /// 此测试用例的目的在于测试消息头的发送是否成功
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            //初始化容器
            InitContainer();

            //伪造一个登录信息
            IAuthenticationContract authenticationContract = ResolveMediator.Resolve<IAuthenticationContract>();
            LoginInfo fakeLoginInfo = authenticationContract.Login(CommonConstants.AdminLoginId, CommonConstants.InitialPassword);

            //将登录信息存入约定位置
            base.Session.Add(GlobalSetting.ApplicationId, fakeLoginInfo);

            //实例化WCF服务端服务接口
            IServerContract serverContract = ResolveMediator.Resolve<IServerContract>();

            //调用服务获取消息头
            string header = serverContract.GetHeader();

            //如果消息头内容即是上述伪造的公钥，即说明整个认证过程没问题
            if (fakeLoginInfo.PublicKey.ToString() == header)
            {
                base.Response.Write("认证通过！");
            }

            ResolveMediator.Dispose();
        }

        /// <summary>
        /// 初始化依赖注入容器
        /// </summary>
        private static void InitContainer()
        {
            if (!ResolveMediator.ContainerBuilt)
            {
                IServiceCollection builder = ResolveMediator.GetServiceCollection();
                builder.RegisterConfigs();

                ResolveMediator.Build();
            }
        }
    }
}