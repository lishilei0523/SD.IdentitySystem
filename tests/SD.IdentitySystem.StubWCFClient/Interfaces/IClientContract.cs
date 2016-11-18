using System.ServiceModel;
using ShSoft.Infrastructure;

namespace SD.IdentitySystem.StubWCFClient.Interfaces
{
    /// <summary>
    /// WCF客户端契约接口
    /// </summary>
    [ServiceContract(Namespace = "http://ShSoft.Infrastructure.WCFTests.Interfaces")]
    public interface IClientContract : IApplicationService
    {
        /// <summary>
        /// 获取订单
        /// </summary>
        /// <returns>订单</returns>
        [OperationContract]
        string GetHeader();
    }
}
