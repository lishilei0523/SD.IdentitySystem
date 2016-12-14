using System;
using SD.IdentitySystem.StubWCFClient.Interfaces;
using SD.IOC.Core.Mediator;
using ShSoft.ValueObjects;

namespace SD.IdentitySystem.WCFClientTests
{
    class Program
    {
        /// <summary>
        /// 此测试用例的目的在于测试消息头的发送是否成功
        /// </summary>
        static void Main()
        {
            //伪造一个公钥
            Guid fakePublishKey = Guid.NewGuid();

            //将公钥存入约定位置
            AppDomain.CurrentDomain.SetData(SessionKey.CurrentPublishKey, fakePublishKey);

            //实例化WCF客户端服务接口
            IClientContract clientContract = ResolveMediator.Resolve<IClientContract>();

            //调用服务获取消息头，
            string header = clientContract.GetHeader();

            //如果消息头内容即是上述伪造的公钥，即说明整个认证过程没问题
            if (fakePublishKey.ToString() == header)
            {
                Console.WriteLine("认证通过！");
            }

            ResolveMediator.Dispose();
            Console.ReadKey();
        }
    }
}
