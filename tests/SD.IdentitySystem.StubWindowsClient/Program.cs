using System;
using SD.IdentitySystem.StubServer.Interfaces;
using SD.IOC.Core.Mediator;
using ShSoft.ValueObjects;

namespace SD.IdentitySystem.StubWindowsClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Guid newGuid = Guid.NewGuid();

            AppDomain.CurrentDomain.SetData(SessionKey.CurrentPublishKey, newGuid);

            IServerContract serverContract = ResolveMediator.Resolve<IServerContract>();

            string header = serverContract.GetHeader();

            Console.WriteLine(header);

            ResolveMediator.Dispose();
            Console.ReadKey();
        }
    }
}
