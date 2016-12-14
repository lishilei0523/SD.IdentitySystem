using System.ServiceModel;
using SD.IdentitySystem.StubWCFClient.Interfaces;
using SD.IdentitySystem.StubWCFServer.Interfaces;

namespace SD.IdentitySystem.StubWCFClient.Implements
{
    /// <summary>
    /// WCF客户端契约实现
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ClientContract : IClientContract
    {

        private readonly IServerContract _serverContract;

        public ClientContract(IServerContract serverContract)
        {
            this._serverContract = serverContract;
        }

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
        public string GetHeader()
        {
            string header = this._serverContract.GetHeader();

            return header;
        }
    }
}