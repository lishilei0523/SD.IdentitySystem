using System.ServiceModel;
using ShSoft.Infrastructure;

namespace SD.IdentitySystem.StubServer.Interfaces
{
    /// <summary>
    /// 服务契约接口
    /// </summary>
    [ServiceContract(Namespace = "http://ShSoft.Infrastructure.WCFTests.Interfaces")]
    public interface IServerContract : IApplicationService
    {
        /// <summary>
        /// 获取消息头
        /// </summary>
        /// <returns>消息头</returns>
        [OperationContract]
        string GetHeader();
    }
}
