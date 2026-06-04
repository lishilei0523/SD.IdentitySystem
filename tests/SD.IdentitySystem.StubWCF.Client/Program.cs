using SD.IdentitySystem.StubWCF.Client.Implements;
using System;
using System.ServiceModel;

namespace SD.IdentitySystem.StubWCF.Client
{
    class Program
    {
        static void Main()
        {
            ServiceHost clientContractHost = new ServiceHost(typeof(ClientContract));
            clientContractHost.Open();

            Console.WriteLine("服务已启动...");
            Console.ReadKey();
        }
    }
}
