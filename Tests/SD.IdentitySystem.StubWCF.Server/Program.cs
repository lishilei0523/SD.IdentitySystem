using SD.IdentitySystem.StubWCF.Server.Implements;
using System;
using System.ServiceModel;

namespace SD.IdentitySystem.StubWCF.Server
{
    class Program
    {
        static void Main()
        {
            ServiceHost serverContractHost = new ServiceHost(typeof(ServerContract));
            serverContractHost.Open();

            Console.WriteLine("服务已启动...");
            Console.ReadKey();
        }
    }
}
