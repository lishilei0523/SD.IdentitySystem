using SD.IdentitySystem.StubWCFServer.Interfaces;
using SD.Infrastructure.Constants;
using SD.IOC.Core.Mediator;
using System;

namespace SD.IdentitySystem.WindowsTests
{
    class Program
    {
        /// <summary>
        /// 此测试用例的目的在于测试消息头的发送是否成功
        /// </summary>
        static void Main()
        {
            //伪造一个登录信息
            LoginInfo fakeLoginInfo = new LoginInfo(null, null, Guid.NewGuid());

            //将登录信息存入约定位置
            AppDomain.CurrentDomain.SetData(CacheConstants.CurrentUserKey, fakeLoginInfo);

            //实例化服务接口
            IServerContract serverContract = ResolveMediator.Resolve<IServerContract>();

            //调用服务获取消息头，
            string header = serverContract.GetHeader();

            //如果消息头内容即是上述伪造的公钥，即说明整个认证过程没问题
            if (fakeLoginInfo.PublicKey.ToString() == header)
            {
                Console.WriteLine("认证通过！");
            }

            ResolveMediator.Dispose();
            Console.ReadKey();
        }
    }
}
