using System;
using SD.IdentitySystem.StubWCFClient.Interfaces;
using SD.IOC.Core.Mediator;
using ShSoft.ValueObjects;

namespace SD.IdentitySystem.StubWCFClientTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Guid newGuid = Guid.NewGuid();

            AppDomain.CurrentDomain.SetData(SessionKey.CurrentPublishKey, newGuid);

            IClientContract serverContract = ResolveMediator.Resolve<IClientContract>();

            string header = serverContract.GetHeader();

            Console.WriteLine(header);

            ResolveMediator.Dispose();
            Console.ReadKey();
        }
    }
}
