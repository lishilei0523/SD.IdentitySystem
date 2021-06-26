using SD.Infrastructure.AppServiceBase;
using System.ServiceModel;

// ReSharper disable once CheckNamespace
namespace SD.IdentitySystem.StubWCF.Client.Interfaces
{
    /// <summary>
    /// WCF客户端契约接口
    /// </summary>
    [ServiceContract(Namespace = "http://SD.Infrastructure.WCFTests.Interfaces")]
    public interface IClientContract : IApplicationService
    {
        /// <summary>
        /// 获取消息头
        /// </summary>
        /// <returns>消息头</returns>
        /// <remarks>
        /// 此方法用于测试，
        /// 将客户端发送的消息头发送给WCF服务端，
        /// 并接收WCF服务端返回的消息头，
        /// 再返回给客户端
        /// </remarks>
        [OperationContract]
        string GetHeader();
    }
}
