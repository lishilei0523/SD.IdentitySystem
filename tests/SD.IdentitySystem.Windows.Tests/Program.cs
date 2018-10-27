﻿using Microsoft.Extensions.DependencyInjection;
using SD.IdentitySystem.StubWCF.Server.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using SD.IOC.Core.Mediators;
using SD.IOC.Extension.NetFx;
using System;

namespace SD.IdentitySystem.Windows.Tests
{
    class Program
    {
        /// <summary>
        /// 此测试用例的目的在于测试消息头的发送是否成功
        /// </summary>
        static void Main()
        {
            //初始化容器
            InitContainer();

            //伪造一个登录信息
            LoginInfo fakeLoginInfo = new LoginInfo(null, null, Guid.NewGuid());

            //将登录信息存入约定位置
            AppDomain.CurrentDomain.SetData(SessionKey.CurrentUser, fakeLoginInfo);

            //实例化服务接口
            IServerContract serverContract = ResolveMediator.Resolve<IServerContract>();

            //调用服务获取消息头
            string header = serverContract.GetHeader();

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
